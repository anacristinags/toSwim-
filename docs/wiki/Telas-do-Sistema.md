# Telas do Sistema

 Voltar para a [Home](Home.md).


## Telas planejadas × entregues

As telas planejadas vêm do documento de requisitos. Detalhes por critério estão em [Planejado × Entregue](Planejado-x-Entregue.md).

| Tela | No documento? | Rota | Título exibido | Acesso |
|---|---|---|---|---|
| Login | Sim | `/login` | Entrar no toSwim | Pública |
| Cadastro | Só em "Possíveis melhorias" | `/cadastro` | Criar Conta no toSwim | Pública |
| Tela Inicial | Sim | — **não entregue** | — | — |
| Ficha de Treino | Sim | `/fichas` (a rota `/` redireciona para cá) | Fichas de Treino Base | Autenticada |
| Execução do treino | Sim | `/treino-execucao` | Execucao do Treino do Dia | Autenticada |
| Histórico de Treinos | Sim | `/historico` | Historico de Treinos | Autenticada |
| Metas | Sim | `/metas` | Metas de Tempo | Autenticada |
| Detalhe da Meta | Sim | — **não entregue** | — | — |
| Configuração da Piscina | Não (piscina prevista como critério do Login) | `/piscina` | Configuracao da Piscina | Autenticada |


### Layout geral (barra de topo e menu)

