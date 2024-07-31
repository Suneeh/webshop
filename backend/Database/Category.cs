namespace backend.Database;

public class Category(string name)
{
	public int Id { get; set; }
	public string Name { get; set; } = name;
	public string? Description { get; set; }
	public ICollection<Product> Products { get; set; } = [];
}
