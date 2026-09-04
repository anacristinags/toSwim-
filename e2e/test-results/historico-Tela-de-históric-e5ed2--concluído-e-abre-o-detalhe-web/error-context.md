# Instructions

- Following Playwright test failed.
- Explain why, be concise, respect Playwright best practices.
- Provide a snippet of code with the fix, if possible.

# Test info

- Name: ui\historico.spec.ts >> Tela de histórico de treinos >> lista treino concluído e abre o detalhe
- Location: tests\ui\historico.spec.ts:16:3

# Error details

```
Error: expect(locator).toBeVisible() failed

Locator: getByText('100m')
Expected: visible
Error: strict mode violation: getByText('100m') resolved to 2 elements:
    1) <div data-no-activator="" class="v-chip__content">100m </div> aka getByText('100m', { exact: true })
    2) <div data-no-activator="" class="v-chip__content">01:10 /100m </div> aka getByText(':10 /100m')

Call log:
  - Expect "toBeVisible" with timeout 5000ms
  - waiting for getByText('100m')

```

# Page snapshot

```yaml
- generic [ref=e4]:
  - navigation [ref=e5]:
    - generic [ref=e6]:
      - generic [ref=e7]:
        - generic [ref=e8]: 󰞍
        - generic [ref=e10]:
          - generic [ref=e11]: toSwim
          - generic [ref=e12]: Natação & Performance
      - separator [ref=e13]
      - list [ref=e14]:
        - link "Fichas de Treino" [ref=e15] [cursor=pointer]:
          - /url: /fichas
          - generic [ref=e16]: 󰨸
        - link "Executar Treino" [ref=e20] [cursor=pointer]:
          - /url: /treino-execucao
          - generic [ref=e21]: 󰐍
        - link "Metas de Tempo" [ref=e25] [cursor=pointer]:
          - /url: /metas
          - generic [ref=e26]: 󰓾
        - link "Histórico" [ref=e30] [cursor=pointer]:
          - /url: /historico
          - generic [ref=e31]: 󰋚
        - separator [ref=e35]
        - link "Configuração Piscina" [ref=e36] [cursor=pointer]:
          - /url: /piscina
          - generic [ref=e37]: 󰘆
        - option "Sair" [ref=e41] [cursor=pointer]:
          - generic [ref=e42]: 󰗽
  - banner [ref=e46]:
    - generic [ref=e47]:
      - button [ref=e48] [cursor=pointer]:
        - generic [ref=e49]: 󰍜
      - generic [ref=e51]: toSwim App
      - generic [ref=e54]:
        - generic [ref=e55]: 󰘆
        - text: "Piscina: 25m"
  - main [ref=e56]:
    - generic [ref=e58]:
      - generic [ref=e59]:
        - heading "Historico de Treinos" [level=1] [ref=e60]:
          - generic [ref=e61]: 󰋚
          - text: Historico de Treinos
        - paragraph [ref=e62]: Consulte todas as suas sessoes de natacao concluidas e seus tempos acumulados.
      - generic [ref=e63]:
        - generic [ref=e67]:
          - generic [ref=e68]: 󰍉
          - generic [ref=e70]:
            - generic: Filtrar por titulo
            - textbox "Filtrar por titulo" [ref=e71]:
              - /placeholder: "Ex: regenerativo"
          - text: 󰅙
        - generic [ref=e75]:
          - generic [ref=e76]: 󰃭
          - textbox "Filtrar por data" [ref=e79]
          - text: 󰅙
          - generic: Filtrar por data
      - table [ref=e83]:
        - rowgroup [ref=e84]:
          - row [ref=e85]:
            - columnheader "Data" [ref=e86]
            - columnheader "Treino" [ref=e87]
            - columnheader "Distancia Total" [ref=e88]
            - columnheader "Tempo Total" [ref=e89]
            - columnheader "Pace Medio" [ref=e90]
            - columnheader "Acoes" [ref=e91]
        - rowgroup [ref=e92]:
          - row [ref=e93]:
            - cell "04/09/2026" [ref=e94]
            - cell "Sessao historico 1788491471697" [ref=e95]
            - cell "100m" [ref=e97]
            - cell "1m 10s" [ref=e100]
            - cell "01:10 /100m" [ref=e101]
            - cell [ref=e104]:
              - button [ref=e105] [cursor=pointer]:
                - generic [ref=e106]: 󰛐
```

# Test source

```ts
  1  | import { test, expect } from '@playwright/test'
  2  | import { autenticarNaPagina } from '../../utils/auth'
  3  | import { concluirTreinoComTempo } from '../../utils/api'
  4  | 
  5  | test.describe('Tela de histórico de treinos', () => {
  6  |   test('exibe estado vazio quando não há treinos concluídos', async ({ page, request }) => {
  7  |     await autenticarNaPagina(page, request)
  8  |     await page.goto('/historico')
  9  | 
  10 |     await expect(page.getByRole('heading', { name: 'Historico de Treinos' })).toBeVisible()
  11 |     await expect(
  12 |       page.getByText('Nenhum treino concluido ainda. Finalize um treino na tela de Execucao.'),
  13 |     ).toBeVisible()
  14 |   })
  15 | 
  16 |   test('lista treino concluído e abre o detalhe', async ({ page, request }) => {
  17 |     const atleta = await autenticarNaPagina(page, request)
  18 |     const titulo = `Sessao historico ${Date.now()}`
  19 |     await concluirTreinoComTempo(request, atleta.token, titulo)
  20 | 
  21 |     await page.goto('/historico')
  22 |     await expect(page.getByText(titulo)).toBeVisible()
> 23 |     await expect(page.getByText('100m')).toBeVisible()
     |                                          ^ Error: expect(locator).toBeVisible() failed
  24 |     await expect(page.getByLabel('Filtrar por titulo')).toBeVisible()
  25 | 
  26 |     await page.getByRole('button').filter({ has: page.locator('.mdi-eye-outline') }).click()
  27 | 
  28 |     await expect(page.getByText(`Realizado em`)).toBeVisible()
  29 |     await expect(page.getByText('Series Executadas')).toBeVisible()
  30 |     await expect(page.getByText('4x 100m Crawl')).toBeVisible()
  31 |     await page.getByRole('button', { name: 'Fechar' }).click()
  32 |   })
  33 | })
  34 | 
```