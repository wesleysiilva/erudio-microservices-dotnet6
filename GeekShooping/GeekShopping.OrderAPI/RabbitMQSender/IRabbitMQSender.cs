using GeekShopping.MessageBus;

namespace GeekShopping.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
