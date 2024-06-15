using Microsoft.EntityFrameworkCore;
using backend.ShopDbContext;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(connectionString));
var app = builder.Build();
using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    context.Database.Migrate();
}
app.MapGet("/", async (ShopDbContext ctx) =>
{
    var prod = await ctx.Products.FirstOrDefaultAsync();
    return prod;
});

app.Run();
