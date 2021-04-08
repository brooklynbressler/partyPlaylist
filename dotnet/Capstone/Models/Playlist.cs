using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Playlist
    {
        // PlaylistId  playlist_id
        // SongId  song_id
        // SongScore  song_score
        // SongName  song_name
        // ArtistName  artist_name
        // Genre  genre
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int SongScore { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }


    }
}
