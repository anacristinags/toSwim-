import { test, expect, type Page } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'
import { criarFicha, criarFichaComSerie } from '../../utils/api'

/**
 * Sincroniza com a carga real da lista de fichas (GET /fichas-base) em vez de
 * depender do timeout padrao das assercoes enquanto a tela ainda esta carregando.
 */
async function abrirTelaDeFichas(page: Page) {
  const listaDeFichas = page.waitForResponse(
    (r) => r.request().method() === 'GET' && new URL(r.url()).pathname === '/fichas-base',
  )
  await page.goto('/fichas')
  expect((await listaDeFichas).status()).toBe(200)
}

test.describe('Tela de fichas de treino', () => {
  test('exibe estado vazio para atleta sem fichas', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await abrirTelaDeFichas(page)

    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()
    await expect(
      page.getByText('Nenhuma ficha ativa ainda. Crie a primeira para montar seus treinos.'),
    ).toBeVisible()
    await expect(page.getByText('Fichas: 0 / 5 Ativas')).toBeVisible()
  })

  test('cria uma ficha e adiciona uma série', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')
    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()

    const titulo = `Treino velocidade ${Date.now()}`

    await page.getByRole('button', { name: 'Nova Ficha' }).click()
    const dialogNovaFicha = page.getByRole('dialog')
    await expect(dialogNovaFicha).toBeVisible()
    await expect(
      dialogNovaFicha.getByRole('heading', { name: 'Criar Nova Ficha Base' }),
    ).toBeVisible()
    await dialogNovaFicha.getByLabel('Titulo da Ficha (Ex: Treino de Velocidade 1500m)').fill(titulo)
    await dialogNovaFicha.getByRole('button', { name: 'Criar Ficha' }).click()
    await expect(dialogNovaFicha).toBeHidden()

    await expect(page.getByText('Ficha criada com sucesso.')).toBeVisible()
    await expect(page.getByText(titulo)).toBeVisible()
    await expect(page.getByText('0 series cadastradas')).toBeVisible()

    await page.getByRole('button', { name: 'Adicionar Serie' }).click()
    const dialogSerie = page.getByRole('dialog')
    await expect(dialogSerie).toBeVisible()
    await expect(
      dialogSerie.getByRole('heading', { name: 'Adicionar Serie ao Treino' }),
    ).toBeVisible()
    await dialogSerie.getByRole('button', { name: 'Adicionar Serie' }).click()
    await expect(dialogSerie).toBeHidden()

    await expect(page.getByText('Serie adicionada com sucesso.')).toBeVisible()
    await expect(page.getByText('4x 100m - Crawl')).toBeVisible()
    await expect(page.getByText('Pausa: 20s | Sem observacoes')).toBeVisible()
  })

  test('cria uma ficha com piscina de 50m e exibe o tamanho no card', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')
    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()

    const titulo = `Treino piscina olimpica ${Date.now()}`

    await page.getByRole('button', { name: 'Nova Ficha' }).click()
    const dialog = page.getByRole('dialog')
    await expect(dialog).toBeVisible()
    await expect(dialog.getByRole('heading', { name: 'Criar Nova Ficha Base' })).toBeVisible()

    await dialog.getByLabel('Titulo da Ficha (Ex: Treino de Velocidade 1500m)').fill(titulo)

    const selectPiscina = dialog.getByLabel('Tamanho da Piscina')
    await selectPiscina.focus()
    await selectPiscina.press('Enter')
    await page.getByRole('option', { name: '50 metros' }).click()

    await dialog.getByRole('button', { name: 'Criar Ficha' }).click()
    await expect(dialog).toBeHidden()

    await expect(page.getByText('Ficha criada com sucesso.')).toBeVisible()
    const card = page.locator('.v-card', { hasText: titulo })
    await expect(card.getByText('Piscina: 50m')).toBeVisible()
  })

  test('cria uma série com tipo de nado Livre e exibe na ficha', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/fichas')
    await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()

    const titulo = `Treino livre ${Date.now()}`

    await page.getByRole('button', { name: 'Nova Ficha' }).click()
    const dialogNovaFicha = page.getByRole('dialog')
    await expect(dialogNovaFicha).toBeVisible()
    await dialogNovaFicha.getByLabel('Titulo da Ficha (Ex: Treino de Velocidade 1500m)').fill(titulo)
    await dialogNovaFicha.getByRole('button', { name: 'Criar Ficha' }).click()
    await expect(dialogNovaFicha).toBeHidden()
    await expect(page.getByText('Ficha criada com sucesso.')).toBeVisible()

    await page.getByRole('button', { name: 'Adicionar Serie' }).click()
    const dialogSerie = page.getByRole('dialog')
    await expect(dialogSerie).toBeVisible()
    await expect(
      dialogSerie.getByRole('heading', { name: 'Adicionar Serie ao Treino' }),
    ).toBeVisible()

    const selectTipoNado = dialogSerie.getByLabel('Tipo de Nado')
    await selectTipoNado.focus()
    await selectTipoNado.press('Enter')
    await page.getByRole('option', { name: 'Livre' }).click()

    await dialogSerie.getByRole('button', { name: 'Adicionar Serie' }).click()
    await expect(dialogSerie).toBeHidden()

    await expect(page.getByText('Serie adicionada com sucesso.')).toBeVisible()
    await expect(page.getByText('4x 100m - Livre')).toBeVisible()
  })

  test('exclui uma serie cadastrada na ficha', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const titulo = `Ficha excluir serie UI ${Date.now()}`
    await criarFichaComSerie(request, atleta.token, titulo)

    await abrirTelaDeFichas(page)
    const card = page.locator('.v-card', { hasText: titulo })
    await expect(card.getByText('1 series cadastradas')).toBeVisible()

    page.once('dialog', (dialog) => dialog.accept())
    await card.getByTitle('Remover serie').click()

    await expect(page.getByText('Serie removida com sucesso.')).toBeVisible()
    await expect(card.getByText('0 series cadastradas')).toBeVisible()
  })

  test('duplica uma ficha existente', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const titulo = `Ficha duplicar UI ${Date.now()}`
    await criarFichaComSerie(request, atleta.token, titulo)

    await abrirTelaDeFichas(page)
    await expect(page.getByText('Fichas: 1 / 5 Ativas')).toBeVisible()
    await page.locator('.v-card', { hasText: titulo }).getByTitle('Duplicar Ficha').click()

    await expect(page.getByText('Ficha duplicada com sucesso.')).toBeVisible()
    await expect(page.getByText('Fichas: 2 / 5 Ativas')).toBeVisible()
    const copia = page.locator('.v-card', { hasText: `${titulo} (Cópia)` })
    await expect(copia).toBeVisible()
    await expect(copia.getByText('1 series cadastradas')).toBeVisible()
  })

  test('exibe mensagem de limite ao atingir 5 fichas ativas', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    for (let i = 1; i <= 5; i++) {
      await criarFicha(request, atleta.token, `Ficha limite ${i} ${Date.now()}`)
    }

    await abrirTelaDeFichas(page)

    await expect(
      page.getByText('Você atingiu o limite de 5 fichas ativas. Exclua uma ficha para criar outra.'),
    ).toBeVisible()
    await expect(page.getByText('Fichas: 5 / 5 Ativas')).toBeVisible()
    await expect(page.getByRole('button', { name: 'Nova Ficha' })).toBeDisabled()
    await expect(page.getByTitle('Duplicar Ficha').first()).toBeDisabled()
  })
})
