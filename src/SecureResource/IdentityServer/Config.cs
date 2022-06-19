using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "favoritesClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "EFCoreCodeFirstSampleWEBAPI" }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("EFCoreCodeFirstSampleWEBAPI", "Filmos Favorites API")
        };
        public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
        };
        public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
        };
    }
}
