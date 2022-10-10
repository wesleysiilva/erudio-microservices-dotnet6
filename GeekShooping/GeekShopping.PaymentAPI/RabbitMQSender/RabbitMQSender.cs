﻿using GeekShopping.PaymentAPI.Messages;
using GeekShopping.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQSender : IRabbitMQSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "FanoutPaymentUpdateExchange";

        public RabbitMQSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false);
                byte[] body = GetMessageAsByteArray(message);
                channel.BasicPublish(exchange: ExchangeName, "", basicProperties: null, body: body);
            }
        }

        private static byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var json = JsonSerializer
                .Serialize<UpdatePaymentResultMessage>(
                    (UpdatePaymentResultMessage)message,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return true;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
