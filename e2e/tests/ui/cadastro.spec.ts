import { test, expect } from '@playwright/test'

test.describe('Tela de cadastro de atleta', () => {
  test.beforeEach(async ({ page }) => {
    await page.goto('/cadastro')
  })

  test('exibe o formulário de cadastro', async ({ page }) => {
    await expect(page.getByRole('heading', { name: 'Criar Conta no toSwim' })).toBeVisible()
    await expect(page.getByLabel('Nome Completo')).toBeVisible()
    await expect(page.getByLabel('E-mail')).toBeVisible()
    await expect(page.getByLabel('Senha', { exact: true })).toBeVisible()
    await expect(page.getByLabel('Confirmar Senha')).toBeVisible()
  })

  test('botão de cadastro fica desabilitado enquanto o formulário é inválido', async ({ page }) => {
    const botaoCadastrar = page.getByRole('button', { name: 'Cadastrar Atleta' })
    await expect(botaoCadastrar).toBeDisabled()
  })

  test('habilita o envio após preencher os campos corretamente', async ({ page }) => {
    await page.getByLabel('Nome Completo').fill('Atleta de Teste')
    await page.getByLabel('E-mail').fill(`atleta.${Date.now()}@toswim.com`)
    await page.getByLabel('Senha', { exact: true }).fill('senha123')
    await page.getByLabel('Confirmar Senha').fill('senha123')

    await expect(page.getByRole('button', { name: 'Cadastrar Atleta' })).toBeEnabled()
  })

  test('valida senha e confirmação de senha diferentes', async ({ page }) => {
    await page.getByLabel('Nome Completo').fill('Atleta de Teste')
    await page.getByLabel('E-mail').fill('atleta@toswim.com')
    await page.getByLabel('Senha', { exact: true }).fill('senha123')
    await page.getByLabel('Confirmar Senha').fill('outrasenha')
    await page.getByLabel('Confirmar Senha').blur()

    await expect(page.getByText('As senhas não coincidem')).toBeVisible()
    await expect(page.getByRole('button', { name: 'Cadastrar Atleta' })).toBeDisabled()
  })
})
