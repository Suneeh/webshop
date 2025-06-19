using System.ComponentModel.DataAnnotations;

namespace Webshop.Domain.Entities;

public class Category(string name)
{
	public int Id { get; init; }
	
	[StringLength(200)]
	public string Name { get; set; } = name;
	
	[StringLength(1000)]
	public string? Description { get; set; }

	public ICollection<Product> Products { get; init; } = [];
}
