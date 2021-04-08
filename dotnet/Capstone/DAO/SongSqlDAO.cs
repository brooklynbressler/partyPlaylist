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
