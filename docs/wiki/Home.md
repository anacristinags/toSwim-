# toSwim — Documentação de QA

## Sobre o produto

O **toSwim** é uma aplicação para organizar, acompanhar e analisar treinos de natação. O  objetivo é ajudar nadadores a controlar seus treinos de forma simples e estruturada, permitindo:

- criar fichas de treino base;
- reutilizar treinos em diferentes dias;
- registrar a execução do treino do dia;
- calcular pace e demais métricas;
- criar e acompanhar metas;
- consultar histórico e evolução;
- manter os dados separados por usuário.

**Tecnologias**:

| Camada | Tecnologia |
|---|---|
| Backend | C# / .NET, com `TargetFramework` `net9.0` nos `.csproj`. |
| Frontend | Vue 3 + Vuetify + Vite (TypeScript) |
| Banco de dados | PostgreSQL 15 (`postgres:15-alpine`), com migrations via Liquibase 4.27 |
| Testes | Playwright (`@playwright/test` ^1.55), com testes de API e de UI |

## Planejado × implementado × validado

Visão resumida. O comparativo de cada critério de aceite e cenário do documento está em [Planejado × Entregue](Planejado-x-Entregue.md). A relação com os casos está na [Matriz de Cobertura](Matriz-de-Cobertura.md).

**Telas previstas no documento:**

| Tela | Situação |
|---|---|
| Login | Entregue, com diferenças |
| Tela Inicial | **Não entregue** |
| Ficha de Treino | Entregue, com diferenças |
| Execução do treino | Somente o treino comum |
| Histórico | Entregue, com diferenças |
| Metas | Entregue, com diferenças |
| Detalhe da Meta | **Não entregue** |

Há **7 divergências** entre o documento e a implementação para avaliação de produto. A principal é que uma ficha pode ser salva sem séries, embora o documento proíba.

| Funcionalidade | Implementado? | Validado? (testes com execução registrada) |
|---|---|---|
| Cadastro, login e dados por usuário | Sim: API `/auth`, `/users`; telas `/login` e `/cadastro`; filtro por usuário nos repositórios | Sim, via API e UI. Isolamento entre atletas coberto em várias funcionalidades. |
| Fichas de treino reutilizáveis | Sim: API `/fichas-base` e séries; tela `/fichas` | Sim, via API e UI |
| Selecionar ficha para o treino do dia | Sim: API `POST /treinos`; tela `/treino-execucao` | Sim, via API e UI |
| Registrar tempo por série | Sim: API de séries e repetições do treino; tela `/treino-execucao` | Sim, via API e UI |
| Calcular pace | Sim: pace alvo da meta, pace médio no histórico e nas métricas | Parcial. Coberto em casos de metas e métricas (API). A fórmula do pace não é descrita na matriz. |
| Histórico | Sim: API `/historico/treinos`; tela `/historico` | Sim, via API e UI |
| Evolução | Parcial: endpoints de métricas na API; **não há tela de métricas** no frontend | Somente via API (METR-001..004) |
| Metas | Sim: API `/metas` e progresso; tela `/metas` | Sim, via API e UI (METR-005 pendente) |
| Configuração de piscina *(no documento, prevista apenas como critério do Login)* | Sim: API `/piscina-configuracao`; tela `/piscina` | Sim, via API e UI |
| Tela inicial (resumo e atalhos) | Não: existe só a API `/dashboard/resumo` | Somente a API  |
| Detalhe da meta | Não | — |


## Páginas

| Página | Conteúdo |
|---|---|
| [Planejado × Entregue](Planejado-x-Entregue.md) | Documento de requisitos comparado com a implementação e os casos |
| [Plano de Testes](Plano-de-Testes.md) | Link da planilha do plano de testes e resumo complementar (abordagem, ambiente, riscos) |
| [Matriz de Cobertura](Matriz-de-Cobertura.md) | Funcionalidades × casos × testes × execução |
| [Casos de Teste](Casos-de-Teste.md) | Os 125 casos da matriz em detalhe|
| [Execução e Evidências](Execucao-e-Evidencias.md) | Execuções registradas, ambiente, versão e evidências |
| [Defeitos e Retestes](Defeitos-e-Retestes.md) | Defeitos, correções e retestes registrados |
| [Métricas](Metricas.md) | Métricas calculadas, com fonte e fórmula |
| [Telas do Sistema](Telas-do-Sistema.md) | Telas identificadas e casos relacionados |
| [Retrospectiva](Retrospectiva.md) | Desafios e aprendizados |
| [Apresentação Final](Apresentacao-Final.md) | Roteiro da apresentação |
