using Microsoft.EntityFrameworkCore;
using backend.Database;
using backend.Api.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using backend.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using App.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options
  => options.AddPolicy(name: "name", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

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
    .AddPolicy("manage", policy => policy.Requirements.Add(
        new RbacRequirement("manage")
    )
);

builder.Services.AddSingleton<IAuthorizationHandler, RbacHandler>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}
using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    context.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseSecureHeaders();
app.RegisterProductEndpoints();
app.UseCors("name");

app.Run();
