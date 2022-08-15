using System.Net;

namespace Beta.OrderService.Domain.Exceptions;

public class BadRequestException:Exception
{
    public BadRequestException(string message):base(message)
    {
        this.Data.Add("status", (int)HttpStatusCode.BadRequest);
    }
}