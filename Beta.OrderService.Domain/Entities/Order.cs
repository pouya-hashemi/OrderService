using Beta.OrderService.Domain.Common;

namespace Beta.OrderService.Domain.Entities;

public class Order:EntityBase
{
    private Order()
    {
    }

    public Order(long orderNumber)
    {
        this.OrderNumber = orderNumber;
        this.SubmitDate = DateTime.Now;
    }
    public long OrderNumber { get;private set; }
    public DateTime SubmitDate { get;private set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}