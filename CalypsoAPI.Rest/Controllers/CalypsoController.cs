using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalypsoAPI.Core;
using CalypsoAPI.Core.Models.State;

namespace CalypsoAPI.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalypsoController : ControllerBase
    {
        private Calypso _calypso;

        public CalypsoController(Calypso calypso)
        {
            _calypso = calypso;
        }

        [HttpGet("measurements")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public ActionResult<List<Measurement>> GetMeasurements()
        {
            if (_calypso == null)
                return StatusCode(503, "Calypso api not initialized.");

            if (!_calypso.IsRunning)
                return StatusCode(503, "Calypso api is not running.");

            return Ok(_calypso.State.LatestMeasurementResults.Measurements);
        }

    }
}
