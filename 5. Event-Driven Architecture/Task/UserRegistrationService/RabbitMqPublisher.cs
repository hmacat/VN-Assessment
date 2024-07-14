using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UserRegistrationService;

// RabbitMQ implementation of the publisher
public class RabbitMqPublisher : IEventPublisher
{
    private readonly IModel _channel;

    public RabbitMqPublisher(IModel channel)
    {
        _channel = channel;
    }

    public void PublishUserRegisteredEvent(User user)
    {
        var eventMessage = JsonSerializer.Serialize(new UserRegisteredEvent { UserId = user.Id, Email = user.Email });
        var body = Encoding.UTF8.GetBytes(eventMessage);
        _channel.BasicPublish(exchange: "",
                              routingKey: "user_registration",
                              basicProperties: null,
                              body: body);
        Console.WriteLine($"Message sent to registration queue: Id:{user.Id} Email:{user.Email}");
    }
}
