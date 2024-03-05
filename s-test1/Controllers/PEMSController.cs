using Microsoft.AspNetCore.Mvc;
using PEMS.Contracts;
using PEMS.Models;

namespace PEMS.Controllers
{
    [ApiController]
    [Route("api/pems")]
    public class PEMSController : ControllerBase
    {
        private readonly ILogger<PEMSController> _logger;

        private IPEMSProvider PEMSProvider;

        public PEMSController(ILogger<PEMSController> logger, IPEMSProvider pemsService)
        {
            _logger = logger;
            this.PEMSProvider = pemsService;
        }

        [Route("candbconnect")]
        [HttpGet]
        public bool CanDBConnect()
        {
            return this.PEMSProvider.CanDBConnected();
        }

        [HttpGet]
        public List<PEMSystem> Get()
        {
            return this.PEMSProvider.GetAll();
        }

        [HttpPost]
        public bool Save(PEMSystem item)
        {
            return this.PEMSProvider.Save(item);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return this.PEMSProvider.Delete(id);
        }
    }
}