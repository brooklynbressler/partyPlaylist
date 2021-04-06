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
	host_user_id int NOT NULL,
	playlist_id int NOT NULL,
	event_name varchar(50) NOT NULL,
	description varchar(255) NOT NULL,
	event_date date,
	start_time time,
	end_time time,
	CONSTRAINT PK_event PRIMARY KEY (event_id),
	CONSTRAINT FK_event_dj FOREIGN KEY (dj_user_id) REFERENCES users(user_id),
	CONSTRAINT FK_event_host FOREIGN KEY (host_user_id) REFERENCES users(user_id),
	CONSTRAINT FK_event_playlist FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id)
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

/* All data above this line must match capstone.sql */

INSERT INTO users(username, first_name, last_name, password_hash, salt, user_role)
VALUES ('tester', 'Bob', 'Testerson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('tester2', 'Janet', 'Testperson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'host')