using CalypsoAPI.Interface;
using CalypsoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace CalypsoAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private ICalypso _calypso;

        public MeasurementsController(ICalypso calypso)
        {
            _calypso = calypso;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public ActionResult<List<Measurement>> Get()
        {
            if (_calypso == null)
                return StatusCode(503, "Calypso api not initialized.");

            if (!_calypso.IsRunning)
                return StatusCode(503, "Calypso api is not running.");

            return Ok(_calypso.State.LatestMeasurementResults.Measurements);
        }

        [HttpGet("table")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public ActionResult<DataView> GetTable()
        {
            if (_calypso == null)
                return StatusCode(503, "Calypso api not initialized.");

            if (!_calypso.IsRunning)
                return StatusCode(503, "Calypso api is not running.");

            return Ok(_calypso.State.LatestMeasurementResults.ChrTable.AsDataView());
        }
    }
}
