CREATE DATABASE rinha;

USE rinha;

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
