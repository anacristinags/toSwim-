# Instructions

- Following Playwright test failed.
- Explain why, be concise, respect Playwright best practices.
- Provide a snippet of code with the fix, if possible.

# Test info

- Name: ui\metas.spec.ts >> Tela de metas de tempo >> cria uma meta a partir de uma série existente
- Location: tests\ui\metas.spec.ts:15:3

# Error details

```
Error: expect(locator).toBeVisible() failed

Locator: getByRole('heading', { name: 'Criar Meta de Tempo' })
Expected: visible
Timeout: 5000ms
Error: element(s) not found

Call log:
  - Expect "toBeVisible" with timeout 5000ms
  - waiting for getByRole('heading', { name: 'Criar Meta de Tempo' })

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
  - heading "Metas de Tempo" [level=1]
  - paragraph: Acompanhe e desafie seus limites com metas por distancia e ritmo.
  - button "Nova Meta"
  - tablist:
    - tab "Todas" [selected]
    - tab "Ativas"
    - tab "Concluidas"
  - alert: Nenhuma meta encontrada para este filtro.
- dialog:
  - text: Criar Meta de Tempo
  - combobox:
    - text: Ficha para meta UI 1788491478743 · 1. 4x100m Crawl
    - combobox "Serie da Ficha": Ficha para meta UI 1788491478743 · 1. 4x100m Crawl
    - text: Serie da Ficha
  - alert
  - text: Titulo da Meta
  - textbox "Titulo da Meta"
  - alert
  - combobox:
    - text: Crawl
    - combobox "Tipo de Nado": Crawl
    - text: Tipo de Nado
  - alert
  - spinbutton "Distancia (Metros)": "100"
  - text: Distancia (Metros)
  - alert
  - spinbutton "Tempo Alvo (Segundos)": "75"
  - text: Tempo Alvo (Segundos)
  - alert
  - button "Cancelar"
  - button "Criar Meta"
```

# Test source

```ts
  1  | import { test, expect } from '@playwright/test'
  2  | import { autenticarNaPagina } from '../../utils/auth'
  3  | import { criarFichaComSerie } from '../../utils/api'
  4  | 
  5  | test.describe('Tela de metas de tempo', () => {
  6  |   test('botão Nova Meta fica desabilitado sem séries de ficha', async ({ page, request }) => {
  7  |     await autenticarNaPagina(page, request)
  8  |     await page.goto('/metas')
  9  | 
  10 |     await expect(page.getByRole('heading', { name: 'Metas de Tempo' })).toBeVisible()
  11 |     await expect(page.getByRole('button', { name: 'Nova Meta' })).toBeDisabled()
  12 |     await expect(page.getByText('Nenhuma meta encontrada para este filtro.')).toBeVisible()
  13 |   })
  14 | 
  15 |   test('cria uma meta a partir de uma série existente', async ({ page, request }) => {
  16 |     const atleta = await autenticarNaPagina(page, request)
  17 |     await criarFichaComSerie(request, atleta.token, `Ficha para meta UI ${Date.now()}`)
  18 | 
  19 |     await page.goto('/metas')
  20 |     await expect(page.getByRole('button', { name: 'Nova Meta' })).toBeEnabled()
  21 | 
  22 |     await page.getByRole('button', { name: 'Nova Meta' }).click()
> 23 |     await expect(page.getByRole('heading', { name: 'Criar Meta de Tempo' })).toBeVisible()
     |                                                                              ^ Error: expect(locator).toBeVisible() failed
  24 | 
  25 |     const titulo = `Meta UI ${Date.now()}`
  26 |     await page.getByLabel('Titulo da Meta').fill(titulo)
  27 |     await page.getByRole('button', { name: 'Criar Meta' }).click()
  28 | 
  29 |     await expect(page.getByText('Meta criada com sucesso.')).toBeVisible()
  30 |     await expect(page.getByText(titulo)).toBeVisible()
  31 |     await expect(page.getByText('Em Andamento')).toBeVisible()
  32 |   })
  33 | })
  34 | 
```