using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Application.ApplicationServices.OrderDetails.Commands
{
    public class UpdateOrderDetailCommand:IRequest
    {
        public long OrderDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateOrderDetailHandler : IRequestHandler<UpdateOrderDetailCommand>
    {
        public UpdateOrderDetailHandler()
        {

        }
        public Task<Unit> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
