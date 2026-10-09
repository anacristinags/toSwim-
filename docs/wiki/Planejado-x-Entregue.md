# Planejado × Entregue

> **Rascunho local.** Ainda não foi publicado na Wiki. Voltar para a [Home](Home.md).

## Fonte e método

- **O que foi planejado:** o documento de requisitos criado inicailmente na fase de planejamento. O documento descreve, por tela, a funcionalidade, a história de usuário, os critérios de aceite, os cenários de teste, as regras de negócio e as possíveis melhorias.
- **O que foi entregue:** o código no commit `664eb29` (rotas, views, controllers e DTOs) e os casos de `docs/matriz-testes.md`.
- **Classificação:** é **desta revisão** e foi feita a partir do código e da matriz. Ela **precisa ser validada pela autora**. "Implementado" significa que o comportamento existe no código. Não significa que foi validado por teste. A validação por teste está na coluna de casos e em [Execução e Evidências](Execucao-e-Evidencias.md).
- **Os cenários do documento não têm ID.** Eles são citados como "Tela — Cenário N", conforme a numeração do próprio documento. Nenhum ID novo foi criado.
- **Associação entre cenários e casos da matriz:** foi feita nesta revisão. Um caso relacionado **não necessariamente valida o cenário por inteiro**: as diferenças estão na coluna de observações.

**Legenda:**

| Situação | Significado |
|---|---|
| ✅ Implementado | Existe no código como descrito |
| 🟡 Parcial | Existe com diferenças em relação ao documento |
| 🔀 Divergente | O código faz algo diferente do que o documento inicial define |
| ❌ Não implementado | Não foi implementado |

## Resumo por tela

