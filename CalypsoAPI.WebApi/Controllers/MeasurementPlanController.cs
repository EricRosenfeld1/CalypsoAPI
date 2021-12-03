using CalypsoAPI.Core.Interface;
using CalypsoAPI.Core.Models.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalypsoAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeasurementPlanController : ControllerBase
    {
        private ICalypso _calypso;

        public MeasurementPlanController(ICalypso calypso)
        {
            _calypso = calypso;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public ActionResult<MeasurementPlanInfo> Get()
        {
            if (_calypso == null)
                return StatusCode(503, "Calypso api not initialized.");

            if (!_calypso.IsRunning)
                return StatusCode(503, "Calypso api is not running.");

            return Ok(_calypso.State.MeasurementPlan);
        }
    }
}
