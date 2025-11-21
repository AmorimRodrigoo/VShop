using Microsoft.AspNetCore.Authentication;
using VShop.Web.Services;
using VShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", option =>
    {
        option.Authority = builder.Configuration["ServiceUri:IdentityServer"];
        option.GetClaimsFromUserInfoEndpoint = true;
        option.ClientId = "vshop";
        option.ClientSecret = builder.Configuration["Client:Secret"];
        option.ResponseType = "code";
        option.ClaimActions.MapJsonKey("role", "role", "role");
        option.ClaimActions.MapJsonKey("sub", "sub", "sub");
        option.TokenValidationParameters.NameClaimType = "name";
        option.TokenValidationParameters.RoleClaimType = "role";
        option.Scope.Add("vshop");
        option.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();