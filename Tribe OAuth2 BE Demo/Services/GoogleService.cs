using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public class GoogleService: IGoogleService
    {
        private IConfiguration _configuration { get; }
        private IAuthService _authService { get; }

        public GoogleService(IConfiguration configuration,
            IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        public async Task<dynamic> Login(string creditenal)
        {
            try
            {
                var settings = new ValidationSettings()
                {
                    Audience = new List<string> { this._configuration["Authentication:Google:ClientId"] }
                };

                var payload = await ValidateAsync(creditenal, settings);

                if (payload != null)
                {
                    var user = new User {
                        Email = payload.Email,
                        Name = payload.Name,
                        Picture = payload.Picture,
                    };

                    var userInformation = JsonSerializer.Serialize(payload);

                    return new { token = _authService.GenerateJWTToken(user, payload.ExpirationTimeSeconds), userInformation = userInformation};
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during the google auth process", e);
            }
            throw new Exception("Error occured during the google auth process");

        }
    }
}
