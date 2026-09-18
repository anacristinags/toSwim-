-- liquibase formatted sql

-- changeset toswim:013-add-tipo-nado-livre-serie-ficha
-- comment: Amplia o CHECK de tipo_nado para incluir o novo tipo Livre (4) em serie_ficha
ALTER TABLE serie_ficha DROP CONSTRAINT chk_serie_ficha_tipo_nado;
ALTER TABLE serie_ficha ADD CONSTRAINT chk_serie_ficha_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3, 4));
-- rollback ALTER TABLE serie_ficha DROP CONSTRAINT chk_serie_ficha_tipo_nado;
-- rollback ALTER TABLE serie_ficha ADD CONSTRAINT chk_serie_ficha_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3));

-- changeset toswim:013-add-tipo-nado-livre-serie-treino
-- comment: Amplia o CHECK de tipo_nado para incluir o novo tipo Livre (4) em serie_treino
ALTER TABLE serie_treino DROP CONSTRAINT chk_serie_treino_tipo_nado;
ALTER TABLE serie_treino ADD CONSTRAINT chk_serie_treino_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3, 4));
-- rollback ALTER TABLE serie_treino DROP CONSTRAINT chk_serie_treino_tipo_nado;
-- rollback ALTER TABLE serie_treino ADD CONSTRAINT chk_serie_treino_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3));

-- changeset toswim:013-add-tipo-nado-livre-meta
-- comment: Amplia o CHECK de tipo_nado para incluir o novo tipo Livre (4) em meta
ALTER TABLE meta DROP CONSTRAINT chk_meta_tipo_nado;
ALTER TABLE meta ADD CONSTRAINT chk_meta_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3, 4));
-- rollback ALTER TABLE meta DROP CONSTRAINT chk_meta_tipo_nado;
-- rollback ALTER TABLE meta ADD CONSTRAINT chk_meta_tipo_nado CHECK (tipo_nado IN (0, 1, 2, 3));
