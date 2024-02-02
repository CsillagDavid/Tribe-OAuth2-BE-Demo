using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Tribe_OAuth2_BE_Demo.Services;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tribe_OAuth2_BE_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleController : ControllerBase
    {
        private readonly IGoogleService _googleService;
        public GoogleController(IGoogleService googleService) {
            _googleService = googleService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string creditenals)
        {
            try
            {
                var token = await _googleService.Login(creditenals);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
