using backend.Database.Entities;
using Microsoft.EntityFrameworkCore;
namespace backend.Database;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Rating> Ratings => Set<Rating>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>()
			.HasOne(product => product.Category)
			.WithMany(category => category.Products);
		
		modelBuilder.Entity<Rating>()
			.HasOne(rating => rating.Product)
			.WithMany(product => product.Ratings);

		modelBuilder.Entity<Rating>()
			.HasIndex(rating => new { rating.ProductId, rating.Email })
			.IsUnique();
	}
}

