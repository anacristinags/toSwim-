# Apresentação Final — Roteiro


---

## 1. Abertura — o produto

- O **toSwim** é uma aplicação para organizar, acompanhar e analisar treinos de natação.
- Fluxo principal: criar fichas reutilizáveis → escolher a ficha do dia → registrar tempos por série → ver pace, histórico e metas.
- Os dados são organizados por usuário.
- Tecnologias: .NET, Vue 3 + Vuetify, PostgreSQL e Playwright.

Apoio: [Home](Home.md)

## 2. O que foi planejado, implementado e validado

- **Planejado:** o documento de requisitos prevê 7 telas, com histórias de usuário, critérios de aceite e 38 cenários de teste.
- **Entregue:**
  - 5 das 7 telas. Faltam a Tela Inicial e o Detalhe da Meta;
  - na execução, só o treino comum. O treino de meta de distância e o vínculo da execução com a meta não foram entregues;
  - a evolução e as métricas existem só na API, sem tela.
- **Fora do documento, mas entregue:** a tela de configuração de piscina, a piscina por ficha e o nado "Livre".
- **Divergências para decisão de produto:** por exemplo, uma ficha pode ser salva sem séries e o login leva a `/fichas`.
- **Cobertura:** 19 dos 38 cenários do documento têm caso relacionado.

Apoio: [Planejado × Entregue](Planejado-x-Entregue.md), [Home](Home.md), [Matriz de Cobertura](Matriz-de-Cobertura.md)

## 3. Telas

- 7 telas: Login, Cadastro, Fichas, Execução do Treino, Metas, Histórico e Piscina.

Apoio: [Telas do Sistema](Telas-do-Sistema.md)

## 4. Abordagem de QA

- A matriz de testes foi construída a partir das regras implementadas no código.
- Testes automatizados com Playwright em dois projetos, API e UI, em ambiente local.
- Os focos são validações, limites, fluxos de tela e **isolamento de dados entre atletas**.

Apoio: [Plano de Testes](Plano-de-Testes.md)

## 5. Cobertura

- 125 casos na matriz, dos quais 120 têm teste (96,0%) e 5 estão pendentes.
- 128 testes na suíte: 92 de API e 36 de UI.
- 37 de 51 endpoints são chamados diretamente pelos testes (72,5%).

Apoio: [Matriz de Cobertura](Matriz-de-Cobertura.md), [Métricas](Metricas.md)

## 6. Resultados comprovados

- **Setembro (log bruto):** 39 de 39 testes de API aprovados; UI com 12 de 17 e depois 13 de 17 aprovados.
- **Rodada final:**
  - R2: 55 de 64 aprovados (85,9%);
  - após as correções, R3a: 64 de 64;
  - **R3: 128 de 128 testes aprovados** (07/10/2026). Este resultado é um registro escrito, sem relatório bruto anexado.

Apoio: [Execução e Evidências](Execucao-e-Evidencias.md)

## 7. Defeitos encontrados e corrigidos

- Início de treino aceitava ficha sem séries: corrigido.
- 28 mensagens de erro com `?` no lugar dos acentos: corrigidas e parcialmente cobertas por teste (15 de 28 linhas).
- 3 textos com caractere corrompido: detectados pelos testes na R2 e corrigidos.
- 

Apoio: [Defeitos e Retestes](Defeitos-e-Retestes.md)

## 8. Retrospectiva

- **Desafios:** regras de negócio, prioridades, frontend, análise de bugs e Playwright.
- **Aprendizados:** o ciclo de desenvolvimento, o fluxo de QA e a automação de testes.

Apoio: [Retrospectiva](Retrospectiva.md)

## 9. Próximos passos

Estes são itens da lista de melhorias futuras que eu gostaria de implementar nesse projeto:
- Finalizar com as telas que eu inicialmente planejei;
- Criar uma comunidade para que amigos e professores/alunos consigam compartilhar os treinos, metricas e resultados entre si.
- Garantir a Qualidade do produto para que p usuário final tenha uma ótima experiência.

Apoio: [Casos de Teste — sugestões](Casos-de-Teste.md#sugestões-pendentes-de-aprovação)
