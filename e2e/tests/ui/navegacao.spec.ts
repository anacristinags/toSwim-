import { test, expect } from '@playwright/test'
import { autenticarNaPagina, CHAVE_TOKEN_FRONTEND } from '../../utils/auth'

test.describe('Navegação principal', () => {
  test.beforeEach(async ({ page, request }) => {
    await autenticarNaPagina(page, request)
  })

  test('rota raiz redireciona para /fichas', async ({ page }) => {
    await page.goto('/')
    await expect(page).toHaveURL(/\/fichas$/)
  })

  test('barra de topo exibe o nome da aplicação', async ({ page }) => {
    await page.goto('/')
    await expect(page.getByText('toSwim App')).toBeVisible()
  })

  test('menu lateral navega entre as telas', async ({ page }) => {
    await page.goto('/')

    await page.getByRole('link', { name: 'Executar Treino' }).click()
    await expect(page).toHaveURL(/\/treino-execucao$/)

    await page.getByRole('link', { name: 'Metas de Tempo' }).click()
    await expect(page).toHaveURL(/\/metas$/)

    await page.getByRole('link', { name: 'Histórico' }).click()
    await expect(page).toHaveURL(/\/historico$/)

    await page.getByRole('link', { name: 'Configuração Piscina' }).click()
    await expect(page).toHaveURL(/\/piscina$/)
  })
})

test.describe('Proteção de rotas e logout', () => {
  test('acesso a rota protegida sem autenticação redireciona para login', async ({ page }) => {
    await page.goto('/fichas')

    await expect(page).toHaveURL(/\/login(\?|$)/)
  })

  test('logout redireciona para login e remove o token da sessao', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')
    await expect(page).toHaveURL(/\/fichas$/)

    await page.getByText('Sair', { exact: true }).click()
    await expect(page).toHaveURL(/\/login(\?|$)/)

    const token = await page.evaluate(
      (chave) => window.localStorage.getItem(chave),
      CHAVE_TOKEN_FRONTEND,
    )
    expect(token).toBeNull()
  })
})
