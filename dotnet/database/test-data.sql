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

--Table of available genres for any event
CREATE TABLE genres (
	genre_id int IDENTITY(1,1) NOT NULL,
	genre_name varchar(20) NOT NULL,
	CONSTRAINT PK_genre PRIMARY KEY (genre_id)
);

--Table to hold event playlists
CREATE TABLE playlists (
	playlist_id int IDENTITY(1,1) NOT NULL,
	playlist_name varchar(50) NOT NULL,
	CONSTRAINT PK_playlist PRIMARY KEY (playlist_id)
);

CREATE TABLE events (
	event_id int IDENTITY(1,1) NOT NULL,
	dj_user_id int NOT NULL,
	host_user_id int,
	playlist_id int NOT NULL,
	event_name varchar(50) NOT NULL,
	description varchar(255) NOT NULL,
	event_date date,
	start_time time,
	end_time time,
	CONSTRAINT PK_event PRIMARY KEY (event_id),
	CONSTRAINT FK_event_dj FOREIGN KEY (dj_user_id) REFERENCES users(user_id),
	--CONSTRAINT FK_event_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id)
);

CREATE TABLE excluded_genres (
	event_id int NOT NULL,
	genre varchar(50) NOT NULL
	CONSTRAINT PK_excluded_genres PRIMARY KEY (event_id, genre)
);

CREATE TABLE songs (
	song_id int IDENTITY(1,1) NOT NULL,
	song_name varchar(255) NOT NULL,
	artist_name varchar(255) NOT NULL,
	genre varchar(50) NOT NULL,
	img_url varchar(255),
	CONSTRAINT PK_song PRIMARY KEY (song_id)
);

