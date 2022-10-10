using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
