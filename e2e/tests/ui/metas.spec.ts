import { test, expect } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'
import { criarFichaComSerie } from '../../utils/api'

test.describe('Tela de metas de tempo', () => {
  test('botão Nova Meta fica desabilitado sem séries de ficha', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/metas')

    await expect(page.getByRole('heading', { name: 'Metas de Tempo' })).toBeVisible()
    await expect(page.getByRole('button', { name: 'Nova Meta' })).toBeDisabled()
    await expect(page.getByText('Nenhuma meta encontrada para este filtro.')).toBeVisible()
  })

  test('cria uma meta a partir de uma série existente', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    await criarFichaComSerie(request, atleta.token, `Ficha para meta UI ${Date.now()}`)

    await page.goto('/metas')
    await expect(page.getByRole('button', { name: 'Nova Meta' })).toBeEnabled()

    await page.getByRole('button', { name: 'Nova Meta' }).click()
    await expect(page.getByRole('heading', { name: 'Criar Meta de Tempo' })).toBeVisible()

    const titulo = `Meta UI ${Date.now()}`
    await page.getByLabel('Titulo da Meta').fill(titulo)
    await page.getByRole('button', { name: 'Criar Meta' }).click()

    await expect(page.getByText('Meta criada com sucesso.')).toBeVisible()
    await expect(page.getByText(titulo)).toBeVisible()
    await expect(page.getByText('Em Andamento')).toBeVisible()
  })
})
