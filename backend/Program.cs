using backend.Authorization;
using backend.Database;
using backend.Extensions;
using backend.Middlewares;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => options.AddPolicy(
    name: "name",
    b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ShopAPI";
    config.Title = "ShopAPI v1";
    config.Version = "v1";
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://suneeh.eu.auth0.com/";
        options.Audience = "webshop";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });


builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("user", policy => policy.RequireAuthenticatedUser())
    .AddPolicy("admin", policy => policy.Requirements.Add(new RbacRequirement("manage")));

builder.Services.AddSingleton<IAuthorizationHandler, RbacHandler>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddMinimalEndpoints();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "WebShop API";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    context.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseSecureHeaders();
app.RegisterMinimalEndpoints();
app.UseCors("name");

app.Run();