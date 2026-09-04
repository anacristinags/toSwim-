# Instructions

- Following Playwright test failed.
- Explain why, be concise, respect Playwright best practices.
- Provide a snippet of code with the fix, if possible.

# Test info

- Name: ui\fichas.spec.ts >> Tela de fichas de treino >> cria uma ficha e adiciona uma série
- Location: tests\ui\fichas.spec.ts:16:3

# Error details

```
Error: expect(locator).toBeVisible() failed

Locator: getByRole('heading', { name: 'Criar Nova Ficha Base' })
Expected: visible
Timeout: 5000ms
Error: element(s) not found

Call log:
  - Expect "toBeVisible" with timeout 5000ms
  - waiting for getByRole('heading', { name: 'Criar Nova Ficha Base' })

```

```yaml
- navigation:
  - text: toSwim Natação & Performance
  - separator
  - list:
    - link "Fichas de Treino":
      - /url: /fichas
    - link "Executar Treino":
      - /url: /treino-execucao
    - link "Metas de Tempo":
      - /url: /metas
    - link "Histórico":
      - /url: /historico
    - separator
    - link "Configuração Piscina":
      - /url: /piscina
    - option "Sair"
- banner:
  - button
  - text: "toSwim App Piscina: 25m"
- main:
  - heading "Fichas de Treino Base" [level=1]
  - paragraph: Crie seus gabaritos de treino reutilizaveis.
  - text: "Fichas: 0 / 5 Ativas"
  - button "Nova Ficha"
  - alert: Nenhuma ficha ativa ainda. Crie a primeira para montar seus treinos.
- dialog:
  - text: "Criar Nova Ficha Base Titulo da Ficha (Ex: Treino de Velocidade 1500m)"
  - 'textbox "Titulo da Ficha (Ex: Treino de Velocidade 1500m)"'
  - alert
  - button "Cancelar"
  - button "Criar Ficha"
```

# Test source

```ts
  1  | import { test, expect } from '@playwright/test'
  2  | import { autenticarNaPagina } from '../../utils/auth'
  3  | 
  4  | test.describe('Tela de fichas de treino', () => {
  5  |   test('exibe estado vazio para atleta sem fichas', async ({ page, request }) => {
  6  |     await autenticarNaPagina(page, request)
  7  |     await page.goto('/fichas')
  8  | 
  9  |     await expect(page.getByRole('heading', { name: 'Fichas de Treino Base' })).toBeVisible()
  10 |     await expect(
  11 |       page.getByText('Nenhuma ficha ativa ainda. Crie a primeira para montar seus treinos.'),
  12 |     ).toBeVisible()
  13 |     await expect(page.getByText('Fichas: 0 / 5 Ativas')).toBeVisible()
  14 |   })
  15 | 
  16 |   test('cria uma ficha e adiciona uma série', async ({ page, request }) => {
  17 |     await autenticarNaPagina(page, request)
  18 |     await page.goto('/fichas')
  19 | 
  20 |     const titulo = `Treino velocidade ${Date.now()}`
  21 | 
  22 |     await page.getByRole('button', { name: 'Nova Ficha' }).click()
> 23 |     await expect(page.getByRole('heading', { name: 'Criar Nova Ficha Base' })).toBeVisible()
     |                                                                                ^ Error: expect(locator).toBeVisible() failed
  24 |     await page.getByLabel('Titulo da Ficha (Ex: Treino de Velocidade 1500m)').fill(titulo)
  25 |     await page.getByRole('button', { name: 'Criar Ficha' }).click()
  26 | 
  27 |     await expect(page.getByText('Ficha criada com sucesso.')).toBeVisible()
  28 |     await expect(page.getByText(titulo)).toBeVisible()
  29 |     await expect(page.getByText('0 series cadastradas')).toBeVisible()
  30 | 
  31 |     await page.getByRole('button', { name: 'Adicionar Serie' }).click()
  32 |     await expect(page.getByRole('heading', { name: 'Adicionar Serie ao Treino' })).toBeVisible()
  33 |     await page.getByRole('button', { name: 'Adicionar Serie' }).last().click()
  34 | 
  35 |     await expect(page.getByText('Serie adicionada com sucesso.')).toBeVisible()
  36 |     await expect(page.getByText('4x 100m - Crawl')).toBeVisible()
  37 |     await expect(page.getByText('Pausa: 20s | Sem observacoes')).toBeVisible()
  38 |   })
  39 | })
  40 | 
```