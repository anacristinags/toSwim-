-- liquibase formatted sql

-- changeset toswim:012-add-tamanho-piscina-ficha-base
-- comment: Adiciona tamanho de piscina (25 ou 50) na ficha base
ALTER TABLE ficha_base
    ADD COLUMN tamanho_piscina_m SMALLINT NOT NULL DEFAULT 25;

ALTER TABLE ficha_base
    ADD CONSTRAINT chk_ficha_base_tamanho_piscina CHECK (tamanho_piscina_m IN (25, 50));
-- rollback ALTER TABLE ficha_base DROP CONSTRAINT chk_ficha_base_tamanho_piscina;
-- rollback ALTER TABLE ficha_base DROP COLUMN tamanho_piscina_m;
