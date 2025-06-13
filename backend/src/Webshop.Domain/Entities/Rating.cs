using System.ComponentModel.DataAnnotations;

namespace Webshop.Domain.Entities;

public class Rating(string email, int value, int productId)
{
	private Product? _product;
	public int Id { get; init; }
	public int Value { get; set; } = value;
	[StringLength(100)]
	public string Email { get; set; } = email;
	public int ProductId { get; set; } = productId;
	public Product Product
	{
		set => _product = value;
		get => _product ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Product));
	}
}
