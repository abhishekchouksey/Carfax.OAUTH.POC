using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ImageGallery.Client.Controllers
{
    [Authorize(AuthenticationSchemes = "Oauth")]
    public class CarFaxConnectController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CarFaxConnectController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [Authorize(AuthenticationSchemes = "Oauth")]
        public async Task<IActionResult> IndexAsync()
        {

            var _IdpCarfaxConnectClient = _httpClientFactory.CreateClient("IdpCarfaxConnectClient");

            var metaDataResponse = await _IdpCarfaxConnectClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new Exception("Problem accessing the discovery endpoint.", metaDataResponse.Exception);
            }

            var accessToken = await HttpContext.GetTokenAsync("Oauth", OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await _IdpCarfaxConnectClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = metaDataResponse.UserInfoEndpoint,
                Token = accessToken
            });

            if (userInfoResponse.IsError)
            {
                throw new Exception("Problem accessing the UserInfo endpoint.", metaDataResponse.Exception);
            }

            //var address = userInfoResponse.Claims.FirstOrDefault(c => c.Type == "address")?.Value;

            return View(userInfoResponse);

            //return View();

        }



        public async Task WriteOutIdentityInformation()
        {
            var identityToken = await HttpContext.GetTokenAsync("oauth", OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token : {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim Type : {claim.Type} - Claim Value : {claim.Value}");
            }
        }
    }
}
