using AD.Template.Dotnet.API.V1_0.Models;
using Microsoft.AspNetCore.Mvc;

namespace AD.Template.Dotnet.API.V1_0.Controllers
{
    public class SampleController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(SampleController));

        [Route("Ping")]
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(500)]
        public IActionResult Ping()
        {
            log.Info(System.DateTime.Now.ToString());
            return Ok(new Response(true, "Pinged"));
        }
    }
}