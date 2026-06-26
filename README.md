# toSwim

Aplicação para registro, acompanhamento e análise de treinos de natação.

## Tecnologias

| Camada | Tecnologia |
|---------|------------|
| Backend | C# / .NET 8 (fase futura) |
| Frontend | Vue 3 / TypeScript (fase futura) |
| Banco de dados | PostgreSQL 15 |
| Migrations | Liquibase 4.27 |
| Infraestrutura | Docker / Docker Compose |

---

## Pré-requisitos

Antes de iniciar, certifique-se de possuir:

- Docker Desktop instalado
- Docker Compose habilitado

---

## Configuração do ambiente

### 1. Clone o repositório

```bash
git clone https://dev.azure.com/MentoriaQA/toSwim/_git/toSwim-api
cd toSwim-api
```

### 2. Configure as variáveis de ambiente

```bash
cp .env.example .env
```

Edite o arquivo `.env` e informe o valor da variável:

```text
POSTGRES_PASSWORD=sua_senha
```

### 3. Inicie o banco de dados

```bash
docker compose up -d postgres
```

### 4. Execute as migrations

```bash
docker compose run --rm liquibase
```

### 5. Verifique se as tabelas foram criadas

```bash
docker exec -it toswim_postgres psql \
-U toswim_user \
-d toswim_dev \
-c "\dt"
```

---

## Comandos úteis

| Comando | Descrição |
|----------|-----------|
| `docker compose up -d postgres` | Inicia o banco de dados |
| `docker compose run --rm liquibase` | Executa as migrations |
| `docker compose ps` | Lista os containers em execução |
| `docker compose down` | Encerra os containers |
| `docker compose down -v` | Remove os containers e o volume do banco |

---

## Estrutura do projeto

```
toswim/
├── database/
│   └── changelog/
│       ├── db.changelog-master.yaml
│       └── migrations/
│           └── 001-init.sql
├── docs/
├── .env.example
├── .gitignore
├── docker-compose.yml
└── README.md
```

---

## Estratégia de branches

| Branch | Finalidade |
|---------|------------|
| `main` | Código estável |
| `develop` | Integração das funcionalidades |
| `feature/*` | Desenvolvimento de novas funcionalidades |
| `fix/*` | Correções de bugs |