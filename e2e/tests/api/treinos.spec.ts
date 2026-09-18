import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import {
  criarConfigPiscina,
  criarFicha,
  criarFichaComSerie,
  iniciarTreino,
  seriesDoTreino,
} from '../../utils/api'

test.describe('POST /treinos', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.post('/treinos', {
      data: { codFicha: 1, tituloTreino: 'X', tamanhoPiscinaM: 25 },
    })
    expect(response.status()).toBe(401)
  })

  test('inicia um treino a partir de uma ficha com séries', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha execução ${Date.now()}`)

    const response = await request.post('/treinos', {
      headers: cabecalhoAuth(token),
      data: {
        codFicha: idFicha,
        tituloTreino: 'Treino do dia',
        observacao: 'foco na pernada',
        tamanhoPiscinaM: 25,
      },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({
      tituloTreino: 'Treino do dia',
      observacao: 'foco na pernada',
      tamanhoPiscinaM: 25,
      status: 0,
      codFicha: idFicha,
    })
    expect(seriesDoTreino(body)).toHaveLength(1)
  })

  test('rejeita tamanho de piscina inválido ao iniciar', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha piscina inválida ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post('/treinos', {
      headers: cabecalhoAuth(token),
      data: { codFicha: idFicha, tituloTreino: 'Inválido', tamanhoPiscinaM: 33 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita iniciar treino a partir de ficha sem nenhuma serie', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const ficha = await criarFicha(request, token, `Ficha sem serie ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post('/treinos', {
      headers: cabecalhoAuth(token),
      data: { codFicha: idFicha, tituloTreino: 'Treino sem serie', tamanhoPiscinaM: 25 },
    })

    expect(response.status()).toBe(400)
    const body = await response.json()
    expect(body.erro).toBeTruthy()
  })

  test('atleta A nao inicia treino a partir de ficha do atleta B (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    await criarConfigPiscina(request, tokenA)
    const { idFicha: idFichaB } = await criarFichaComSerie(
      request,
      tokenB,
      `Ficha treino B isolamento ${Date.now()}`,
    )

    const response = await request.post('/treinos', {
      headers: cabecalhoAuth(tokenA),
      data: { codFicha: idFichaB, tituloTreino: 'Treino com ficha de outro atleta', tamanhoPiscinaM: 25 },
    })

    expect(response.status()).toBe(404)
  })
})

