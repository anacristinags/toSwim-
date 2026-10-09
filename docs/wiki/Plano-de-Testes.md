# Plano de Testes

> **Rascunho local.** Ainda não foi publicado na Wiki. Voltar para a [Home](Home.md).

## Planilha do plano de testes

Segue uma plano de teste sobre como executar um teste padrão:

**Link da planilha:** [Plano de testes — toSwim](https://dtidigital-my.sharepoint.com/:x:/g/personal/rickson_lima_dtidigital_com_br/IQD0rTecl171SZdzZQAXDAryAbtF5YJD3lkSCqpQKlAUdA0?e=HP5sSm)


## 1. Escopo

### 1.1 Em escopo (funcionalidades com casos na matriz)

| # | Funcionalidade | Endpoint / tela | Casos |
|---|---|---|---|
| 1 | Autenticação | `/auth` | 10 |
| 2 | Fichas base e séries | `/fichas-base`, `/fichas-base/{id}/series`; tela `/fichas` | 38 |
| 3 | Metas de tempo, incluindo o progresso por tamanho de piscina | `/metas`; tela `/metas` | 20 |
| 4 | Configuração de piscina | `/piscina-configuracao`; tela `/piscina` | 8 |
| 5 | Treinos: execução, séries e repetições | `/treinos`; tela `/treino-execucao` | 20 |
| 6 | Histórico | `/historico/treinos`; tela `/historico` | 7 |
| 7 | Métricas | `/dashboard/resumo`, `/metricas/*` | 5 |
| 8 | Usuários | `/users` | 3 |
| 9 | Cadastro e login (UI) | telas `/cadastro`, `/login` | 9 |
| 10 | Navegação, logout e proteção de rotas (UI) | menu, rotas protegidas | 5 |
| | **Total** | | **125** |

### 1.2 Fora do escopo 

Estes itens são **melhorias futuras**:

- exclusão do treino inteiro;
- mudança no contrato de adicionar série a treino em andamento;
- testes de filtros e ordenação do histórico;
- cobertura dos endpoints restantes;
- testes de Treino × Meta, sem alterar o comportamento atual do vínculo;
- padronização dos códigos de acesso entre atletas;
- nova tela inicial e métricas na UI;
- tela de detalhe da meta;
- tipos de ficha;
- total acumulado no histórico;
- UI de edição de série;
- regras de meta, como meta ativa por série e conclusão automática;
- validação do tipo de nado da meta;
- textos ainda corrompidos e não confirmados por teste.

## 2. Abordagem

| Aspecto | Registro nas fontes |
|---|---|
| Ferramenta | Playwright (`@playwright/test` ^1.55.0) |
| Projetos | `api` (`e2e/tests/api`), com chamadas HTTP diretas à API .NET; `web` (`e2e/tests/ui`), com navegador Desktop Chrome contra o frontend |
| Frontend nos testes de UI | Build de produção servido por `vite preview` na porta 5173, iniciado automaticamente pelo Playwright. O motivo foi eliminar a instabilidade do `vite dev` em paralelo. |


### Tipos de teste

- **Funcional de API:** contratos e regras dos endpoints, como códigos HTTP e corpo da resposta.
- **Funcional de UI (E2E):** fluxos nas telas, como criar ficha, executar treino, filtros e mensagens.
- **Validação e limites:** campos obrigatórios, intervalos (`Range`), tamanhos de piscina e limite de 5 fichas ativas.
- **Controle de acesso e isolamento entre atletas:** respostas 401 sem token e 403/404 ao acessar recursos de outro atleta.
- **Regressão:** reexecução da suíte completa na rodada final (R3).

## 3. Ambiente

| Componente | Configuração registrada |
|---|---|
| Tipo | **Local**, na máquina de quem testa. As URLs padrão são `localhost`. |
| Frontend | `http://localhost:5173` (`FRONTEND_URL`) |
| API | `http://localhost:5064` (`API_URL`). Precisa ser iniciada manualmente com `dotnet run` em `backend/ToSwim.Api`. |
| Banco | PostgreSQL 15 via `docker compose up -d postgres`, com migrations via `docker compose run --rm liquibase` |
| Node.js | 18 ou superior (`e2e/README.md`) |


## 4. Dados de teste

- Os testes de API criam atletas com **e-mails únicos** (`e2e/utils/auth.ts`), para não colidir com a restrição de e-mail único entre execuções.
- Fichas, séries, treinos e metas são criados via API pelos helpers de `e2e/utils`, inclusive como pré-condição dos testes de UI.
- Os cenários de isolamento usam **duas contas**: o atleta A tenta acessar um recurso do atleta B.


## 5. Riscos e pontos de atenção para melhoria

| Risco / ponto | Descrição |
|---|---|
| Escopo planejado não entregue | O documento prevê a Tela Inicial, o Detalhe da Meta, o treino de meta de distância, o vínculo da execução com a meta e os filtros por tipo de nado, entre outros, que não foram implementados |
| Códigos HTTP sem padronização | O acesso a recursos de outro atleta retorna 403, 404 ou 200 com lista vazia, conforme o recurso. |
| Endpoints sem cobertura | Dos 51 endpoints, 12 não são chamados por nenhum teste e 2 só são chamados indiretamente pela UI. |


## 6. Comandos (definidos em `e2e/package.json`)

| Comando | Uso |
|---|---|
| `npm test` | UI + API |
| `npm run test:api` | Somente o projeto `api` |
| `npm run test:ui` | Somente o projeto `web` |
| `npm run report` | Abre o último relatório HTML |
