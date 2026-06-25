-- liquibase formatted sql

-- changeset toswim:001-init
-- comment: Configuração inicial do banco toSwim

ALTER DATABASE toswim_dev SET timezone TO 'America/Sao_Paulo';

CREATE TABLE IF NOT EXISTS toswim_config (
    chave      VARCHAR(100) PRIMARY KEY,
    valor      VARCHAR(255) NOT NULL,
    created_at TIMESTAMPTZ  DEFAULT NOW()
);

INSERT INTO toswim_config (chave, valor)
VALUES ('versao', '1.0.0')
ON CONFLICT (chave) DO NOTHING;

-- rollback DROP TABLE IF EXISTS toswim_config;