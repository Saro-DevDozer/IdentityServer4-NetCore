using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityClient.Controllers
{
    public class HomeController : Controller
    {
        public IHttpClientFactory _httpClientFactory { get; }

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            //retrieve access token
            var serverClient = _httpClientFactory.CreateClient();
            var discoverDoc = await serverClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoverDoc.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",

                    Scope = "IdentityApi"
                });

            //retrieve secret message

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:5001/secret");


            return Ok(new
            {
                client_Token = tokenResponse.AccessToken,
                message = response.Content.ReadAsStringAsync(),
            });
        }


    }
}
