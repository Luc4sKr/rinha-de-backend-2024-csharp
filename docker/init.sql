DROP TABLE IF EXISTS saldos;
CREATE TABLE saldos(
	id SERIAL PRIMARY KEY,
	user_id INTEGER NOT NULL,
	total INTEGER NOT NULL,
	limite INTEGER NOT NULL
);

DROP TABLE IF EXISTS tansactions;
CREATE TABLE transactions(
	id SERIAL PRIMARY KEY,
	user_id INTEGER NOT NULL,
	valor INTEGER NOT NULL,
	tipo CHAR(1) NOT NULL,
	descricao VARCHAR(10) NOT NULL,
	realizada_em TIMESTAMP NOT NULL
);

CREATE INDEX ids_transactions_ids_user_id ON transactions (user_id);
CREATE INDEX ids_saldos_ids_user_id ON saldos (user_id);

DO $$
BEGIN
	INSERT INTO saldos (user_id, limite, total)
	VALUES (1,   1000 * 100, 0),
	     	(2,    800 * 100, 0),
	   	(3,  10000 * 100, 0),
	   	(4, 100000 * 100, 0),
	   	(5,   5000 * 100, 0);
END;
$$;
