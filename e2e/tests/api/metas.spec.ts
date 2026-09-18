import { test, expect } from '@playwright/test'
import { API_BASE, cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import { concluirTreinoComPiscina, criarConfigPiscina, criarFichaComSerie, criarMeta } from '../../utils/api'

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

  test('cria meta com tipoNado=4 (Livre)', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta livre ${Date.now()}`)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta 100m Livre',
        tipoNado: 4,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({ tipoNado: 4 })
  })

  test('meta vinculada a ficha de 25m retorna tamanhoPiscinaM 25', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta 25m ${Date.now()}`, 25)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta piscina 25m',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({ tamanhoPiscinaM: 25 })
  })

  test('meta vinculada a ficha de 50m retorna tamanhoPiscinaM 50', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta 50m ${Date.now()}`, 50)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta piscina 50m',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(201)
    const body = await response.json()
    expect(body).toMatchObject({ tamanhoPiscinaM: 50 })
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

  test('rejeita tempo alvo negativo', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta tempo negativo ${Date.now()}`)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta tempo negativo',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: -10,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(400)
  })

  test('rejeita distancia alvo zero', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta distancia zero ${Date.now()}`)

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(token),
      data: {
        codSerieFicha: idSerie,
        tituloMeta: 'Meta distancia zero',
        tipoNado: 0,
        distanciaAlvoM: 0,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(400)
  })

  test('atleta A nao usa serie do atleta B para criar meta (isolamento)', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const { idSerie: idSerieB } = await criarFichaComSerie(
      request,
      tokenB,
      `Ficha serie de B para meta ${Date.now()}`,
    )

    const response = await request.post('/metas', {
      headers: cabecalhoAuth(tokenA),
      data: {
        codSerieFicha: idSerieB,
        tituloMeta: 'Meta usando serie de outro atleta',
        tipoNado: 0,
        distanciaAlvoM: 100,
        tempoAlvoSeg: 75,
        modoAvaliacao: 0,
      },
    })

    expect(response.status()).toBe(403)
  })
})

test.describe('isolamento de metas entre atletas', () => {
  test('atleta A nao acessa meta do atleta B', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const { idSerie: idSerieB } = await criarFichaComSerie(
      request,
      tokenB,
      `Ficha meta B isolamento ${Date.now()}`,
    )
    const metaB = await criarMeta(request, tokenB, {
      codSerieFicha: idSerieB,
      tituloMeta: 'Meta do atleta B',
    })
    const idMetaB = metaB.codMeta ?? metaB.CodMeta

    const response = await request.get(`/metas/${idMetaB}`, {
      headers: cabecalhoAuth(tokenA),
    })

    expect(response.status()).toBe(404)
  })

  test('atleta A nao exclui meta do atleta B', async ({ request }) => {
    const { token: tokenA } = await registrarAtleta(request)
    const { token: tokenB } = await registrarAtleta(request)
    const { idSerie: idSerieB } = await criarFichaComSerie(
      request,
      tokenB,
      `Ficha meta B excluir isolamento ${Date.now()}`,
    )
    const metaB = await criarMeta(request, tokenB, {
      codSerieFicha: idSerieB,
      tituloMeta: 'Meta do atleta B para exclusao',
    })
    const idMetaB = metaB.codMeta ?? metaB.CodMeta

    const excluir = await request.delete(`/metas/${idMetaB}`, {
      headers: cabecalhoAuth(tokenA),
    })
    expect(excluir.status()).toBe(404)

    const aindaExiste = await request.get(`/metas/${idMetaB}`, {
      headers: cabecalhoAuth(tokenB),
    })
    expect(aindaExiste.status()).toBe(200)
  })
})

test.describe('progresso da meta filtrado por tamanho de piscina', () => {
  test('treino de 25m nao influencia o progresso de uma meta de 50m', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    // Completa um treino de 25m primeiro (pace bem rapido, sem ele o teste seria falso-positivo).
    await criarConfigPiscina(request, token, { tamanhoM: 25 })
    await concluirTreinoComPiscina(request, token, `Treino 25m ${Date.now()}`, 25)

    // Troca a piscina atual do atleta para 50m e cria a ficha/meta de 50m.
    await request.put(`${API_BASE}/piscina-configuracao/me`, {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 50, formaContagem: 0 },
    })
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta 50m progresso ${Date.now()}`, 50)
    const meta = await criarMeta(request, token, {
      codSerieFicha: idSerie,
      tituloMeta: 'Meta 50m sem treino compativel',
      tempoAlvoSeg: 60,
    })
    const idMeta = meta.codMeta ?? meta.CodMeta

    const progresso = await request.get(`${API_BASE}/metas/${idMeta}/progresso`, {
      headers: cabecalhoAuth(token),
    })

    expect(progresso.status()).toBe(200)
    const body = await progresso.json()
    expect(body.historicoTentativas ?? body.HistoricoTentativas).toEqual([])
    expect(body.melhorTempoRealizadoSeg ?? body.MelhorTempoRealizadoSeg).toBeFalsy()
    expect(body.percentualAtingimento ?? body.PercentualAtingimento).toBe(0)
  })

  test('treino de 50m nao influencia o progresso de uma meta de 25m', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    // Completa um treino de 50m primeiro.
    await criarConfigPiscina(request, token, { tamanhoM: 50 })
    await concluirTreinoComPiscina(request, token, `Treino 50m ${Date.now()}`, 50)

    // Troca a piscina atual do atleta para 25m e cria a ficha/meta de 25m.
    await request.put(`${API_BASE}/piscina-configuracao/me`, {
      headers: cabecalhoAuth(token),
      data: { tamanhoM: 25, formaContagem: 0 },
    })
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta 25m progresso ${Date.now()}`, 25)
    const meta = await criarMeta(request, token, {
      codSerieFicha: idSerie,
      tituloMeta: 'Meta 25m sem treino compativel',
      tempoAlvoSeg: 60,
    })
    const idMeta = meta.codMeta ?? meta.CodMeta

    const progresso = await request.get(`${API_BASE}/metas/${idMeta}/progresso`, {
      headers: cabecalhoAuth(token),
    })

    expect(progresso.status()).toBe(200)
    const body = await progresso.json()
    expect(body.historicoTentativas ?? body.HistoricoTentativas).toEqual([])
    expect(body.melhorTempoRealizadoSeg ?? body.MelhorTempoRealizadoSeg).toBeFalsy()
    expect(body.percentualAtingimento ?? body.PercentualAtingimento).toBe(0)
  })

  test('treino na mesma piscina da ficha da meta conta no progresso', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    await criarConfigPiscina(request, token, { tamanhoM: 50 })
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha meta compativel 50m ${Date.now()}`, 50)
    const meta = await criarMeta(request, token, {
      codSerieFicha: idSerie,
      tituloMeta: 'Meta 50m com treino compativel',
      tempoAlvoSeg: 9999,
    })
    const idMeta = meta.codMeta ?? meta.CodMeta

    await concluirTreinoComPiscina(request, token, `Treino compativel 50m ${Date.now()}`, 50)

    const progresso = await request.get(`${API_BASE}/metas/${idMeta}/progresso`, {
      headers: cabecalhoAuth(token),
    })

    expect(progresso.status()).toBe(200)
    const body = await progresso.json()
    expect((body.historicoTentativas ?? body.HistoricoTentativas).length).toBeGreaterThan(0)
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
