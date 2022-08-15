using Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands;
using Beta.OrderService.Application.ApplicationServices.Orders.Commands;
using Beta.OrderService.Application.ApplicationServices.Orders.Queries;
using Beta.OrderService.Application.Common;
using Beta.OrderService.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Beta.OrderService.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private readonly ISender _mediatorSender;

    public OrderController(ISender mediatorSender)
    {
        _mediatorSender = mediatorSender;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDto>>> GetOrders([FromQuery]GetOrdersQuery query)
    {
        var orders = await _mediatorSender.Send(query);
        return Ok(orders);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrdersById(long? id)
    {
        var orders =  await _mediatorSender.Send(new GetOrdersQuery(){OrderId = id});
        return Ok(orders.List.FirstOrDefault());
    }
    
    
    [HttpPost]
    public async Task<ActionResult<long>> CreateOrder([FromBody]CreateOrderCommand command)
    {
        var orderDto = await _mediatorSender.Send(command);
        return CreatedAtAction("GetOrdersById",new { orderDto.Id},orderDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
         await _mediatorSender.Send(new DeleteOrderCommand(){OrderId = id});
        return Ok();
    }
}