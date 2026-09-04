import { test, expect } from '@playwright/test'
import { cabecalhoAuth, registrarAtleta } from '../../utils/auth'
import { concluirTreinoComTempo, criarFichaComSerie, iniciarTreino } from '../../utils/api'

test.describe('GET /historico/treinos', () => {
  test('retorna 401 sem token de autenticação', async ({ request }) => {
    const response = await request.get('/historico/treinos')
    expect(response.status()).toBe(401)
  })

  test('lista vazia quando não há treinos concluídos', async ({ request }) => {
    const { token } = await registrarAtleta(request)

    const response = await request.get('/historico/treinos', {
      headers: cabecalhoAuth(token),
    })

    expect(response.status()).toBe(200)
    expect(await response.json()).toEqual([])
  })

  test('lista o treino após finalização e rejeita detalhe de treino em andamento', async ({
    request,
  }) => {
    const { token } = await registrarAtleta(request)
    const titulo = `Treino histórico ${Date.now()}`
    const finalizado = await concluirTreinoComTempo(request, token, titulo)
    const idFinalizado = finalizado.codTreino ?? finalizado.CodTreino

    const lista = await request.get('/historico/treinos', {
      headers: cabecalhoAuth(token),
    })
    expect(lista.status()).toBe(200)
    const itens = await lista.json()
    expect(itens).toEqual(
      expect.arrayContaining([
        expect.objectContaining({ tituloTreino: titulo, status: 1, distanciaTotalM: 100 }),
      ]),
    )

    const detalhe = await request.get(`/historico/treinos/${idFinalizado}`, {
      headers: cabecalhoAuth(token),
    })
    expect(detalhe.status()).toBe(200)
    expect(await detalhe.json()).toMatchObject({ tituloTreino: titulo, status: 1 })

    const { idFicha } = await criarFichaComSerie(request, token, `Ficha andamento ${Date.now()}`)
    const emAndamento = await iniciarTreino(request, token, {
      codFicha: idFicha,
      tituloTreino: 'Ainda na água',
    })
    const idAndamento = emAndamento.codTreino ?? emAndamento.CodTreino

    const detalheAndamento = await request.get(`/historico/treinos/${idAndamento}`, {
      headers: cabecalhoAuth(token),
    })
    expect(detalheAndamento.status()).toBe(400)
  })
})
