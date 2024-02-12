using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.DataAdapters;
using Tribe_OAuth2_BE_Demo.Enums;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public class GoogleService : IGoogleService
    {
        private IConfiguration _configuration { get; }
        private IAuthService _authService { get; }
        private IUserDataAdapter _userDataAdapter { get; }
        private IUserDetailsDataAdapter _userDetailsDataAdapter { get; }
        private IMapper _mapper { get; }

        public GoogleService(IConfiguration configuration,
            IMapper mapper,
            IAuthService authService,
            IUserDataAdapter userDataAdapter,
            IUserDetailsDataAdapter userDetailsDataAdapter,
            ISessionDataAdapter sessionDataAdapter)
        {
            _configuration = configuration;
            _authService = authService;
            _userDataAdapter = userDataAdapter;
            _mapper = mapper;
            _userDetailsDataAdapter = userDetailsDataAdapter;
        }

        public async Task<dynamic> Signup(string creditenal)
        {
            try
            {
                var payload = await ValidateAsync(creditenal);

                if (payload != null)
                {

                    var user = _mapper.Map<User>(payload);
                    var userDetails = _mapper.Map<UserDetails>(payload);

                    try
                    {
                        user = _userDataAdapter.Insert(user);

                        userDetails.UserId = user.UserId;
                        userDetails = _userDetailsDataAdapter.Insert(userDetails);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    var userInformation = JsonSerializer.Serialize(payload);
                    return new { token = _authService.GenerateJWTToken(user, payload.ExpirationTimeSeconds ?? 999999), userInformation = userInformation };
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error occured during the google auth process", e);
            }
            throw new Exception("Error occured during the google auth process");
        }

        public async Task<dynamic> Login(string creditenal)
        {
            try
            {
                var payload = await ValidateAsync(creditenal);

                if (payload != null)
                {
                    var foundedUser = _userDataAdapter.FindUserByEmail(payload.Email);

                    if (foundedUser != null)
                    {
                        var userInformation = JsonSerializer.Serialize(payload);
                        var defaultExpirationTime = 36000;

                        var session = _authService.CreateSession(foundedUser, defaultExpirationTime, nameof(CdSessionTypeEnum.GOOGLE));
                        //var session = _authService.CreateSession(foundedUser, payload.ExpirationTimeSeconds ?? defaultExpirationTime, nameof(CdSessionTypeEnum.GOOGLE));

                        return new { token = session.Token, session = session, userInformation = userInformation };
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            throw new Exception("Error occured during the google auth process");

        }

        public User NHibernate(string email)
        {
            return _userDataAdapter.FindUserByEmail(email);
        }

        private Task<Payload> ValidateAsync(string creditenal)
        {
            var settings = new ValidationSettings()
            {
                Audience = new List<string> { _configuration["Authentication:Google:ClientId"] },
            };

            return Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(creditenal, settings);
        }

        public Task<dynamic> TestToken(string creditenals)
        {
            return Login(creditenals);
        }

        public async Task<dynamic> LoginWithOauth2(string code)
        {
            var googleTokenResponse = await GetGoogleTokenResponseAsync(code);

            if (googleTokenResponse != null) {
                var googleUserProfile = await GetUserProfile(googleTokenResponse);
                return new { code = code, googleTokenResponse = googleTokenResponse, googleUserProfile = googleUserProfile };
            }


            return new { code = code, googleTokenResponse = googleTokenResponse };
        }

        private async Task<GoogleUserProfile> GetUserProfile(GoogleTokenResponse googleTokenResponse)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"https://www.googleapis.com"),
            };
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization" ,$"Bearer {googleTokenResponse.AccessToken}");

            var requestUri = "/userinfo/v2/me";
            var response = await httpClient.GetAsync(requestUri);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var body = await response.Content.ReadAsStringAsync();
                var googleUserProfile = JsonSerializer.Deserialize<GoogleUserProfile>(body);
                return googleUserProfile;
            }
            return null;
        }

        public async Task<GoogleTokenResponse> GetGoogleTokenResponseAsync(string code)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"https://oauth2.googleapis.com"),
            };

            var redirectUri = "http%3A%2F%2Flocalhost%3A4200%2Flogin";
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientSecret = "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";
            var scope = "email%20profile%20openid%20https:%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.profile%20https:%2F%2Fwww.googleapis.com%2Fauth%2Fuserinfo.email";
            var grantType = "authorization_code";

            var requestUri = $"/token?code={code}&redirect_uri={redirectUri}&client_id={clientId}&client_secret={clientSecret}&scope={scope}&grant_type={grantType}";

            var response = await httpClient.PostAsync(requestUri, null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var body = await response.Content.ReadAsStringAsync();
                var googleTokenResponse = JsonSerializer.Deserialize<GoogleTokenResponse>(body);
                return googleTokenResponse;
            }
            return null;
        }
    }
}
