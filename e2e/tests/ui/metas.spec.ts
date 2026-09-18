import { test, expect } from '@playwright/test'
import { API_BASE, autenticarNaPagina, cabecalhoAuth } from '../../utils/auth'
import { adicionarSerie, criarFicha, criarFichaComSerie, criarMeta } from '../../utils/api'

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
    const dialog = page.getByRole('dialog')
    await expect(dialog).toBeVisible()
    await expect(dialog.getByRole('heading', { name: 'Criar Meta de Tempo' })).toBeVisible()

    const titulo = `Meta UI ${Date.now()}`
    await dialog.getByLabel('Titulo da Meta').fill(titulo)
    await dialog.getByRole('button', { name: 'Criar Meta' }).click()
    await expect(dialog).toBeHidden()

    await expect(page.getByText('Meta criada com sucesso.')).toBeVisible()
    await expect(page.getByText(titulo)).toBeVisible()
    await expect(page.getByText('Em Andamento')).toBeVisible()
  })

  test('meta vinculada a série com tipo de nado Livre exibe "Livre"', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const ficha = await criarFicha(request, atleta.token, `Ficha meta livre UI ${Date.now()}`)
    const idFicha = ficha.codFicha ?? ficha.CodFicha
    await adicionarSerie(request, atleta.token, idFicha, { tipoNado: 4 })

    await page.goto('/metas')
    await page.getByRole('button', { name: 'Nova Meta' }).click()
    const dialog = page.getByRole('dialog')
    await expect(dialog).toBeVisible()
    await expect(dialog.getByRole('heading', { name: 'Criar Meta de Tempo' })).toBeVisible()

    const titulo = `Meta livre UI ${Date.now()}`
    await dialog.getByLabel('Titulo da Meta').fill(titulo)
    await dialog.getByRole('button', { name: 'Criar Meta' }).click()
    await expect(dialog).toBeHidden()

    await expect(page.getByText('Meta criada com sucesso.')).toBeVisible()
    const card = page.locator('.v-card', { hasText: titulo })
    await expect(card.getByText(/Livre/)).toBeVisible()
    await expect(card.getByText('Piscina: 25m')).toBeVisible()
  })

  test('meta vinculada a ficha de 50m exibe "Piscina: 50m" no card', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    await criarFichaComSerie(request, atleta.token, `Ficha meta piscina 50m UI ${Date.now()}`, 50)

    await page.goto('/metas')
    await page.getByRole('button', { name: 'Nova Meta' }).click()
    const dialog = page.getByRole('dialog')
    await expect(dialog).toBeVisible()

    const titulo = `Meta piscina 50m UI ${Date.now()}`
    await dialog.getByLabel('Titulo da Meta').fill(titulo)
    await dialog.getByRole('button', { name: 'Criar Meta' }).click()
    await expect(dialog).toBeHidden()

    await expect(page.getByText('Meta criada com sucesso.')).toBeVisible()
    const card = page.locator('.v-card', { hasText: titulo })
    await expect(card.getByText('Piscina: 50m')).toBeVisible()
  })

  test('filtro por status (aba Ativas/Concluidas) reduz a lista exibida', async ({
    page,
    request,
  }) => {
    const atleta = await autenticarNaPagina(page, request)
    const { idSerie: idSerieAtiva } = await criarFichaComSerie(
      request,
      atleta.token,
      `Ficha meta ativa ${Date.now()}`,
    )
    const { idSerie: idSerieConcluida } = await criarFichaComSerie(
      request,
      atleta.token,
      `Ficha meta concluida ${Date.now()}`,
    )
    const tituloAtiva = `Meta ativa ${Date.now()}`
    const tituloConcluida = `Meta concluida ${Date.now()}`
    await criarMeta(request, atleta.token, { codSerieFicha: idSerieAtiva, tituloMeta: tituloAtiva })
    const metaConcluida = await criarMeta(request, atleta.token, {
      codSerieFicha: idSerieConcluida,
      tituloMeta: tituloConcluida,
    })
    const idMetaConcluida = metaConcluida.codMeta ?? metaConcluida.CodMeta
    await request.put(`${API_BASE}/metas/${idMetaConcluida}/status?status=1`, {
      headers: cabecalhoAuth(atleta.token),
    })

    await page.goto('/metas')
    await expect(page.getByText(tituloAtiva)).toBeVisible()
    await expect(page.getByText(tituloConcluida)).toBeVisible()

    await page.getByRole('tab', { name: 'Ativas' }).click()
    await expect(page.getByText(tituloAtiva)).toBeVisible()
    await expect(page.getByText(tituloConcluida)).toHaveCount(0)

    await page.getByRole('tab', { name: 'Concluidas' }).click()
    await expect(page.getByText(tituloConcluida)).toBeVisible()
    await expect(page.getByText(tituloAtiva)).toHaveCount(0)
  })
})
