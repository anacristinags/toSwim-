# Execução e Evidências

Esta página registra as execuções dos testes automatizados (Playwright) do projeto, o que cada uma comprova e como gerar novas evidências. Voltar para a [Home](Home).

## Resumo

- **Execução mais recente (R3, 07/10/2026 00:00):** suíte completa com **128 testes, 128 aprovados, 0 reprovados**, em 96,5 s. A evidência é um **registro escrito** em `docs/matriz-testes.md`, sem log nem relatório anexado.
- **Execução mais recente com log bruto (B3, 04/09/2026):** 17 testes de UI, com 13 aprovados e 4 reprovados. A causa das falhas não está registrada.
- **Evidência visual da rodada final:** não localizada.

## Níveis de evidência

| Nível | O que é | O que comprova |
|---|---|---|
| **Log bruto** | Saída do Playwright salva no repositório (`e2e/resultado-*.txt`) | O resultado de cada teste naquela execução. O log não traz data: usamos a data de modificação do arquivo e do commit que o adicionou. |
| **Registro escrito** | Tabela de execuções em `docs/matriz-testes.md` | Apenas os totais declarados pela pessoa responsável. Sem relatório, trace ou log. |
| **Relatório HTML** | `e2e/playwright-report/index.html` | Somente o que está gravado nele (veja a ressalva em [Relatório HTML](#relatório-html-versionado)). |

## Execuções registradas

### Com log bruto

| # | Data | Escopo | Exec. | Aprov. | Reprov. | Duração | Arquivo |
|---|---|---|---|---|---|---|---|
| B1 | 03/09/2026, 23:49 | `npm run test:api` | 39 | 39 | 0 | 1,1 min | `e2e/resultado-api.txt` |
| B2 | 03/09/2026, 23:52 | `npm run test:ui` | 17 | 12 | 5 | 1,2 min | `e2e/resultado-ui.txt` |
| B3 | 04/09/2026, 00:01 | `npm run test:ui -- --reporter=list` | 17 | 13 | 4 | 1,1 min | `e2e/resultado-ui-final.txt` |

Os logs foram adicionados ao repositório no commit `4f763e8` (04/09/2026, 00:19). O nome `resultado-ui-final.txt` indica apenas a última execução de UI daquele dia, não a última do projeto.

### Apenas com registro escrito

| # | Data / hora (BRT) | Escopo | Exec. | Aprov. | Reprov. | Duração |
|---|---|---|---|---|---|---|
| R2 | 06/10/2026, 23:55 | Arquivos afetados, antes da correção do `FichaBaseService` | 64 | 55 | 9 | 130,8 s |
| R3-parcial | 06/10/2026, 23:59 | Arquivos afetados, código final | 64 | 64 | 0 | 80,1 s |
| **R3** | **07/10/2026, 00:00** | **Suíte completa, código final** | **128** | **128** | **0** | **96,5 s** |
| R3-iso | 07/10/2026, 00:02 | UI-FICHA-001 repetido 3 vezes | 3 tentativas (1 caso) | 3 | 0 | 38,9 s |

R3-iso conta como **3 tentativas de um único caso**, não como 3 casos.

### Relatório HTML versionado

O arquivo `e2e/playwright-report/index.html` (04/09/2026, 00:10) registra **56 testes como "skipped"**, com duração total de 0,6 s. Ele **não comprova aprovação nem reprovação de nenhum caso** e não corresponde à rodada final.

## Falhas registradas

**B2 (5 falhas):** UI-FICHA-001; UI-FICHA-002; `ui/historico` "lista treino concluído e abre o detalhe"; `ui/metas` "cria uma meta a partir de uma série existente"; `ui/treinos` "inicia o treino, registra um tempo e finaliza".

**B3 (4 falhas):** as mesmas de B2, exceto UI-FICHA-001, que passou.

Nos logs, as falhas aparecem como `expect(locator).toBeVisible() failed` (timeout de 5 s) e, no teste de execução de treino, como clique interceptado por `v-field__input`. **A causa não está registrada.**

**R2 (9 falhas):**
- FICHA-020, FICHA-021 e UI-FICHA-004, por texto corrompido;
- UI-LOGIN-003 e UI-LOGIN-004, por seletor do próprio teste;
- 4 testes de UI de fichas, entre eles o UI-FICHA-001 (os outros 3 não são identificados na fonte). Eles rodaram junto com erros `Npgsql … TimeoutException` no log da API. **A causa não foi comprovada.**

O escopo de 64 testes da R2 coincide com a soma dos 5 arquivos alterados na rodada final: `api/fichas` (30), `api/treinos` (19), `api/users` (3), `ui/fichas` (7) e `ui/login` (5). A fonte não lista os arquivos efetivamente executados.


## Ambiente das execuções

| Item | Registro |
|---|---|
| Tipo | Local (URLs padrão `localhost`; caminhos `C:\Users\…` nos logs) |
| Workers | 4 (B1–B3 e R3) |
| Retentativas | `retries: 0` fora de CI; cada teste tem uma única tentativa por execução. Nenhuma execução registrada menciona CI. |

## Evidências visuais


## Limitações conhecidas

- O arquivo `e2e/test-results/.last-run.json` (commit `b9cf2e2`, 16/09/2026) registra `"status": "passed"` e `"failedTests": []`, mas não identifica a execução nem os testes.
- A matriz registra 110 testes passando em 16/09/2026, mas **não há log dessa execução**.
- Os resultados R2, R3 e R3-iso dependem do registro escrito: não há log, relatório ou trace que permita auditá-los.

## Como gerar evidências rastreáveis

1. Suba o ambiente local conforme o `e2e/README.md`: Postgres, Liquibase e API.
2. Em `e2e/`, rode `npm test`. O relatório HTML fica em `e2e/playwright-report/`.
3. Registre a data, o commit (`git rev-parse HEAD`) e o `git status`, que deve estar limpo.
4. Guarde o relatório HTML, ou exporte JSON com `--reporter=json`, junto da documentação.
5. Para ter screenshots das telas, ative `screenshot: 'on'` numa execução dedicada ou capture as telas manualmente. Depois, adicione as imagens em [Telas do Sistema](Telas-do-Sistema).