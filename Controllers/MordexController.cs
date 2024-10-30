using Microsoft.AspNetCore.Mvc;

namespace MordexIntegration.Controllers
{
    using MordexIntegration.Models;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IMordexServices _mordexService;

        public TokenController(IMordexServices mordexService)
        {
            _mordexService = mordexService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTokens([FromBody] TokenTransfer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mordexService.CreateTokens(model.Amount);
            if (result)
            {
                return Ok(new { message = "Tokens created successfully" });
            }
            return BadRequest(new { message = "Failed to create tokens" });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferTokens([FromBody] TokenTransfer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mordexService.TransferTokens(model.Amount, model.RecipientAddress);
            if (result)
            {
                return Ok(new { message = "Tokens transferred successfully" });
            }
            return BadRequest(new { message = "Failed to transfer tokens" });
        }

    }
}
