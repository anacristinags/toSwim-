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