test.describe('execução, repetição, finalizar e cancelar', () => {
  test('registra tiro, atualiza a série e finaliza o treino', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha finalizar ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Finalizar via API',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino
    const serie = seriesDoTreino(treino)[0]
    const idSerie = serie.codSerieTreino ?? serie.CodSerieTreino

    const repeticao = await request.post(`/treinos/${idTreino}/series/${idSerie}/repeticoes`, {
      headers: cabecalhoAuth(token),
      data: { numeroRepeticao: 1, distanciaRealM: 100, duracaoSeg: 70 },
    })
    expect(repeticao.status()).toBe(201)
    const tiro = await repeticao.json()
    expect(tiro).toMatchObject({ numeroRepeticao: 1, distanciaRealM: 100, duracaoSeg: 70 })
    expect(tiro.paceSeg ?? tiro.PaceSeg).toBe(70)

    const atualizarSerie = await request.put(`/treinos/${idTreino}/series/${idSerie}`, {
      headers: cabecalhoAuth(token),
      data: {
        ordem: 1,
        tipoNado: 0,
        quantidadeRepeticoesPlanejada: 4,
        distanciaPlanejadaM: 100,
        tempoPausaSeg: 20,
        tempoTotalSeg: 70,
        distanciaTotalM: 100,
      },
    })
    expect(atualizarSerie.status()).toBe(200)

    const finalizar = await request.put(`/treinos/${idTreino}/finalizar`, {
      headers: cabecalhoAuth(token),
    })
    expect(finalizar.status()).toBe(200)
    expect(await finalizar.json()).toMatchObject({
      status: 1,
      distanciaTotalM: 100,
      duracaoTotalSeg: 70,
    })
  })

  test('cancela treino em andamento', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha cancelar ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Cancelar via API',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino

    const cancelar = await request.put(`/treinos/${idTreino}/cancelar`, {
      headers: cabecalhoAuth(token),
    })
    expect(cancelar.status()).toBe(200)
    expect((await cancelar.json()).status).toBe(2)
  })

  test('rejeita repetição com duração zero', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha tiro inválido ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Tiro inválido',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino
    const idSerie = seriesDoTreino(treino)[0].codSerieTreino ?? seriesDoTreino(treino)[0].CodSerieTreino

    const response = await request.post(`/treinos/${idTreino}/series/${idSerie}/repeticoes`, {
      headers: cabecalhoAuth(token),
      data: { numeroRepeticao: 1, distanciaRealM: 100, duracaoSeg: 0 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita finalizar um treino ja finalizado (dupla finalizacao)', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha dupla finalizacao ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Dupla finalizacao',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino

    const primeiraFinalizacao = await request.put(`/treinos/${idTreino}/finalizar`, {
      headers: cabecalhoAuth(token),
    })
    expect(primeiraFinalizacao.status()).toBe(200)

    const segundaFinalizacao = await request.put(`/treinos/${idTreino}/finalizar`, {
      headers: cabecalhoAuth(token),
    })
    expect(segundaFinalizacao.status()).toBe(400)
  })

  test('rejeita cancelar um treino ja finalizado', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha cancelar finalizado ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Cancelar apos finalizar',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino

    const finalizar = await request.put(`/treinos/${idTreino}/finalizar`, {
      headers: cabecalhoAuth(token),
    })
    expect(finalizar.status()).toBe(200)

    const cancelar = await request.put(`/treinos/${idTreino}/cancelar`, {
      headers: cabecalhoAuth(token),
    })
    expect(cancelar.status()).toBe(400)
  })

  test('rejeita registrar repeticao em treino ja finalizado', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha repeticao pos finalizar ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Repeticao apos finalizar',
    })
    const idTreino = treino.codTreino ?? treino.CodTreino
    const idSerie = seriesDoTreino(treino)[0].codSerieTreino ?? seriesDoTreino(treino)[0].CodSerieTreino

    const finalizar = await request.put(`/treinos/${idTreino}/finalizar`, {
      headers: cabecalhoAuth(token),
    })
    expect(finalizar.status()).toBe(200)

    const response = await request.post(`/treinos/${idTreino}/series/${idSerie}/repeticoes`, {
      headers: cabecalhoAuth(token),
      data: { numeroRepeticao: 1, distanciaRealM: 100, duracaoSeg: 70 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita registrar repeticao em treino inexistente', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    await criarConfigPiscina(request, token)
    const { idFicha } = await criarFichaComSerie(request, token, `Ficha treino inexistente ${Date.now()}`)
    const treino = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Treino existente para pegar id de serie',
    })
    const idSerie = seriesDoTreino(treino)[0].codSerieTreino ?? seriesDoTreino(treino)[0].CodSerieTreino
    const idTreinoInexistente = 999999999

    const response = await request.post(
      `/treinos/${idTreinoInexistente}/series/${idSerie}/repeticoes`,
      {
        headers: cabecalhoAuth(token),
        data: { numeroRepeticao: 1, distanciaRealM: 100, duracaoSeg: 70 },
      },
    )

    expect(response.status()).toBe(404)
  })

  test('atleta A nao acessa nem finaliza treino do atleta B (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    await criarConfigPiscina(request, tokenB)
    const { idFicha: idFichaB } = await criarFichaComSerie(
      request,
      tokenB,
      `Ficha treino B para isolamento ${Date.now()}`,
    )
    const treinoB = await iniciarTreino(request, tokenB, {
      codFicha: idFichaB,
      tituloTreino: 'Treino do atleta B',
    })
    const idTreinoB = treinoB.codTreino ?? treinoB.CodTreino

    const acessar = await request.get(`/treinos/${idTreinoB}`, {
      headers: cabecalhoAuth(tokenA),
    })
    expect(acessar.status()).toBe(404)

    const finalizar = await request.put(`/treinos/${idTreinoB}/finalizar`, {
      headers: cabecalhoAuth(tokenA),
    })
    expect(finalizar.status()).toBe(404)
  })
})
