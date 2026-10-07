using CalculoCDBApp.Application.Interface;
using CalculoCDBApp.Application.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculoCDBApp.Api.Controllers
{
    [Route("api/cdb")]
    [ApiController]
    public class CdbController : ControllerBase
    {
        private readonly ICdbService _cdbService;

        public CdbController(ICdbService cdbService)
        {
            _cdbService = cdbService;
        }

        [HttpPost("calcular")]
        public IActionResult Calcular([FromBody] CalcularCdbRequest request)
        {
            var resultado = _cdbService.Calcular(request);

            return Ok(resultado);
        }
    }
}
