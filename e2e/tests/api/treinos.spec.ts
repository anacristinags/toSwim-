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
})