--Table of event playlist songs
CREATE TABLE playlist_songs (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	song_score int DEFAULT 0,
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

CREATE TABLE potential_playlist_songs (
	playlist_id int NOT NULL,
	song_id int NOT NULL,
	song_name varchar(255) NOT NULL,
	artist_name varchar(255) NOT NULL,
	genre varchar(50) NOT NULL,
	img_url varchar(255),
	song_score int DEFAULT 0,
	CONSTRAINT PK_potential_playlist_song PRIMARY KEY (playlist_id, song_id),
	CONSTRAINT FK_potential_playlist_id FOREIGN KEY (playlist_id) REFERENCES playlists(playlist_id),
	CONSTRAINT FK_potential_song_id FOREIGN KEY (song_id) REFERENCES songs(song_id)
);

/* All data above this line must match capstone.sql */

INSERT INTO users(username, first_name, last_name, password_hash, salt, user_role)
VALUES ('tester', 'Bob', 'Testerson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('tester2', 'Janet', 'Testperson', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'host'),
('murphy', 'Zak', 'Murphy', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('bressler', 'Brooklyn', 'Bressler', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('morgan', 'David', 'Morgan', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj'),
('davis', 'Brian', 'Davis', 'sPsfjtTKoQW18nwCVVXlFoxbGjI=', 'Gbup072H2/M=', 'dj');

SET IDENTITY_INSERT genres ON;

INSERT INTO genres(genre_id, genre_name)
VALUES
	(1,'Country'),
	(2, 'Pop'),
	(3, 'Rock'),
	(4, 'Metal'),
	(5, 'Alternative'),
	(6, 'Hip Hop');

SET IDENTITY_INSERT genres OFF;

INSERT INTO songs(song_name, artist_name, genre, img_url)
VALUES ('Friends In Low Places', 'Garth Brooks', 'Country', 'https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg'),
('The Thunder Rolls', 'Garth Brooks', 'Country','https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg'),
('The River', 'Garth Brooks', 'Country', 'https://upload.wikimedia.org/wikipedia/en/5/5a/Garth_Brooks-Ropin%27_the_Wind_%28album_cover%29.jpg'),
('Boot Scootin Boogie', 'Brooks & Dunn', 'Country', 'https://upload.wikimedia.org/wikipedia/en/3/33/BD-GreatestHitsCollection.jpg'),
('Neon Moon', 'Brooks & Dunn', 'Country', 'https://upload.wikimedia.org/wikipedia/en/3/33/BD-GreatestHitsCollection.jpg'),
('My Maria', 'Brooks & Dunn', 'Country', 'https://upload.wikimedia.org/wikipedia/en/3/33/BD-GreatestHitsCollection.jpg'),
('Drinkin Problem', 'Midland', 'Country', 'https://i.pinimg.com/originals/a6/14/56/a6145635a4298c7e2b339d3f8ca4f728.jpg'),
('Fourteen Gears', 'Midland', 'Country', 'https://i.pinimg.com/originals/a6/14/56/a6145635a4298c7e2b339d3f8ca4f728.jpg'),
('Thriller', 'Michael Jackson', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png'),
('Billie Jean', 'Michael Jackson', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png'),
('Beat It', 'Michael Jackson', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png'),
('Man In The Mirror', 'Michael Jackson', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/55/Michael_Jackson_-_Thriller.png'),
('Smells Like Teen Spirit', 'Nirvana', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b7/NirvanaNevermindalbumcover.jpg'),
('Come As You Are', 'Nirvana', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b7/NirvanaNevermindalbumcover.jpg'),
('Lithium', 'Nirvana', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b7/NirvanaNevermindalbumcover.jpg'),
('I Want It That Way', 'Backstreet Boys', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/c/c2/Albumus.jpg'),
('Everybody', 'Backstreet Boys', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/c/c2/Albumus.jpg'),
('As Long As You Love Me', 'Backstreet Boys', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/c/c2/Albumus.jpg'),
('Wonderwall', 'Oasis', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b1/Oasis_-_%28What%27s_The_Story%29_Morning_Glory_album_cover.jpg'),
('Champagne Supernova', 'Oasis', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b1/Oasis_-_%28What%27s_The_Story%29_Morning_Glory_album_cover.jpg'),
('Dont Look Back In Anger', 'Oasis', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/b/b1/Oasis_-_%28What%27s_The_Story%29_Morning_Glory_album_cover.jpg'),
('My Heart Will Go On', 'Celine Dion', 'Pop', 'https://i.pinimg.com/236x/6f/39/5a/6f395a560665bea1cc744e85f8e825a4--music-heart-love-you-more.jpg'),
('All By Myself', 'Celine Dion', 'Pop', 'https://i.pinimg.com/236x/6f/39/5a/6f395a560665bea1cc744e85f8e825a4--music-heart-love-you-more.jpg'),
('Nothing Broken But My Heart', 'Celine Dion', 'Pop', 'https://i.pinimg.com/236x/6f/39/5a/6f395a560665bea1cc744e85f8e825a4--music-heart-love-you-more.jpg'),
('You Shook Me All Night Long', 'AC/DC', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/a/ac/Acdc_Highway_to_Hell.JPG'),
('Thunderstruck', 'AC/DC', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/a/ac/Acdc_Highway_to_Hell.JPG'),
('Highway To Hell', 'AC/DC', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/a/ac/Acdc_Highway_to_Hell.JPG'),
('Money', 'Pink Floyd', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
('Wish You Were Here', 'Pink Floyd', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
('Time', 'Pink Floyd', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
('Comfortably Numb', 'Pink Floyd', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
('Stairway To Heaven', 'Led Zeppelin', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/e/ef/Led_Zeppelin_-_Led_Zeppelin_%281969%29_front_cover.png'),
('Whole Lotta Love', 'Led Zeppelin', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/e/ef/Led_Zeppelin_-_Led_Zeppelin_%281969%29_front_cover.png'),
('Dazed And Confused', 'Led Zeppelin', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/e/ef/Led_Zeppelin_-_Led_Zeppelin_%281969%29_front_cover.png'),
('Kashmir', 'Led Zeppelin', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/e/ef/Led_Zeppelin_-_Led_Zeppelin_%281969%29_front_cover.png'),
('Ms. Jackson', 'Outkast', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/0/0b/OutKast_-_Stankonia.JPG'),
('Hey Ya', 'Outkast', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/0/0b/OutKast_-_Stankonia.JPG'),
('So Fresh, So Clean', 'Outkast', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/0/0b/OutKast_-_Stankonia.JPG'),
('Baby Got Back', 'Sir Mix-a-Lot', 'Hip Hop', 'https://img.discogs.com/lLbLKfPQOLEnpBZfRx-sAQWDSqQ=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-388252-1143900857.jpeg.jpg'),
('It Was A Good Day', 'Ice Cube', 'Hip Hop', 'https://img.discogs.com/84GiZIsueXirBySrofKfVHvHuSg=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-349388-1489891729-6218.jpeg.jpg'),
('X Gon Give It To Ya', 'DMX', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/a/ad/ItsdarkDMX.jpg'),
('Ruff Ryders Anthem', 'DMX', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/a/ad/ItsdarkDMX.jpg'),
('Where The Hood At', 'DMX', 'Hip Hop', 'https://upload.wikimedia.org/wikipedia/en/a/ad/ItsdarkDMX.jpg'),
('Dont Speak', 'No Doubt', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/9/9d/No_Doubt_-_Tragic_Kingdom.png'),
('Creep', 'Radiohead', 'Alternative', 'https://upload.wikimedia.org/wikipedia/en/0/0f/Radiohead.pablohoney.albumart.jpg'),
('Waterfalls', 'TLC', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/5b/TLC_-_CrazySexyCool_album_cover.png'),
('No Scrubs', 'TLC', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/5/5b/TLC_-_CrazySexyCool_album_cover.png'),
('Africa', 'Toto', 'Rock', 'https://upload.wikimedia.org/wikipedia/en/b/bd/Toto_Toto_IV.jpg'),
('Material Girl', 'Madonna', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/7/75/Celebration_cover.png'),
('Whats Love Got To Do With It', 'Tina Turner', 'Pop', 'https://upload.wikimedia.org/wikipedia/en/4/42/Tina_Turner_Private_Dancer_US_CD_cover_art_1984_original.jpg');

INSERT INTO playlists (playlist_name)
VALUES ('Party with Brooklyn'), ('Zak Shack');

INSERT INTO events (dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time)
VALUES (6, 4, 1, 'Party with Brooklyn', 'Aint no party like a Brooklyn party cause a Brooklyn party dont stop!', '2021-04-07', '17:00', '18:00'),
	   (6, 3, 2, 'Zak Shack', 'Zak loves Toto!!!!', '2021-04-08', '19:00', '20:00');

INSERT INTO playlist_songs (playlist_id, song_id, song_score)
VALUES (1, 9, 1), (1, 10, 2), (1, 11, 0), (1, 12, 3), (1, 13, 2), (1, 14, 4), (1, 15, 5), (1, 16, 2), (1, 17, 0), (1, 18, 0),
(2, 48, 10);

INSERT INTO excluded_genres (event_id, genre)
VALUES (2, 'Country'), (2, 'Alternative');