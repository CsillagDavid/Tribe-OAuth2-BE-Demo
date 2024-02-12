using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tribe_OAuth2_BE_Demo.Services;

namespace Tribe_OAuth2_BE_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private IUserService _userService;

        public UserDetailsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUserDetails")]
        public async Task<IActionResult> GetUserDetails([FromBody] string email)
        {
            try
            {
                var userDetails = _userService.GetUserDetails(email);
                return Ok(userDetails);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
