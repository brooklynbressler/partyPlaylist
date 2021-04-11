using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ISongDAO
    {
        bool AddSongVote(SongVote songVote);
        bool AddNewSong(Song newSong);
        bool AddSongToPlaylist(int playlistId, int songId);
        bool RemoveSongFromPlaylist(int playlistId, int songId);
        List<PlaylistSong> GetPlaylistSongs(int playlistId);
        List<Song> GetAllPossibleSongs(int eventId);
        bool AddSongShoutOut(SongShoutOut songWithShoutOut);
    }
}
