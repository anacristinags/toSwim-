import { test, expect } from '@playwright/test'
import { autenticarNaPagina } from '../../utils/auth'
import { criarConfigPiscina, criarFichaComSerie } from '../../utils/api'

test.describe('Tela de execução do treino', () => {
  test('alerta para configurar a piscina antes de treinar', async ({ page, request }) => {
    await autenticarNaPagina(page, request)
    await page.goto('/treino-execucao')

    await expect(page.getByRole('heading', { name: 'Execucao do Treino do Dia' })).toBeVisible()
    await expect(
      page.getByText('Configure o tamanho da piscina antes de iniciar um treino.'),
    ).toBeVisible()
    await expect(page.getByRole('link', { name: 'Ir para configuracao' })).toBeVisible()
    await expect(page.getByRole('button', { name: 'Iniciar Treino na Piscina' })).toBeDisabled()
  })

  test('inicia o treino, registra um tempo e finaliza', async ({ page, request }) => {
    const atleta = await autenticarNaPagina(page, request)
    const tituloFicha = `Ficha execução UI ${Date.now()}`
    await criarConfigPiscina(request, atleta.token)
    await criarFichaComSerie(request, atleta.token, tituloFicha)

    await page.goto('/treino-execucao')
    await expect(page.getByRole('heading', { name: 'Escolha a Ficha para Treinar Hoje' })).toBeVisible()

    const selecaoFicha = page.getByLabel('Selecione uma Ficha Base')
    await selecaoFicha.focus()
    await selecaoFicha.press('Enter')
    await page.getByRole('option', { name: tituloFicha }).click()

    await expect(page.getByLabel('Titulo do treino do dia')).toHaveValue(tituloFicha)
    await page.getByRole('button', { name: 'Iniciar Treino na Piscina' }).click()

    await expect(page.getByText('Treino em Andamento')).toBeVisible()
    await expect(page.getByText(/Serie 1:/)).toBeVisible()

    await page.getByLabel('Tempo (seg)').first().fill('70')
    await page.getByRole('button', { name: 'Finalizar Treino' }).click()

    await expect(
      page.getByText('Treino finalizado! Confira o resultado no Historico.'),
    ).toBeVisible()
  })
})
