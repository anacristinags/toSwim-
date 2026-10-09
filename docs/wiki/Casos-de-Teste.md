# Casos de Teste

Os casos de teste do projeto, com seus IDs, cenários e resultados esperados. Voltar para a [Home](Home).

## Como ler esta página

- **Fonte dos casos:** `docs/matriz-testes.md` (commit `664eb29`). Os IDs e os textos são os da matriz. O plano de testes completo está em [Plano de Testes](Plano-de-Testes).
- **Resultados esperados:** foram derivados das regras implementadas nos services, e não de uma validação caso a caso contra o documento de requisitos. Quando um caso se relaciona com um cenário do documento ("toSwim - Documento.pdf"), isso aparece no campo **Cenário do documento de requisitos**, inclusive nas divergências. Veja [Planejado × Entregue](Planejado-x-Entregue).
- **Status na matriz:** indica a situação da **automação** (`Automatizado`, `Implementado nesta análise`, `Implementado na rodada final` ou `Pendente`), e não o resultado de uma execução. O resultado aparece no campo **Execuções registradas**.
- **Localização atual:** arquivo e linha do teste no commit `664eb29`. Alguns números de linha da matriz original estão desatualizados, por isso a referência original também foi mantida.
- **Objetivo:** a matriz não tem um campo de objetivo. O campo **Cenário** descreve o comportamento verificado.
- **Execuções:** R2, R3-parcial, R3 e R3-iso constam como registro escrito em `docs/matriz-testes.md`. Veja [Execução e Evidências](Execucao-e-Evidencias).

## Resumo

