# Matriz de Cobertura

[Voltar para a Home](Home).

## Descrição

Esta página relaciona **funcionalidades** a casos da matriz. A relação entre cada critério de aceite e cenário do documento de requisitos e os casos está em [Planejado × Entregue](Planejado-x-Entregue): **19 dos 38 cenários do documento** têm ao menos um caso relacionado. Veja [Execução e Evidências](Execucao-e-Evidencias) para o significado de R3 e dos níveis de evidência.

## 1. Funcionalidades × casos

| Funcionalidade | Casos relacionados | Camadas | Situação |
|---|---|---|---|
| Cadastro e login | AUTH-001..010, USER-001..002, UI-CAD-001..004, UI-LOGIN-001..005 | API, UI | AUTH-008 e AUTH-010 pendentes. A UI de cadastro só tem casos de validação do formulário. |
| Dados organizados por usuário (isolamento e proteção) | FICHA-010..012, SERIE-007..008, META-008..010, TREINO-008, TREINO-013, REP-001, HIST-004, USER-003, NAV-004..005; 401 sem token em AUTH, FICHA-001, META-001, PISC-001, TREINO-001, HIST-001, METR-001, USER-001 | API, UI | METR-005 (isolamento do progresso de meta) pendente |
| Criar fichas de treino reutilizáveis | FICHA-001..021, SERIE-001..012, UI-FICHA-001..005 | API, UI | SERIE-006 pendente |
| Selecionar ficha para o treino do dia | TREINO-002, 003, 007, 008, 014..018; teste sem ID `ui/treinos.spec.ts:18` | API, UI | Coberto |
| Registrar o tempo de cada série | TREINO-004..006, 009..012, 019, REP-001; teste sem ID `ui/treinos.spec.ts:18` | API, UI | Coberto |
| Calcular pace | META-003 (pace alvo), METR-003 (evolução de pace), METR-006..008 (progresso por piscina), TREINO-019 (pace `null` sem tempo) | API | Sem caso de UI dedicado. A fórmula do pace não está documentada na matriz. |
| Histórico | HIST-001..007; testes sem ID `ui/historico.spec.ts:57` e `:67` | API, UI | Filtro por data **a confirmar** (HIST-005) |
| Evolução | METR-001..004 | API | **Sem tela** no frontend. Métricas na UI estão listadas como melhoria futura. |
| Metas | META-001..017, METR-004..008; testes sem ID `ui/metas.spec.ts:6` e `:15` | API, UI | META-011 e METR-005 pendentes |
| Configuração de piscina **(no documento, só como critério do Login)** | PISC-001..008, TREINO-015..017; testes sem ID `ui/piscina.spec.ts:5`, `:19` e `:51` | API, UI | Coberto |
| Navegação | NAV-001..003 | UI | Coberto |
## 2. Resumo por seção da matriz

