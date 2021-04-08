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
    }
}
