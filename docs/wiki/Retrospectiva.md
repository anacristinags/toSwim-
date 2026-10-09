# Retrospectiva

Principais desafios e aprendizados deste projeto, com os registros que os confirmam. Voltar para a [Home](Home).

## Principais desafios enfrentados

### 1. Estruturar e planejar as regras de negócio e o que se espera do produto

Inicialmente planejei 7 telas, com critérios de aceite e regras. Durante o desenvolvimento e a implementação houve divergências entre o planejado e o resultado final, como a ficha salva sem séries e a ausência de tipos de meta ([Planejado × Entregue](Planejado-x-Entregue#divergências-para-avaliação)).

### 2. Ter resiliência para mudar os planos e priorizar o que tratar no desenvolvimento

Alguns planejamentos iniciais tiveram a rota recalculada por causa da complexidade, e a princípio focamos em fazer o "arroz com feijão". A Tela Inicial, o Detalhe da Meta, o treino de meta de distância e o vínculo da execução com a meta ficaram de fora, junto com as métricas na UI e os testes de Treino × Meta. 

### 3. Implementar o frontend

Design e estruturação visual são um ponto fraco meu, e esta foi uma das partes mais desafiadoras para mim.

### 4. Resolver os bugs encontrados nos testes e mudar a perspectiva sobre comportamentos esperados

Mais de uma vez voltamos ao ponto do planejado × o que seria melhor: tive uma mudança de perspectiva e várias vezes tiver que fazer a análise de qual comportamento seria melhor para o usuário final, por exemplo: a implementação do tipo de nado "Livre" que foi uma caso que não estava no escopo inicial do projeto porém reconhecemos essa necessidade. 


### 5. Implementar os testes automatizados

Era algo novo para mim, e eu nunca tinha utilizado. A suíte começou com 56 testes em setembro e chegou a 128 na rodada final de outubro ([Execução e Evidências](Execução-e-Evidências)). 

## Aprendizados técnicos e de QA

### 1. Visão completa do ciclo de desenvolvimento

Pude ter uma noção muito mais completa do ciclo de desenvolvimento de um produto: da estruturação e da análise de regras e critérios de aceite até a implementação e a cobertura final como QA. A matriz de testes liga cada cenário à sua pré-condição, dado, resultado esperado e teste, e a rodada final registra a versão testada e as execuções.

### 2. Análise mais técnica e padronizada do fluxo de QA

Passei a trabalhar o fluxo de QA de forma mais técnica e padronizada, com planos de teste, matrizes e casos de teste ([Plano de Testes](Plano-de-Testes)).

### 3. Uso de testes automatizados com Playwright

Passei a utilizar testes automatizados no Playwright: escrevi testes de API e de UI em dois projetos e acompanhei os logs de execução desde setembro.
