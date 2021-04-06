using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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

                    string sql = "INSERT INTO events(dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time) VALUES (@dj_user_id, @host_user_id, @playlist_id, @event_name, @description, @event_date, @start_time, @end_time);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dj_user_id", newEvent.DjUserId);
                    cmd.Parameters.AddWithValue("@host_user_id", newEvent.HostUserId);
                    cmd.Parameters.AddWithValue("@playlist_id", newEvent.PlaylistId);
                    cmd.Parameters.AddWithValue("@event_name", newEvent.EventName);
                    cmd.Parameters.AddWithValue("@description", newEvent.EventDescription);
                    cmd.Parameters.AddWithValue("@event_date", newEvent.EventDate);
                    cmd.Parameters.AddWithValue("@start_time", newEvent.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", newEvent.EndTime);

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

                    string sql = "SELECT event_id, dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time, dj.first_name AS djfirstname, dj.last_name AS djlastname, host.first_name AS hostfirstname, host.last_name AS hostlastname FROM events JOIN users dj ON dj_user_id = dj.user_id JOIN users host ON host_user_id = host.user_id;";

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
                EventDescription = Convert.ToString(reader["description"])
            };
            // Dealing with dates and times
            string tempStart = Convert.ToString(reader["start_time"]);
            TimeSpan tempStartTime = TimeSpan.Parse(tempStart);
            e.StartTime = timeConverter(tempStartTime);
            string tempEnd = Convert.ToString(reader["end_time"]);
            TimeSpan tempEndTime = TimeSpan.Parse(tempEnd);
            e.EndTime = timeConverter(tempEndTime);
            string tempDate = Convert.ToString(reader["event_date"]);
            string[] temp = tempDate.Split(' ');
            e.EventDate = temp[0];

            // Dj and host names
            string djFirst = Convert.ToString(reader["djfirstname"]);
            string djLast = Convert.ToString(reader["djlastname"]).Substring(0, 1);
            e.DjName = $"{djFirst} {djLast}";
            string hostFirst = Convert.ToString(reader["hostfirstname"]);
            string hostLast = Convert.ToString(reader["hostlastname"]).Substring(0, 1);
            e.HostName = $"{hostFirst} {hostLast}";

            return e;
        }
        private string timeConverter(TimeSpan time)
        {
            int hours = 0;
            string tT = "AM";
            if (time.Hours > 12)
            {
                hours = time.Hours - 12;
                tT = "PM";
            }
            else
            {
                hours = time.Hours;
            }
            return $"{hours}:{time.Minutes} {tT}";
        }

    }
}
