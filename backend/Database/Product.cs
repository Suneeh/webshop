namespace backend.Database;

public class Product(string name, double netPrice, double taxRate)
{
	public int Id { get; set; }
	public string Name { get; set; } = name;
	public string? Description { get; set; }
	public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset ChangedDate { get; set; }
	public double NetPrice { get; set; } = netPrice;
	public double TaxRate { get; set; } = taxRate;
}
