using Beta.OrderService.Domain.Common;
using Beta.OrderService.Domain.Exceptions;

namespace Beta.OrderService.Domain.Entities;

public class OrderDetail:EntityBase
{
    private OrderDetail(){}
    public OrderDetail(long orderId,long productId,int quantity)
    {
        this.OrderId = orderId;
        this.ProductId = productId;
        this.Quantity = ValidateQuantity(quantity);
    }
    public long OrderId { get;private set; }
    public Order Order { get;private set; }
    public long ProductId { get;private set; }
    public Product Product { get;private set; }
    public int Quantity { get;private set; }
    
    private int ValidateQuantity(int quantity)
    {
        if (quantity<=0)
        {
            throw new BadRequestException("quantity must be greater than 0.");
        }

        return quantity;
    }

    internal void SetQuantity(int quantity)
    {
        this.Quantity = ValidateQuantity(quantity);
    }

    
}