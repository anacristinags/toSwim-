# toSwim E2E

Testes end-to-end (UI) e de API do toSwim, usando [Playwright](https://playwright.dev/).

## Estrutura

```
e2e/
├── playwright.config.ts   # 2 projetos: "web" (UI) e "api" (backend)
├── tests/
│   ├── ui/                 # testes de interface (Vue + Vuetify)
│   │   ├── navegacao.spec.ts
│   │   └── cadastro.spec.ts
│   └── api/                 # testes de API (.NET)
│       ├── auth.spec.ts
│       └── users.spec.ts
└── utils/
    └── auth.ts             # helpers (registro de atleta de teste, etc.)
```

## Pré-requisitos

- Node.js 18+
- Para os testes de **API**: backend (.NET) e Postgres em execução
  ```bash
  # na raiz do repositório
  docker compose up -d postgres
  docker compose run --rm liquibase
  cd backend/ToSwim.Api
  dotnet run
  ```
- Para os testes de **UI**: nada além de instalar dependências — o Playwright
  inicia o `npm run dev` do frontend automaticamente (porta 5173).

## Instalação

```bash
cd e2e
npm install
npx playwright install chromium   # ou "npx playwright install" para todos os browsers
```

Opcional: copie `.env.example` para `.env` para customizar as URLs:

```bash
cp .env.example .env
```

| Variável              | Padrão                  | Descrição                                   |
|-----------------------|--------------------------|----------------------------------------------|
| `FRONTEND_URL`        | http://localhost:5173    | URL do frontend usada pelos testes de UI     |
| `API_URL`             | http://localhost:5064    | URL base da API usada pelos testes de API    |
| `WEB_SERVER_AUTOSTART`| true                      | Se `false`, não inicia o Vite automaticamente|

## Executando os testes

```bash
npm test           # UI + API
npm run test:ui     # apenas os testes de UI (projeto "web")
npm run test:api    # apenas os testes de API (projeto "api")
npm run test:headed # UI com navegador visível
npm run test:debug  # modo debug (Playwright Inspector)
npm run report      # abre o último relatório HTML
```

Para gerar testes gravando ações no navegador:

```bash
npm run codegen
```

## Notas

- Os testes de API criam atletas com e-mails únicos (`utils/auth.ts`) para não
  colidir com a constraint UNIQUE de e-mail no banco entre execuções.
- A tela de cadastro (`/cadastro`) ainda não está integrada ao backend (apenas
  exibe um `alert()`), então os testes de UI cobrem validação de formulário;
  a integração real de cadastro/login é coberta pelos testes de API.
