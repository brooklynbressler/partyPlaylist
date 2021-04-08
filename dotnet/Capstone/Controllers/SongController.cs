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

        [HttpPost("/up-vote")]
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
    }
}
