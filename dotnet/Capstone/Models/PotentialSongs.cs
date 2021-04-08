using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PotentialSongs
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int SongScore { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public bool HasUpVoted { get; set; } = false;
        public bool HasDownVoted { get; set; } = false;
    }
}
