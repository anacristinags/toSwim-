import { test, expect, type APIRequestContext } from '@playwright/test'
import { API_BASE, autenticarNaPagina, cabecalhoAuth } from '../../utils/auth'
import { concluirTreinoComTempo, criarFichaComSerie, iniciarTreino, seriesDoTreino } from '../../utils/api'

/**
 * Conclui um treino igual a `concluirTreinoComTempo`, mas sem configurar a piscina novamente
 * (util quando o mesmo atleta ja possui configuracao e precisa concluir mais de um treino).
 */
async function concluirTreinoSemConfigurarPiscina(
  request: APIRequestContext,
  token: string,
  tituloTreino: string,
) {
  const { idFicha } = await criarFichaComSerie(request, token, `Ficha ${tituloTreino}`)
  const treino = await iniciarTreino(request, token, { codFicha: idFicha, tituloTreino })
  const idTreino = treino.codTreino ?? treino.CodTreino
  const serie = seriesDoTreino(treino)[0]
  const idSerie = serie.codSerieTreino ?? serie.CodSerieTreino
  const distancia = serie.distanciaPlanejadaM ?? serie.DistanciaPlanejadaM
  const duracaoSeg = 70

  await request.post(`${API_BASE}/treinos/${idTreino}/series/${idSerie}/repeticoes`, {
    headers: cabecalhoAuth(token),
    data: { numeroRepeticao: 1, distanciaRealM: distancia, duracaoSeg },
  })

  await request.put(`${API_BASE}/treinos/${idTreino}/series/${idSerie}`, {
    headers: cabecalhoAuth(token),
    data: {
      codSerieFicha: serie.codSerieFicha ?? serie.CodSerieFicha,
      ordem: serie.ordem ?? serie.Ordem,
      tipoNado: serie.tipoNado ?? serie.TipoNado,
      quantidadeRepeticoesPlanejada:
        serie.quantidadeRepeticoesPlanejada ?? serie.QuantidadeRepeticoesPlanejada,
      distanciaPlanejadaM: distancia,
      tempoPausaSeg: serie.tempoPausaSeg ?? serie.TempoPausaSeg,
      tempoTotalSeg: duracaoSeg,
      distanciaTotalM: distancia,
      observacoes: serie.observacoes ?? serie.Observacoes,
    },
  })

  const finalizar = await request.put(`${API_BASE}/treinos/${idTreino}/finalizar`, {
    headers: cabecalhoAuth(token),
  })
  return finalizar.json()
}

test.describe('Tela de histórico de treinos', () => {
  test('exibe estado vazio quando não há treinos concluídos', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/historico')

    await expect(page.getByRole('heading', { name: 'Historico de Treinos' })).toBeVisible()
    await expect(
      page.getByText('Nenhum treino concluido ainda. Finalize um treino na tela de Execucao.'),
    ).toBeVisible()
  })

  test('lista treino concluído e abre o detalhe', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const titulo = `Sessao historico ${Date.now()}`
    await concluirTreinoComTempo(request, atleta.token, titulo)

    await page.goto('/historico')
    const linha = page.getByRole('row', { name: titulo })
    await expect(linha).toBeVisible()
    await expect(linha.getByText('100m', { exact: true })).toBeVisible()
    await expect(page.getByLabel('Filtrar por titulo', { exact: true })).toBeVisible()

    await linha.getByRole('button').filter({ has: page.locator('.mdi-eye-outline') }).click()

    await expect(page.getByText(`Realizado em`)).toBeVisible()
    await expect(page.getByText('Series Executadas')).toBeVisible()
    await expect(page.getByText('4x 100m Crawl')).toBeVisible()
    await page.getByRole('button', { name: 'Fechar' }).click()
  })

  test('filtro por titulo reduz a lista exibida', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const tituloA = `Regenerativo ${Date.now()}`
    const tituloB = `Velocidade ${Date.now()}`
    await concluirTreinoComTempo(request, atleta.token, tituloA)
    await concluirTreinoSemConfigurarPiscina(request, atleta.token, tituloB)

    await page.goto('/historico')
    await expect(page.getByRole('row', { name: tituloA })).toBeVisible()
    await expect(page.getByRole('row', { name: tituloB })).toBeVisible()

    await page.getByLabel('Filtrar por titulo', { exact: true }).fill('Regenerativo')

    await expect(page.getByRole('row', { name: tituloA })).toBeVisible()
    await expect(page.getByRole('row', { name: tituloB })).toHaveCount(0)
  })
})
