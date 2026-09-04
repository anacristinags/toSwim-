import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import { criarFichaComSerie, criarMeta } from '../../utils/api'

test.describe('GET /metas', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.get('/metas')
    expect(response.status()).toBe(401)
  })

  test('lista vazia para atleta sem metas', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.get('/metas', {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(200)
    expect(await response.json()).toEqual([])
  })
})

test.describe('POST /metas', () => {
  test('cria meta vinculada a uma série da ficha e calcula o pace alvo', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta ${Date.now()}`)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta 100m Crawl',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({
      tituloMeta: 'Meta 100m Crawl',
      tipoNado: 0,
      distanciaAlvoM: 100,
      tempoAlvoSeg: 75,
      paceAlvoSeg: 75,
      status: 0,
      codSerieFicha: idSerie,
    })
  })

  test('rejeita tempo alvo zero', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta inválida ${Date.now()}`)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta inválida',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 0,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(400)
  })
})

test.describe('status e exclusão de meta', () => {
  test('conclui e depois exclui a meta', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha status meta ${Date.now()}`)
    const meta = await criarMeta(request, token, {
      codSerieFicha: idSerie,
      tituloMeta: 'Meta para concluir',
    })
    const idMeta = meta.codMeta ?? meta.CodMeta

    const concluir = await request.put(`/metas/${idMeta}/status?status=1`, {
      headers: cabecalhoAuth(token),
    })
    expect(concluir.status()).toBe(200)
    expect((await concluir.json()).status).toBe(1)

    const excluir = await request.delete(`/metas/${idMeta}`, {
      headers: cabecalhoAuth(token),
    })
    expect(excluir.status()).toBe(204)

    const lista = await request.get('/metas', { headers: cabecalhoAuth(token) })
    expect(await lista.json()).toEqual([])
  })
})
