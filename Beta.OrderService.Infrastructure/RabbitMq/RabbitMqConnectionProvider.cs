using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beta.OrderService.Infrastructure.RabbitMq
{
    public interface IRabbitMqConnectionProvider
    {
        IConnection GetConnection();
        IModel GetModel();
    }

    public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _model;
        private readonly ConnectionFactory _factory;

        public RabbitMqConnectionProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
            _factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration["RabbitMqConnection"])
            };
            _connection = _factory.CreateConnection();
            _model = GetConnection().CreateModel();

        }

        public IConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = _factory.CreateConnection();
            }

            return _connection;
        }
        public IModel GetModel()
        {
            if (_model == null)
            {
                _model = GetConnection().CreateModel();
            }

            return _model;
        }
    }
}
