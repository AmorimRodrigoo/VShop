using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDataBase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public void InitializeSeedRole()
    {
        //Se o perfil Admin não existir, criar perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            //cria perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }

        //Se o perfil Client não existir, criar perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUser()
    {
        //se o usuario admin não existe cria o usuário, define uma senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            //define dados do usuario admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuário",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            //cria usuário Admin e atribui senha
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Balanaagulha1!").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuario admin
                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }

        //se o usuario client não existe cria o usuário, define uma senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            //define dados do usuario client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@com.br",
                NormalizedEmail = "CLIENT1",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuário",
                LastName = "Client1",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            //cria usuário Client e atribui senha
            IdentityResult resultAdmin = _userManager.CreateAsync(client, "Balanaagulha1!").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil client
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuario client
                var adminClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }
    }
}