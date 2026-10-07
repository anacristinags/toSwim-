import { test, expect, type Page } from '@playwright/test'
import { registrarAtleta } from '../../utils/auth'

const botaoEntrar = (page: Page) => page.getByRole('main').getByRole('button', { name: 'Entrar' })

async function preencherLogin(page: Page, email: string, senha: string) {
  await page.getByLabel('E-mail', { exact: true }).fill(email)
  await page.getByLabel('Senha', { exact: true }).fill(senha)
}

test.describe('Tela de login', () => {
  test('login valido direciona para /fichas', async ({ page, request }) => {
    const atleta = await registrarAtleta(request)

    await page.goto('/login')
    await expect(page.getByRole('heading', { name: 'Entrar no toSwim' })).toBeVisible()
    await preencherLogin(page, atleta.email, atleta.senha)
    await botaoEntrar(page).click()

    await expect(page).toHaveURL(/\/fichas$/)
    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()
  })

  test('preserva o parametro redirect apos o login', async ({ page, request }) => {
    const atleta = await registrarAtleta(request)

    await page.goto('/metas')
    await expect(page).toHaveURL(/\/login\?redirect=(\/|%2F)metas$/)

    await preencherLogin(page, atleta.email, atleta.senha)
    await botaoEntrar(page).click()

    await expect(page).toHaveURL(/\/metas$/)
    await expect(page.getByRole('heading', { name: 'Metas de Tempo' })).toBeVisible()
  })

  test('senha incorreta exibe erro e permanece no login', async ({ page, request }) => {
    const atleta = await registrarAtleta(request)

    await page.goto('/login')
    await preencherLogin(page, atleta.email, 'senhaErrada999')
    await botaoEntrar(page).click()

    await expect(page.locator('.v-alert')).toHaveText('Email ou senha inválidos')
    await expect(page).toHaveURL(/\/login$/)
  })

  test('e-mail nao cadastrado exibe erro e permanece no login', async ({ page }) => {
    await page.goto('/login')
    await preencherLogin(page, `inexistente.${Date.now()}@toswim.test`, 'qualquer123')
    await botaoEntrar(page).click()

    await expect(page.locator('.v-alert')).toHaveText('Email ou senha inválidos')
    await expect(page).toHaveURL(/\/login$/)
  })

  test('campos vazios impedem o envio', async ({ page }) => {
    await page.goto('/login')
    await expect(botaoEntrar(page)).toBeDisabled()

    await page.getByLabel('E-mail', { exact: true }).fill('atleta@toswim.test')
    await expect(botaoEntrar(page)).toBeDisabled()

    await page.getByLabel('E-mail', { exact: true }).fill('')
    await page.getByLabel('Senha', { exact: true }).fill('senha123')
    await expect(botaoEntrar(page)).toBeDisabled()

    await expect(page).toHaveURL(/\/login$/)
  })
})
