using System;

namespace Capstone.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int DjUserId { get; set; }
        public int HostUserId { get; set; }
        public int PlaylistId { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}