using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
             {
                 new ApiResource("IdentityApi"),
             };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
         {
             new IdentityResources.OpenId(),
             new IdentityResources.Profile(),
         };
        }

        public static IEnumerable<Client> GetClients()
        => new List<Client>
         {
             new Client
             {
                ClientId="client_id",
                ClientSecrets ={new Secret("client_secret".ToSha256())},

                 AllowedGrantTypes=GrantTypes.ClientCredentials,
                 AllowedScopes={"IdentityApi" }
             }
         };

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
         {
             new TestUser
             {
                 SubjectId = "1",
                 Username = "james",
                 Password = "password",
                 Claims = new List<Claim>
                 {
                     new Claim("name", "James Bond"),
                     new Claim("website", "https://james.com")
                 }
             }
         };
        }

    }
}
