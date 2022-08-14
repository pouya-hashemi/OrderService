using Beta.OrderService.Domain.Common;
using Beta.OrderService.Domain.Exceptions;

namespace Beta.OrderService.Domain.Entities;

public class OrderDetail:EntityBase
{
    public OrderDetail(long orderId,long productId,int quantity)
    {
        this.OrderId = orderId;
        this.ProductId = productId;
        this.Quantiy = ValidateQuantity(quantity);
    }
    public long OrderId { get;private set; }
    public Order Order { get;private set; }
    public long ProductId { get;private set; }
    public Product Product { get;private set; }
    public int Quantiy { get;private set; }
    
    private int ValidateQuantity(int quantity)
    {
        if (quantity<=0)
        {
            throw new BadRequestException("quantity must be greater than 0.");
        }

        return quantity;
    }

    private void SetQuantity(int quantity)
    {
        this.Quantiy = ValidateQuantity(quantity);
    }

    
}