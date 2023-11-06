using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Tribe_OAuth2_BE_Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuth2Controller : Controller
    {
        [HttpGet("random")]
        public int Get()
        {
            var rng = new Random();
            return rng.Next(0, 100);
        }

        [HttpGet("signin-facebook")]
        public string SignInFaceBook()
        {
            return "FB_SignIn";
        }

        [HttpGet("sign-in")]
        public async Task<IActionResult> SignIn(string access_token, string data_access_expiration_time, string expires_in)
        {

            var client = new RestClient("https://graph.facebook.com/v8.0");
            //var client = new RestClient("https://www.facebook.com/v18.0");
            var request = new RestRequest($"/me?access_token={access_token}");
            //var client = new RestClient("https://www.facebook.com/v18.0");
            //var request = new RestRequest("/dialog/oauth?client_id=1192839698300926&response_type=token&redirect_uri=https://localhost:44359/oauth2/sign-in");
            var response = await client.GetAsync(request);

            if (!response.IsSuccessful)
                return NotFound(response.ErrorMessage!);

            //// get data from response and account from db
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content!);

            return Ok(data);
        }
    }
}
