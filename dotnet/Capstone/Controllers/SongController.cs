using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {

        private readonly IUserDAO userDAO;
        private readonly IEventDAO eventDAO;
        private readonly ISongDAO songDAO;

        public SongController(IUserDAO _userDAO, IEventDAO _eventDAO, ISongDAO _songDAO)
        {
            userDAO = _userDAO;
            eventDAO = _eventDAO;
            songDAO = _songDAO;
        }

        [HttpPost("/vote")]
        public ActionResult<bool> SongVoteUpdate(SongVote songVote)
        {

            if (songVote != null)
            {
                return Ok(songDAO.AddSongVote(songVote));
            }
            else
            {
                return BadRequest("Incomplete or missing vote data");
            }
        }

        [HttpGet("/possible-songs")]
        public ActionResult<List<Song>> GetPossibleSongs(List<string> excludedGenres)
        {
            List<Song> allPossibleSongs = songDAO.GetAllPossibleSongs(excludedGenres);
            if (allPossibleSongs != null)
            {
                return Ok(allPossibleSongs);
            }
            else
            {
                return BadRequest("This did not work because you are a loser");
            }
        }

        [HttpGet("/playlist/{playlistId}")]
        public ActionResult<List<PlaylistSong>> GetPlaylist(int playlistId)
        {
            List<PlaylistSong> playlist = songDAO.GetPlaylistSongs(playlistId);
            if (playlist != null)
            {
                return Ok(playlist);
            }
            else
            {
                return BadRequest("Playlist not found");
            }
        }

        [HttpPost("/add-new-song")]
        public ActionResult AddNewSongToDb(Song newSong)
        {
            if (newSong != null)
            {
                bool wasCreated = songDAO.AddNewSong(newSong);
                if (wasCreated)
                {
                    return Created("Song added", newSong);
                }
                else
                {
                    return Conflict("Song not added due to a server error");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/add-to-playlist")]
        public ActionResult AddToPlaylist(AddRemoveSong addSong)
        {
            bool songAdded = songDAO.AddSongToPlaylist(addSong.PlaylistId, addSong.SongId);
            if (songAdded)
            {
                return Created("Song added to playlist", addSong);
            }
            else
            {
                return BadRequest("Unable to add song to playlist");
            }
        }

        [HttpDelete("/remove-from-playlist")]
        public ActionResult RemoveFromPlaylist(AddRemoveSong removeSong)
        {
            bool songRemoved = songDAO.RemoveSongFromPlaylist(removeSong.PlaylistId, removeSong.SongId);
            if (songRemoved)
            {
                return Ok("Song removed from playlist");
            }
            else
            {
                return BadRequest("Unable to remove song from playlist");
            }
        }
    }
}
