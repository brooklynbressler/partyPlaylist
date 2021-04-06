using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class EventSqlDAO : IEventDAO
    {
        private readonly string connectionString;

        public EventSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void addEvent(Event newEvent)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO events(dj_user_id, host_user_id, playlist_id, event_name, event_date, start_time, end_time) VALUES (@dj_user_id, @host_user_id, @playlist_id, @event_name, @event_date, @start_time, @end_time);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dj_user_id", newEvent.DjUserId);
                    cmd.Parameters.AddWithValue("@host_user_id", newEvent.HostUserId);
                    cmd.Parameters.AddWithValue("@playlist_id", newEvent.PlaylistId);
                    cmd.Parameters.AddWithValue("@event_name", newEvent.EventName);
                    if (newEvent.EventDate != null)
                    {
                        cmd.Parameters.AddWithValue("@event_date", newEvent.EventDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@event_date", "");
                    }
                    if (newEvent.StartTime != null)
                    {
                        cmd.Parameters.AddWithValue("@start_time", newEvent.StartTime);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@start_time", "");
                    }
                    if (newEvent.EndTime != null)
                    {
                        cmd.Parameters.AddWithValue("@end_time", newEvent.EndTime);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@end_time", "");
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Event> getEvents()
        {
            List<Event> allEvents = new List<Event>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    string sql = "SELECT event_id, dj_user_id, host_user_id, playlist_id, event_name, event_date, start_time, end_time FROM events;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allEvents.Add(GetEventFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return allEvents;
        }

        private Event GetEventFromReader(SqlDataReader reader)
        {
            Event e = new Event()
            {
                EventId = Convert.ToInt32(reader["event_id"]),
                DjUserId = Convert.ToInt32(reader["dj_user_id"]),
                HostUserId = Convert.ToInt32(reader["host_user_id"]),
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                EventName = Convert.ToString(reader["event_name"]),
                EventDate = Convert.ToString(reader["event_date"]),
                StartTime = Convert.ToString(reader["start_time"]),
                EndTime = Convert.ToString(reader["end_time"])
                
            };

            return e;
        }

    }
}
