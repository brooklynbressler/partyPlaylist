USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	first_name varchar(50) NOT NULL,
	last_name varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
);

CREATE TABLE playlists (
	playlist_id int IDENTITY(1,1) NOT NULL,
	playlist_name varchar(50) NOT NULL,
	CONSTRAINT PK_playlist PRIMARY KEY (playlist_id)
);

CREATE TABLE events (
	event_id int IDENTITY(1,1) NOT NULL,
	dj_user_id int NOT NULL,
	host_user_id int,
	playlist_id int,
	event_name varchar(50) NOT NULL,
	description varchar(255) NOT NULL,
	event_date date,
	start_time time,
	end_time time,
	CONSTRAINT PK_event PRIMARY KEY (event_id),
	CONSTRAINT FK_event_dj FOREIGN KEY (dj_user_id) REFERENCES users(user_id)
);

CREATE TABLE songs (
	song_id int IDENTITY(1,1) NOT NULL,
	song_name varchar(255) NOT NULL,
	artist_name varchar(255) NOT NULL,
	genre varchar(50) NOT NULL,
	CONSTRAINT PK_song PRIMARY KEY (song_id)
);

CREATE TABLE playlist_songs (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	CONSTRAINT PK_playlist_song PRIMARY KEY (playlist_id, song_id),
	CONSTRAINT FK_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_song_id FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

CREATE TABLE playlist_songs_shoutouts (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	shoutout_id int IDENTITY(1,1) NOT NULL,
	shoutout_message varchar(255),
	CONSTRAINT PK_playlist_song_shoutouts PRIMARY KEY (playlist_id, song_id, shoutout_id),
	CONSTRAINT FK_playlist_id_shoutouts FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_song_id_shoutouts FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

/* All data above this line must match capstone.sql */

INSERT INTO users(username, first_name, last_name, password_hash, salt, user_role)
VALUES ('tester', 'Bob', 'Testerson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('tester2', 'Janet', 'Testperson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'host'),
('murphy', 'Zak', 'Murphy', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('bressler', 'Brooklyn', 'Bressler', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('morgan', 'David', 'Morgan', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('davis', 'Brian', 'Davis', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj');

INSERT INTO songs(song_name, artist_name, genre)
VALUES ('Friends In Low Places', 'Garth Brooks', 'Country'),
('The Thunder Rolls', 'Garth Brooks', 'Country'),
('The River', 'Garth Brooks', 'Country'),
('Boot Scootin Boogie', 'Brooks & Dunn', 'Country'),
('Neon Moon', 'Brooks & Dunn', 'Country'),
('My Maria', 'Brooks & Dunn', 'Country'),
('Drinkin Problem', 'Midland', 'Country'),
('Sixteen Gears', 'Midland', 'Country'),
('Thriller', 'Michael Jackson', 'Pop'),
('Billie Jean', 'Michael Jackson', 'Pop'),
('Beat It', 'Michael Jackson', 'Pop'),
('Man In The Mirror', 'Michael Jackson', 'Pop'),
('Smells Like Teen Spirit', 'Nirvana', 'Alternative'),
('Come As You Are', 'Nirvana', 'Alternative'),
('Lithium', 'Nirvana', 'Alternative'),
('I Want It That Way', 'Backstreet Boys', 'Pop'),
('Everybody', 'Backstreet Boys', 'Pop'),
('As Long As You Love Me', 'Backstreet Boys', 'Pop'),
('Wonderwall', 'Oasis', 'Alternative'),
('Champagne Supernova', 'Oasis', 'Alternative'),
('Dont Look Back In Anger', 'Oasis', 'Alternative'),
('My Heart Will Go On', 'Celine Dion', 'Pop'),
('All By Myself', 'Celine Dion', 'Pop'),
('Nothing Broken But My Heart', 'Celine Dion', 'Pop'),
('You Shook Me All Night Long', 'AC/DC', 'Rock'),
('Thunderstruck', 'AC/DC', 'Rock'),
('Highway To Hell', 'AC/DC', 'Rock'),
('Money', 'Pink Floyd', 'Rock'),
('Wish You Were Here', 'Pink Floyd', 'Rock'),
('Time', 'Pink Floyd', 'Rock'),
('Comfortably Numb', 'Pink Floyd', 'Rock'),
('Stairway To Heaven', 'Led Zeppelin', 'Rock'),
('Whole Lotta Love', 'Led Zeppelin', 'Rock'),
('Dazed And Confused', 'Led Zeppelin', 'Rock'),
('Kashmir', 'Led Zeppelin', 'Rock'),
('Ms. Jackson', 'Outkast', 'Hip Hop'),
('Hey Ya', 'Outkast', 'Hip Hop'),
('So Fresh, So Clean', 'Outkast', 'Hip Hop'),
('Baby Got Back', 'Sir Mix-a-Lot', 'Hip Hop'),
('It Was A Good Day', 'Ice Cube', 'Hip Hop'),
('X Gon Give It To Ya', 'DMX', 'Hip Hop'),
('Ruff Ryders Anthem', 'DMX', 'Hip Hop'),
('Where The Hood At', 'DMX', 'Hip Hop'),
('Dont Speak', 'No Doubt', 'Alternative'),
('Creep', 'Radiohead', 'Alternative'),
('Waterfalls', 'TLC', 'Pop'),
('No Scrubs', 'TLC', 'Pop'),
('Africa', 'Toto', 'Rock'),
('Material Girl', 'Madonna', 'Pop'),
('Whats Love Got To Do With It', 'Tina Turner', 'Pop');

INSERT INTO playlists (playlist_name)
VALUES ('Brooklyn'), ('Brian'), ('Zak');

INSERT INTO playlist_songs (playlist_id, song_id)
VALUES (3, 48),
(1, 9), (1, 10), (1, 11), (1, 12), (1, 13), (1, 14), (1, 15), (1, 16), (1, 17), (1, 18),
(2, 2), (2, 21), (2, 7), (2, 49), (2, 24), (2, 28), (2, 19), (2, 32), (2, 36), (2, 44);

INSERT INTO events (dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time)
VALUES (6, 4, 3, 'Party with Brooklyn', 'Aint no party like a Brooklyn party cause a Brooklyn party dont stop!', '2021-04-07', '17:00', '18:00'),
	   (6, 3, 3, 'Zak Shack', 'Zak loves Toto!!!!', '2021-04-08', '19:00', '20:00');

