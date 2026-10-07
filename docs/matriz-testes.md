# Matriz de Testes — toSwim

> Gerada em 2026-09-16 a partir da inspeção do código-fonte real: controllers/services/DTOs do
> backend (`backend/ToSwim.Api`, `backend/ToSwim.Application`), views do frontend
> (`frontend/src/views`, `frontend/src/router`) e suíte E2E existente (`e2e/tests`).
> Os resultados esperados de cada caso foram derivados das regras efetivamente implementadas nos
> services (não de suposições) — quando o comportamento do código diverge do que seria desejável,
> isso está anotado na coluna de observações e listado na seção **Defeitos encontrados**.
>
> Baseline confirmada antes de qualquer alteração: **56/56 testes passando** (39 API + 17 UI).
>
> **Atualização de 2026-10-07 (rodada final de QA):** a suíte passou a ter **128 testes**
> (92 API + 36 UI). Execução completa em 2026-10-07 00:00 (BRT): **128/128 aprovados**. A versão
> testada é o commit `4e10660` **mais alterações locais ainda não commitadas** (ver seção
> "Rodada final de QA (2026-10-07)" no fim deste arquivo). Os casos desta rodada estão marcados
> como `Implementado na rodada final`.

Legenda de **Status**: `Automatizado` (já existe teste e passa), `Pendente` (sem teste ainda),
`Implementado nesta análise` (adicionado como parte deste trabalho), `Implementado na rodada final`
(adicionado em 2026-10-07).

Legenda de **Prioridade**: `Alta`, `Média`, `Baixa`.

---

## 1. Autenticação (`/auth`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| AUTH-001 | Auth | Registro cria atleta e retorna JWT | API | Nenhuma | nome, email único, senha≥6 | 201, body com nome/email e token | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:5` |
| AUTH-002 | Auth | Rejeita e-mail com formato inválido | API | Nenhuma | email="nao-e-um-email" | 400 (`[EmailAddress]`) | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:19` |
| AUTH-003 | Auth | Rejeita senha curta (<6) | API | Nenhuma | senha="123" | 400 (`[MinLength(6)]`) | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:27` |
| AUTH-004 | Auth | Rejeita e-mail duplicado | API | Atleta já cadastrado com o e-mail | email repetido | 400 (`AuthService.RegistrarAsync`) | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:35` |
| AUTH-005 | Auth | Login autentica atleta cadastrado | API | Atleta cadastrado | email/senha corretos | 200, token presente | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:48` |
| AUTH-006 | Auth | Rejeita login com senha incorreta | API | Atleta cadastrado | senha errada | 401 | Alta | Automatizado | `e2e/tests/api/auth.spec.ts:60` |
| AUTH-007 | Auth | Rejeita login de e-mail não cadastrado | API | Nenhuma | email inexistente | 401 | Média | Automatizado | `e2e/tests/api/auth.spec.ts:70` |
| AUTH-008 | Auth | Rejeita nome vazio no registro | API | Nenhuma | nome="" | 400 (`[Required]`) | Média | Pendente | — |
| AUTH-009 | Auth | Rejeita nome composto apenas por espaços | API | Nenhuma | nome="   " | 400 (`[Required]` do ASP.NET Core faz `Trim()` antes de validar o tamanho da string) | Alta | **Implementado nesta análise** | `auth.spec.ts` |
| AUTH-010 | Auth | Rejeita e-mail acima do limite de 150 caracteres | API | Nenhuma | email longo válido porém >150 chars | 400 (`[MaxLength(150)]`) | Baixa | Pendente | — |

