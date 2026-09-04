import { test, expect } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'

test.describe('Tela de fichas de treino', () => {
  test('exibe estado vazio para atleta sem fichas', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')

    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()
    await expect(
      page.getByText('Nenhuma ficha ativa ainda. Crie a primeira para montar seus treinos.'),
    ).toBeVisible()
    await expect(page.getByText('Fichas: 0 / 5 Ativas')).toBeVisible()
  })

  test('cria uma ficha e adiciona uma série', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')

    const titulo = `Treino velocidade ${Date.now()}`

    await page.getByRole('button', { name: 'Nova Ficha' }).click()
    await expect(page.getByRole('heading', { name: 'Criar Nova Ficha Base' })).toBeVisible()
    await page.getByLabel('Titulo da Ficha (Ex: Treino de Velocidade 1500m)').fill(titulo)
    await page.getByRole('button', { name: 'Criar Ficha' }).click()

    await expect(page.getByText('Ficha criada com sucesso.')).toBeVisible()
    await expect(page.getByText(titulo)).toBeVisible()
    await expect(page.getByText('0 series cadastradas')).toBeVisible()

    await page.getByRole('button', { name: 'Adicionar Serie' }).click()
    await expect(page.getByRole('heading', { name: 'Adicionar Serie ao Treino' })).toBeVisible()
    await page.getByRole('button', { name: 'Adicionar Serie' }).last().click()

    await expect(page.getByText('Serie adicionada com sucesso.')).toBeVisible()
    await expect(page.getByText('4x 100m - Crawl')).toBeVisible()
    await expect(page.getByText('Pausa: 20s | Sem observacoes')).toBeVisible()
  })
})
