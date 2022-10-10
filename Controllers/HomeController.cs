using Microsoft.AspNetCore.Mvc;

namespace AgendamentoEventos.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("health-check")]
        public IActionResult Get()
        {
            return Ok(new
            {
                teste = "Testando"
            });
        }
    }
}
