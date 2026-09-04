import { test, expect } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'
import { concluirTreinoComTempo } from '../../utils/api'

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
    await expect(page.getByText(titulo)).toBeVisible()
    await expect(page.getByText('100m')).toBeVisible()
    await expect(page.getByLabel('Filtrar por titulo')).toBeVisible()

    await page.getByRole('button').filter({ has: page.locator('.mdi-eye-outline') }).click()

    await expect(page.getByText(`Realizado em`)).toBeVisible()
    await expect(page.getByText('Series Executadas')).toBeVisible()
    await expect(page.getByText('4x 100m Crawl')).toBeVisible()
    await page.getByRole('button', { name: 'Fechar' }).click()
  })
})
