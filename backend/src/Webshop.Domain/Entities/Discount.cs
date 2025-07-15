using System.ComponentModel.DataAnnotations;

namespace Webshop.Domain.Entities;

public class Discount(double value, string name, string? description = null, DateTimeOffset? validUntil = null)
{
	public int Id { get; init; }

	[StringLength(50)]
	public string Name { get; init; } = name;
	
	[StringLength(100)]
	public string Description { get; init; } = description ?? string.Empty;
	
	public DateTimeOffset? ValidUntil { get; init; } = validUntil;

	public double Value { get; init; } = value;
	
	public List<Product> Products { get; init; } = [];
}
