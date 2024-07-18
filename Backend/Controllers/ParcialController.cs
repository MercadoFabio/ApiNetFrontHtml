using Microsoft.AspNetCore.Mvc;
using Parcial.Dtos;
using Parcial.Servicios.Interfaces;

namespace Parcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcialController : Controller
    {
        private readonly IParcialService _parcialService;

        public ParcialController(IParcialService parcialService)
        {
            _parcialService = parcialService;
        }

        [HttpGet("GetObras")]
        public async Task<IActionResult> GetObras()
        {
            var response = await _parcialService.GetObrasAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAlbaniles")]
        public async Task<IActionResult> GetAlbaniles([FromQuery] Guid obraId)
        {
            var response = await _parcialService.GetAlbanilesNotObraAsync(obraId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("PostAlbanilXObra")]
        public async Task<IActionResult> PostAlbanilXObra([FromBody] AlbanilXObraDto albanilXObraDto)
        {
            var response = await _parcialService.PostAlbanilXObraAsync(albanilXObraDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("PostAlbanil")]
        public async Task<IActionResult> PostAlbanil([FromBody] AlbanilDto albanilDto)
        {
            var response = await _parcialService.PostAlbanilAsync(albanilDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
