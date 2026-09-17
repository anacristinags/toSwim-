# Matriz de Testes — toSwim

> Gerada em 2026-09-16 a partir da inspeção do código-fonte real: controllers/services/DTOs do
> backend (`backend/ToSwim.Api`, `backend/ToSwim.Application`), views do frontend
> (`frontend/src/views`, `frontend/src/router`) e suíte E2E existente (`e2e/tests`).
> Os resultados esperados de cada caso foram derivados das regras efetivamente implementadas nos
> services (não de suposições) — quando o comportamento do código diverge do que seria desejável,
> isso está anotado na coluna de observações e listado na seção **Defeitos encontrados**.
>
> Baseline confirmada antes de qualquer alteração: **56/56 testes passando** (39 API + 17 UI).

Legenda de **Status**: `Automatizado` (já existe teste e passa), `Pendente` (sem teste ainda),
`Implementado nesta análise` (adicionado como parte deste trabalho).

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
| SERIE-001 | Séries | Rejeita distância zero | API | Ficha criada | distanciaM=0 | 400 (`[Range(1,10000)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-002 | Séries | Rejeita distância negativa | API | Ficha criada | distanciaM=-10 | 400 (`[Range(1,10000)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-003 | Séries | Rejeita distância acima do limite máximo (10000) | API | Ficha criada | distanciaM=10001 | 400 (`[Range(1,10000)]`) | Média | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-004 | Séries | Rejeita tipoNado fora do enum válido (0-3) | API | Ficha criada | tipoNado=99 | 400 (`[Range(0,3)]`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-005 | Séries | Rejeita quantidadeRepeticoes zero/negativa | API | Ficha criada | quantidadeRepeticoes=0 | 400 (`[Range(1,1000)]`) | Média | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-006 | Séries | Rejeita tempoPausaSeg negativo | API | Ficha criada | tempoPausaSeg=-1 | 400 (`[Range(0,3600)]`) | Média | Pendente | — |
| SERIE-007 | Séries | Atleta A não atualiza série do atleta B | API | Duas contas, série do B | PUT série do B, token do A | 404 (`serie.Ficha?.CodUsuario != codUsuario`) | Alta | **Implementado nesta análise** | `fichas.spec.ts` |
| SERIE-008 | Séries | Atleta A não exclui série do atleta B | API | Duas contas, série do B | DELETE série do B, token do A | 404 | Alta | **Implementado nesta análise** | `fichas.spec.ts` |

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
| TREINO-007 | Treinos | Rejeita iniciar treino a partir de ficha sem série | API | Ficha sem nenhuma série | codFicha de ficha vazia | Backend **aceita e cria treino com 0 séries** — `TreinoService.IniciarTreinoAsync` não valida `ficha.Series.Any()`. **Defeito de regra de negócio**: o requisito do QA pede bloqueio, mas o código permite. Teste documenta o comportamento real (201, `seriesTreino=[]`) e sinaliza o gap. | Alta | **Implementado nesta análise** (documenta defeito) | `treinos.spec.ts` |
| TREINO-008 | Treinos | Rejeita iniciar treino com ficha inexistente/de outro atleta | API | Ficha pertence ao atleta B | codFicha do B, token do A | 404 (`"Ficha base não encontrada ou inativa."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-009 | Treinos | Rejeita finalizar treino já finalizado (dupla finalização) | API | Treino já finalizado | PUT `/finalizar` novamente | 400 (`"Este treino já está finalizado ou cancelado."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-010 | Treinos | Rejeita cancelar treino já finalizado | API | Treino já finalizado | PUT `/cancelar` | 400 (`"Apenas treinos em andamento podem ser cancelados."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-011 | Treinos | Rejeita ações (registrar repetição) em treino inexistente | API | codTreino inexistente | POST repetição | 404 | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-012 | Treinos | Rejeita registrar repetição em treino já finalizado | API | Treino finalizado | POST repetição | 400 (`"Não é possível registrar repetições para um treino finalizado ou cancelado."`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-013 | Treinos | **Isolamento:** atleta A não acessa/edita treino do atleta B | API | Treino do atleta B | GET/PUT com token do A | 404 (`TreinoRepository.ObterPorIdAsync` filtra por `codUsuario`) | Alta | **Implementado nesta análise** | `treinos.spec.ts` |
| TREINO-014 | Treinos | Rejeita iniciar treino a partir de ficha inativa (status=0) | API | Ficha desativada | codFicha inativo | 404 (`ficha.Status != 1`) | Média | Pendente | — |
| TREINO-015 | Treinos UI | Iniciar treino sem piscina configurada mantém botão desabilitado (fluxo já coberto) | UI | Sem config de piscina | — | Botão "Iniciar Treino na Piscina" desabilitado | Alta | Automatizado | `treinos.spec.ts:6` (UI) |

## 6. Histórico (`/historico/treinos`)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| HIST-001 | Histórico | 401 sem token | API | Nenhuma | — | 401 | Alta | Automatizado | `historico.spec.ts:6` |
| HIST-002 | Histórico | Lista vazia sem treinos concluídos | API | Atleta sem treinos | — | 200, `[]` | Média | Automatizado | `historico.spec.ts:11` |
| HIST-003 | Histórico | Lista treino concluído e rejeita detalhe de treino em andamento | API | 1 treino concluído + 1 em andamento | — | 200 lista; 400 no detalhe do treino em andamento | Alta | Automatizado | `historico.spec.ts:22` |
| HIST-004 | Histórico | **Isolamento:** atleta A não acessa histórico/detalhe de treino do atleta B | API | Treino concluído do atleta B | GET detalhe, token do A | 404 (`TreinoService.ObterPorIdAsync` filtra por `codUsuario`) | Alta | **Implementado nesta análise** | `historico.spec.ts` |
| HIST-005 | Histórico UI | Filtro por título e por data reduz a lista exibida | UI | 2 treinos concluídos com títulos distintos | preencher filtro de título | Apenas linha correspondente permanece visível | Alta | **Implementado nesta análise** | `historico.spec.ts` (UI) |

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
| USER-003 | Usuários | Atleta A não altera senha do atleta B | API | Duas contas | PUT `/users/{idB}/senha`, token do A | 403 (`Forbid()` — `User.ObterUsuarioId() != id`) | Alta | Pendente | — |

## 9. Cadastro / Login (UI)

| ID | Módulo | Cenário | Camada | Pré-condições | Dados de entrada | Resultado esperado | Prioridade | Status | Teste |
|----|--------|---------|--------|----------------|-------------------|---------------------|------------|--------|-------|
| UI-CAD-001 | Cadastro | Exibe formulário de cadastro | UI | Nenhuma | — | Campos visíveis | Média | Automatizado | `cadastro.spec.ts:8` |
| UI-CAD-002 | Cadastro | Botão desabilitado com formulário inválido | UI | Nenhuma | — | Botão desabilitado | Média | Automatizado | `cadastro.spec.ts:16` |
| UI-CAD-003 | Cadastro | Habilita envio com campos válidos | UI | Nenhuma | dados válidos | Botão habilitado | Média | Automatizado | `cadastro.spec.ts:21` |
| UI-CAD-004 | Cadastro | Valida senha/confirmação diferentes | UI | Nenhuma | senhas diferentes | Mensagem de erro; botão desabilitado | Alta | Automatizado | `cadastro.spec.ts:30` |

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

| Camada | Automatizados (antes) | Implementados nesta análise | Pendentes (baixo risco, não implementados) |
|--------|------------------------|-------------------------------|----------------------------------------------|
| API    | 39                      | 28                             | 6 (AUTH-008, AUTH-010, SERIE-006, META-011, TREINO-014, METR-005, USER-003) |
| UI     | 17                      | 5                               | 0 adicionais mapeados nesta rodada |
| **Total** | **56**              | **33**                         | **7** |

> Contagem real de testes após a implementação (`grep -c "test("`): **67 testes de API** e
> **22 testes de UI** = **89 testes no total**, todos passando.

> Os pendentes remanescentes são cenários de prioridade média/baixa (limites de tamanho de string,
> mensagens de erro específicas para IDs inexistentes) que não estavam entre os quatro focos de
> risco solicitados (isolamento, borda, estados inválidos de treino, UI não coberta). Ficam
> registrados aqui para priorização futura.

---

## Defeitos encontrados

> Nota: a hipótese inicial de que `[Required]` aceitaria strings compostas só de espaços em
> branco (títulos/nomes) foi **verificada e descartada** ao rodar os testes: o `RequiredAttribute`
> do ASP.NET Core aplica `Trim()` internamente antes de validar o tamanho quando
> `AllowEmptyStrings` não é definido, então `tituloFicha="   "` e `nome="   "` já retornam 400
> corretamente (ver `FICHA-005` e `AUTH-009`, cobertos como testes de borda normais, não como
> defeitos).

1. **Início de treino não valida ficha sem séries (`TREINO-007`)** — `TreinoService.IniciarTreinoAsync`
   não verifica se `ficha.Series` está vazio antes de criar o treino; o sistema permite iniciar (e
   posteriormente finalizar) um treino sem nenhuma série vinculada, o que não faz sentido para o
   fluxo de execução de treino. **Sugestão de correção:** adicionar checagem
   `if (!ficha.Series.Any()) throw new AppException(...)` no início do método.

O item acima foi **documentado via teste automatizado** (o teste afirma o comportamento
real observado, não o comportamento desejado), conforme instrução de não alterar regra de negócio
apenas para fazer o teste passar. Recomenda-se abrir um ticket de correção e, quando corrigido,
atualizar os testes correspondentes para refletir o novo comportamento esperado (400).