| Tela do documento | Situação | Rota no frontend | Cenários do documento com caso relacionado |
|---|---|---|---|
| [Login](#login) | ✅ Implementado  | `/login` | 5 de 5 |
| [Tela Inicial](#tela-inicial) | ❌ Não implementada (existe apenas o endpoint na API) | — | 0 de 5 |
| [Ficha de Treino](#ficha-de-treino) | 🟡 Parcial | `/fichas` | 5 de 6 |
| [Execução do treino](#execução-do-treino) | 🟡 Parcial (somente o treino comum) | `/treino-execucao` | 1 de 6 |
| [Histórico de Treinos](#histórico-de-treinos) | 🟡 Parcial | `/historico` | 4 de 6 |
| [Metas](#metas) | 🟡 Parcial | `/metas` | 4 de 5 |
| [Detalhe da Meta](#detalhe-da-meta) | ❌ Não implementada | — | 0 de 5 |
| **Total** | | | **19 de 38** |

Também foram entregues telas e funcionalidades que **não foram planejadas inicialmente**:

- **Cadastro (`/cadastro`):** inicalmente, era uma "Possíveil melhoria".
- **Configuração da Piscina (`/piscina`):** o plano inicial era para que a escolha da piscina de 25 ou 50 m fosse realizado na tela de Login e a configuração da pisicna seria uma melhoria futura.
- **Tamanho de piscina por ficha** 
- **Tipo de nado "Livre"** 

## Objetivos do sistema (documento)

| Objetivo | Situação | Observação |
|---|---|---|
| Criar fichas de treino base | ✅ | Tela `/fichas` |
| Reutilizar treinos em diferentes dias | ✅ | A ficha é clonada a cada treino iniciado  |
| Registrar a execução do treino do dia | ✅ | Tela `/treino-execucao`, apenas para o treino comum |
| Calcular pace e demais métricas | 🟡 | O pace aparece no histórico e nas metas. As métricas (resumo, pace médio geral, melhores tempos) existem só na API, sem tela. |
| Criar e acompanhar metas | 🟡 | A tela de detalhe da meta não foi implementada |
| Consultar histórico e evolução | 🟡 | O histórico tem tela. A evolução existe só na API. |
| Manter os dados separados por usuário | ✅ | Coberto por casos de isolamento entre atletas (veja a [Matriz](Matriz-de-Cobertura.md)) |

## Login

### Critérios de aceite

| Critério do documento | Situação | Evidência e observação |
|---|---|---|
| Informar as credenciais de acesso | ✅ | `LoginView.vue`. Casos UI-LOGIN-001, AUTH-005. |
| Escolher uma piscina de 25 ou 50 m | 🔀 | A escolha **não** fica no login. Ela é feita na tela `/piscina` e na criação da ficha. Foi uma melhoria implementada seguindo o documento inicial. |
| Validar os dados informados | ✅ | UI-LOGIN-003..005, AUTH-006/007 |
| Login válido direciona para a tela inicial | 🔀 | Direciona para `/fichas` (UI-LOGIN-001), porque a tela inicial não existe |
| Login inválido exibe mensagem de erro | ✅ | O alerta "Email ou senha inválidos" aparece (UI-LOGIN-003/004) |
| Não permite acesso sem autenticação | ✅ | NAV-004; 401 sem token nos endpoints |
| Informações carregadas pertencem só ao usuário autenticado | ✅ | Casos de isolamento (FICHA-010..012, META-008..010, TREINO-013, HIST-004, entre outros) |

### Cenários de teste

| Cenário do documento planejado | Casos relacionados | Observação |
|---|---|---|
| Login — Cenário 1: login válido | [UI-LOGIN-001](Casos-de-Teste.md#ui-login-001), [AUTH-005](Casos-de-Teste.md#auth-005) | O resultado esperado diverge: o teste espera `/fichas` e o planejamento espera a tela inicial |
| Login — Cenário 2: senha incorreta | [UI-LOGIN-003](Casos-de-Teste.md#ui-login-003), [AUTH-006](Casos-de-Teste.md#auth-006) | — |
| Login — Cenário 3: usuário inexistente | [UI-LOGIN-004](Casos-de-Teste.md#ui-login-004), [AUTH-007](Casos-de-Teste.md#auth-007) | — |
| Login — Cenário 4: campos obrigatórios vazios | [UI-LOGIN-005](Casos-de-Teste.md#ui-login-005) | O planejamento espera "exibe validações nos campos". O botão fica desabilitado. Mensagens nos campos: **a confirmar**. |
| Login — Cenário 5: rota interna sem autenticação | [NAV-004](Casos-de-Teste.md#nav-004) | — |

**Possíveis melhorias do documento:**

- menos texto nas telas de login e cadastro;
- seleção da piscina como segunda etapa do cadastro;
- padrão e limites de caracteres para usuário e senha. AUTH-010, que trata do limite do e-mail, está pendente.

## Tela Inicial

**Situação:** ❌ não implementada. O planejamento inical prevê:

- um resumo do usuário: quantidade de fichas, fichas, metas ativas e evolução recente;
- atalhos para as principais funcionalidades;
- um estado vazio com orientação para criar o primeiro treino.

No código, a rota `/` redireciona para `/fichas` (NAV-001). A API tem o endpoint `GET /dashboard/resumo`, coberto por METR-001..003, sem uso no frontend. A matriz lista "nova tela inicial e métricas na UI" como **melhoria futura**.

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Tela Inicial — Cenários 1 a 5 (dados cadastrados, fichas, execuções recentes, sem metas, atalhos) | Nenhum | Tela não implementada. METR-001..003 cobrem apenas a API de resumo. |

**Possível melhoria do documento:** exibir os melhores tempos por prova, com filtro, em vez do pace médio. A API tem `GET /metricas/melhores-tempos`, que não é chamado por nenhum teste, segundo a matriz.

## Ficha de Treino

### Critérios de aceite e regras

| Critério ou regra do documento | Situação | Evidência e observação |
|---|---|---|
| Criar uma ficha de treino base | ✅ | UI-FICHA-002, FICHA-003 |
| A ficha contém informações gerais e séries com detalhes | ✅ | Título, piscina e séries (repetições, distância, tipo de nado, pausa) |
| Adicionar séries à ficha | 🔀 | Feito por diálogo, depois que a ficha já foi criada. O documento descreve um "subcard" no mesmo fluxo de salvamento. |
| Excluir séries **antes de salvar** | 🔀 | É possível excluir séries de uma ficha já salva (UI-FICHA-003, FICHA-018). Não há etapa "antes de salvar", porque cada série é gravada individualmente. |
| Duplicar uma ficha existente | ✅ | Cria a cópia "(Cópia)" (UI-FICHA-004, FICHA-007, FICHA-016) |
| No máximo 5 fichas; impedir a criação ao atingir o limite | ✅ | Limite de 5 fichas **ativas**. Uma ficha inativa pode ser reativada se houver vaga (FICHA-009, FICHA-021, UI-FICHA-005). A regra para fichas inativas não foi planejada inicialmente no documento. |
| **A ficha deve conter ao menos uma série para ser salva** | 🔀 | A ficha é criada sem séries: FICHA-003 envia só título e tipo, e UI-FICHA-002 mostra "0 series cadastradas" após criar. A regra só é aplicada **ao iniciar um treino** (TREINO-007). |
| A ficha salva fica disponível para a execução do treino do dia | ✅ | TREINO-002; seleção na tela `/treino-execucao` |

### Cenários de teste

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Ficha — Cenário 1: cadastro com sucesso | [UI-FICHA-002](Casos-de-Teste.md#ui-ficha-002), [FICHA-003](Casos-de-Teste.md#ficha-003) | O fluxo implementado é feito em duas etapas: primeiro a ficha, depois as séries |
| Ficha — Cenário 2: adicionar nova série | [UI-FICHA-002](Casos-de-Teste.md#ui-ficha-002), [SERIE-010](Casos-de-Teste.md#serie-010), [FICHA-007](Casos-de-Teste.md#ficha-007) | Diálogo, em vez de subcard |
| Ficha — Cenário 3: excluir série | [UI-FICHA-003](Casos-de-Teste.md#ui-ficha-003), [FICHA-018](Casos-de-Teste.md#ficha-018) | — |
| Ficha — Cenário 4: duplicar ficha | [UI-FICHA-004](Casos-de-Teste.md#ui-ficha-004), [FICHA-007](Casos-de-Teste.md#ficha-007), [FICHA-016](Casos-de-Teste.md#ficha-016) | — |
| Ficha — Cenário 5: limite de 5 fichas | [UI-FICHA-005](Casos-de-Teste.md#ui-ficha-005), [FICHA-009](Casos-de-Teste.md#ficha-009), [FICHA-021](Casos-de-Teste.md#ficha-021) | Na UI, os botões ficam desabilitados e a tela mostra uma mensagem. Não é uma tentativa de salvar bloqueada. |
| Ficha — Cenário 6: salvar ficha sem séries | Nenhum | 🔀 **Divergente:** a implementação permite. Veja o critério acima. |

**Possíveis melhorias do documento:**

- dois tipos de ficha, "Treino normal" e "Tirada de tempo". O backend tem o campo `tipoFicha` (0 = comum, 1 = meta_tempo; FICHA-006 valida o intervalo), mas a UI sempre envia 0. "Tipos de ficha" são uma melhoria futura na matriz;
- campo de observações por série, para registrar equipamentos etc (Foi uma melhoria implementada ✅).

## Execução do treino

### Critérios de aceite e regras

| Critério ou regra do documento | Situação | Evidência e observação |
|---|---|---|
| Selecionar uma ficha base previamente cadastrada | ✅ | "Selecione uma Ficha Base" em `TreinoExecucaoView.vue`; TREINO-002 |
| Registrar a execução do treino do dia | ✅ | Registro do tempo por tiro e finalização (TREINO-004; teste sem ID `ui/treinos.spec.ts:18`) |
| Treino comum: informar só o tempo de cada série | 🔀 | A tela tem campos de tempo por tiro. |
| Treino de meta de distância: editar a distância das séries | ❌ | Não há edição de distância na UI. O tipo de ficha "meta" não é usado pela UI. |
| Treino de meta de distância: adicionar novas séries | ❌ (UI) /  (API) | A UI não tem essa ação. A API tem `POST /treinos/{id}/series`, sem chamada nos testes. Foi considerada uma melhoria futura. |
| Não criar treinos durante a execução; não alterar a estrutura da ficha base | ✅ (UI)  | A tela não oferece essas ações. |
| Execução vinculada à ficha base | ✅ | O treino é criado a partir da ficha (TREINO-002) |
| Execução vinculada à meta, quando aplicável | ❌ (UI) / (API) | O frontend não chama os endpoints Treino × Meta. Eles existem na API, sem testes, e estão listados como melhoria futura. |
| Salvar a execução no histórico | ✅ | Um treino finalizado aparece no histórico (HIST-003) |
| Calcular as métricas com base nos dados informados | 🟡 | O pace por série e o pace médio aparecem no histórico. A fórmula não está documentada (TREINO-019, METR-003). |

### Cenários de teste

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Execução — Cenário 1: treino comum com sucesso | Teste sem ID `ui/treinos.spec.ts:18` ([lista](Casos-de-Teste.md#testes-automatizados-sem-id-na-matriz)), [TREINO-002](Casos-de-Teste.md#treino-002), [TREINO-004](Casos-de-Teste.md#treino-004) | — |
| Execução — Cenários 2 e 3: meta de distância (editar distância, adicionar série) | Nenhum | ❌ Não implementado na UI |
| Execução — Cenário 4: campos não permitidos | Nenhum | Sem caso de teste |
| Execução — Cenário 5: execução vinculada à meta | Nenhum | ❌ Não implementado na UI |
| Execução — Cenário 6: impedir a criação de treino | Nenhum | Sem caso de teste |

**Possíveis melhorias do documento:**

- protótipo para a ficha de tirada de tempo;
- botão para salvar o treino com os tempos e a estrutura completa.

## Histórico de Treinos

### Critérios de aceite e regras

| Critério ou regra do documento | Situação | Evidência e observação |
|---|---|---|
| Visualizar a lista de execuções | ✅ | `HistoricoView.vue`; HIST-003; teste sem ID `ui/historico.spec.ts:67` |
| Execuções exibidas por data, em ordem cronológica | ✅  | A coluna "Data" existe e `TreinoRepository.cs` ordena por `DataTreino` decrescente. Não há teste de ordenação, que consta como melhoria futura. |
| Filtrar por **data** | 🟡 | O campo "Filtrar por data" existe na UI. **Não há teste** (HIST-005 cobre só o título). |
| Filtrar por **título da ficha** | ✅ | "Filtrar por titulo" (HIST-005). O filtro usa o título do treino, que por padrão vem da ficha. |
| Filtrar por **tipo de nado** | ❌ | Não há esse filtro na UI |
| Filtros isolados ou combinados | 🟡 | Título e data são combináveis. Não há teste. |
| Exibir a **distância total acumulada** | ❌ | A tela mostra a "Distancia Total" de cada treino, não o acumulado. "Total acumulado no histórico" está listado como melhoria futura. |
| Acessar os detalhes de uma execução | ✅ | Diálogo de detalhe (teste sem ID `ui/historico.spec.ts:67`). |
| Somente execuções do usuário autenticado | ✅ | HIST-004 |

### Cenários de teste

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Histórico — Cenário 1: com dados | Teste sem ID `ui/historico.spec.ts:67`, [HIST-003](Casos-de-Teste.md#hist-003), [HIST-007](Casos-de-Teste.md#hist-007) | — |
| Histórico — Cenário 2: vazio | Teste sem ID `ui/historico.spec.ts:57`, [HIST-002](Casos-de-Teste.md#hist-002) | A UI mostra "Nenhum treino concluido ainda. Finalize um treino na tela de Execucao." |
| Histórico — Cenário 3: filtrar por data | Nenhum | O filtro existe, mas não tem teste |
| Histórico — Cenário 4: filtrar por título | [HIST-005](Casos-de-Teste.md#hist-005) | — |
| Histórico — Cenário 5: filtrar por tipo de nado | Nenhum | ❌ Não implementado |
| Histórico — Cenário 6: detalhes da execução | Teste sem ID `ui/historico.spec.ts:67` | Diálogo, em vez de tela |

## Metas

### Critérios de aceite e regras

| Critério ou regra do documento | Situação | Evidência e observação |
|---|---|---|
| Visualizar as metas cadastradas | ✅ | Cards em `MetasView.vue` (META-016, META-017) |
| Criar uma meta | ✅ | Diálogo "Criar Meta de Tempo" (teste sem ID `ui/metas.spec.ts:15`; META-003). A meta é vinculada a uma série de ficha, conforme "as metas são com base no treino criado pelo usuário". |
| Exibir o **tipo de meta** (distância ou velocidade) | ❌ / 🔀 | Não existe tipo distância/velocidade. O DTO tem `ModoAvaliacao` (Repetição, Média, Total), que não é exibido como tipo de meta. |
| Exibir o tipo de nado, a distância alvo e o tempo alvo | ✅ | Card com o tipo de nado e a distância; formulário com o tempo alvo |
| Tipo de nado obrigatório | ✅  | O tipo de nado é preenchido a partir da série escolhida. |
| Tempo alvo opcional para meta de distância e obrigatório para meta de velocidade | 🔀 | O tempo alvo é **sempre obrigatório** e maior que zero, porque não há tipos de meta. |
| Exibir o progresso de cada meta | ✅ | Percentual de progresso no card, via `GET /metas/{id}/progresso` |
| Exibir o status da meta | ✅ | Abas Todas, Ativas e Concluídas (META-012) |
| Mensagem quando não há metas | 🟡 | "Nenhuma meta encontrada para este filtro." Não há caso de UI para esse estado (META-002 cobre só a API). |
| Acessar o detalhe de uma meta | ❌ | A tela de detalhe da meta não foi implementada (melhoria futura) |
| Progresso considera as **execuções vinculadas** | ✅  | O progresso considera treinos concluídos com o mesmo tipo de nado e na mesma piscina da ficha da meta. |
| Cada meta pertence a um único usuário | ✅ | META-008..010 |

### Cenários de teste

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Metas — Cenário 1: metas cadastradas | [META-016](Casos-de-Teste.md#meta-016), [META-017](Casos-de-Teste.md#meta-017), [META-012](Casos-de-Teste.md#meta-012) | — |
| Metas — Cenário 2: sem metas | [META-002](Casos-de-Teste.md#meta-002) | Apenas na API. Mensagem na UI sem teste. |
| Metas — Cenário 3: criar meta | Testes sem ID `ui/metas.spec.ts:15` e `:6`, [META-003](Casos-de-Teste.md#meta-003) | O documento espera ser "direcionado para a tela de cadastro". A implementação abre um diálogo. |
| Metas — Cenário 4: detalhe da meta | Nenhum | ❌ Não implementado |
| Metas — Cenário 5: exibição do progresso | [METR-004](Casos-de-Teste.md#metr-004), [METR-006](Casos-de-Teste.md#metr-006), [METR-007](Casos-de-Teste.md#metr-007), [METR-008](Casos-de-Teste.md#metr-008) | Apenas na API. Não há caso de UI dedicado ao percentual exibido. |

## Detalhe da Meta

**Situação:** ❌ não implementada. A matriz lista a "tela de detalhe da meta" como melhoria futura. O documento prevê:

- o tipo de meta, o tipo de nado, a distância e o tempo alvo;
- o progresso e o status;
- uma estimativa de alcance;
- as execuções relacionadas;
- o histórico filtrado pela meta.


A API `GET /metas/{id}/progresso` retorna o percentual, o melhor pace e o histórico de tentativas (METR-004, METR-006..008). Ela é usada apenas nos cards da tela de Metas.

| Cenário do documento | Casos relacionados | Observação |
|---|---|---|
| Detalhe da Meta — Cenários 1 a 5 | Nenhum | Tela não implementada |

## Principais divergências Planejadas X Implementadas


| Tema | Documento | Implementação | Casos afetados |
|---|---|---|---|
| Ficha sem séries | Não pode ser salva | Pode ser salva; o bloqueio só ocorre ao iniciar treino | FICHA-003, UI-FICHA-002, TREINO-007 |
| Destino após o login | Tela inicial | `/fichas` (Não há tela inicial)| UI-LOGIN-001, UI-LOGIN-002, NAV-001 |
| Escolha da piscina | No login | Tela `/piscina` | PISC-*, FICHA-013..017 |
| Tipos de meta | Distância ou velocidade | Inexistentes; tempo alvo sempre obrigatório = Há apenas a meta de velocidade | META-004, META-005 |
| Criar meta | Direciona para tela de cadastro | Diálogo na mesma tela | Teste sem ID `ui/metas.spec.ts:15` |
| Detalhe de execução e de meta | Telas de detalhe | Diálogo (execução); inexistente (meta) | Teste sem ID `ui/historico.spec.ts:67` |
