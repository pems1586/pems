using Microsoft.AspNetCore.Mvc;

namespace s_test1.Controllers
{
    [ApiController]
    [Route("api/pems")]
    public class PEMSController : ControllerBase
    {
        private readonly ILogger<PEMSController> _logger;

        public PEMSController(ILogger<PEMSController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<PEMS> Get()
        {
            return new List<PEMS>();
        }
    }
}