## 2. Fichas Base (`/fichas-base`) e Séries (`/fichas-base/{id}/series`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| FICHA-001 | Fichas | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `fichas.spec.ts:6` |
| FICHA-002 | Fichas | Lista vazia para atleta novo | API | Atleta sem fichas | — | 200, `[]` | Média | Automatizado | `fichas.spec.ts:11` |
| FICHA-003 | Fichas | Cria ficha base | API | Atleta autenticado | tituloFicha, tipoFicha=0 | 201, `status=1` | Alta | Automatizado | `fichas.spec.ts:24` |
| FICHA-004 | Fichas | Rejeita ficha com título vazio | API | Atleta autenticado | tituloFicha="" | 400 (`[Required]`) | Alta | Automatizado | `fichas.spec.ts:39` |
| FICHA-005 | Fichas | Rejeita título composto apenas por espaços | API | Atleta autenticado | tituloFicha="   " | 400 (`[Required]` do ASP.NET Core faz `Trim()` antes de validar o tamanho da string) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-006 | Fichas | Rejeita tipoFicha fora do range 0-1 | API | Atleta autenticado | tipoFicha=5 | 400 (`[Range(0,1)]`) | Média | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-007 | Fichas | Adiciona série, consulta detalhe e duplica ficha | API | Ficha criada | série válida | 201 série; detalhe com 1 série; duplicar cria cópia com `(Cópia)` | Alta | Automatizado | `fichas.spec.ts:52` |
| FICHA-008 | Fichas | Exclui ficha sem vínculos (delete físico) | API | Ficha + série, sem treino iniciado | — | 204; some da listagem | Alta | Automatizado | `fichas.spec.ts:95` |
| FICHA-009 | Fichas | Limite de 5 fichas ativas: 6ª criação é rejeitada | API | 5 fichas ativas já criadas | 6ª ficha | 400 (`FichaBaseService.CriarAsync` — regra de negócio explícita) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-010 | Fichas | Atleta A não acessa ficha do atleta B (`GET /fichas-base/{id}`) | API | Duas contas, ficha do B | id da ficha do B, token do A | 404 (isolado por `codUsuario` na query) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-011 | Fichas | Atleta A não exclui ficha do atleta B | API | Duas contas, ficha do B | DELETE com token do A | 404 | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-012 | Fichas | Atleta A não adiciona série na ficha do atleta B | API | Duas contas, ficha do B | POST série, token do A | 404 | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-013 | Fichas | Cria ficha com tamanho de piscina 50m | API | Atleta autenticado | tamanhoPiscinaM=50 | 201, `tamanhoPiscinaM=50` | Alta | Automatizado | `fichas.spec.ts:52` |
| FICHA-014 | Fichas | Usa 25m como padrão quando o tamanho da piscina não é informado | API | Atleta autenticado | sem `tamanhoPiscinaM` no payload | 201, `tamanhoPiscinaM=25` | Alta | Automatizado | `fichas.spec.ts:66` |
| FICHA-015 | Fichas | Rejeita tamanho de piscina inválido na ficha (não 25/50) | API | Atleta autenticado | tamanhoPiscinaM=33 | 400 (`FichaBaseService.CriarAsync`) | Alta | Automatizado | `fichas.spec.ts:80` |
| FICHA-016 | Fichas | Duplicar ficha mantém o tamanho de piscina da ficha original | API | Ficha de 50m | POST `/duplicar` | 201, cópia com `tamanhoPiscinaM=50` | Alta | Automatizado | `fichas.spec.ts:228` |
| FICHA-017 | Fichas UI | Cria ficha selecionando piscina de 50m e exibe "Piscina: 50m" no card | UI | Atleta autenticado | seleciona "50 metros" no diálogo | Chip "Piscina: 50m" visível no card criado | Alta | Automatizado | `fichas.spec.ts:51` (UI) |
| SERIE-009 | Séries | Cria série com tipoNado=4 (Livre) | API | Ficha criada | tipoNado=4 | 201, `tipoNado=4` | Alta | Automatizado | `fichas.spec.ts:241` |
| SERIE-010 | Séries UI | Seleciona tipo de nado "Livre" ao cadastrar série e exibe na ficha | UI | Ficha criada | seleciona "Livre" no diálogo | "4x 100m - Livre" visível na ficha | Alta | Automatizado | `fichas.spec.ts:78` (UI) |
| SERIE-001 | Séries | Rejeita distância zero | API | Ficha criada | distanciaM=0 | 400 (`[Range(1,10000)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-002 | Séries | Rejeita distância negativa | API | Ficha criada | distanciaM=-10 | 400 (`[Range(1,10000)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-003 | Séries | Rejeita distância acima do limite máximo (10000) | API | Ficha criada | distanciaM=10001 | 400 (`[Range(1,10000)]`) | Média | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-004 | Séries | Rejeita tipoNado fora do enum válido (0-4, incluindo o novo tipo 4=Livre) | API | Ficha criada | tipoNado=99 | 400 (`[Range(0,4)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-005 | Séries | Rejeita quantidadeRepeticoes zero/negativa | API | Ficha criada | quantidadeRepeticoes=0 | 400 (`[Range(1,1000)]`) | Média | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-006 | Séries | Rejeita tempoPausaSeg negativo | API | Ficha criada | tempoPausaSeg=-1 | 400 (`[Range(0,3600)]`) | Média | Pendente | — |
| SERIE-007 | Séries | Atleta A não atualiza série do atleta B | API | Duas contas, série do B | PUT série do B, token do A | 404 (`serie.Ficha?.CodUsuario != codUsuario`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-008 | Séries | Atleta A não exclui série do atleta B | API | Duas contas, série do B | DELETE série do B, token do A | 404 | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| FICHA-018 | Séries | Exclui série própria e reordena as restantes | API | Ficha com 2 séries | DELETE da 1ª série | 204; ficha fica com 1 série, `ordem=1` | Alta | **Implementado na rodada final** | `fichas.spec.ts:375` |
| SERIE-011 | Séries | Edita série própria | API | Ficha com série | PUT `/fichas-base/series/{id}` com novos valores | 200; corpo e detalhe da ficha refletem os novos valores | Alta | **Implementado na rodada final** | `fichas.spec.ts:394` |
| SERIE-012 | Séries | Rejeita editar série para ordem já ocupada | API | Ficha com 2 séries | PUT da 2ª série com `ordem=1` | 400 "Ja existe uma serie na posicao 1 nesta ficha." | Média | **Implementado na rodada final** | `fichas.spec.ts:426` |
| FICHA-019 | Fichas | Inativa e reativa ficha | API | Ficha ativa | PUT `/status` com `0` e depois `1` | 204 / 204; some da lista e volta com `status=1` | Alta | **Implementado na rodada final** | `fichas.spec.ts:450` |
| FICHA-020 | Fichas | Rejeita status diferente de 0/1 | API | Ficha ativa | PUT `/status` com `2` | 400 "Status inválido. Use 0 para inativo ou 1 para ativo." | Média | **Implementado na rodada final** | `fichas.spec.ts:466` |
| FICHA-021 | Fichas | Rejeita reativar ficha com 5 ativas | API | 1 ficha inativa + 5 ativas | PUT `/status` com `1` | 400 "Não é possível ativar esta ficha. Limite de 5 fichas ativas excedido." | Alta | **Implementado na rodada final** | `fichas.spec.ts:476` |
| UI-FICHA-001 | Fichas UI | Estado vazio para atleta sem fichas | UI | Atleta novo | abrir `/fichas` aguardando a resposta de `GET /fichas-base` | Resposta 200; "Nenhuma ficha ativa ainda..." e "Fichas: 0 / 5 Ativas" | Alta | Automatizado (sincronização ajustada na rodada final) | `fichas.spec.ts:18` (UI) |
| UI-FICHA-002 | Fichas UI | Cria ficha e adiciona série | UI | Atleta autenticado | criar ficha e série pelos diálogos | "0 series cadastradas" após criar; "4x 100m - Crawl" após a série | Alta | Automatizado | `fichas.spec.ts:29` (UI) |
| UI-FICHA-003 | Fichas UI | Exclui série cadastrada | UI | Ficha com 1 série | "Remover serie" + confirmar | "Serie removida com sucesso."; "0 series cadastradas" | Alta | **Implementado na rodada final** | `fichas.spec.ts:125` (UI) |
| UI-FICHA-004 | Fichas UI | Duplica ficha existente | UI | Ficha com 1 série | "Duplicar Ficha" | "Ficha duplicada com sucesso."; "Fichas: 2 / 5 Ativas"; card "<título> (Cópia)" com 1 série | Alta | **Implementado na rodada final** | `fichas.spec.ts:141` (UI) |
| UI-FICHA-005 | Fichas UI | Mensagem ao atingir 5 fichas ativas | UI | 5 fichas criadas via API | abrir `/fichas` | "Você atingiu o limite de 5 fichas ativas. Exclua uma ficha para criar outra."; "Fichas: 5 / 5 Ativas"; "Nova Ficha" e "Duplicar Ficha" desabilitados | Alta | **Implementado na rodada final** | `fichas.spec.ts:157` (UI) |

## 3. Metas de Tempo (`/metas`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| META-001 | Metas | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `metas.spec.ts:6` |
| META-002 | Metas | Lista vazia sem metas | API | Atleta sem metas | — | 200, `[]` | Média | Automatizado | `metas.spec.ts:11` |
| META-003 | Metas | Cria meta vinculada a série própria, calcula pace alvo | API | Ficha+série do atleta | dados válidos | 201, `paceAlvoSeg` calculado | Alta | Automatizado | `metas.spec.ts:24` |
| META-004 | Metas | Rejeita tempoAlvoSeg = 0 | API | Ficha+série do atleta | tempoAlvoSeg=0 | 400 (`MetaService.CriarMetaAsync`) | Alta | Automatizado | `metas.spec.ts:53` |
| META-005 | Metas | Rejeita tempoAlvoSeg negativo | API | Ficha+série do atleta | tempoAlvoSeg=-10 | 400 (`<=0`) | Alta | **Implementado nesta análise** | `metas.spec.ts` |
| META-006 | Metas | Rejeita distanciaAlvoM = 0 | API | Ficha+série do atleta | distanciaAlvoM=0 | 400 (`<=0`) | Alta | **Implementado nesta análise** | `metas.spec.ts` |
| META-007 | Metas | Conclui e depois exclui meta | API | Meta ativa | status=1 depois DELETE | 200 status=1; 204 exclusão | Alta | Automatizado | `metas.spec.ts:74` |
| META-008 | Metas | **Isolamento:** atleta A não usa série do atleta B para criar meta | API | Série pertence ao atleta B | codSerieFicha do B, token do A | 403 (`MetaService.CriarMetaAsync` — "não pertence ao usuário autenticado") | Alta | **Implementado nesta análise** | `metas.spec.ts` |
| META-009 | Metas | **Isolamento:** atleta A não acessa meta do atleta B (`GET /metas/{id}`) | API | Meta do atleta B | id da meta do B, token do A | 404 (`MetaRepository.ObterPorIdAsync` filtra por `codUsuario`) | Alta | **Implementado nesta análise** | `metas.spec.ts` |
| META-010 | Metas | **Isolamento:** atleta A não exclui meta do atleta B | API | Meta do atleta B | DELETE, token do A | 404 | Alta | **Implementado nesta análise** | `metas.spec.ts` |
| META-011 | Metas | Rejeita criação de meta com série inexistente | API | codSerieFicha inválido | codSerieFicha=999999 | 404 (`"A série de ficha informada não existe."`) | Média | Pendente | — |
| META-012 | Metas UI | Filtro de status (Todas/Ativas/Concluídas) reflete a lista exibida | UI | Metas ativa e concluída existentes | clicar nas abas | Lista muda conforme aba selecionada | Alta | **Implementado nesta análise** | `metas.spec.ts` (UI) |
| META-013 | Metas | Cria meta com tipoNado=4 (Livre) | API | Ficha+série do atleta | tipoNado=4 | 201, `tipoNado=4` | Alta | Automatizado | `metas.spec.ts:53` |
| META-014 | Metas | Meta vinculada a ficha de 25m retorna `tamanhoPiscinaM=25` (derivado via `Meta -> SerieFicha -> FichaBase`, sem coluna nova em `meta`) | API | Ficha de 25m com série | dados válidos | 201, `tamanhoPiscinaM=25` | Alta | **Implementado nesta análise** | `metas.spec.ts:74` |
| META-015 | Metas | Meta vinculada a ficha de 50m retorna `tamanhoPiscinaM=50` | API | Ficha de 50m com série | dados válidos | 201, `tamanhoPiscinaM=50` | Alta | **Implementado nesta análise** | `metas.spec.ts:88` |
| META-016 | Metas UI | Meta vinculada a série com tipo de nado Livre exibe "Livre" e "Piscina: 25m" no card | UI | Série tipoNado=4, ficha 25m | criar meta | Chips "Livre" e "Piscina: 25m" visíveis | Alta | Automatizado | `metas.spec.ts:37` (UI) |
| META-017 | Metas UI | Meta vinculada a ficha de 50m exibe "Piscina: 50m" no card | UI | Ficha de 50m com série | criar meta | Chip "Piscina: 50m" visível | Alta | **Implementado nesta análise** | `metas.spec.ts:60` (UI) |
| METR-006 | Métricas | Progresso da meta: treino de 25m NÃO influencia meta vinculada a ficha de 50m (`MetricasService.ObterProgressoMetaAsync` filtra `s.Treino.TamanhoPiscinaM == tamanhoPiscinaMeta`) | API | Treino 25m concluído + meta em ficha 50m, mesmo tipoNado | GET `/metas/{id}/progresso` | `historicoTentativas=[]`, `percentualAtingimento=0` | Alta | **Implementado nesta análise** | `metas.spec.ts:233` |
| METR-007 | Métricas | Progresso da meta: treino de 50m NÃO influencia meta vinculada a ficha de 25m | API | Treino 50m concluído + meta em ficha 25m, mesmo tipoNado | GET `/metas/{id}/progresso` | `historicoTentativas=[]`, `percentualAtingimento=0` | Alta | **Implementado nesta análise** | `metas.spec.ts:264` |
| METR-008 | Métricas | Progresso da meta: treino na mesma piscina da ficha da meta conta normalmente (fórmula de pace inalterada) | API | Treino 50m concluído + meta em ficha 50m, mesmo tipoNado | GET `/metas/{id}/progresso` | `historicoTentativas` com pelo menos 1 item | Alta | **Implementado nesta análise** | `metas.spec.ts:295` |

## 4. Configuração de Piscina (`/piscina-configuracao`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| PISC-001 | Piscina | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `piscina.spec.ts:5` |
| PISC-002 | Piscina | 404 quando não configurada | API | Atleta sem config | — | 404, body `erro` | Média | Automatizado | `piscina.spec.ts:10` |
| PISC-003 | Piscina | Cria configuração válida | API | Atleta autenticado | tamanhoM=25, formaContagem=0 | 201, `status=1` | Alta | Automatizado | `piscina.spec.ts:24` |
| PISC-004 | Piscina | Rejeita tamanho inválido (não 25/50) | API | Atleta autenticado | tamanhoM=30 | 400 (`ValidarCampos`) | Alta | Automatizado | `piscina.spec.ts:42` |
| PISC-005 | Piscina | Rejeita segunda configuração para o mesmo atleta | API | Config já existente | nova config | 400 | Alta | Automatizado | `piscina.spec.ts:53` |
| PISC-006 | Piscina | Atualiza configuração existente | API | Config existente | tamanhoM=50, formaContagem=1 | 200, valores atualizados | Alta | Automatizado | `piscina.spec.ts:71` |
| PISC-007 | Piscina | Rejeita formaContagem fora de 0/1 | API | Atleta autenticado | formaContagem=5 | 400 (`ValidarCampos`) | Média | **Implementado nesta análise** | `piscina.spec.ts` |
| PISC-008 | Piscina UI | Configuração salva persiste após reload da página | UI | Config salva | reload | Valores selecionados continuam refletidos após reload | Média | **Implementado nesta análise** | `piscina.spec.ts` (UI) |

## 5. Treinos — execução (`/treinos`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| TREINO-001 | Treinos | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `treinos.spec.ts:12` |
| TREINO-002 | Treinos | Inicia treino a partir de ficha com séries | API | Ficha+série, piscina configurada | dados válidos | 201, 1 série clonada | Alta | Automatizado | `treinos.spec.ts:19` |
| TREINO-003 | Treinos | Rejeita tamanho de piscina inválido ao iniciar | API | Ficha criada | tamanhoPiscinaM=33 | 400 | Alta | Automatizado | `treinos.spec.ts:46` |
| TREINO-004 | Treinos | Registra tiro, atualiza série e finaliza treino | API | Treino em andamento | tempo/distância válidos | 201 tiro; 200 finalizar, `status=1` | Alta | Automatizado | `treinos.spec.ts:61` |
| TREINO-005 | Treinos | Cancela treino em andamento | API | Treino em andamento | — | 200, `status=2` | Alta | Automatizado | `treinos.spec.ts:107` |
| TREINO-006 | Treinos | Rejeita repetição com duração zero | API | Treino em andamento | duracaoSeg=0 | 400 | Alta | Automatizado | `treinos.spec.ts:124` |
| TREINO-007 | Treinos | Rejeita iniciar treino a partir de ficha sem série | API | Ficha ativa sem nenhuma série | codFicha de ficha vazia | 400, `{ erro: "A ficha deve possuir pelo menos uma série." }` (`TreinoService.IniciarTreinoAsync` valida `ficha.Series.Any()` antes de criar o treino) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-008 | Treinos | Rejeita iniciar treino com ficha inexistente/de outro atleta | API | Ficha pertence ao atleta B | codFicha do B, token do A | 404 (`"Ficha base não encontrada ou inativa."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-009 | Treinos | Rejeita finalizar treino já finalizado (dupla finalização) | API | Treino já finalizado | PUT `/finalizar` novamente | 400 (`"Este treino já está finalizado ou cancelado."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-010 | Treinos | Rejeita cancelar treino já finalizado | API | Treino já finalizado | PUT `/cancelar` | 400 (`"Apenas treinos em andamento podem ser cancelados."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-011 | Treinos | Rejeita ações (registrar repetição) em treino inexistente | API | codTreino inexistente | POST repetição | 404 | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-012 | Treinos | Rejeita registrar repetição em treino já finalizado | API | Treino finalizado | POST repetição | 400 (`"Não é possível registrar repetições para um treino finalizado ou cancelado."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-013 | Treinos | **Isolamento:** atleta A não acessa/edita treino do atleta B | API | Treino do atleta B | GET/PUT com token do A | 404 (`TreinoRepository.ObterPorIdAsync` filtra por `codUsuario`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-014 | Treinos | Rejeita iniciar treino a partir de ficha inativa (status=0) | API | Ficha desativada | codFicha inativo | 404 "Ficha base não encontrada ou inativa." | Média | **Implementado na rodada final** | `treinos.spec.ts:398` |
| TREINO-015 | Treinos UI | Iniciar treino sem piscina configurada mantém botão desabilitado (fluxo já coberto) | UI | Sem config de piscina | — | Botão "Iniciar Treino na Piscina" desabilitado | Alta | Automatizado | `treinos.spec.ts:6` (UI) |
| TREINO-016 | Treinos | Rejeita iniciar treino quando a piscina da ficha diverge da configuração atual do atleta | API | Ficha 25m, config do atleta em 50m | POST `/treinos` | 400, mensagem citando os dois tamanhos | Alta | Automatizado | `treinos.spec.ts:95` |
| TREINO-017 | Treinos | Rejeita iniciar treino sem o atleta ter configurado a piscina | API | Ficha com série, sem `ConfigPiscina` | POST `/treinos` | 400, `"Configure o tamanho da sua piscina..."` | Alta | Automatizado | `treinos.spec.ts:83` |
| TREINO-018 | Treinos | **Autoridade do backend:** `dto.TamanhoPiscinaM` enviado pelo cliente é ignorado; o treino criado sempre recebe `FichaBase.TamanhoPiscinaM` (já validado contra a config do atleta) — o cliente não consegue forçar outro valor no payload | API | Config=25m, ficha=25m | payload com `tamanhoPiscinaM=50` | 201, treino persistido com `tamanhoPiscinaM=25` (valor do cliente é descartado, não gera erro — contrato documentado em `IniciarTreinoRequestDto.cs` e `TreinoService.IniciarTreinoAsync`) | Alta | **Implementado nesta análise** | `treinos.spec.ts:46` |
| TREINO-019 | Treinos | Série sem tempo informado (totais zerados) é gravada com totais `null` e o treino finaliza | API | Treino em andamento | PUT série com `tempoTotalSeg=0`, `distanciaTotalM=0` | 200; totais e pace da série `null`; finalizar 200, `status=1` | Alta | Automatizado (commit `4e10660`) | `treinos.spec.ts:195` |
| REP-001 | Repetições | **Isolamento:** atleta A não registra, lista, consulta, altera nem exclui repetições do atleta B | API | Treino do B com 1 repetição | POST/GET/GET item/PUT/DELETE com token do A | 404 "Série de treino não encontrada para este treino." (POST e lista); 404 "Repetição não encontrada." (item, PUT, DELETE); repetição do B continua intacta | Alta | **Implementado na rodada final** | `treinos.spec.ts:420` |

> **Rodada final:** os testes TREINO-006, 007, 008, 009, 011, 012 e 013 passaram a conferir o **texto
> exato** da mensagem `erro`. Antes eles só verificavam o código HTTP (ou se `erro` existia), e por
> isso não detectavam que a API devolvia `?` no lugar dos acentos nessas mensagens (ver "Rodada
> final de QA").

## 6. Histórico (`/historico/treinos`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| HIST-001 | Histórico | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `historico.spec.ts:6` |
| HIST-002 | Histórico | Lista vazia sem treinos concluídos | API | Atleta sem treinos | — | 200, `[]` | Média | Automatizado | `historico.spec.ts:11` |
| HIST-003 | Histórico | Lista treino concluído e rejeita detalhe de treino em andamento | API | 1 treino concluído + 1 em andamento | — | 200 lista; 400 no detalhe do treino em andamento | Alta | Automatizado | `historico.spec.ts:22` |
| HIST-004 | Histórico | **Isolamento:** atleta A não acessa histórico/detalhe de treino do atleta B | API | Treino concluído do atleta B | GET detalhe, token do A | 404 (`TreinoService.ObterPorIdAsync` filtra por `codUsuario`) | Alta | **Implementado nesta análise** | `historico.spec.ts` |
| HIST-005 | Histórico UI | Filtro por título e por data reduz a lista exibida | UI | 2 treinos concluídos com títulos distintos | preencher filtro de título | Apenas linha correspondente permanece visível | Alta | **Implementado nesta análise** | `historico.spec.ts` (UI) |
| HIST-006 | Histórico | Lista e detalhe refletem `tamanhoPiscinaM=50` de um treino concluído (campo já existia em `Treino`/`TreinoResponseDto`; nenhuma coluna nova foi criada) | API | Treino de 50m concluído | — | 200; lista e detalhe com `tamanhoPiscinaM=50` | Alta | **Implementado nesta análise** | `historico.spec.ts:70` |
| HIST-007 | Histórico UI | Listagem e detalhe do treino exibem "Piscina: 50m" | UI | Treino de 50m concluído | abrir `/historico` e o detalhe | Chip "Piscina: 50m" visível na linha da tabela e no diálogo de detalhe | Alta | **Implementado nesta análise** | `historico.spec.ts:88` (UI) |

## 7. Métricas (`/dashboard/resumo`, `/metricas/*`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| METR-001 | Métricas | 401 sem token no resumo | API | Nenhuma | — | 401 | Alta | Automatizado | `metricas.spec.ts:6` |
| METR-002 | Métricas | Resumo zerado sem treinos | API | Atleta novo | — | 200, contadores em 0 | Média | Automatizado | `metricas.spec.ts:11` |
| METR-003 | Métricas | Resumo e evolução de pace após treino concluído | API | 1 treino concluído | — | 200; `totalTreinos=1`; pace/recordes coerentes | Alta | Automatizado | `metricas.spec.ts:28` |
| METR-004 | Métricas | Progresso de meta inicia em zero sem tentativas | API | Meta criada sem execuções | — | 200, `percentualAtingimento=0` | Média | Automatizado | `metricas.spec.ts:62` |
| METR-005 | Métricas | **Isolamento:** atleta A não acessa progresso de meta do atleta B | API | Meta do atleta B | GET progresso, token do A | 404 (`MetricasService` usa `MetaRepository.ObterPorIdAsync` filtrado por `codUsuario`) | Média | Pendente | — |

## 8. Usuários (`/users`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| USER-001 | Usuários | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `users.spec.ts:5` |
| USER-002 | Usuários | Retorna dados do atleta autenticado | API | Atleta cadastrado | — | 200, nome/email corretos | Alta | Automatizado | `users.spec.ts:10` |
| USER-003 | Usuários | Atleta A não altera senha do atleta B | API | Duas contas | PUT `/users/{idB}/senha`, token do A | 403 (`Forbid()` — `User.ObterUsuarioId() != id`); login do B com a senha original continua 200 | Alta | **Implementado na rodada final** | `users.spec.ts:24` |

## 9. Cadastro / Login (UI)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| UI-CAD-001 | Cadastro | Exibe formulário de cadastro | UI | Nenhuma | — | Campos visíveis | Média | Automatizado | `cadastro.spec.ts:8` |
| UI-CAD-002 | Cadastro | Botão desabilitado com formulário inválido | UI | Nenhuma | — | Botão desabilitado | Média | Automatizado | `cadastro.spec.ts:16` |
| UI-CAD-003 | Cadastro | Habilita envio com campos válidos | UI | Nenhuma | dados válidos | Botão habilitado | Média | Automatizado | `cadastro.spec.ts:21` |
| UI-CAD-004 | Cadastro | Valida senha/confirmação diferentes | UI | Nenhuma | senhas diferentes | Mensagem de erro; botão desabilitado | Alta | Automatizado | `cadastro.spec.ts:30` |
| UI-LOGIN-001 | Login | Login válido direciona para `/fichas` | UI | Atleta cadastrado via API | e-mail e senha corretos; "Entrar" | URL `/fichas`; título "Fichas de Treino Base" | Alta | **Implementado na rodada final** | `login.spec.ts:12` |
| UI-LOGIN-002 | Login | Preserva o parâmetro `redirect` | UI | Atleta cadastrado; sem token | abrir `/metas` → `/login?redirect=/metas` → login | URL `/metas`; título "Metas de Tempo" | Alta | **Implementado na rodada final** | `login.spec.ts:24` |
| UI-LOGIN-003 | Login | Senha incorreta | UI | Atleta cadastrado | senha errada | Alerta "Email ou senha inválidos"; permanece em `/login` | Alta | **Implementado na rodada final** | `login.spec.ts:37` |
| UI-LOGIN-004 | Login | E-mail não cadastrado | UI | Nenhuma | e-mail inexistente | Alerta "Email ou senha inválidos"; permanece em `/login` | Alta | **Implementado na rodada final** | `login.spec.ts:48` |
| UI-LOGIN-005 | Login | Campos vazios impedem o envio | UI | Nenhuma | ambos vazios; só e-mail; só senha | "Entrar" desabilitado nos três casos; permanece em `/login` | Alta | **Implementado na rodada final** | `login.spec.ts:57` |

## 10. Navegação, logout e proteção de rotas (UI)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| NAV-001 | Navegação | Rota raiz redireciona para `/fichas` | UI | Autenticado | — | URL `/fichas` | Média | Automatizado | `navegacao.spec.ts:9` |
| NAV-002 | Navegação | Barra de topo exibe nome da aplicação | UI | Autenticado | — | Texto "toSwim App" visível | Baixa | Automatizado | `navegacao.spec.ts:14` |
| NAV-003 | Navegação | Menu lateral navega entre telas | UI | Autenticado | — | URLs corretas ao clicar em cada item | Alta | Automatizado | `navegacao.spec.ts:19` |
| NAV-004 | Navegação | Rota protegida redireciona para `/login` quando não autenticado | UI | Sem token no localStorage | acessar `/fichas` diretamente | Redirecionado para `/login?redirect=/fichas` (`router/index.ts` `beforeEach`) | Alta | **Implementado nesta análise** | `navegacao.spec.ts` |
| NAV-005 | Navegação | Logout redireciona para login e remove o token da sessão | UI | Autenticado | clicar em "Sair" | Redireciona para `/login`; `localStorage` não contém mais o token (`tokenStorage`) | Alta | **Implementado nesta análise** | `navegacao.spec.ts` |

---

## Resumo de cobertura

| Camada | Automatizados (baseline anterior) | Implementados nesta análise | Implementados na rodada de piscina/metas/histórico | Pendentes (baixo risco) |
|--------|-------------------------------------|-------------------------------|--------------------------------------------------------|---------------------------|
| API    | 39                                   | 28                             | 14 (FICHA-013..016, SERIE-009, META-013..015, METR-006..008, TREINO-016..018, HIST-006) | 7 na época (AUTH-008, AUTH-010, SERIE-006, META-011, TREINO-014, METR-005, USER-003) |
| UI     | 17                                   | 5                               | 4 (FICHA-017, SERIE-010, META-016..017, HIST-007)      | 0 adicionais mapeados nesta rodada |
| **Total** | **56**                           | **33**                         | **18**                                                  | **7** |

> Registro histórico (2026-09-16): **82 testes de API** e **28 testes de UI** = **110 testes no
> total**, todos passando em execução paralela (4 workers, ~1m30s).

**Situação em 2026-10-07 (rodada final):**

| Camada | Testes em 2026-09-16 | + commit `4e10660` | + rodada final | Total atual | Pendentes |
|--------|----------------------|--------------------|----------------|-------------|-----------|
| API | 82 | +1 (TREINO-019) | +9 (FICHA-018..021, SERIE-011, SERIE-012, TREINO-014, REP-001, USER-003) | **92** | 5 (AUTH-008, AUTH-010, SERIE-006, META-011, METR-005) |
| UI | 28 | — | +8 (UI-FICHA-003..005, UI-LOGIN-001..005) | **36** | — |
| **Total** | **110** | **+1** | **+17** | **128** | **5** |

> Execução completa em 2026-10-07 00:00:54 (BRT), 4 workers: **128 executados, 128 aprovados,
> 0 reprovados, 0 ignorados**, 96,5 s. Detalhes em "Rodada final de QA (2026-10-07)".

> Os pendentes remanescentes são cenários de prioridade média/baixa (limites de tamanho de string,
> mensagens de erro específicas para IDs inexistentes) que não estavam entre os focos de risco
> solicitados. Ficam registrados aqui para priorização futura.

### Estabilidade da suíte em paralelo

O `e2e/tests/ui/fichas.spec.ts:16` falhava de forma intermitente quando a suíte completa rodava em
paralelo (`fullyParallel: true`). A causa raiz não era o teste em si: o `playwright.config.ts`
subia o frontend com `vite dev` (`npm run dev`), que compila cada rota **sob demanda** na primeira
requisição (todas as rotas são `() => import(...)` lazy no router). Com vários workers acessando
rotas diferentes pela primeira vez ao mesmo tempo, esse cold-compile no único dev server
compartilhado podia estourar o timeout padrão de asserção do Playwright.

**Correção**: `webServer` agora builda o frontend (`npm run build`) e serve os assets já
compilados via `vite preview` (`playwright.config.ts`), eliminando o cold-compile como fonte de
flakiness. Os testes de diálogo também passaram a escopar as asserções em `page.getByRole('dialog')`
(ver `fichas.spec.ts`, `metas.spec.ts`, `historico.spec.ts`) e as interações com `v-select` do
Vuetify passaram a usar `focus()` + `press('Enter')` em vez de `click()` (que era interceptado pelo
overlay do próprio componente) — nenhum `force: true`, `waitForTimeout`, `test.only`, `test.skip`
ou `test.fixme` é usado em `e2e/tests` ou `e2e/utils`.

**Atualização 2026-10-06/07 — UI-FICHA-001** (`ui/fichas.spec.ts`, "exibe estado vazio..."):

- 2026-10-06 22:17: falhou uma vez numa execução completa (texto de estado vazio não apareceu em
  5 s; a captura mostra a tela ainda carregando). Nessa execução, os 4 testes de UI de fichas,
  primeiros a chamar a API, levaram de 31 a 34 s, contra 4,6 a 6,1 s numa execução seguinte.
- Ajuste no teste (rodada final): o teste passou a **aguardar a resposta de `GET /fichas-base`** e
  a conferir que ela é 200 antes de verificar o texto. O timeout das asserções não foi alterado e
  não foi criado global setup.
- 2026-10-06 23:55, primeira execução dos arquivos afetados logo após iniciar a API: o
  `GET /fichas-base` voltou **500**. No mesmo período, o log da API registrou 6 erros
  `Npgsql ... TimeoutException` ao abrir conexão com o PostgreSQL. Os outros 3 testes de UI de
  fichas que rodavam em paralelo também falharam (diálogo não fechou). **A causa não foi
  comprovada**; o registro é só do que foi observado.
- Depois disso, o caso passou na reexecução dos arquivos afetados (23:59), na execução completa
  (00:00) e numa repetição isolada 3× (00:02, 3/3).

---

## Defeitos encontrados

> Nota: a hipótese inicial de que `[Required]` aceitaria strings compostas só de espaços em
> branco (títulos/nomes) foi **verificada e descartada** ao rodar os testes: o `RequiredAttribute`
> do ASP.NET Core aplica `Trim()` internamente antes de validar o tamanho quando
> `AllowEmptyStrings` não é definido, então `tituloFicha="   "` e `nome="   "` já retornam 400
> corretamente (ver `FICHA-005` e `AUTH-009`, cobertos como testes de borda normais, não como
> defeitos).

Nenhum defeito em aberto no momento.

- ~~Início de treino não validava ficha sem séries (`TREINO-007`)~~ — **corrigido.**
  `TreinoService.IniciarTreinoAsync` agora verifica `ficha.Series.Any()` logo após localizar a
  ficha e antes de validar o tamanho da piscina ou criar o treino; se a ficha não tiver nenhuma
  série, lança `AppException("A ficha deve possuir pelo menos uma série.", 400)` e nenhum treino é
  persistido. Teste atualizado em `treinos.spec.ts` para esperar 400 no lugar de 201.
- Mudança documentada (commit `4e10660`): séries sem tempo informado gravam totais e pace como
  `null` em vez de 0 (TREINO-019).
- **Corrigido na rodada final:** 28 mensagens de erro em `TreinoService.cs` (16) e
  `RepeticaoSerieTreinoService.cs` (12) continham `?` literal no lugar dos acentos (ex.: "A ficha
  deve possuir pelo menos uma s?rie."), texto que a tela de execução exibe ao usuário. Os acentos
  foram restaurados e os arquivos salvos em UTF-8. Os testes TREINO-006..009, 011..013, TREINO-014
  e REP-001 conferem o texto exato de 8 mensagens distintas (textos que aparecem em 15 das 28
  linhas corrigidas); as outras 13 linhas têm textos que nenhum teste atual confere.
- **Corrigido na rodada final (confirmado por teste):** em `FichaBaseService.cs`, três textos
  tinham o caractere de substituição U+FFFD gravado no arquivo e chegavam assim à resposta:
  "Status inv�lido...", "N�o � poss�vel ativar esta ficha..." e o sufixo da cópia "(C�pia)".
  Os testes FICHA-020, FICHA-021 e UI-FICHA-004 falharam com o texto corrompido e passaram depois da
  correção. Nenhum ID formal de defeito foi criado.

## Rodada final de QA (2026-10-07)

### Versão testada

- Base: commit `4e1066092d48f156ed11c5586f4951a192aab98e` (`develop`).
- **Mais alterações locais não commitadas** (8 arquivos alterados e 1 novo; impressão digital
  SHA-256 dos 16 primeiros caracteres de `git diff -- backend frontend e2e` + `login.spec.ts`:
  `71cb58bf31acc1fa`):
  - `backend/ToSwim.Application/Services/TreinoService.cs` — 16 mensagens com acentos restaurados
  - `backend/ToSwim.Application/Services/RepeticaoSerieTreinoService.cs` — 12 mensagens com acentos restaurados
  - `backend/ToSwim.Application/Services/FichaBaseService.cs` — 3 textos com U+FFFD corrigidos
  - `frontend/src/views/FichasView.vue` — alerta de limite de 5 fichas
  - `e2e/tests/api/fichas.spec.ts`, `e2e/tests/api/treinos.spec.ts`, `e2e/tests/api/users.spec.ts`,
    `e2e/tests/ui/fichas.spec.ts` — testes novos e ajustados
  - `e2e/tests/ui/login.spec.ts` — novo

### Execuções (ambiente local)

| Execução | Início (BRT) | Escopo | Executados | Aprovados | Reprovados | Ignorados | Duração |
|---|---|---|---|---|---|---|---|
| R2 | 2026-10-06 23:55:14 | Arquivos afetados (código antes da correção do `FichaBaseService`) | 64 | 55 | 9 | 0 | 130,8 s |
| R3a | 2026-10-06 23:59:16 | Arquivos afetados (código final) | 64 | 64 | 0 | 0 | 80,1 s |
| **R3** | **2026-10-07 00:00:54** | **Suíte completa (código final)** | **128** | **128** | **0** | **0** | **96,5 s** |
| R3-iso | 2026-10-07 00:02:50 | UI-FICHA-001 × 3 | 3 | 3 | 0 | 0 | 38,9 s |

Falhas da R2: 2 de API por texto corrompido (FICHA-020, FICHA-021), 1 de UI por texto corrompido
(UI-FICHA-004), 2 de UI por seletor do próprio teste (UI-LOGIN-003/004: `getByRole('alert')`
também encontrava as mensagens dos campos do Vuetify; corrigido para `.v-alert`) e 4 de UI de
fichas que rodaram junto com os erros de conexão descritos em "Estabilidade da suíte".

### Códigos HTTP atuais para acesso a recursos de outro atleta (sem padronização nesta rodada)

| Recurso | Código atual | Coberto por teste? |
|---|---|---|
| `PUT /users/{idB}/senha` | 403 | Sim (USER-003) |
| Repetições: `POST`, `GET` lista | 404 "Série de treino não encontrada para este treino." | Sim (REP-001) |
| Repetições: `GET` item, `PUT`, `DELETE` | 404 "Repetição não encontrada." | Sim (REP-001) |
| `PUT /fichas-base/{id}`, `PUT /fichas-base/{id}/status`, `GET /fichas-base/{id}/series`, `POST /fichas-base/series/{id}/duplicar` | 404 | Não (lido no código) |
| `PUT /metas/{id}`, `GET /metas/{id}/progresso` | 404 | Não (lido no código; METR-005 pendente) |
| `POST /treinos/{id}/series`; `GET`, `PUT`, `DELETE /treinos/{id}/series/{idSerie}` | 404 | `PUT` não referenciado com token de outro atleta; demais não (lido no código) |
| `POST /treinos/{idB}/metas/{idMeta}` | 404 | Não (lido no código) |
| `DELETE` de vínculo Treino × Meta pertencente a B | 403 (404 se o vínculo não existir) | Não (lido no código) |
| `GET /treinos/{idB}/series`, `GET /treinos/{idB}/metas`, `GET /metas/{idB}/treinos` | **200 com lista vazia** | Não (lido no código) |

### Cobertura de endpoints: chamadas diretas × indiretas

51 endpoints mapeados nos controllers. **Direta** = o endpoint é chamado por um teste de API (ou
helper de `e2e/utils`). **Indireta** = o endpoint só é chamado pelo frontend durante um teste de UI,
sem que o teste verifique a resposta.

| Situação | Quantidade | Endpoints |
|---|---|---|
| Chamada direta | 37 | os 31 anteriores + `PUT /users/{id}/senha`, `PUT /fichas-base/{id}/status`, `GET .../repeticoes`, `GET .../repeticoes/{idRep}`, `PUT .../repeticoes/{idRep}`, `DELETE .../repeticoes/{idRep}` |
| Só indireta (pela UI, em UI-TREINO-001) | 2 | `GET /treinos` (com `status=0`), `PUT /treinos/{id}` |
| Nenhuma chamada | 12 | `PUT /fichas-base/{id}`, `GET /fichas-base/{id}/series`, `POST /fichas-base/series/{id}/duplicar`, `POST /treinos/{id}/series`, `GET /treinos/{id}/series`, `GET /treinos/{id}/series/{idSerie}`, `DELETE /treinos/{id}/series/{idSerie}`, `PUT /metas/{id}`, 4 endpoints de Treino × Meta |

### Deixado como melhoria futura nesta rodada

Exclusão do treino inteiro; mudança no contrato de adicionar série a treino em andamento; testes de
filtros e ordenação do histórico; cobertura dos endpoints restantes; testes de Treino × Meta (sem
alterar o comportamento atual do vínculo); padronização dos códigos de acesso entre atletas; nova
tela inicial e métricas na UI; tela de detalhe da meta; tipos de ficha; total acumulado no
histórico; UI de edição de série; regras de meta (meta ativa por série, conclusão automática) e
validação do tipo de nado da meta; textos ainda corrompidos não confirmados por teste
(`FichaBaseService.cs`: mensagem de limite ao duplicar e "Ficha de treino n�o encontrada..."; demais
arquivos em ISO-8859 não verificados).