| Item | Quantidade | Fonte |
|---|---|---|
| Casos com ID na matriz | 125 | `docs/matriz-testes.md` |
| Casos com teste automatizado | 120 | matriz × arquivos `e2e/tests` |
| Casos pendentes (sem teste) | 5 | matriz (`Status = Pendente`) |
| Testes automatizados **sem ID** na matriz | 8 | arquivos `e2e/tests` ([lista](#testes-automatizados-sem-id-na-matriz)) |
| Testes na suíte | 128 (92 API + 36 UI) | arquivos `e2e/tests` |

## Índice por funcionalidade

- **1. Autenticação (/auth):** [AUTH-001](#auth-001), [AUTH-002](#auth-002), [AUTH-003](#auth-003), [AUTH-004](#auth-004), [AUTH-005](#auth-005), [AUTH-006](#auth-006), [AUTH-007](#auth-007), [AUTH-008](#auth-008), [AUTH-009](#auth-009), [AUTH-010](#auth-010)
- **2. Fichas Base (/fichas-base) e Séries (/fichas-base/{id}/series):** [FICHA-001](#ficha-001), [FICHA-002](#ficha-002), [FICHA-003](#ficha-003), [FICHA-004](#ficha-004), [FICHA-005](#ficha-005), [FICHA-006](#ficha-006), [FICHA-007](#ficha-007), [FICHA-008](#ficha-008), [FICHA-009](#ficha-009), [FICHA-010](#ficha-010), [FICHA-011](#ficha-011), [FICHA-012](#ficha-012), [FICHA-013](#ficha-013), [FICHA-014](#ficha-014), [FICHA-015](#ficha-015), [FICHA-016](#ficha-016), [FICHA-017](#ficha-017), [SERIE-009](#serie-009), [SERIE-010](#serie-010), [SERIE-001](#serie-001), [SERIE-002](#serie-002), [SERIE-003](#serie-003), [SERIE-004](#serie-004), [SERIE-005](#serie-005), [SERIE-006](#serie-006), [SERIE-007](#serie-007), [SERIE-008](#serie-008), [FICHA-018](#ficha-018), [SERIE-011](#serie-011), [SERIE-012](#serie-012), [FICHA-019](#ficha-019), [FICHA-020](#ficha-020), [FICHA-021](#ficha-021), [UI-FICHA-001](#ui-ficha-001), [UI-FICHA-002](#ui-ficha-002), [UI-FICHA-003](#ui-ficha-003), [UI-FICHA-004](#ui-ficha-004), [UI-FICHA-005](#ui-ficha-005)
- **3. Metas de Tempo (/metas):** [META-001](#meta-001), [META-002](#meta-002), [META-003](#meta-003), [META-004](#meta-004), [META-005](#meta-005), [META-006](#meta-006), [META-007](#meta-007), [META-008](#meta-008), [META-009](#meta-009), [META-010](#meta-010), [META-011](#meta-011), [META-012](#meta-012), [META-013](#meta-013), [META-014](#meta-014), [META-015](#meta-015), [META-016](#meta-016), [META-017](#meta-017), [METR-006](#metr-006), [METR-007](#metr-007), [METR-008](#metr-008)
- **4. Configuração de Piscina (/piscina-configuracao):** [PISC-001](#pisc-001), [PISC-002](#pisc-002), [PISC-003](#pisc-003), [PISC-004](#pisc-004), [PISC-005](#pisc-005), [PISC-006](#pisc-006), [PISC-007](#pisc-007), [PISC-008](#pisc-008)
- **5. Treinos — execução (/treinos):** [TREINO-001](#treino-001), [TREINO-002](#treino-002), [TREINO-003](#treino-003), [TREINO-004](#treino-004), [TREINO-005](#treino-005), [TREINO-006](#treino-006), [TREINO-007](#treino-007), [TREINO-008](#treino-008), [TREINO-009](#treino-009), [TREINO-010](#treino-010), [TREINO-011](#treino-011), [TREINO-012](#treino-012), [TREINO-013](#treino-013), [TREINO-014](#treino-014), [TREINO-015](#treino-015), [TREINO-016](#treino-016), [TREINO-017](#treino-017), [TREINO-018](#treino-018), [TREINO-019](#treino-019), [REP-001](#rep-001)
- **6. Histórico (/historico/treinos):** [HIST-001](#hist-001), [HIST-002](#hist-002), [HIST-003](#hist-003), [HIST-004](#hist-004), [HIST-005](#hist-005), [HIST-006](#hist-006), [HIST-007](#hist-007)
- **7. Métricas (/dashboard/resumo, /metricas/*):** [METR-001](#metr-001), [METR-002](#metr-002), [METR-003](#metr-003), [METR-004](#metr-004), [METR-005](#metr-005)
- **8. Usuários (/users):** [USER-001](#user-001), [USER-002](#user-002), [USER-003](#user-003)
- **9. Cadastro / Login (UI):** [UI-CAD-001](#ui-cad-001), [UI-CAD-002](#ui-cad-002), [UI-CAD-003](#ui-cad-003), [UI-CAD-004](#ui-cad-004), [UI-LOGIN-001](#ui-login-001), [UI-LOGIN-002](#ui-login-002), [UI-LOGIN-003](#ui-login-003), [UI-LOGIN-004](#ui-login-004), [UI-LOGIN-005](#ui-login-005)
- **10. Navegação, logout e proteção de rotas (UI):** [NAV-001](#nav-001), [NAV-002](#nav-002), [NAV-003](#nav-003), [NAV-004](#nav-004), [NAV-005](#nav-005)

## 1. Autenticação (/auth)

### AUTH-001

**Registro cria atleta e retorna JWT**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Registro cria atleta e retorna JWT |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | nome, email único, senha≥6 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:5`. |
| Resultado esperado | 201, body com nome/email e token _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:5` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:5` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-002

**Rejeita e-mail com formato inválido**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita e-mail com formato inválido |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | email="nao-e-um-email" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:19`. |
| Resultado esperado | 400 (`[EmailAddress]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:19` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:19` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-003

**Rejeita senha curta (<6)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita senha curta (<6) |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | senha="123" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:27`. |
| Resultado esperado | 400 (`[MinLength(6)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:27` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:27` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-004

**Rejeita e-mail duplicado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita e-mail duplicado |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta já cadastrado com o e-mail |
| Dados de teste | email repetido |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:44`. |
| Resultado esperado | 400 (`AuthService.RegistrarAsync`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:44` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:35` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-005

**Login autentica atleta cadastrado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Login autentica atleta cadastrado |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado |
| Dados de teste | email/senha corretos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:57`. |
| Resultado esperado | 200, token presente _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 1 (login válido), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:57` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:48` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-006

**Rejeita login com senha incorreta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita login com senha incorreta |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado |
| Dados de teste | senha errada |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:69`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 2 (senha incorreta), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:69` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:60` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-007

**Rejeita login de e-mail não cadastrado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita login de e-mail não cadastrado |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | email inexistente |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:79`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 3 (usuário inexistente), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:79` |
| Teste — referência na matriz | `e2e/tests/api/auth.spec.ts:70` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-008

**Rejeita nome vazio no registro**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita nome vazio no registro |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Média |
| Tipo / camada | Funcional API (sem teste automatizado) |
| Pré-condições | Nenhuma |
| Dados de teste | nome="" |
| Passos | Não informado (caso pendente; sem passos documentados). |
| Resultado esperado | 400 (`[Required]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não informado |
| Status na matriz (automação) | Pendente |
| Status de execução | Não informado. Caso pendente de automação; nenhum registro de execução, manual ou automatizada, foi localizado. |
| Execuções registradas | Nenhuma localizada |
| Evidência | Não localizada nas fontes analisadas |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | — |
| Teste — referência na matriz | — |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-009

**Rejeita nome composto apenas por espaços**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita nome composto apenas por espaços |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | nome="   " |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/auth.spec.ts:35`. |
| Resultado esperado | 400 (`[Required]` do ASP.NET Core faz `Trim()` antes de validar o tamanho da string) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Hipótese de defeito verificada e descartada ([Observação 1](Defeitos-e-Retestes.md#observação-1)). |
| Teste — localização atual | `e2e/tests/api/auth.spec.ts:35` |
| Teste — referência na matriz | `auth.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### AUTH-010

**Rejeita e-mail acima do limite de 150 caracteres**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita e-mail acima do limite de 150 caracteres |
| Funcionalidade | Autenticação (`/auth`) — Auth |
| Prioridade | Baixa |
| Tipo / camada | Funcional API (sem teste automatizado) |
| Pré-condições | Nenhuma |
| Dados de teste | email longo válido porém >150 chars |
| Passos | Não informado (caso pendente; sem passos documentados). |
| Resultado esperado | 400 (`[MaxLength(150)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não informado |
| Status na matriz (automação) | Pendente |
| Status de execução | Não informado. Caso pendente de automação; nenhum registro de execução, manual ou automatizada, foi localizado. |
| Execuções registradas | Nenhuma localizada |
| Evidência | Não localizada nas fontes analisadas |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | — |
| Teste — referência na matriz | — |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 2. Fichas Base (/fichas-base) e Séries (/fichas-base/{id}/series)

### FICHA-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:6`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:6` |
| Teste — referência na matriz | `fichas.spec.ts:6` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-002

**Lista vazia para atleta novo**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Lista vazia para atleta novo |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta sem fichas |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:11`. |
| Resultado esperado | 200, `[]` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:11` |
| Teste — referência na matriz | `fichas.spec.ts:11` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-003

**Cria ficha base**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria ficha base |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tituloFicha, tipoFicha=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:37`. |
| Resultado esperado | 201, `status=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 1 (cadastro com sucesso). **Divergência:** a ficha é criada sem séries, mas o documento exige ao menos uma série para salvar (Ficha — Cenário 6). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:37` |
| Teste — referência na matriz | `fichas.spec.ts:24` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-004

**Rejeita ficha com título vazio**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita ficha com título vazio |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tituloFicha="" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:91`. |
| Resultado esperado | 400 (`[Required]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:91` |
| Teste — referência na matriz | `fichas.spec.ts:39` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-005

**Rejeita título composto apenas por espaços**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita título composto apenas por espaços |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tituloFicha="   " |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:113`. |
| Resultado esperado | 400 (`[Required]` do ASP.NET Core faz `Trim()` antes de validar o tamanho da string) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Hipótese de defeito verificada e descartada ([Observação 1](Defeitos-e-Retestes.md#observação-1)). |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:113` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-006

**Rejeita tipoFicha fora do range 0-1**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tipoFicha fora do range 0-1 |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tipoFicha=5 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:102`. |
| Resultado esperado | 400 (`[Range(0,1)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:102` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-007

**Adiciona série, consulta detalhe e duplica ficha**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Adiciona série, consulta detalhe e duplica ficha |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | série válida |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:185`. |
| Resultado esperado | 201 série; detalhe com 1 série; duplicar cria cópia com `(Cópia)` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenários 2 (adicionar série) e 4 (duplicar ficha), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:185` |
| Teste — referência na matriz | `fichas.spec.ts:52` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-008

**Exclui ficha sem vínculos (delete físico)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Exclui ficha sem vínculos (delete físico) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha + série, sem treino iniciado |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:256`. |
| Resultado esperado | 204; some da listagem _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:256` |
| Teste — referência na matriz | `fichas.spec.ts:95` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-009

**Limite de 5 fichas ativas: 6ª criação é rejeitada**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Limite de 5 fichas ativas: 6ª criação é rejeitada |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | 5 fichas ativas já criadas |
| Dados de teste | 6ª ficha |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:126`. |
| Resultado esperado | 400 (`FichaBaseService.CriarAsync` — regra de negócio explícita) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 5 (limite de 5 fichas), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:126` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-010

**Atleta A não acessa ficha do atleta B (`GET /fichas-base/{id}`)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não acessa ficha do atleta B (`GET /fichas-base/{id}`) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas, ficha do B |
| Dados de teste | id da ficha do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:22`. |
| Resultado esperado | 404 (isolado por `codUsuario` na query) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:22` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-011

**Atleta A não exclui ficha do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não exclui ficha do atleta B |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas, ficha do B |
| Dados de teste | DELETE com token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:146`. |
| Resultado esperado | 404 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:146` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-012

**Atleta A não adiciona série na ficha do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não adiciona série na ficha do atleta B |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas, ficha do B |
| Dados de teste | POST série, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:163`. |
| Resultado esperado | 404 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:163` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-013

**Cria ficha com tamanho de piscina 50m**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria ficha com tamanho de piscina 50m |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tamanhoPiscinaM=50 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:52`. |
| Resultado esperado | 201, `tamanhoPiscinaM=50` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:52` |
| Teste — referência na matriz | `fichas.spec.ts:52` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-014

**Usa 25m como padrão quando o tamanho da piscina não é informado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Usa 25m como padrão quando o tamanho da piscina não é informado |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | sem `tamanhoPiscinaM` no payload |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:66`. |
| Resultado esperado | 201, `tamanhoPiscinaM=25` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:66` |
| Teste — referência na matriz | `fichas.spec.ts:66` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-015

**Rejeita tamanho de piscina inválido na ficha (não 25/50)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tamanho de piscina inválido na ficha (não 25/50) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tamanhoPiscinaM=33 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:80`. |
| Resultado esperado | 400 (`FichaBaseService.CriarAsync`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:80` |
| Teste — referência na matriz | `fichas.spec.ts:80` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-016

**Duplicar ficha mantém o tamanho de piscina da ficha original**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Duplicar ficha mantém o tamanho de piscina da ficha original |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha de 50m |
| Dados de teste | POST `/duplicar` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:228`. |
| Resultado esperado | 201, cópia com `tamanhoPiscinaM=50` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 4 (duplicar ficha), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:228` |
| Teste — referência na matriz | `fichas.spec.ts:228` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-017

**Cria ficha selecionando piscina de 50m e exibe "Piscina: 50m" no card**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria ficha selecionando piscina de 50m e exibe "Piscina: 50m" no card |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | seleciona "50 metros" no diálogo |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:64`. |
| Resultado esperado | Chip "Piscina: 50m" visível no card criado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:64` |
| Teste — referência na matriz | `fichas.spec.ts:51` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-009

**Cria série com tipoNado=4 (Livre)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria série com tipoNado=4 (Livre) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | tipoNado=4 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:241`. |
| Resultado esperado | 201, `tipoNado=4` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:241` |
| Teste — referência na matriz | `fichas.spec.ts:241` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-010

**Seleciona tipo de nado "Livre" ao cadastrar série e exibe na ficha**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Seleciona tipo de nado "Livre" ao cadastrar série e exibe na ficha |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | seleciona "Livre" no diálogo |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:91`. |
| Resultado esperado | "4x 100m - Livre" visível na ficha _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 2 (adicionar nova série). Diálogo, em vez de "subcard". |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:91` |
| Teste — referência na matriz | `fichas.spec.ts:78` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-001

**Rejeita distância zero**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita distância zero |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | distanciaM=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:275`. |
| Resultado esperado | 400 (`[Range(1,10000)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:275` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-002

**Rejeita distância negativa**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita distância negativa |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | distanciaM=-10 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:288`. |
| Resultado esperado | 400 (`[Range(1,10000)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:288` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-003

**Rejeita distância acima do limite máximo (10000)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita distância acima do limite máximo (10000) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | distanciaM=10001 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:301`. |
| Resultado esperado | 400 (`[Range(1,10000)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:301` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-004

**Rejeita tipoNado fora do enum válido (0-4, incluindo o novo tipo 4=Livre)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tipoNado fora do enum válido (0-4, incluindo o novo tipo 4=Livre) |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | tipoNado=99 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:314`. |
| Resultado esperado | 400 (`[Range(0,4)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:314` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Observação | O título do teste diz "(0-3)", mas a matriz cita o intervalo 0-4 (com o tipo 4 = Livre). O teste envia `tipoNado=99`. Ajustar o título do teste: a confirmar. |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-005

**Rejeita quantidadeRepeticoes zero/negativa**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita quantidadeRepeticoes zero/negativa |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | quantidadeRepeticoes=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:327`. |
| Resultado esperado | 400 (`[Range(1,1000)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:327` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-006

**Rejeita tempoPausaSeg negativo**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tempoPausaSeg negativo |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Média |
| Tipo / camada | Funcional API (sem teste automatizado) |
| Pré-condições | Ficha criada |
| Dados de teste | tempoPausaSeg=-1 |
| Passos | Não informado (caso pendente; sem passos documentados). |
| Resultado esperado | 400 (`[Range(0,3600)]`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não informado |
| Status na matriz (automação) | Pendente |
| Status de execução | Não informado. Caso pendente de automação; nenhum registro de execução, manual ou automatizada, foi localizado. |
| Execuções registradas | Nenhuma localizada |
| Evidência | Não localizada nas fontes analisadas |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | — |
| Teste — referência na matriz | — |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-007

**Atleta A não atualiza série do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não atualiza série do atleta B |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas, série do B |
| Dados de teste | PUT série do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:342`. |
| Resultado esperado | 404 (`serie.Ficha?.CodUsuario != codUsuario`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:342` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-008

**Atleta A não exclui série do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não exclui série do atleta B |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas, série do B |
| Dados de teste | DELETE série do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:358`. |
| Resultado esperado | 404 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:358` |
| Teste — referência na matriz | `fichas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-018

**Exclui série própria e reordena as restantes**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Exclui série própria e reordena as restantes |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha com 2 séries |
| Dados de teste | DELETE da 1ª série |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:375`. |
| Resultado esperado | 204; ficha fica com 1 série, `ordem=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 3 (excluir série), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:375` |
| Teste — referência na matriz | `fichas.spec.ts:375` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-011

**Edita série própria**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Edita série própria |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha com série |
| Dados de teste | PUT `/fichas-base/series/{id}` com novos valores |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:394`. |
| Resultado esperado | 200; corpo e detalhe da ficha refletem os novos valores _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:394` |
| Teste — referência na matriz | `fichas.spec.ts:394` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### SERIE-012

**Rejeita editar série para ordem já ocupada**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita editar série para ordem já ocupada |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Séries |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha com 2 séries |
| Dados de teste | PUT da 2ª série com `ordem=1` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:426`. |
| Resultado esperado | 400 "Ja existe uma serie na posicao 1 nesta ficha." _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:426` |
| Teste — referência na matriz | `fichas.spec.ts:426` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-019

**Inativa e reativa ficha**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Inativa e reativa ficha |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha ativa |
| Dados de teste | PUT `/status` com `0` e depois `1` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:450`. |
| Resultado esperado | 204 / 204; some da lista e volta com `status=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:450` |
| Teste — referência na matriz | `fichas.spec.ts:450` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-020

**Rejeita status diferente de 0/1**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita status diferente de 0/1 |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha ativa |
| Dados de teste | PUT `/status` com `2` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:466`. |
| Resultado esperado | 400 "Status inválido. Use 0 para inativo ou 1 para ativo." _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 3](Defeitos-e-Retestes.md#registro-3): falhou na R2 com texto corrompido (U+FFFD); aprovado no reteste (R3a/R3). |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:466` |
| Teste — referência na matriz | `fichas.spec.ts:466` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### FICHA-021

**Rejeita reativar ficha com 5 ativas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita reativar ficha com 5 ativas |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | 1 ficha inativa + 5 ativas |
| Dados de teste | PUT `/status` com `1` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/fichas.spec.ts:476`. |
| Resultado esperado | 400 "Não é possível ativar esta ficha. Limite de 5 fichas ativas excedido." _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 5 (limite de 5 fichas), ao reativar uma ficha inativa. A regra para fichas inativas não está no documento: **a confirmar**. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 3](Defeitos-e-Retestes.md#registro-3): falhou na R2 com texto corrompido (U+FFFD); aprovado no reteste (R3a/R3). |
| Teste — localização atual | `e2e/tests/api/fichas.spec.ts:476` |
| Teste — referência na matriz | `fichas.spec.ts:476` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-FICHA-001

**Estado vazio para atleta sem fichas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Estado vazio para atleta sem fichas |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta novo |
| Dados de teste | abrir `/fichas` aguardando a resposta de `GET /fichas-base` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:18`. |
| Resultado esperado | Resposta 200; "Nenhuma ficha ativa ainda..." e "Fichas: 0 / 5 Ativas" _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado (sincronização ajustada na rodada final) |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Falhou**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 6](Defeitos-e-Retestes.md#registro-6): falha intermitente em 06/10 22:17 e na R2; sincronização do teste ajustada; aprovado na R3a, na R3 e em 3 repetições isoladas (R3-iso). |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:18` |
| Teste — referência na matriz | `fichas.spec.ts:18` (UI) |
| Observação | Status na matriz: "Automatizado (sincronização ajustada na rodada final)". |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-FICHA-002

**Cria ficha e adiciona série**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria ficha e adiciona série |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | criar ficha e série pelos diálogos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:29`. |
| Resultado esperado | "0 series cadastradas" após criar; "4x 100m - Crawl" após a série _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenários 1 e 2. **Divergência:** o caso registra a ficha criada com "0 series cadastradas", mas o documento exige ao menos uma série para salvar (Ficha — Cenário 6). A série é adicionada por diálogo, não por "subcard". |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Falhou**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 5](Defeitos-e-Retestes.md#registro-5): instabilidade em execução paralela (`ui/fichas.spec.ts:16` à época); correção na configuração do Playwright. |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:29` |
| Teste — referência na matriz | `fichas.spec.ts:29` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-FICHA-003

**Exclui série cadastrada**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Exclui série cadastrada |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Ficha com 1 série |
| Dados de teste | "Remover serie" + confirmar |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:125`. |
| Resultado esperado | "Serie removida com sucesso."; "0 series cadastradas" _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 3 (excluir série). No documento, a exclusão ocorre "antes de salvar"; aqui a série já está salva. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:125` |
| Teste — referência na matriz | `fichas.spec.ts:125` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-FICHA-004

**Duplica ficha existente**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Duplica ficha existente |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Ficha com 1 série |
| Dados de teste | "Duplicar Ficha" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:141`. |
| Resultado esperado | "Ficha duplicada com sucesso."; "Fichas: 2 / 5 Ativas"; card "<título> (Cópia)" com 1 série _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 4 (duplicar ficha existente). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 3](Defeitos-e-Retestes.md#registro-3): falhou na R2 com o sufixo "(C�pia)" corrompido; aprovado no reteste (R3a/R3). |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:141` |
| Teste — referência na matriz | `fichas.spec.ts:141` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-FICHA-005

**Mensagem ao atingir 5 fichas ativas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Mensagem ao atingir 5 fichas ativas |
| Funcionalidade | Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`) — Fichas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | 5 fichas criadas via API |
| Dados de teste | abrir `/fichas` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/fichas.spec.ts:157`. |
| Resultado esperado | "Você atingiu o limite de 5 fichas ativas. Exclua uma ficha para criar outra."; "Fichas: 5 / 5 Ativas"; "Nova Ficha" e "Duplicar Ficha" desabilitados _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Ficha — Cenário 5 (limite de 5 fichas). Na UI, os botões ficam desabilitados. Não há tentativa de salvar bloqueada. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/fichas.spec.ts:157` |
| Teste — referência na matriz | `fichas.spec.ts:157` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 3. Metas de Tempo (/metas)

### META-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:6`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:6` |
| Teste — referência na matriz | `metas.spec.ts:6` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-002

**Lista vazia sem metas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Lista vazia sem metas |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta sem metas |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:11`. |
| Resultado esperado | 200, `[]` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 2 (sem metas), apenas na camada de API. A mensagem de estado vazio da UI não tem caso. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:11` |
| Teste — referência na matriz | `metas.spec.ts:11` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-003

**Cria meta vinculada a série própria, calcula pace alvo**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria meta vinculada a série própria, calcula pace alvo |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série do atleta |
| Dados de teste | dados válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:24`. |
| Resultado esperado | 201, `paceAlvoSeg` calculado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 3 (criar meta), na camada de API. O documento espera ser "direcionado para a tela de cadastro". A implementação usa um diálogo. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:24` |
| Teste — referência na matriz | `metas.spec.ts:24` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-004

**Rejeita tempoAlvoSeg = 0**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tempoAlvoSeg = 0 |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série do atleta |
| Dados de teste | tempoAlvoSeg=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:116`. |
| Resultado esperado | 400 (`MetaService.CriarMetaAsync`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | **Divergência:** o documento prevê tempo alvo opcional para metas de distância, mas a implementação sempre exige tempo alvo maior que zero. Não existem tipos de meta. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:116` |
| Teste — referência na matriz | `metas.spec.ts:53` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-005

**Rejeita tempoAlvoSeg negativo**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tempoAlvoSeg negativo |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série do atleta |
| Dados de teste | tempoAlvoSeg=-10 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:135`. |
| Resultado esperado | 400 (`<=0`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | **Divergência:** o documento prevê tempo alvo opcional para metas de distância, mas a implementação sempre exige tempo alvo maior que zero. Não existem tipos de meta. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:135` |
| Teste — referência na matriz | `metas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-006

**Rejeita distanciaAlvoM = 0**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita distanciaAlvoM = 0 |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série do atleta |
| Dados de teste | distanciaAlvoM=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:154`. |
| Resultado esperado | 400 (`<=0`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:154` |
| Teste — referência na matriz | `metas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-007

**Conclui e depois exclui meta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Conclui e depois exclui meta |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Meta ativa |
| Dados de teste | status=1 depois DELETE |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:334`. |
| Resultado esperado | 200 status=1; 204 exclusão _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:334` |
| Teste — referência na matriz | `metas.spec.ts:74` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-008

**Isolamento: atleta A não usa série do atleta B para criar meta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não usa série do atleta B para criar meta |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Série pertence ao atleta B |
| Dados de teste | codSerieFicha do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:173`. |
| Resultado esperado | 403 (`MetaService.CriarMetaAsync` — "não pertence ao usuário autenticado") _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:173` |
| Teste — referência na matriz | `metas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-009

**Isolamento: atleta A não acessa meta do atleta B (`GET /metas/{id}`)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não acessa meta do atleta B (`GET /metas/{id}`) |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Meta do atleta B |
| Dados de teste | id da meta do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:199`. |
| Resultado esperado | 404 (`MetaRepository.ObterPorIdAsync` filtra por `codUsuario`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:199` |
| Teste — referência na matriz | `metas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-010

**Isolamento: atleta A não exclui meta do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não exclui meta do atleta B |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Meta do atleta B |
| Dados de teste | DELETE, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:220`. |
| Resultado esperado | 404 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:220` |
| Teste — referência na matriz | `metas.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-011

**Rejeita criação de meta com série inexistente**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita criação de meta com série inexistente |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Média |
| Tipo / camada | Funcional API (sem teste automatizado) |
| Pré-condições | codSerieFicha inválido |
| Dados de teste | codSerieFicha=999999 |
| Passos | Não informado (caso pendente; sem passos documentados). |
| Resultado esperado | 404 (`"A série de ficha informada não existe."`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não informado |
| Status na matriz (automação) | Pendente |
| Status de execução | Não informado. Caso pendente de automação; nenhum registro de execução, manual ou automatizada, foi localizado. |
| Execuções registradas | Nenhuma localizada |
| Evidência | Não localizada nas fontes analisadas |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | — |
| Teste — referência na matriz | — |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-012

**Filtro de status (Todas/Ativas/Concluídas) reflete a lista exibida**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Filtro de status (Todas/Ativas/Concluídas) reflete a lista exibida |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Metas ativa e concluída existentes |
| Dados de teste | clicar nas abas |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/metas.spec.ts:79`. |
| Resultado esperado | Lista muda conforme aba selecionada _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 1 (visualizar metas); relacionado ao critério "exibir o status da meta". |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/metas.spec.ts:79` |
| Teste — referência na matriz | `metas.spec.ts` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-013

**Cria meta com tipoNado=4 (Livre)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria meta com tipoNado=4 (Livre) |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série do atleta |
| Dados de teste | tipoNado=4 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:53`. |
| Resultado esperado | 201, `tipoNado=4` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:53` |
| Teste — referência na matriz | `metas.spec.ts:53` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-014

**Meta vinculada a ficha de 25m retorna `tamanhoPiscinaM=25` (derivado via `Meta -> SerieFicha -> FichaBase`, sem coluna nova em `meta`)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Meta vinculada a ficha de 25m retorna `tamanhoPiscinaM=25` (derivado via `Meta -> SerieFicha -> FichaBase`, sem coluna nova em `meta`) |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha de 25m com série |
| Dados de teste | dados válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:74`. |
| Resultado esperado | 201, `tamanhoPiscinaM=25` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:74` |
| Teste — referência na matriz | `metas.spec.ts:74` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-015

**Meta vinculada a ficha de 50m retorna `tamanhoPiscinaM=50`**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Meta vinculada a ficha de 50m retorna `tamanhoPiscinaM=50` |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha de 50m com série |
| Dados de teste | dados válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:95`. |
| Resultado esperado | 201, `tamanhoPiscinaM=50` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:95` |
| Teste — referência na matriz | `metas.spec.ts:88` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-016

**Meta vinculada a série com tipo de nado Livre exibe "Livre" e "Piscina: 25m" no card**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Meta vinculada a série com tipo de nado Livre exibe "Livre" e "Piscina: 25m" no card |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Série tipoNado=4, ficha 25m |
| Dados de teste | criar meta |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/metas.spec.ts:37`. |
| Resultado esperado | Chips "Livre" e "Piscina: 25m" visíveis _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 1 (visualizar metas cadastradas). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/metas.spec.ts:37` |
| Teste — referência na matriz | `metas.spec.ts:37` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### META-017

**Meta vinculada a ficha de 50m exibe "Piscina: 50m" no card**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Meta vinculada a ficha de 50m exibe "Piscina: 50m" no card |
| Funcionalidade | Metas de Tempo (`/metas`) — Metas UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Ficha de 50m com série |
| Dados de teste | criar meta |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/metas.spec.ts:60`. |
| Resultado esperado | Chip "Piscina: 50m" visível _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 1 (visualizar metas cadastradas). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/metas.spec.ts:60` |
| Teste — referência na matriz | `metas.spec.ts:60` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-006

**Progresso da meta: treino de 25m NÃO influencia meta vinculada a ficha de 50m (`MetricasService.ObterProgressoMetaAsync` filtra `s.Treino.TamanhoPiscinaM == tamanhoPiscinaMeta`)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Progresso da meta: treino de 25m NÃO influencia meta vinculada a ficha de 50m (`MetricasService.ObterProgressoMetaAsync` filtra `s.Treino.TamanhoPiscinaM == tamanhoPiscinaMeta`) |
| Funcionalidade | Metas de Tempo (`/metas`) — Métricas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino 25m concluído + meta em ficha 50m, mesmo tipoNado |
| Dados de teste | GET `/metas/{id}/progresso` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:247`. |
| Resultado esperado | `historicoTentativas=[]`, `percentualAtingimento=0` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 5 (exibição do progresso), na camada de API. O documento define o progresso "com base nas execuções vinculadas"; aqui ele é filtrado por tipo de nado e tamanho de piscina. A equivalência entre as duas regras está **a confirmar**. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:247` |
| Teste — referência na matriz | `metas.spec.ts:233` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-007

**Progresso da meta: treino de 50m NÃO influencia meta vinculada a ficha de 25m**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Progresso da meta: treino de 50m NÃO influencia meta vinculada a ficha de 25m |
| Funcionalidade | Metas de Tempo (`/metas`) — Métricas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino 50m concluído + meta em ficha 25m, mesmo tipoNado |
| Dados de teste | GET `/metas/{id}/progresso` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:278`. |
| Resultado esperado | `historicoTentativas=[]`, `percentualAtingimento=0` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 5 (exibição do progresso), na camada de API. O documento define o progresso "com base nas execuções vinculadas"; aqui ele é filtrado por tipo de nado e tamanho de piscina. A equivalência entre as duas regras está **a confirmar**. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:278` |
| Teste — referência na matriz | `metas.spec.ts:264` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-008

**Progresso da meta: treino na mesma piscina da ficha da meta conta normalmente (fórmula de pace inalterada)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Progresso da meta: treino na mesma piscina da ficha da meta conta normalmente (fórmula de pace inalterada) |
| Funcionalidade | Metas de Tempo (`/metas`) — Métricas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino 50m concluído + meta em ficha 50m, mesmo tipoNado |
| Dados de teste | GET `/metas/{id}/progresso` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metas.spec.ts:309`. |
| Resultado esperado | `historicoTentativas` com pelo menos 1 item _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 5 (exibição do progresso), na camada de API. O documento define o progresso "com base nas execuções vinculadas"; aqui ele é filtrado por tipo de nado e tamanho de piscina. A equivalência entre as duas regras está **a confirmar**. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metas.spec.ts:309` |
| Teste — referência na matriz | `metas.spec.ts:295` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 4. Configuração de Piscina (/piscina-configuracao)

### PISC-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:5`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:5` |
| Teste — referência na matriz | `piscina.spec.ts:5` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-002

**404 quando não configurada**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 404 quando não configurada |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta sem config |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:10`. |
| Resultado esperado | 404, body `erro` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:10` |
| Teste — referência na matriz | `piscina.spec.ts:10` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-003

**Cria configuração válida**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cria configuração válida |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tamanhoM=25, formaContagem=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:24`. |
| Resultado esperado | 201, `status=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:24` |
| Teste — referência na matriz | `piscina.spec.ts:24` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-004

**Rejeita tamanho inválido (não 25/50)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tamanho inválido (não 25/50) |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | tamanhoM=30 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:42`. |
| Resultado esperado | 400 (`ValidarCampos`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:42` |
| Teste — referência na matriz | `piscina.spec.ts:42` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-005

**Rejeita segunda configuração para o mesmo atleta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita segunda configuração para o mesmo atleta |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Config já existente |
| Dados de teste | nova config |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:64`. |
| Resultado esperado | 400 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:64` |
| Teste — referência na matriz | `piscina.spec.ts:53` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-006

**Atualiza configuração existente**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atualiza configuração existente |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Config existente |
| Dados de teste | tamanhoM=50, formaContagem=1 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:82`. |
| Resultado esperado | 200, valores atualizados _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:82` |
| Teste — referência na matriz | `piscina.spec.ts:71` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-007

**Rejeita formaContagem fora de 0/1**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita formaContagem fora de 0/1 |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta autenticado |
| Dados de teste | formaContagem=5 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/piscina.spec.ts:53`. |
| Resultado esperado | 400 (`ValidarCampos`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/piscina.spec.ts:53` |
| Teste — referência na matriz | `piscina.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### PISC-008

**Configuração salva persiste após reload da página**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Configuração salva persiste após reload da página |
| Funcionalidade | Configuração de Piscina (`/piscina-configuracao`) — Piscina UI |
| Prioridade | Média |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Config salva |
| Dados de teste | reload |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/piscina.spec.ts:34`. |
| Resultado esperado | Valores selecionados continuam refletidos após reload _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/piscina.spec.ts:34` |
| Teste — referência na matriz | `piscina.spec.ts` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 5. Treinos — execução (/treinos)

### TREINO-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:12`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:12` |
| Teste — referência na matriz | `treinos.spec.ts:12` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-002

**Inicia treino a partir de ficha com séries**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Inicia treino a partir de ficha com séries |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha+série, piscina configurada |
| Dados de teste | dados válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:19`. |
| Resultado esperado | 201, 1 série clonada _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Execução — Cenário 1 (executar treino comum), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:19` |
| Teste — referência na matriz | `treinos.spec.ts:19` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-003

**Rejeita tamanho de piscina inválido ao iniciar**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita tamanho de piscina inválido ao iniciar |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha criada |
| Dados de teste | tamanhoPiscinaM=33 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:70`. |
| Resultado esperado | 400 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:70` |
| Teste — referência na matriz | `treinos.spec.ts:46` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-004

**Registra tiro, atualiza série e finaliza treino**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Registra tiro, atualiza série e finaliza treino |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino em andamento |
| Dados de teste | tempo/distância válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:149`. |
| Resultado esperado | 201 tiro; 200 finalizar, `status=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Execução — Cenário 1 (executar treino comum), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:149` |
| Teste — referência na matriz | `treinos.spec.ts:61` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-005

**Cancela treino em andamento**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Cancela treino em andamento |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino em andamento |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:238`. |
| Resultado esperado | 200, `status=2` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:238` |
| Teste — referência na matriz | `treinos.spec.ts:107` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-006

**Rejeita repetição com duração zero**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita repetição com duração zero |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino em andamento |
| Dados de teste | duracaoSeg=0 |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:255`. |
| Resultado esperado | 400 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:255` |
| Teste — referência na matriz | `treinos.spec.ts:124` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-007

**Rejeita iniciar treino a partir de ficha sem série**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita iniciar treino a partir de ficha sem série |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha ativa sem nenhuma série |
| Dados de teste | codFicha de ficha vazia |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:112`. |
| Resultado esperado | 400, `{ erro: "A ficha deve possuir pelo menos uma série." }` (`TreinoService.IniciarTreinoAsync` valida `ficha.Series.Any()` antes de criar o treino) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Regra do documento "a ficha deve conter ao menos uma série para ser salva" (Ficha — Cenário 6). A implementação aplica a regra **somente ao iniciar o treino**, não ao salvar a ficha. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 1](Defeitos-e-Retestes.md#registro-1): defeito corrigido (o início de treino não validava ficha sem séries); o teste passou a esperar 400 no lugar de 201. Também confere o texto da mensagem após a correção dos acentos ([Registro 2](Defeitos-e-Retestes.md#registro-2)). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:112` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-008

**Rejeita iniciar treino com ficha inexistente/de outro atleta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita iniciar treino com ficha inexistente/de outro atleta |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha pertence ao atleta B |
| Dados de teste | codFicha do B, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:128`. |
| Resultado esperado | 404 (`"Ficha base não encontrada ou inativa."`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:128` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-009

**Rejeita finalizar treino já finalizado (dupla finalização)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita finalizar treino já finalizado (dupla finalização) |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino já finalizado |
| Dados de teste | PUT `/finalizar` novamente |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:275`. |
| Resultado esperado | 400 (`"Este treino já está finalizado ou cancelado."`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:275` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-010

**Rejeita cancelar treino já finalizado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita cancelar treino já finalizado |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino já finalizado |
| Dados de teste | PUT `/cancelar` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:297`. |
| Resultado esperado | 400 (`"Apenas treinos em andamento podem ser cancelados."`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:297` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-011

**Rejeita ações (registrar repetição) em treino inexistente**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita ações (registrar repetição) em treino inexistente |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | codTreino inexistente |
| Dados de teste | POST repetição |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:345`. |
| Resultado esperado | 404 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:345` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-012

**Rejeita registrar repetição em treino já finalizado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita registrar repetição em treino já finalizado |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino finalizado |
| Dados de teste | POST repetição |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:318`. |
| Resultado esperado | 400 (`"Não é possível registrar repetições para um treino finalizado ou cancelado."`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:318` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-013

**Isolamento: atleta A não acessa/edita treino do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não acessa/edita treino do atleta B |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino do atleta B |
| Dados de teste | GET/PUT com token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:368`. |
| Resultado esperado | 404 (`TreinoRepository.ObterPorIdAsync` filtra por `codUsuario`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:368` |
| Teste — referência na matriz | `treinos.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-014

**Rejeita iniciar treino a partir de ficha inativa (status=0)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita iniciar treino a partir de ficha inativa (status=0) |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha desativada |
| Dados de teste | codFicha inativo |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:398`. |
| Resultado esperado | 404 "Ficha base não encontrada ou inativa." _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:398` |
| Teste — referência na matriz | `treinos.spec.ts:398` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-015

**Iniciar treino sem piscina configurada mantém botão desabilitado (fluxo já coberto)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Iniciar treino sem piscina configurada mantém botão desabilitado (fluxo já coberto) |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Sem config de piscina |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/treinos.spec.ts:6`. |
| Resultado esperado | Botão "Iniciar Treino na Piscina" desabilitado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/treinos.spec.ts:6` |
| Teste — referência na matriz | `treinos.spec.ts:6` (UI) |
| Observação | A matriz registra o cenário como "fluxo já coberto". |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-016

**Rejeita iniciar treino quando a piscina da ficha diverge da configuração atual do atleta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita iniciar treino quando a piscina da ficha diverge da configuração atual do atleta |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha 25m, config do atleta em 50m |
| Dados de teste | POST `/treinos` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:95`. |
| Resultado esperado | 400, mensagem citando os dois tamanhos _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:95` |
| Teste — referência na matriz | `treinos.spec.ts:95` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-017

**Rejeita iniciar treino sem o atleta ter configurado a piscina**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rejeita iniciar treino sem o atleta ter configurado a piscina |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Ficha com série, sem `ConfigPiscina` |
| Dados de teste | POST `/treinos` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:83`. |
| Resultado esperado | 400, `"Configure o tamanho da sua piscina..."` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:83` |
| Teste — referência na matriz | `treinos.spec.ts:83` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-018

**Autoridade do backend: `dto.TamanhoPiscinaM` enviado pelo cliente é ignorado; o treino criado sempre recebe `FichaBase.TamanhoPiscinaM` (já validado contra a config do atleta) — o cliente não consegue forçar outro valor no payload**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Autoridade do backend:** `dto.TamanhoPiscinaM` enviado pelo cliente é ignorado; o treino criado sempre recebe `FichaBase.TamanhoPiscinaM` (já validado contra a config do atleta) — o cliente não consegue forçar outro valor no payload |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Config=25m, ficha=25m |
| Dados de teste | payload com `tamanhoPiscinaM=50` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:46`. |
| Resultado esperado | 201, treino persistido com `tamanhoPiscinaM=25` (valor do cliente é descartado, não gera erro — contrato documentado em `IniciarTreinoRequestDto.cs` e `TreinoService.IniciarTreinoAsync`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:46` |
| Teste — referência na matriz | `treinos.spec.ts:46` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### TREINO-019

**Série sem tempo informado (totais zerados) é gravada com totais `null` e o treino finaliza**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Série sem tempo informado (totais zerados) é gravada com totais `null` e o treino finaliza |
| Funcionalidade | Treinos — execução (`/treinos`) — Treinos |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino em andamento |
| Dados de teste | PUT série com `tempoTotalSeg=0`, `distanciaTotalM=0` |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:195`. |
| Resultado esperado | 200; totais e pace da série `null`; finalizar 200, `status=1` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado (commit `4e10660`) |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Mudança de comportamento documentada no commit `4e10660` (totais `null` em vez de 0). Não registrada como defeito. |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:195` |
| Teste — referência na matriz | `treinos.spec.ts:195` |
| Observação | Status na matriz: "Automatizado (commit `4e10660`)". |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### REP-001

**Isolamento: atleta A não registra, lista, consulta, altera nem exclui repetições do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não registra, lista, consulta, altera nem exclui repetições do atleta B |
| Funcionalidade | Treinos — execução (`/treinos`) — Repetições |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino do B com 1 repetição |
| Dados de teste | POST/GET/GET item/PUT/DELETE com token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/treinos.spec.ts:420`. |
| Resultado esperado | 404 "Série de treino não encontrada para este treino." (POST e lista); 404 "Repetição não encontrada." (item, PUT, DELETE); repetição do B continua intacta _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 2](Defeitos-e-Retestes.md#registro-2): passou a conferir o texto exato da mensagem `erro` após a correção dos acentos (rodada final). |
| Teste — localização atual | `e2e/tests/api/treinos.spec.ts:420` |
| Teste — referência na matriz | `treinos.spec.ts:420` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 6. Histórico (/historico/treinos)

### HIST-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/historico.spec.ts:12`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/historico.spec.ts:12` |
| Teste — referência na matriz | `historico.spec.ts:6` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-002

**Lista vazia sem treinos concluídos**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Lista vazia sem treinos concluídos |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta sem treinos |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/historico.spec.ts:17`. |
| Resultado esperado | 200, `[]` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Histórico — Cenário 2 (histórico vazio), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/historico.spec.ts:17` |
| Teste — referência na matriz | `historico.spec.ts:11` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-003

**Lista treino concluído e rejeita detalhe de treino em andamento**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Lista treino concluído e rejeita detalhe de treino em andamento |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | 1 treino concluído + 1 em andamento |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/historico.spec.ts:28`. |
| Resultado esperado | 200 lista; 400 no detalhe do treino em andamento _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Histórico — Cenário 1 (histórico com dados), na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/historico.spec.ts:28` |
| Teste — referência na matriz | `historico.spec.ts:22` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-004

**Isolamento: atleta A não acessa histórico/detalhe de treino do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não acessa histórico/detalhe de treino do atleta B |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino concluído do atleta B |
| Dados de teste | GET detalhe, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/historico.spec.ts:91`. |
| Resultado esperado | 404 (`TreinoService.ObterPorIdAsync` filtra por `codUsuario`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/historico.spec.ts:91` |
| Teste — referência na matriz | `historico.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-005

**Filtro por título e por data reduz a lista exibida**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Filtro por título e por data reduz a lista exibida |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | 2 treinos concluídos com títulos distintos |
| Dados de teste | preencher filtro de título |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/historico.spec.ts:112`. |
| Resultado esperado | Apenas linha correspondente permanece visível _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Histórico — Cenário 4 (filtrar por título). O Cenário 3 (filtrar por data) não tem caso, embora o filtro exista na UI. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/historico.spec.ts:112` |
| Teste — referência na matriz | `historico.spec.ts` (UI) |
| Observação | A matriz cita filtro "por título e por data", mas o teste atual só cobre o filtro por título. Cobertura do filtro por data: **A confirmar**. |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-006

**Lista e detalhe refletem `tamanhoPiscinaM=50` de um treino concluído (campo já existia em `Treino`/`TreinoResponseDto`; nenhuma coluna nova foi criada)**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Lista e detalhe refletem `tamanhoPiscinaM=50` de um treino concluído (campo já existia em `Treino`/`TreinoResponseDto`; nenhuma coluna nova foi criada) |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Treino de 50m concluído |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/historico.spec.ts:71`. |
| Resultado esperado | 200; lista e detalhe com `tamanhoPiscinaM=50` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/historico.spec.ts:71` |
| Teste — referência na matriz | `historico.spec.ts:70` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### HIST-007

**Listagem e detalhe do treino exibem "Piscina: 50m"**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Listagem e detalhe do treino exibem "Piscina: 50m" |
| Funcionalidade | Histórico (`/historico/treinos`) — Histórico UI |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Treino de 50m concluído |
| Dados de teste | abrir `/historico` e o detalhe |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/historico.spec.ts:89`. |
| Resultado esperado | Chip "Piscina: 50m" visível na linha da tabela e no diálogo de detalhe _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Histórico — Cenário 1 (histórico com dados). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/historico.spec.ts:89` |
| Teste — referência na matriz | `historico.spec.ts:88` (UI) |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 7. Métricas (/dashboard/resumo, /metricas/*)

### METR-001

**401 sem token no resumo**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token no resumo |
| Funcionalidade | Métricas (`/dashboard/resumo`, `/metricas/*`) — Métricas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metricas.spec.ts:6`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Relacionado à Tela Inicial do documento (resumo), que não foi implementada no frontend. Apenas na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metricas.spec.ts:6` |
| Teste — referência na matriz | `metricas.spec.ts:6` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-002

**Resumo zerado sem treinos**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Resumo zerado sem treinos |
| Funcionalidade | Métricas (`/dashboard/resumo`, `/metricas/*`) — Métricas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta novo |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metricas.spec.ts:11`. |
| Resultado esperado | 200, contadores em 0 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Relacionado à Tela Inicial do documento (resumo), que não foi implementada no frontend. Apenas na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metricas.spec.ts:11` |
| Teste — referência na matriz | `metricas.spec.ts:11` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-003

**Resumo e evolução de pace após treino concluído**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Resumo e evolução de pace após treino concluído |
| Funcionalidade | Métricas (`/dashboard/resumo`, `/metricas/*`) — Métricas |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | 1 treino concluído |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metricas.spec.ts:28`. |
| Resultado esperado | 200; `totalTreinos=1`; pace/recordes coerentes _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Relacionado à Tela Inicial do documento (resumo e evolução), que não foi implementada no frontend. Apenas na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metricas.spec.ts:28` |
| Teste — referência na matriz | `metricas.spec.ts:28` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-004

**Progresso de meta inicia em zero sem tentativas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Progresso de meta inicia em zero sem tentativas |
| Funcionalidade | Métricas (`/dashboard/resumo`, `/metricas/*`) — Métricas |
| Prioridade | Média |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Meta criada sem execuções |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/metricas.spec.ts:62`. |
| Resultado esperado | 200, `percentualAtingimento=0` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Metas — Cenário 5 (exibição do progresso), apenas na camada de API. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/metricas.spec.ts:62` |
| Teste — referência na matriz | `metricas.spec.ts:62` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### METR-005

**Isolamento: atleta A não acessa progresso de meta do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | **Isolamento:** atleta A não acessa progresso de meta do atleta B |
| Funcionalidade | Métricas (`/dashboard/resumo`, `/metricas/*`) — Métricas |
| Prioridade | Média |
| Tipo / camada | Funcional API (sem teste automatizado) |
| Pré-condições | Meta do atleta B |
| Dados de teste | GET progresso, token do A |
| Passos | Não informado (caso pendente; sem passos documentados). |
| Resultado esperado | 404 (`MetricasService` usa `MetaRepository.ObterPorIdAsync` filtrado por `codUsuario`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não informado |
| Status na matriz (automação) | Pendente |
| Status de execução | Não informado. Caso pendente de automação; nenhum registro de execução, manual ou automatizada, foi localizado. |
| Execuções registradas | Nenhuma localizada |
| Evidência | Não localizada nas fontes analisadas |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | — |
| Teste — referência na matriz | — |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 8. Usuários (/users)

### USER-001

**401 sem token**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | 401 sem token |
| Funcionalidade | Usuários (`/users`) — Usuários |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/users.spec.ts:5`. |
| Resultado esperado | 401 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/users.spec.ts:5` |
| Teste — referência na matriz | `users.spec.ts:5` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### USER-002

**Retorna dados do atleta autenticado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Retorna dados do atleta autenticado |
| Funcionalidade | Usuários (`/users`) — Usuários |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/users.spec.ts:10`. |
| Resultado esperado | 200, nome/email corretos _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 (log `e2e/resultado-api.txt`, 39 testes de API): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-api.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/users.spec.ts:10` |
| Teste — referência na matriz | `users.spec.ts:10` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### USER-003

**Atleta A não altera senha do atleta B**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Atleta A não altera senha do atleta B |
| Funcionalidade | Usuários (`/users`) — Usuários |
| Prioridade | Alta |
| Tipo / camada | Funcional API — automatizado E2E (Playwright) |
| Pré-condições | Duas contas |
| Dados de teste | PUT `/users/{idB}/senha`, token do A |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/api/users.spec.ts:24`. |
| Resultado esperado | 403 (`Forbid()` — `User.ObterUsuarioId() != id`); login do B com a senha original continua 200 _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/api/users.spec.ts:24` |
| Teste — referência na matriz | `users.spec.ts:24` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 9. Cadastro / Login (UI)

### UI-CAD-001

**Exibe formulário de cadastro**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Exibe formulário de cadastro |
| Funcionalidade | Cadastro / Login (UI) — Cadastro |
| Prioridade | Média |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/cadastro.spec.ts:8`. |
| Resultado esperado | Campos visíveis _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/cadastro.spec.ts:8` |
| Teste — referência na matriz | `cadastro.spec.ts:8` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-CAD-002

**Botão desabilitado com formulário inválido**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Botão desabilitado com formulário inválido |
| Funcionalidade | Cadastro / Login (UI) — Cadastro |
| Prioridade | Média |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/cadastro.spec.ts:16`. |
| Resultado esperado | Botão desabilitado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/cadastro.spec.ts:16` |
| Teste — referência na matriz | `cadastro.spec.ts:16` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-CAD-003

**Habilita envio com campos válidos**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Habilita envio com campos válidos |
| Funcionalidade | Cadastro / Login (UI) — Cadastro |
| Prioridade | Média |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | dados válidos |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/cadastro.spec.ts:21`. |
| Resultado esperado | Botão habilitado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/cadastro.spec.ts:21` |
| Teste — referência na matriz | `cadastro.spec.ts:21` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-CAD-004

**Valida senha/confirmação diferentes**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Valida senha/confirmação diferentes |
| Funcionalidade | Cadastro / Login (UI) — Cadastro |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | senhas diferentes |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/cadastro.spec.ts:30`. |
| Resultado esperado | Mensagem de erro; botão desabilitado _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/cadastro.spec.ts:30` |
| Teste — referência na matriz | `cadastro.spec.ts:30` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-LOGIN-001

**Login válido direciona para `/fichas`**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Login válido direciona para `/fichas` |
| Funcionalidade | Cadastro / Login (UI) — Login |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado via API |
| Dados de teste | e-mail e senha corretos; "Entrar" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/login.spec.ts:12`. |
| Resultado esperado | URL `/fichas`; título "Fichas de Treino Base" _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 1 (login válido). **Divergência:** o documento espera o redirecionamento para a tela inicial; o caso espera `/fichas`, porque a tela inicial não existe. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/login.spec.ts:12` |
| Teste — referência na matriz | `login.spec.ts:12` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-LOGIN-002

**Preserva o parâmetro `redirect`**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Preserva o parâmetro `redirect` |
| Funcionalidade | Cadastro / Login (UI) — Login |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado; sem token |
| Dados de teste | abrir `/metas` → `/login?redirect=/metas` → login |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/login.spec.ts:24`. |
| Resultado esperado | URL `/metas`; título "Metas de Tempo" _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/login.spec.ts:24` |
| Teste — referência na matriz | `login.spec.ts:24` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-LOGIN-003

**Senha incorreta**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Senha incorreta |
| Funcionalidade | Cadastro / Login (UI) — Login |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Atleta cadastrado |
| Dados de teste | senha errada |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/login.spec.ts:37`. |
| Resultado esperado | Alerta "Email ou senha inválidos"; permanece em `/login` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 2 (senha incorreta). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 7](Defeitos-e-Retestes.md#registro-7): falhou na R2 por seletor do próprio teste; seletor corrigido; aprovado na R3a/R3. |
| Teste — localização atual | `e2e/tests/ui/login.spec.ts:37` |
| Teste — referência na matriz | `login.spec.ts:37` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-LOGIN-004

**E-mail não cadastrado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | E-mail não cadastrado |
| Funcionalidade | Cadastro / Login (UI) — Login |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | e-mail inexistente |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/login.spec.ts:48`. |
| Resultado esperado | Alerta "Email ou senha inválidos"; permanece em `/login` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 3 (usuário inexistente). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R2 — 06/10/2026 23:55 (registro escrito): **Falhou**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | [Registro 7](Defeitos-e-Retestes.md#registro-7): falhou na R2 por seletor do próprio teste; seletor corrigido; aprovado na R3a/R3. |
| Teste — localização atual | `e2e/tests/ui/login.spec.ts:48` |
| Teste — referência na matriz | `login.spec.ts:48` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### UI-LOGIN-005

**Campos vazios impedem o envio**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Campos vazios impedem o envio |
| Funcionalidade | Cadastro / Login (UI) — Login |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Nenhuma |
| Dados de teste | ambos vazios; só e-mail; só senha |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/login.spec.ts:57`. |
| Resultado esperado | "Entrar" desabilitado nos três casos; permanece em `/login` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 4 (campos obrigatórios). O documento espera "validações nos campos obrigatórios". O caso verifica o botão "Entrar" desabilitado. Mensagens nos campos: **a confirmar**. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado na rodada final** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/login.spec.ts:57` |
| Teste — referência na matriz | `login.spec.ts:57` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## 10. Navegação, logout e proteção de rotas (UI)

### NAV-001

**Rota raiz redireciona para `/fichas`**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rota raiz redireciona para `/fichas` |
| Funcionalidade | Navegação, logout e proteção de rotas (UI) — Navegação |
| Prioridade | Média |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Autenticado |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/navegacao.spec.ts:9`. |
| Resultado esperado | URL `/fichas` _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | **Divergência:** o documento prevê uma Tela Inicial, que não foi implementada. A rota `/` redireciona para `/fichas`. |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/navegacao.spec.ts:9` |
| Teste — referência na matriz | `navegacao.spec.ts:9` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### NAV-002

**Barra de topo exibe nome da aplicação**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Barra de topo exibe nome da aplicação |
| Funcionalidade | Navegação, logout e proteção de rotas (UI) — Navegação |
| Prioridade | Baixa |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Autenticado |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/navegacao.spec.ts:14`. |
| Resultado esperado | Texto "toSwim App" visível _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/navegacao.spec.ts:14` |
| Teste — referência na matriz | `navegacao.spec.ts:14` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### NAV-003

**Menu lateral navega entre telas**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Menu lateral navega entre telas |
| Funcionalidade | Navegação, logout e proteção de rotas (UI) — Navegação |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Autenticado |
| Dados de teste | — |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/navegacao.spec.ts:19`. |
| Resultado esperado | URLs corretas ao clicar em cada item _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | Automatizado |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | 03/09/2026 23:52 (log `e2e/resultado-ui.txt`): **Aprovado**<br>04/09/2026 00:01 (log `e2e/resultado-ui-final.txt`): **Aprovado**<br>R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `e2e/resultado-ui.txt`; `e2e/resultado-ui-final.txt`; `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/navegacao.spec.ts:19` |
| Teste — referência na matriz | `navegacao.spec.ts:19` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### NAV-004

**Rota protegida redireciona para `/login` quando não autenticado**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Rota protegida redireciona para `/login` quando não autenticado |
| Funcionalidade | Navegação, logout e proteção de rotas (UI) — Navegação |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Sem token no localStorage |
| Dados de teste | acessar `/fichas` diretamente |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/navegacao.spec.ts:37`. |
| Resultado esperado | Redirecionado para `/login?redirect=/fichas` (`router/index.ts` `beforeEach`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Login — Cenário 5 (acesso a tela interna sem autenticação). |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/navegacao.spec.ts:37` |
| Teste — referência na matriz | `navegacao.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

### NAV-005

**Logout redireciona para login e remove o token da sessão**

| Campo | Conteúdo |
|---|---|
| Cenário / objetivo | Logout redireciona para login e remove o token da sessão |
| Funcionalidade | Navegação, logout e proteção de rotas (UI) — Navegação |
| Prioridade | Alta |
| Tipo / camada | Funcional UI — automatizado E2E (Playwright) |
| Pré-condições | Autenticado |
| Dados de teste | clicar em "Sair" |
| Passos | Não documentados na matriz. Os passos automatizados estão no teste `e2e/tests/ui/navegacao.spec.ts:43`. |
| Resultado esperado | Redireciona para `/login`; `localStorage` não contém mais o token (`tokenStorage`) _(inferido da implementação — validar com a regra de negócio)_ |
| Cenário do documento de requisitos | Nenhuma relação identificada nesta revisão |
| Resultado obtido | Não detalhado por caso. A última execução registrada (R3) informa 128 de 128 testes aprovados. |
| Status na matriz (automação) | **Implementado nesta análise** |
| Status de execução | Aprovado na última execução registrada (R3, 07/10/2026). Registro agregado, sem relatório bruto por caso. |
| Execuções registradas | R3 — 07/10/2026 00:00 (registro escrito, suíte completa 128/128): **Aprovado** |
| Evidência | `docs/matriz-testes.md` (seção "Rodada final de QA") |
| Defeito / reteste | Nenhum registro localizado nas fontes analisadas |
| Teste — localização atual | `e2e/tests/ui/navegacao.spec.ts:43` |
| Teste — referência na matriz | `navegacao.spec.ts` |
| Fonte | `docs/matriz-testes.md`; arquivos `e2e/tests`; logs e registros citados acima |

## Testes automatizados sem ID na matriz

Estes 8 testes fazem parte da suíte de 128 testes, mas não aparecem na matriz com um ID próprio. **Não foram criados IDs novos.** A atribuição de IDs fica **A confirmar** pela pessoa responsável pela matriz.

| Arquivo e linha (commit `664eb29`) | Título do teste | Funcionalidade | Execuções registradas |
|---|---|---|---|
| `e2e/tests/ui/historico.spec.ts:57` | exibe estado vazio quando não há treinos concluídos | Histórico (UI) | 03/09 23:52: Aprovado · 04/09 00:01: Aprovado · R3: Aprovado |
| `e2e/tests/ui/historico.spec.ts:67` | lista treino concluído e abre o detalhe | Histórico (UI) | 03/09 23:52: **Falhou** · 04/09 00:01: **Falhou** · R3: Aprovado |
| `e2e/tests/ui/metas.spec.ts:6` | botão Nova Meta fica desabilitado sem séries de ficha | Metas (UI) | 03/09 23:52: Aprovado · 04/09 00:01: Aprovado · R3: Aprovado |
| `e2e/tests/ui/metas.spec.ts:15` | cria uma meta a partir de uma série existente | Metas (UI) | 03/09 23:52: **Falhou** · 04/09 00:01: **Falhou** · R3: Aprovado |
| `e2e/tests/ui/piscina.spec.ts:5` | exibe o formulário e salva a configuração padrão | Piscina (UI) | 03/09 23:52: Aprovado · 04/09 00:01: Aprovado · R3: Aprovado |
| `e2e/tests/ui/piscina.spec.ts:19` | permite alterar para piscina olímpica e salvar novamente | Piscina (UI) | 03/09 23:52: Aprovado · 04/09 00:01: Aprovado · R3: Aprovado |
| `e2e/tests/ui/piscina.spec.ts:51` | o cabeçalho reflete o tamanho da piscina imediatamente após salvar, sem reload | Piscina (UI) | R3: Aprovado |
| `e2e/tests/ui/treinos.spec.ts:18` | inicia o treino, registra um tempo e finaliza | Treino do dia (UI) | 03/09 23:52: **Falhou** · 04/09 00:01: **Falhou** · R3: Aprovado |

> O teste `ui/treinos.spec.ts:18` é citado como "UI-TREINO-001" na seção "Cobertura de endpoints" da matriz, mas esse ID não aparece em nenhuma tabela de casos.
>
> Os resultados de 03–04/09 foram associados pelo **título** do teste, porque os números de linha mudaram desde então. "R3: Aprovado" vem do registro agregado de 128 de 128 testes aprovados.