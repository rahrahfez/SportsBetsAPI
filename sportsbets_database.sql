CREATE TABLE accounts (
	id UUID PRIMARY KEY,
	username VARCHAR(32) UNIQUE,
	available_balance INTEGER,
	created_at TIMESTAMP,
	updated_at TIMESTAMP,
	last_login TIMESTAMP,
	password_hash VARCHAR(128)
);

CREATE TABLE wagers (
	id SERIAL PRIMARY KEY
);

CREATE TABLE teams (
	id SERIAL PRIMARY KEY,
	name VARCHAR(64),
	sport VARCHAR(64)
);

INSERT INTO teams (name, sport) VALUES 
	("Boston Celtics", "NBA"),
	("Brooklyn Nets", "NBA"),
	("New York Knicks", "NBA"),
	("Philadelphia 76ers", "NBA"),
	("Toronto Raptors", "NBA"),
	("Denver Nuggets", "NBA"),
	("Minnesota Timberwolves", "NBA"),
	("Oklahoma City Thunder", "NBA"),
	("Portland Trail Blazers", "NBA"),
	("Utah Jazz", "NBA"),
	("Chicago Bulls", "NBA"),
	("Cleveland Cavaliers", "NBA"),
	("Detroit Pistons", "NBA"),
	("Indiana Pacers", "NBA"),
	("Milwaukee Bucks", "NBA"),
	("Golden State Warriors", "NBA"),
	("LA Clippers", "NBA"),
	("Los Angeles Lakers", "NBA"),
	("Phoenix Suns", "NBA"),
	("Sacramento Kings", "NBA"),
	("Atlanta Hawks", "NBA"),
	("Charlotte Hornets", "NBA"),
	("Miami Heat", "NBA"),
	("Orlando Magic", "NBA"),
	("Washington Wizards", "NBA"),
	("Dallas Mavericks", "NBA"),
	("Houston Rockets", "NBA"),
	("Memphis Grizzlies", "NBA"),
	("New Orleans Pelicans", "NBA"),
	("San Antonio Spurs", "NBA");

INSERT INTO sports (id, name) VALUES 

CREATE TABLE win_conditions (
);

CREATE TABLE roles (
	id SERIAL,
	title VARCHAR(24)
);