using Beta.OrderService.Domain.Common;

namespace Beta.OrderService.Domain.Entities;

public class Product:EntityBase
{
    public string Name { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}