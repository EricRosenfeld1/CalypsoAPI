using CalypsoAPI.Core;
using CalypsoAPI.Core.Models.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalypsoAPI.Rest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private ICalypso _calypso;
        public StateController(ICalypso calypso)
        {
            _calypso = calypso;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public ActionResult<Status> Get()
        {
            if (_calypso == null)
                return StatusCode(503, "Calypso api not initialized.");

            if (!_calypso.IsRunning)
                return StatusCode(503, "Calypso api is not running.");

            return Ok(Enum.GetName(typeof(Status), _calypso.State.Status));
        }
    }
}
