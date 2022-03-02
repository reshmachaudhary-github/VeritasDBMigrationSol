using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SQLDatabaseController : ControllerBase
    {
        private readonly ILogger<SQLDatabaseController> _logger;

        public SQLDatabaseController(ILogger<SQLDatabaseController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetData")]
        public string Get()
        {
            return "This is data from SQL DB";
        }
    }
}