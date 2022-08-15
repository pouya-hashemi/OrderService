using Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands;
using Beta.OrderService.Application.ApplicationServices.OrderDetails.Queries;
using Beta.OrderService.Application.Common;
using Beta.OrderService.Application.Dtos;
using Beta.OrderService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Beta.OrderService.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderDetailController:ControllerBase
{
    private readonly ISender _mediatorSender;

    public OrderDetailController(ISender mediatorSender)
    {
        _mediatorSender = mediatorSender;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderDetailDto>>> GetOrderDetails([FromQuery]GetOrderDetailsQuery query)
    {
        var orderDetails = await _mediatorSender.Send(query);
        return Ok(orderDetails);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailDto>> GetOrderDetailById(long? id)
    {
        var orderDetails =  await _mediatorSender.Send(new GetOrderDetailsQuery(){OrderDetailId = id});
        if (!orderDetails.List.Any())
        {
            throw new NotFoundException("Order item was not found");
        }
        return Ok(orderDetails.List.FirstOrDefault());
    }
    
    
    [HttpPost]
    public async Task<ActionResult<long>> CreateOrderDetail([FromBody]CreateOrderDetailCommand command)
    {
        var orderDetailDto = await _mediatorSender.Send(command);
        return CreatedAtAction("GetOrderDetailById",new { orderDetailDto.Id},orderDetailDto);
    }
    [HttpPut]
    public async Task<ActionResult> UpdateOrderDetail([FromBody]UpdateOrderDetailCommand command)
    {
         await _mediatorSender.Send(command);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(long id)
    {
        await _mediatorSender.Send(new DeleteOrderDetailCommand(){OrderDetailId = id});
        return Ok();
    }
}