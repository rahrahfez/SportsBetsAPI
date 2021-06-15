CREATE TABLE accounts (
	id UUID PRIMARY KEY DEFAULT,
	username VARCHAR(32) UNIQUE,
	available_balance INTEGER,
	created_at TIMESTAMP,
	updated_at TIMESTAMP,
	role INTEGER,
	password_hash VARCHAR(128)
);

CREATE TABLE wagers (
	id SERIAL PRIMARY KEY,
);

CREATE TABLE teams (
	id SERIAL PRIMARY KEY,
	name VARCHAR(64),
	sport VARCHAR(64)
);

CREATE TABLE win_conditions (
);