namespace BrewUp.Purchases.SharedKernel.Dtos;

public class Order
{
	public Guid SupplierId { get; set; }
	public DateTime Date { get; set; }
	public IEnumerable<OrderLine> Lines { get; set; } = [];
	public Guid Id { get; } = Guid.NewGuid();
}