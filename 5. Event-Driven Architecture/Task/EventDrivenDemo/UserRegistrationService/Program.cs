using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Threading.Channels;

namespace UserRegistrationService;

class Program
{
    static void Main(string[] args)
    {
        //Initialize the user registration queue.
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "user_registration",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        // send a message to the queue every 5 seconds. 
        while (true)
        {
            PublishMessage(channel);
            Thread.Sleep(5000);
        }
    }

    public static void PublishMessage(IModel channel)
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id,
            Email = $"{id}@example.com"
        };

        var message = JsonSerializer.Serialize(user);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                 routingKey: "user_registration",
                 basicProperties: null,
                 body: body);

        Console.WriteLine($"Message sent: {message}");
    }
}
