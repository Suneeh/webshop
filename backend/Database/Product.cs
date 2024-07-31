namespace backend.Database;

public class Product(string name, double netPrice, double taxRate)
{
	private Category? _category;
	public int Id { get; set; }
	public string Name { get; set; } = name;
	public string? Description { get; set; }
	public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset ChangedDate { get; set; }
	public double NetPrice { get; set; } = netPrice;
	public double TaxRate { get; set; } = taxRate;
	public int? CategoryId { get; set; } = null;

	public Category Category
	{
		set => _category = value;
		get => _category ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Category));
	}
}
