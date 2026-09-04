# Instructions

- Following Playwright test failed.
- Explain why, be concise, respect Playwright best practices.
- Provide a snippet of code with the fix, if possible.

# Test info

- Name: ui\treinos.spec.ts >> Tela de execução do treino >> inicia o treino, registra um tempo e finaliza
- Location: tests\ui\treinos.spec.ts:18:3

# Error details

```
Test timeout of 30000ms exceeded.
```

```
Error: locator.click: Test timeout of 30000ms exceeded.
Call log:
  - waiting for getByLabel('Selecione uma Ficha Base')
    - locator resolved to <input size="1" value="" type="text" role="combobox" id="input-v-23" inputmode="none" aria-expanded="false" aria-controls="menu-v-21" aria-labelledby="input-v-23-label" aria-describedby="input-v-23-messages"/>
  - attempting click action
    2 × waiting for element to be visible, enabled and stable
      - element is visible, enabled and stable
      - scrolling into view if needed
      - done scrolling
      - <div data-no-activator="" class="v-field__input">…</div> intercepts pointer events
    - retrying click action
    - waiting 20ms
    2 × waiting for element to be visible, enabled and stable
      - element is visible, enabled and stable
      - scrolling into view if needed
      - done scrolling
      - <div data-no-activator="" class="v-field__input">…</div> intercepts pointer events
    - retrying click action
      - waiting 100ms
    52 × waiting for element to be visible, enabled and stable
       - element is visible, enabled and stable
       - scrolling into view if needed
       - done scrolling
       - <div data-no-activator="" class="v-field__input">…</div> intercepts pointer events
     - retrying click action
       - waiting 500ms

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
        - heading "Execucao do Treino do Dia" [level=1] [ref=e60]:
          - generic [ref=e61]: 󰐍
          - text: Execucao do Treino do Dia
        - paragraph [ref=e62]: Registre seus tempos e tiros diretamente na borda da piscina.
      - generic [ref=e63]:
        - heading "Escolha a Ficha para Treinar Hoje" [level=2] [ref=e64]
        - generic [ref=e65]:
          - combobox [ref=e67] [cursor=pointer]:
            - generic [ref=e68]:
              - generic: Selecione uma Ficha Base
              - generic [ref=e69]:
                - combobox "Selecione uma Ficha Base"
            - generic [ref=e70]: 󰍝
          - alert [ref=e72]:
            - generic [ref=e73]: Ate 5 fichas ativas
        - generic:
          - generic:
            - generic:
              - generic:
                - generic: Titulo do treino do dia
                - textbox "Titulo do treino do dia" [disabled]:
                  - /placeholder: "Ex: Treino regenerativo"
          - alert:
            - generic: Esse nome aparece no historico
        - generic:
          - generic:
            - generic:
              - generic:
                - generic: Comentario (opcional)
                - textbox "Comentario (opcional)" [disabled]:
                  - /placeholder: "Ex: piscina cheia, foquei na pernada"
          - alert
        - button "Iniciar Treino na Piscina" [disabled]:
          - generic: 󰓣
```

# Test source

```ts
  1  | import { test, expect } from '@playwright/test'
  2  | import { autenticarNaPagina } from '../../utils/auth'
  3  | import { criarConfigPiscina, criarFichaComSerie } from '../../utils/api'
  4  | 
  5  | test.describe('Tela de execução do treino', () => {
  6  |   test('alerta para configurar a piscina antes de treinar', async ({ page, request }) => {
  7  |     await autenticarNaPagina(page, request)
  8  |     await page.goto('/treino-execucao')
  9  | 
  10 |     await expect(page.getByRole('heading', { name: 'Execucao do Treino do Dia' })).toBeVisible()
  11 |     await expect(
  12 |       page.getByText('Configure o tamanho da piscina antes de iniciar um treino.'),
  13 |     ).toBeVisible()
  14 |     await expect(page.getByRole('link', { name: 'Ir para configuracao' })).toBeVisible()
  15 |     await expect(page.getByRole('button', { name: 'Iniciar Treino na Piscina' })).toBeDisabled()
  16 |   })
  17 | 
  18 |   test('inicia o treino, registra um tempo e finaliza', async ({ page, request }) => {
  19 |     const atleta = await autenticarNaPagina(page, request)
  20 |     const tituloFicha = `Ficha execução UI ${Date.now()}`
  21 |     await criarConfigPiscina(request, atleta.token)
  22 |     await criarFichaComSerie(request, atleta.token, tituloFicha)
  23 | 
  24 |     await page.goto('/treino-execucao')
  25 |     await expect(page.getByRole('heading', { name: 'Escolha a Ficha para Treinar Hoje' })).toBeVisible()
  26 | 
> 27 |     await page.getByLabel('Selecione uma Ficha Base').click()
     |                                                       ^ Error: locator.click: Test timeout of 30000ms exceeded.
  28 |     await page.getByRole('option', { name: tituloFicha }).click()
  29 | 
  30 |     await expect(page.getByLabel('Titulo do treino do dia')).toHaveValue(tituloFicha)
  31 |     await page.getByRole('button', { name: 'Iniciar Treino na Piscina' }).click()
  32 | 
  33 |     await expect(page.getByText('Treino em Andamento')).toBeVisible()
  34 |     await expect(page.getByText(/Serie 1:/)).toBeVisible()
  35 | 
  36 |     await page.getByLabel('Tempo (seg)').first().fill('70')
  37 |     await page.getByRole('button', { name: 'Finalizar Treino' }).click()
  38 | 
  39 |     await expect(
  40 |       page.getByText('Treino finalizado! Confira o resultado no Historico.'),
  41 |     ).toBeVisible()
  42 |   })
  43 | })
  44 | 
```