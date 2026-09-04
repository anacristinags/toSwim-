import { test, expect } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'

test.describe('Tela de configuração da piscina', () => {
  test('exibe o formulário e salva a configuração padrão', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/piscina')

    await expect(page.getByRole('heading', { name: 'Configuracao da Piscina' })).toBeVisible()
    await expect(page.getByText('Piscina Curta (25 Metros)')).toBeVisible()
    await expect(page.getByText('Piscina Olimpica (50 Metros)')).toBeVisible()
    await expect(page.getByText('Por Distancia Absoluta em Metros (Ex: 100m, 200m)')).toBeVisible()

    await page.getByRole('button', { name: 'Salvar Configuracoes' }).click()

    await expect(page.getByText('Configuracoes da piscina salvas com sucesso!')).toBeVisible()
  })

  test('permite alterar para piscina olímpica e salvar novamente', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/piscina')

    await page.getByRole('radio', { name: 'Piscina Curta (25 Metros)' }).check()
    await page.getByRole('button', { name: 'Salvar Configuracoes' }).click()
    await expect(page.getByText('Configuracoes da piscina salvas com sucesso!')).toBeVisible()

    await page.getByRole('radio', { name: 'Piscina Olimpica (50 Metros)' }).check()
    await page.getByRole('radio', { name: 'Por Numero de Voltas/Piscinas (Ex: 4 voltas, 8 voltas)' }).check()
    await page.getByRole('button', { name: 'Salvar Configuracoes' }).click()

    await expect(page.getByText('Configuracoes da piscina salvas com sucesso!')).toBeVisible()
  })
})
