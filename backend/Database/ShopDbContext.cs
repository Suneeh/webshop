﻿using Microsoft.EntityFrameworkCore;
namespace backend.Database;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Category> Categories => Set<Category>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>()
			.HasOne(x => x.Category)
			.WithMany(x => x.Products);
	}
}

