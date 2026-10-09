# Defeitos e Retestes

Esta página lista os defeitos e as instabilidades de teste encontrados durante o desenvolvimento, o que foi alterado para corrigi-los e como cada correção foi conferida. Voltar para a [Home](Home).

## Sobre este registro

-  Os itens abaixo vêm de `docs/matriz-testes.md` (seções "Defeitos encontrados", "Estabilidade da suíte" e rodada final) e dos logs `e2e/resultado-*.txt`.
- A numeração "Registro N" serve apenas para organizar esta página. **Não é um ID formal de defeito.**
- Datas de abertura e correção, severidade e responsável não foram registradas, salvo quando indicado.
- Divergências entre o documento de requisitos e a implementação, como a ficha que pode ser salva sem séries, dependem de decisão de produto e ficam em [Planejado × Entregue](Planejado-x-Entregue#divergências-para-avaliação).
- Os resultados de reteste citados como "registro escrito" vêm da tabela de execuções da matriz. Veja os níveis de evidência em [Execução e Evidências](Execução-e-Evidências).

## Resumo

| Registro | Tipo | Descrição | Situação | Casos relacionados |
|---|---|---|---|---|
| [1](#registro-1) | Defeito do produto | Início de treino aceitava ficha sem séries | Corrigido | TREINO-007 |
| [2](#registro-2) | Defeito do produto | 28 mensagens de erro com `?` no lugar dos acentos | Corrigido | TREINO-006..009, 011..013, 014, REP-001 |
| [3](#registro-3) | Defeito do produto | 3 textos com o caractere U+FFFD em `FichaBaseService.cs` | Corrigido | FICHA-020, FICHA-021, UI-FICHA-004 |
| [4](#registro-4) | Instabilidade da suíte | Falha intermitente de UI de fichas em execução paralela | Corrigido na configuração | UI-FICHA-002 |
| [5](#registro-5) | Instabilidade da suíte | UI-FICHA-001 falhou de forma intermitente | Teste ajustado | UI-FICHA-001 |
| [6](#registro-6) | Defeito do teste | Seletor `getByRole('alert')` ambíguo nos testes de login | Corrigido | UI-LOGIN-003, UI-LOGIN-004 |

## Registros

### Registro 1

**Início de treino não validava ficha sem séries.** Tipo: defeito do produto.

- **Problema:** em 16/09/2026 (commit `b9cf2e2`), o backend aceitava e criava treino com 0 séries (resposta 201, `seriesTreino=[]`). O requisito pedia bloqueio, mas o código permitia. O teste documentava o comportamento real.
- **Correção:** `TreinoService.IniciarTreinoAsync` passou a verificar `ficha.Series.Any()` antes de validar a piscina ou criar o treino. Se a ficha não tem séries, a API responde 400 com "A ficha deve possuir pelo menos uma série." e nenhum treino é persistido. A correção já aparece na matriz do commit `a00a887` "Tratamento de erro" (18/09/2026).
- **Reteste:** TREINO-007 foi atualizado para esperar 400 e passou na R3.

### Registro 2

**Mensagens de erro com `?` no lugar dos acentos.** Tipo: defeito do produto.

- **Problema:** 28 mensagens em `TreinoService.cs` (16) e `RepeticaoSerieTreinoService.cs` (12) continham `?` literal no lugar dos acentos, como em "A ficha deve possuir pelo menos uma s?rie." A tela de execução exibe esse texto ao usuário. Os testes só verificavam o código HTTP ou a existência do campo `erro`, por isso o problema passou despercebido.
- **Correção:** os acentos foram restaurados e os arquivos foram salvos em UTF-8 (rodada final, 06 a 07/10/2026).
- **Reteste:** TREINO-006..009, 011..013, TREINO-014 e REP-001 passaram a conferir o texto exato de 8 mensagens distintas, que cobrem 15 das 28 linhas corrigidas. Todos passaram na R3.

### Registro 3

**Caractere U+FFFD em textos de `FichaBaseService.cs`.** Tipo: defeito do produto.

- **Problema:** três textos chegavam corrompidos à resposta da API:
  - "Status inv�lido...";
  - "N�o � poss�vel ativar esta ficha...";
  - o sufixo da cópia, "(C�pia)".
- **Detecção:** FICHA-020, FICHA-021 e UI-FICHA-004 falharam na R2 (06/10/2026, 23:55).
- **Correção e reteste:** os textos foram corrigidos e os três casos passaram na R3-parcial (23:59) e na R3 (07/10/2026, 00:00).

### Registro 4

**Instabilidade em execução paralela.** Tipo: instabilidade da suíte, não é defeito do produto.

- **Sintoma:** `e2e/tests/ui/fichas.spec.ts:16`, hoje UI-FICHA-002, falhava de forma intermitente quando a suíte rodava em paralelo.
- **Causa:** o `vite dev` compila cada rota sob demanda. Com vários workers acessando rotas pela primeira vez ao mesmo tempo, essa compilação podia estourar o timeout das asserções.
- **Correção:**
  - o `webServer` passou a usar `npm run build` + `vite preview`;
  - as asserções foram escopadas em `getByRole('dialog')`;
  - os `v-select` passaram a usar `focus()` + `press('Enter')`.

  Nenhum `force: true` ou `waitForTimeout` foi usado.
- **Reteste:** em 16/09/2026, a matriz registra 110 testes passando em execução paralela. O caso também passou na R3.

### Registro 5

**Falha intermitente de UI-FICHA-001.** Tipo: instabilidade da suíte.

- **Sintoma:** em 06/10/2026, às 22:17, UI-FICHA-001 falhou uma vez numa execução completa. O texto de estado vazio não apareceu em 5 s e a tela ainda estava carregando. Na R2 (23:55), o `GET /fichas-base` respondeu 500 e o log da API registrou 6 erros `Npgsql … TimeoutException`.
- **Ajuste no teste:** o teste passou a aguardar a resposta `GET /fichas-base` e a conferir o status 200. O timeout não foi alterado.
- **Reteste:** o caso passou na R3-parcial, na R3 e na R3-iso (3 de 3 tentativas).

### Registro 6

**Seletor ambíguo nos testes de login.** Tipo: defeito do próprio teste.

- **Sintoma:** UI-LOGIN-003 e UI-LOGIN-004 falharam na R2, porque `getByRole('alert')` também encontrava as mensagens de campo do Vuetify.
- **Correção:** o seletor foi trocado para `.v-alert`.
- **Reteste:** os dois casos passaram na R3-parcial e na R3.

## Observações (não são defeitos)

### Observação 1

**Hipótese descartada.** A hipótese de que `[Required]` aceitaria textos só com espaços foi verificada e descartada: o ASP.NET Core aplica `Trim()` antes da validação. Os casos FICHA-005 e AUTH-009 seguem como testes de borda.

### Observação 2

**Mudança de comportamento documentada.** No commit `4e10660`, séries sem tempo informado passaram a gravar totais e pace como `null`, e não como 0 (TREINO-019).