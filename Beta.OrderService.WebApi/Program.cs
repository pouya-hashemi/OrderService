using Beta.OrderService.Application.Common;
using Beta.OrderService.Domain.Common;
using Beta.OrderService.Infrastructure.Common;
using Beta.OrderService.WebApi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDomainInjections();
builder.Services.AddApplicationInjections();
builder.Services.AddInferastructureInjections(builder.Configuration);

builder.Services.AddHostedService<RabbitMqSubscriber>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlerMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();