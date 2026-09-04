-- liquibase formatted sql

-- changeset toswim:011-add-ignorar-no-pace-serie-ficha
-- comment: Adiciona flag para ignorar a serie no calculo do pace medio (serie_ficha)
ALTER TABLE serie_ficha
    ADD COLUMN ignorar_no_pace BOOLEAN NOT NULL DEFAULT FALSE;
-- rollback ALTER TABLE serie_ficha DROP COLUMN ignorar_no_pace;

-- changeset toswim:011-add-ignorar-no-pace-serie-treino
-- comment: Adiciona flag para ignorar a serie no calculo do pace medio (serie_treino), snapshot copiado da ficha ao iniciar o treino
ALTER TABLE serie_treino
    ADD COLUMN ignorar_no_pace BOOLEAN NOT NULL DEFAULT FALSE;
-- rollback ALTER TABLE serie_treino DROP COLUMN ignorar_no_pace;
