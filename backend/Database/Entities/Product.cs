using System.ComponentModel.DataAnnotations;

namespace backend.Database.Entities;

public class Product(string name, double netPrice, double taxRate)
{
	private Category? _category;
	public int Id { get; init; }
	
	[StringLength(200)]
	public string Name { get; set; } = name;
	
	[StringLength(1000)]
	public string? Description { get; set; }
	
	[StringLength(6)]
	public string ColorCodeHex { get; set; }
	public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset ChangedDate { get; set; }
	public double NetPrice { get; set; } = netPrice;
	public double TaxRate { get; set; } = taxRate;
	public int? CategoryId { get; set; }

	public Category Category
	{
		set => _category = value;
		get => _category ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Category));
	}
}
