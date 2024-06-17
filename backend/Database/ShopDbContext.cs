using Microsoft.EntityFrameworkCore;
namespace backend.Database;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products => Set<Product>();
}