- **Elementos:** barra de topo com o nome "toSwim App"; menu lateral para navegar entre as telas; opção "Sair" (logout).
- **Rotas protegidas:** sem token, o acesso redireciona para `/login?redirect=<rota>`.
- **Casos:** [NAV-001](Casos-de-Teste.md#nav-001), [NAV-002](Casos-de-Teste.md#nav-002), [NAV-003](Casos-de-Teste.md#nav-003), [NAV-004](Casos-de-Teste.md#nav-004), [NAV-005](Casos-de-Teste.md#nav-005).
- **Screenshot:** _pendente_.

<!-- ![Layout geral com menu lateral](images/telas/NOME-DO-ARQUIVO.png) -->

### Login (`/login`)

- **Elementos:** campos de e-mail e senha e botão "Entrar". O botão fica desabilitado com campos vazios. Credenciais inválidas exibem o alerta "Email ou senha inválidos". Após o login, a tela respeita o parâmetro `redirect`.
- **Diferenças em relação ao documento:** o login leva a `/fichas`, não à tela inicial. A escolha da piscina não fica no login.
- **Casos:** [UI-LOGIN-001](Casos-de-Teste.md#ui-login-001) a [UI-LOGIN-005](Casos-de-Teste.md#ui-login-005).
- **Screenshot:** _pendente_.

<!-- ![Tela de login](images/telas/NOME-DO-ARQUIVO.png) -->

### Cadastro (`/cadastro`)

- **Elementos:** formulário de cadastro com confirmação de senha. O botão fica desabilitado enquanto o formulário é inválido.
- **Integração com o backend:** a view chama `authService.cadastrar`. O `e2e/README.md` ainda diz que a tela não está integrada, ponto **a confirmar**.
- **Casos:** [UI-CAD-001](Casos-de-Teste.md#ui-cad-001) a [UI-CAD-004](Casos-de-Teste.md#ui-cad-004). Eles cobrem apenas a validação do formulário.
- **Screenshot:** _pendente_.

<!-- ![Tela de cadastro](images/telas/NOME-DO-ARQUIVO.png) -->

### Fichas de Treino Base (`/fichas`)

- **Elementos:**
  - cards das fichas ativas, com o chip "Piscina: 25m/50m" e o contador "Fichas: X / 5 Ativas";
  - diálogo "Criar Nova Ficha Base", com piscina de 25 ou 50 metros;
  - diálogo "Adicionar Serie ao Treino";
  - ações para duplicar e excluir a ficha e para duplicar e remover séries;
  - mensagem de limite ao atingir 5 fichas ativas.
- **Diferenças em relação ao documento:** a ficha pode ser salva sem séries. As séries entram por diálogo, depois que a ficha já foi criada.
- **Casos:** [UI-FICHA-001](Casos-de-Teste.md#ui-ficha-001) a [UI-FICHA-005](Casos-de-Teste.md#ui-ficha-005), [FICHA-017](Casos-de-Teste.md#ficha-017), [SERIE-010](Casos-de-Teste.md#serie-010).
- **Screenshot:** _pendente_. Sugestões de captura: a lista de fichas, o diálogo de criação e o limite de 5 fichas.

<!-- ![Fichas de treino base](images/telas/NOME-DO-ARQUIVO.png) -->

### Execução do Treino do Dia (`/treino-execucao`)

- **Elementos:**
  - seção "Escolha a Ficha para Treinar Hoje", com a seleção da ficha base e o título do treino do dia;
  - botão "Iniciar Treino na Piscina", desabilitado sem configuração de piscina;
  - registro do tempo de cada tiro e finalização do treino.
- **Diferenças em relação ao documento:** existe apenas o treino comum. Não há treino de meta de distância, edição de distância, adição de séries nem vínculo com a meta.
- **Casos:** [TREINO-015](Casos-de-Teste.md#treino-015) e o teste sem ID `ui/treinos.spec.ts:18` ([lista](Casos-de-Teste.md#testes-automatizados-sem-id-na-matriz)).
- **Screenshot:** _pendente_.

<!-- ![Execução do treino do dia](images/telas/NOME-DO-ARQUIVO.png) -->

### Metas de Tempo (`/metas`)

- **Elementos:**
  - cards de meta com tipo de nado, piscina e percentual de progresso;
  - abas de filtro por status (Todas, Ativas e Concluídas);
  - diálogo "Criar Meta de Tempo";
  - botão "Nova Meta", desabilitado quando não há séries.
- **Diferenças em relação ao documento:** não há tipos de meta (distância ou velocidade). A criação usa um diálogo. Não há tela de detalhe.
- **Casos:** [META-012](Casos-de-Teste.md#meta-012), [META-016](Casos-de-Teste.md#meta-016), [META-017](Casos-de-Teste.md#meta-017) e os testes sem ID `ui/metas.spec.ts:6` e `:15`.
- **Screenshot:** _pendente_.

<!-- ![Metas de tempo](images/telas/NOME-DO-ARQUIVO.png) -->

### Histórico de Treinos (`/historico`)

- **Elementos:**
  - tabela de treinos concluídos, com as colunas "Data", "Distancia Total", "Tempo Total" e "Pace Medio" (/100m) e o chip de piscina;
  - filtros por título e por data;
  - diálogo de detalhe com as séries e a marcação "Fora do pace médio".
- **Diferenças em relação ao documento:** não há filtro por tipo de nado nem distância total acumulada.
- **Casos:** [HIST-005](Casos-de-Teste.md#hist-005), [HIST-007](Casos-de-Teste.md#hist-007) e os testes sem ID `ui/historico.spec.ts:57` e `:67`.
- **Screenshot:** _pendente_.

<!-- ![Histórico de treinos](images/telas/NOME-DO-ARQUIVO.png) -->

### Configuração da Piscina (`/piscina`)

- **Elementos:** seções "Tamanho da Piscina" e "Modo de Contagem Padrao". O cabeçalho reflete o tamanho salvo.
- **Casos:** [PISC-008](Casos-de-Teste.md#pisc-008) e os testes sem ID `ui/piscina.spec.ts:5`, `:19` e `:51`.
- **Screenshot:** _pendente_.

<!-- ![Configuração da piscina](images/telas/NOME-DO-ARQUIVO.png) -->

## Telas planejadas e não entregues

- **Tela Inicial:** resumo das fichas, metas ativas e evolução recente, com atalhos. Não há rota no frontend. A API tem `GET /dashboard/resumo`.
- **Detalhe da Meta:** progresso, estimativa de alcance, execuções relacionadas e histórico filtrado. Não há rota no frontend.

Como essas telas não existem, **não há prints a incluir** para elas.

## Funcionalidades sem tela

As métricas e a evolução (`/dashboard/resumo`, `/metricas/pace-medio`, `/metricas/melhores-tempos`) existem na API, mas **não têm rota no frontend**. O progresso da meta (`/metas/{id}/progresso`) aparece na tela de Metas.
