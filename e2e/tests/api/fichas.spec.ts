import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import { adicionarSerie, criarFicha } from '../../utils/api'

test.describe('GET /fichas-base', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.get('/fichas-base')
    expect(response.status()).toBe(401)
  })

  test('lista vazia para atleta recém-cadastrado', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.get('/fichas-base', {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(200)
    expect(await response.json()).toEqual([])
  })

  test('atleta A nao acessa ficha do atleta B (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const fichaB = await criarFicha(request, tokenB, `Ficha do atleta B ${Date.now()}`)
    const idFichaB = fichaB.codFicha ?? fichaB.CodFicha

    const response = await request.get(`/fichas-base/${idFichaB}`, {
      headers: cabecalhoAuth(tokenA),
    })

    expect(response.status()).toBe(404)
  })
})

test.describe('POST /fichas-base', () => {
  test('cria uma ficha base', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const titulo = `Treino técnico ${Date.now()}`

    const response = await request.post('/fichas-base', {
      headers: cabecalhoAuth(token),
      data: { tituloFicha: titulo, tipoFicha: 0 },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({ tituloFicha: titulo, tipoFicha: 0, status: 1 })
    expect(body.codFicha ?? body.CodFicha).toBeTruthy()
  })

  test('rejeita ficha sem título', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.post('/fichas-base', {
      headers: cabecalhoAuth(token),
      data: { tituloFicha: '', tipoFicha: 0 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita tipoFicha fora do range permitido (0-1)', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.post('/fichas-base', {
      headers: cabecalhoAuth(token),
      data: { tituloFicha: `Ficha tipo invalido ${Date.now()}`, tipoFicha: 5 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita titulo composto apenas por espacos', async ({ request }) => {
    // [Required] no ASP.NET Core faz Trim() antes de validar o tamanho, entao uma string
    // so com espacos e corretamente rejeitada (mesmo comportamento de string vazia).
    const { token } = await registrarAtleta(request)

    const response = await request.post('/fichas-base', {
      headers: cabecalhoAuth(token),
      data: { tituloFicha: '   ', tipoFicha: 0 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita a 6a ficha ativa (limite de 5 fichas ativas)', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const prefixo = `Ficha limite ${Date.now()}`

    for (let i = 1; i <= 5; i++) {
      const criar = await request.post('/fichas-base', {
        headers: cabecalhoAuth(token),
        data: { tituloFicha: `${prefixo} ${i}`, tipoFicha: 0 },
      })
      expect(criar.status()).toBe(201)
    }

    const sexta = await request.post('/fichas-base', {
      headers: cabecalhoAuth(token),
      data: { tituloFicha: `${prefixo} 6`, tipoFicha: 0 },
    })

    expect(sexta.status()).toBe(400)
  })

  test('atleta A nao exclui ficha do atleta B (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const fichaB = await criarFicha(request, tokenB, `Ficha exclusao B ${Date.now()}`)
    const idFichaB = fichaB.codFicha ?? fichaB.CodFicha

    const excluir = await request.delete(`/fichas-base/${idFichaB}`, {
      headers: cabecalhoAuth(tokenA),
    })
    expect(excluir.status()).toBe(404)

    const aindaExiste = await request.get(`/fichas-base/${idFichaB}`, {
      headers: cabecalhoAuth(tokenB),
    })
    expect(aindaExiste.status()).toBe(200)
  })

  test('atleta A nao adiciona serie na ficha do atleta B (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const fichaB = await criarFicha(request, tokenB, `Ficha serie B ${Date.now()}`)
    const idFichaB = fichaB.codFicha ?? fichaB.CodFicha

    const response = await request.post(`/fichas-base/${idFichaB}/series`, {
      headers: cabecalhoAuth(tokenA),
      data: {
        tipoNado: 0,
        quantidadeRepeticoes: 4,
        distanciaM: 100,
        tempoPausaSeg: 20,
        isGoalSeries: false,
      },
    })

    expect(response.status()).toBe(404)
  })
})

test.describe('séries e duplicação de ficha', () => {
  test('adiciona série, consulta a ficha e duplica', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha séries ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const serieResponse = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: {
        tipoNado: 0,
        quantidadeRepeticoes: 4,
        distanciaM: 100,
        tempoPausaSeg: 20,
        isGoalSeries: false,
        observacoes: 'aquecimento',
      },
    })

    expect(serieResponse.status()).toBe(201)
    const serie = await serieResponse.json()
    expect(serie).toMatchObject({
      tipoNado: 0,
      quantidadeRepeticoes: 4,
      distanciaM: 100,
      tempoPausaSeg: 20,
    })

    const detalhe = await request.get(`/fichas-base/${idFicha}`, {
      headers: cabecalhoAuth(token),
    })
    expect(detalhe.status()).toBe(200)
    const bodyDetalhe = await detalhe.json()
    const series = bodyDetalhe.series ?? bodyDetalhe.Series
    expect(series).toHaveLength(1)

    const duplicar = await request.post(`/fichas-base/${idFicha}/duplicar`, {
      headers: cabecalhoAuth(token),
    })
    expect(duplicar.status()).toBe(201)
    const copia = await duplicar.json()
    expect(copia.fichaCopiada ?? copia.FichaCopiada).toBe(idFicha)
    expect(copia.tituloFicha ?? copia.TituloFicha).toContain(ficha.tituloFicha ?? ficha.TituloFicha)
  })

  test('exclui ficha sem vínculos e some da listagem', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha para excluir ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha
    await adicionarSerie(request, token, idFicha)

    const excluir = await request.delete(`/fichas-base/${idFicha}`, {
      headers: cabecalhoAuth(token),
    })
    expect(excluir.status()).toBe(204)

    const lista = await request.get('/fichas-base', {
      headers: cabecalhoAuth(token),
    })
    expect(await lista.json()).toEqual([])
  })
})

test.describe('validações de borda ao adicionar série', () => {
  test('rejeita distancia zero', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha borda distancia 0 ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: { tipoNado: 0, quantidadeRepeticoes: 4, distanciaM: 0, tempoPausaSeg: 20 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita distancia negativa', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha borda distancia negativa ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: { tipoNado: 0, quantidadeRepeticoes: 4, distanciaM: -10, tempoPausaSeg: 20 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita distancia acima do limite maximo (10000)', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha borda distancia maxima ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: { tipoNado: 0, quantidadeRepeticoes: 4, distanciaM: 10001, tempoPausaSeg: 20 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita tipoNado fora do enum valido (0-3)', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha borda tipoNado invalido ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: { tipoNado: 99, quantidadeRepeticoes: 4, distanciaM: 100, tempoPausaSeg: 20 },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita quantidadeRepeticoes zero', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const ficha = await criarFicha(request, token, `Ficha borda repeticoes 0 ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha

    const response = await request.post(`/fichas-base/${idFicha}/series`, {
      headers: cabecalhoAuth(token),
      data: { tipoNado: 0, quantidadeRepeticoes: 0, distanciaM: 100, tempoPausaSeg: 20 },
    })

    expect(response.status()).toBe(400)
  })
})

test.describe('isolamento de séries entre atletas', () => {
  test('atleta A nao atualiza serie do atleta B', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const fichaB = await criarFicha(request, tokenB, `Ficha serie atualizar B ${Date.now()}`)
    const idFichaB = fichaB.codFicha ?? fichaB.CodFicha
    const serieB = await adicionarSerie(request, tokenB, idFichaB)
    const idSerieB = serieB.codSerieFicha ?? serieB.CodSerieFicha

    const response = await request.put(`/fichas-base/series/${idSerieB}`, {
      headers: cabecalhoAuth(tokenA),
      data: { tipoNado: 1, quantidadeRepeticoes: 8, distanciaM: 200, tempoPausaSeg: 30 },
    })

    expect(response.status()).toBe(404)
  })

  test('atleta A nao exclui serie do atleta B', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const fichaB = await criarFicha(request, tokenB, `Ficha serie excluir B ${Date.now()}`)
    const idFichaB = fichaB.codFicha ?? fichaB.CodFicha
    const serieB = await adicionarSerie(request, tokenB, idFichaB)
    const idSerieB = serieB.codSerieFicha ?? serieB.CodSerieFicha

    const response = await request.delete(`/fichas-base/series/${idSerieB}`, {
      headers: cabecalhoAuth(tokenA),
    })

    expect(response.status()).toBe(404)
  })
})
