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
    public class EventController : ControllerBase
    {

        private readonly IUserDAO userDAO;
        private readonly IEventDAO eventDAO;

        public EventController(IUserDAO _userDAO, IEventDAO _eventDAO)
        {
            userDAO = _userDAO;
            eventDAO = _eventDAO;
        }

        [Authorize]
        [HttpPost("/create-event")]
        public ActionResult CreateEvent(Event newEvent)
        {
            
            if (newEvent != null)
            {
                eventDAO.addEvent(newEvent);
                return Ok(newEvent);
            }
            else
            {
                return BadRequest("Incomplete or missing event data");
            }
        }

        [HttpGet("/events")]
        public ActionResult<List<Event>> GetEvents()
        {
            List<Event> allEvents = eventDAO.getEvents();
            if (allEvents != null)
            {
                return Ok(allEvents);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