| # | Seção | Casos | Com teste | Pendentes |
|---|---|---|---|---|
| 1 | Autenticação | 10 | 8 | 2 (AUTH-008, AUTH-010) |
| 2 | Fichas base e séries | 38 | 37 | 1 (SERIE-006) |
| 3 | Metas de tempo | 20 | 19 | 1 (META-011) |
| 4 | Configuração de piscina | 8 | 8 | 0 |
| 5 | Treinos: execução | 20 | 20 | 0 |
| 6 | Histórico | 7 | 7 | 0 |
| 7 | Métricas | 5 | 4 | 1 (METR-005) |
| 8 | Usuários | 3 | 3 | 0 |
| 9 | Cadastro e login (UI) | 9 | 9 | 0 |
| 10 | Navegação e rotas (UI) | 5 | 5 | 0 |
| | **Total** | **125** | **120** | **5** |
Além desses casos, há **8 testes automatizados sem ID** na matriz, num total de 120 + 8 = 128 testes na suíte. A lista está em [Casos de Teste](Casos-de-Teste#testes-automatizados-sem-id-na-matriz).

## 3. Cobertura de endpoints (registrada na matriz)

A matriz registra este levantamento na rodada final. O total de 51 endpoints foi reconferido nesta revisão pela contagem de atributos `[Http*]` nos controllers. A classificação direta, indireta ou sem chamada **não** foi reconferida.

| Situação | Endpoints |
|---|---|
| Chamada direta por teste de API ou helper | 37 |
| Somente chamada indireta, pela UI | 2 (`GET /treinos` com `status=0` e `PUT /treinos/{id}`) |
| Nenhuma chamada | 12 (`PUT /fichas-base/{id}`, `GET /fichas-base/{id}/series`, `POST /fichas-base/series/{id}/duplicar`, `POST /treinos/{id}/series`, `GET /treinos/{id}/series`, `GET /treinos/{id}/series/{idSerie}`, `DELETE /treinos/{id}/series/{idSerie}`, `PUT /metas/{id}` e 4 endpoints de Treino × Meta) |
## 4. Matriz por caso

**Legenda:**

- **Automação:** indica se existe teste no commit `664eb29`.

- **Execução:** é o último resultado registrado.

- **Nível de evidência:** **log bruto** é a saída do Playwright salva no repositório. **Registro escrito** é o texto da rodada final em `docs/matriz-testes.md`, sem relatório bruto.

| ID | Seção | Camada | Prioridade | Automação | Teste (`e2e/tests/…`) | Execução | Nível de evidência |
|---|---|---|---|---|---|---|---|
| [AUTH-001](Casos-de-Teste#auth-001) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:5` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-002](Casos-de-Teste#auth-002) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:19` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-003](Casos-de-Teste#auth-003) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:27` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-004](Casos-de-Teste#auth-004) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:44` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-005](Casos-de-Teste#auth-005) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:57` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-006](Casos-de-Teste#auth-006) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:69` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-007](Casos-de-Teste#auth-007) | Autenticação | API | Média | Automatizado | `api/auth.spec.ts:79` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [AUTH-008](Casos-de-Teste#auth-008) | Autenticação | API | Média | Pendente (sem teste) | — | Não informado | — |
| [AUTH-009](Casos-de-Teste#auth-009) | Autenticação | API | Alta | Automatizado | `api/auth.spec.ts:35` | Aprovado (R3) | Registro escrito (R3) |
| [AUTH-010](Casos-de-Teste#auth-010) | Autenticação | API | Baixa | Pendente (sem teste) | — | Não informado | — |
| [FICHA-001](Casos-de-Teste#ficha-001) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:6` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-002](Casos-de-Teste#ficha-002) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:11` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-003](Casos-de-Teste#ficha-003) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:37` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-004](Casos-de-Teste#ficha-004) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:91` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-005](Casos-de-Teste#ficha-005) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:113` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-006](Casos-de-Teste#ficha-006) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:102` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-007](Casos-de-Teste#ficha-007) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:185` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-008](Casos-de-Teste#ficha-008) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:256` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [FICHA-009](Casos-de-Teste#ficha-009) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:126` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-010](Casos-de-Teste#ficha-010) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:22` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-011](Casos-de-Teste#ficha-011) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:146` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-012](Casos-de-Teste#ficha-012) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:163` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-013](Casos-de-Teste#ficha-013) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:52` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-014](Casos-de-Teste#ficha-014) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:66` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-015](Casos-de-Teste#ficha-015) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:80` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-016](Casos-de-Teste#ficha-016) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:228` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-017](Casos-de-Teste#ficha-017) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:64` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-009](Casos-de-Teste#serie-009) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:241` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-010](Casos-de-Teste#serie-010) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:91` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-001](Casos-de-Teste#serie-001) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:275` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-002](Casos-de-Teste#serie-002) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:288` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-003](Casos-de-Teste#serie-003) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:301` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-004](Casos-de-Teste#serie-004) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:314` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-005](Casos-de-Teste#serie-005) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:327` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-006](Casos-de-Teste#serie-006) | Fichas Base | API | Média | Pendente (sem teste) | — | Não informado | — |
| [SERIE-007](Casos-de-Teste#serie-007) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:342` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-008](Casos-de-Teste#serie-008) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:358` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-018](Casos-de-Teste#ficha-018) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:375` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-011](Casos-de-Teste#serie-011) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:394` | Aprovado (R3) | Registro escrito (R3) |
| [SERIE-012](Casos-de-Teste#serie-012) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:426` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-019](Casos-de-Teste#ficha-019) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:450` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-020](Casos-de-Teste#ficha-020) | Fichas Base | API | Média | Automatizado | `api/fichas.spec.ts:466` | Aprovado (R3) | Registro escrito (R3) |
| [FICHA-021](Casos-de-Teste#ficha-021) | Fichas Base | API | Alta | Automatizado | `api/fichas.spec.ts:476` | Aprovado (R3) | Registro escrito (R3) |
| [UI-FICHA-001](Casos-de-Teste#ui-ficha-001) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:18` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-FICHA-002](Casos-de-Teste#ui-ficha-002) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:29` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-FICHA-003](Casos-de-Teste#ui-ficha-003) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:125` | Aprovado (R3) | Registro escrito (R3) |
| [UI-FICHA-004](Casos-de-Teste#ui-ficha-004) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:141` | Aprovado (R3) | Registro escrito (R3) |
| [UI-FICHA-005](Casos-de-Teste#ui-ficha-005) | Fichas Base | UI | Alta | Automatizado | `ui/fichas.spec.ts:157` | Aprovado (R3) | Registro escrito (R3) |
| [META-001](Casos-de-Teste#meta-001) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:6` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [META-002](Casos-de-Teste#meta-002) | Metas de Tempo | API | Média | Automatizado | `api/metas.spec.ts:11` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [META-003](Casos-de-Teste#meta-003) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:24` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [META-004](Casos-de-Teste#meta-004) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:116` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [META-005](Casos-de-Teste#meta-005) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:135` | Aprovado (R3) | Registro escrito (R3) |
| [META-006](Casos-de-Teste#meta-006) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:154` | Aprovado (R3) | Registro escrito (R3) |
| [META-007](Casos-de-Teste#meta-007) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:334` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [META-008](Casos-de-Teste#meta-008) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:173` | Aprovado (R3) | Registro escrito (R3) |
| [META-009](Casos-de-Teste#meta-009) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:199` | Aprovado (R3) | Registro escrito (R3) |
| [META-010](Casos-de-Teste#meta-010) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:220` | Aprovado (R3) | Registro escrito (R3) |
| [META-011](Casos-de-Teste#meta-011) | Metas de Tempo | API | Média | Pendente (sem teste) | — | Não informado | — |
| [META-012](Casos-de-Teste#meta-012) | Metas de Tempo | UI | Alta | Automatizado | `ui/metas.spec.ts:79` | Aprovado (R3) | Registro escrito (R3) |
| [META-013](Casos-de-Teste#meta-013) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:53` | Aprovado (R3) | Registro escrito (R3) |
| [META-014](Casos-de-Teste#meta-014) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:74` | Aprovado (R3) | Registro escrito (R3) |
| [META-015](Casos-de-Teste#meta-015) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:95` | Aprovado (R3) | Registro escrito (R3) |
| [META-016](Casos-de-Teste#meta-016) | Metas de Tempo | UI | Alta | Automatizado | `ui/metas.spec.ts:37` | Aprovado (R3) | Registro escrito (R3) |
| [META-017](Casos-de-Teste#meta-017) | Metas de Tempo | UI | Alta | Automatizado | `ui/metas.spec.ts:60` | Aprovado (R3) | Registro escrito (R3) |
| [METR-006](Casos-de-Teste#metr-006) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:247` | Aprovado (R3) | Registro escrito (R3) |
| [METR-007](Casos-de-Teste#metr-007) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:278` | Aprovado (R3) | Registro escrito (R3) |
| [METR-008](Casos-de-Teste#metr-008) | Metas de Tempo | API | Alta | Automatizado | `api/metas.spec.ts:309` | Aprovado (R3) | Registro escrito (R3) |
| [PISC-001](Casos-de-Teste#pisc-001) | Configuração de Piscina | API | Alta | Automatizado | `api/piscina.spec.ts:5` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-002](Casos-de-Teste#pisc-002) | Configuração de Piscina | API | Média | Automatizado | `api/piscina.spec.ts:10` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-003](Casos-de-Teste#pisc-003) | Configuração de Piscina | API | Alta | Automatizado | `api/piscina.spec.ts:24` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-004](Casos-de-Teste#pisc-004) | Configuração de Piscina | API | Alta | Automatizado | `api/piscina.spec.ts:42` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-005](Casos-de-Teste#pisc-005) | Configuração de Piscina | API | Alta | Automatizado | `api/piscina.spec.ts:64` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-006](Casos-de-Teste#pisc-006) | Configuração de Piscina | API | Alta | Automatizado | `api/piscina.spec.ts:82` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [PISC-007](Casos-de-Teste#pisc-007) | Configuração de Piscina | API | Média | Automatizado | `api/piscina.spec.ts:53` | Aprovado (R3) | Registro escrito (R3) |
| [PISC-008](Casos-de-Teste#pisc-008) | Configuração de Piscina | UI | Média | Automatizado | `ui/piscina.spec.ts:34` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-001](Casos-de-Teste#treino-001) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:12` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-002](Casos-de-Teste#treino-002) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:19` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-003](Casos-de-Teste#treino-003) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:70` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-004](Casos-de-Teste#treino-004) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:149` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-005](Casos-de-Teste#treino-005) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:238` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-006](Casos-de-Teste#treino-006) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:255` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-007](Casos-de-Teste#treino-007) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:112` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-008](Casos-de-Teste#treino-008) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:128` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-009](Casos-de-Teste#treino-009) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:275` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-010](Casos-de-Teste#treino-010) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:297` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-011](Casos-de-Teste#treino-011) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:345` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-012](Casos-de-Teste#treino-012) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:318` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-013](Casos-de-Teste#treino-013) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:368` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-014](Casos-de-Teste#treino-014) | Treinos — execução | API | Média | Automatizado | `api/treinos.spec.ts:398` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-015](Casos-de-Teste#treino-015) | Treinos — execução | UI | Alta | Automatizado | `ui/treinos.spec.ts:6` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [TREINO-016](Casos-de-Teste#treino-016) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:95` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-017](Casos-de-Teste#treino-017) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:83` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-018](Casos-de-Teste#treino-018) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:46` | Aprovado (R3) | Registro escrito (R3) |
| [TREINO-019](Casos-de-Teste#treino-019) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:195` | Aprovado (R3) | Registro escrito (R3) |
| [REP-001](Casos-de-Teste#rep-001) | Treinos — execução | API | Alta | Automatizado | `api/treinos.spec.ts:420` | Aprovado (R3) | Registro escrito (R3) |
| [HIST-001](Casos-de-Teste#hist-001) | Histórico | API | Alta | Automatizado | `api/historico.spec.ts:12` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [HIST-002](Casos-de-Teste#hist-002) | Histórico | API | Média | Automatizado | `api/historico.spec.ts:17` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [HIST-003](Casos-de-Teste#hist-003) | Histórico | API | Alta | Automatizado | `api/historico.spec.ts:28` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [HIST-004](Casos-de-Teste#hist-004) | Histórico | API | Alta | Automatizado | `api/historico.spec.ts:91` | Aprovado (R3) | Registro escrito (R3) |
| [HIST-005](Casos-de-Teste#hist-005) | Histórico | UI | Alta | Automatizado | `ui/historico.spec.ts:112` | Aprovado (R3) | Registro escrito (R3) |
| [HIST-006](Casos-de-Teste#hist-006) | Histórico | API | Alta | Automatizado | `api/historico.spec.ts:71` | Aprovado (R3) | Registro escrito (R3) |
| [HIST-007](Casos-de-Teste#hist-007) | Histórico | UI | Alta | Automatizado | `ui/historico.spec.ts:89` | Aprovado (R3) | Registro escrito (R3) |
| [METR-001](Casos-de-Teste#metr-001) | Métricas | API | Alta | Automatizado | `api/metricas.spec.ts:6` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [METR-002](Casos-de-Teste#metr-002) | Métricas | API | Média | Automatizado | `api/metricas.spec.ts:11` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [METR-003](Casos-de-Teste#metr-003) | Métricas | API | Alta | Automatizado | `api/metricas.spec.ts:28` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [METR-004](Casos-de-Teste#metr-004) | Métricas | API | Média | Automatizado | `api/metricas.spec.ts:62` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [METR-005](Casos-de-Teste#metr-005) | Métricas | API | Média | Pendente (sem teste) | — | Não informado | — |
| [USER-001](Casos-de-Teste#user-001) | Usuários | API | Alta | Automatizado | `api/users.spec.ts:5` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [USER-002](Casos-de-Teste#user-002) | Usuários | API | Alta | Automatizado | `api/users.spec.ts:10` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [USER-003](Casos-de-Teste#user-003) | Usuários | API | Alta | Automatizado | `api/users.spec.ts:24` | Aprovado (R3) | Registro escrito (R3) |
| [UI-CAD-001](Casos-de-Teste#ui-cad-001) | Cadastro / Login | UI | Média | Automatizado | `ui/cadastro.spec.ts:8` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-CAD-002](Casos-de-Teste#ui-cad-002) | Cadastro / Login | UI | Média | Automatizado | `ui/cadastro.spec.ts:16` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-CAD-003](Casos-de-Teste#ui-cad-003) | Cadastro / Login | UI | Média | Automatizado | `ui/cadastro.spec.ts:21` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-CAD-004](Casos-de-Teste#ui-cad-004) | Cadastro / Login | UI | Alta | Automatizado | `ui/cadastro.spec.ts:30` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [UI-LOGIN-001](Casos-de-Teste#ui-login-001) | Cadastro / Login | UI | Alta | Automatizado | `ui/login.spec.ts:12` | Aprovado (R3) | Registro escrito (R3) |
| [UI-LOGIN-002](Casos-de-Teste#ui-login-002) | Cadastro / Login | UI | Alta | Automatizado | `ui/login.spec.ts:24` | Aprovado (R3) | Registro escrito (R3) |
| [UI-LOGIN-003](Casos-de-Teste#ui-login-003) | Cadastro / Login | UI | Alta | Automatizado | `ui/login.spec.ts:37` | Aprovado (R3) | Registro escrito (R3) |
| [UI-LOGIN-004](Casos-de-Teste#ui-login-004) | Cadastro / Login | UI | Alta | Automatizado | `ui/login.spec.ts:48` | Aprovado (R3) | Registro escrito (R3) |
| [UI-LOGIN-005](Casos-de-Teste#ui-login-005) | Cadastro / Login | UI | Alta | Automatizado | `ui/login.spec.ts:57` | Aprovado (R3) | Registro escrito (R3) |
| [NAV-001](Casos-de-Teste#nav-001) | Navegação, logout e proteção de rotas | UI | Média | Automatizado | `ui/navegacao.spec.ts:9` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [NAV-002](Casos-de-Teste#nav-002) | Navegação, logout e proteção de rotas | UI | Baixa | Automatizado | `ui/navegacao.spec.ts:14` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [NAV-003](Casos-de-Teste#nav-003) | Navegação, logout e proteção de rotas | UI | Alta | Automatizado | `ui/navegacao.spec.ts:19` | Aprovado (R3) | Log bruto (03–04/09) + registro escrito (R3) |
| [NAV-004](Casos-de-Teste#nav-004) | Navegação, logout e proteção de rotas | UI | Alta | Automatizado | `ui/navegacao.spec.ts:37` | Aprovado (R3) | Registro escrito (R3) |
| [NAV-005](Casos-de-Teste#nav-005) | Navegação, logout e proteção de rotas | UI | Alta | Automatizado | `ui/navegacao.spec.ts:43` | Aprovado (R3) | Registro escrito (R3) |
