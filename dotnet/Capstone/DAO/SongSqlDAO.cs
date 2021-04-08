using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;



namespace Capstone.DAO
{
    public class SongSqlDAO : ISongDAO
    {
        private readonly string connectionString;

        public SongSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool AddSongVote(SongVote songVote)
        {
            int currentSongVoteValue = 0;
            bool transactionCompleted = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // sql statements
                    string sql = "SELECT song_score FROM playlist_songs WHERE playlist_id = @playlistId AND song_id = @songId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlistId", songVote.PlaylistId);
                    cmd.Parameters.AddWithValue("@songId", songVote.SongId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        currentSongVoteValue = Convert.ToInt32(reader["song_score"]);
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            currentSongVoteValue += songVote.VoteValue;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "UPDATE playlist_songs SET song_score = @songScore WHERE playlist_id = @playlistId AND song_id = @songId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@songScore", currentSongVoteValue);
                    cmd.Parameters.AddWithValue("@playlistId", songVote.PlaylistId);
                    cmd.Parameters.AddWithValue("@songId", songVote.SongId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transactionCompleted = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return transactionCompleted;
        }
        public bool AddNewSong(Song newSong)
        {
            bool wasCreated = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO songs(song_name, artist_name, genre) VALUES (@song_name, @artist_name, @genre);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@song_name", newSong.SongName);
                    cmd.Parameters.AddWithValue("@artist_name", newSong.ArtistName);
                    cmd.Parameters.AddWithValue("@genre", newSong.Genre);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        wasCreated = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return wasCreated;
        }
        public bool AddSongToPlaylist(int playlistId, int songId)
        {
            bool wasAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO playlist_songs(playlist_id, song_id) VALUES (@playlist_id, @song_id);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);
                    cmd.Parameters.AddWithValue("@song_id", songId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        wasAdded = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return wasAdded;

        }
        public bool RemoveSongFromPlaylist(int playlistId, int songId)
        {
            bool songRemoved = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM playlist_songs WHERE playlist_id = @playlist_id AND song_id = @song_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);
                    cmd.Parameters.AddWithValue("@song_id", songId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        songRemoved = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return songRemoved;
        }
        public List<PlaylistSong> GetPlaylistSongs(int playlistId)
        {
            List<PlaylistSong> playlist = new List<PlaylistSong>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT ps.playlist_id, ps.song_id, ps.song_score, s.song_name, s.artist_name, s.genre FROM playlist_songs ps JOIN songs s ON s.song_id = ps.song_id WHERE ps.playlist_id = @playlist_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        playlist.Add(GetPlaylistSongFromReader(reader));
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }

            return playlist;
        }
        public List<Song> GetAllPossibleSongs(List<string> excludedGenres)
        {
            List<Song> allPossibleSongs = new List<Song>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT song_id, song_name, artist_name, genre FROM songs;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Song s = (GetSongFromReader(reader));
                        if (excludedGenres.Contains(s.Genre))
                        {
                            continue;
                        }
                        else
                        {
                            allPossibleSongs.Add(s);
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }
            return allPossibleSongs;
        }
        private PlaylistSong GetPlaylistSongFromReader(SqlDataReader reader)
        {
            PlaylistSong pls = new PlaylistSong()
            {
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                SongId = Convert.ToInt32(reader["song_id"]),
                SongScore = Convert.ToInt32(reader["song_score"]),
                SongName = Convert.ToString(reader["song_name"]),
                ArtistName = Convert.ToString(reader["artist_name"]),
                Genre = Convert.ToString(reader["genre"])
            };
            return pls;
        }
        private Song GetSongFromReader(SqlDataReader reader)
        {
            Song s = new Song()
            {
                SongId = Convert.ToInt32(reader["song_id"]),
                SongName = Convert.ToString(reader["song_name"]),
                ArtistName = Convert.ToString(reader["artist_name"]),
                Genre = Convert.ToString(reader["genre"])
            };
            return s;
        }
    }
}
