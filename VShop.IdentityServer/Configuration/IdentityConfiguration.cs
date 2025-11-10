using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            // vshop é a aplicação web que vai acessar 
            // o IdentityServer para obeter tokens
            new ApiScope("vshop", "VShop Server"),
            new ApiScope(name: "read", displayName: "Read your data."),
            new ApiScope(name: "write", displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data.")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // cliente genérico
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("balanaagulha1".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do cliente
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
            ClientId = "vshop",
            ClientSecrets = { new Secret("balanaagulha1".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code, // via código
            RedirectUris = {"https://localhost:7002/signin-oidc" }, //login
            PostLogoutRedirectUris = {"https://localhost:7002/signout-callback-oidc" }, //logout
            AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "vshop"
                }
            }
        };
}