import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import { concluirTreinoComTempo, criarFichaComSerie, criarMeta } from '../../utils/api'

test.describe('GET /dashboard/resumo e métricas', () => {
  test('retorna 401 em /dashboard/resumo sem token', async ({ request }) => {
    const response = await request.get('/dashboard/resumo')
    expect(response.status()).toBe(401)
  })

  test('resumo zerado para atleta sem treinos concluídos', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.get('/dashboard/resumo', {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(200)
    expect(await response.json()).toMatchObject({
      totalTreinos: 0,
      totalMetrosNadados: 0,
      totalTempoSegundos: 0,
      metasAtivasContagem: 0,
      metasConcluidasContagem: 0,
    })
  })

  test('resumo e evolução de pace após um treino concluído', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const titulo = `Treino métricas ${Date.now()}`
    await concluirTreinoComTempo(request, token, titulo)

    const resumo = await request.get('/dashboard/resumo', {
      headers: cabecalhoAuth(token),
    })
    expect(resumo.status()).toBe(200)
    expect(await resumo.json()).toMatchObject({
      totalTreinos: 1,
      totalMetrosNadados: 100,
      totalTempoSegundos: 70,
    })

    const pace = await request.get('/metricas/pace-medio', {
      headers: cabecalhoAuth(token),
    })
    expect(pace.status()).toBe(200)
    const evolucao = await pace.json()
    expect(evolucao.length).toBeGreaterThan(0)
    expect(evolucao[0]).toMatchObject({
      tituloTreino: titulo,
      tipoNado: 0,
      distanciaM: 100,
    })

    const recordes = await request.get('/metricas/melhores-tempos', {
      headers: cabecalhoAuth(token),
    })
    expect(recordes.status()).toBe(200)
    expect(Array.isArray(await recordes.json())).toBe(true)
  })

  test('progresso de meta inicia em zero sem tentativas', async ({ request }) => {
    const { token } = await registrarAtleta(request)
    const { idSerie } = await criarFichaComSerie(request, token, `Ficha progresso ${Date.now()}`)
    const meta = await criarMeta(request, token, {
      codSerieFicha: idSerie,
      tituloMeta: 'Meta sem tentativas',
    })
    const idMeta = meta.codMeta ?? meta.CodMeta

    const response = await request.get(`/metas/${idMeta}/progresso`, {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(200)
    expect(await response.json()).toMatchObject({
      codMeta: idMeta,
      tituloMeta: 'Meta sem tentativas',
      percentualAtingimento: 0,
    })
  })
})
