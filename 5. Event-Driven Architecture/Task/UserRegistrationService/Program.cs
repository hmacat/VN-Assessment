using RabbitMQ.Client;

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

        var eventPublisher = new RabbitMqPublisher(channel);
        var userService = new UserService(eventPublisher);

        //register a user every 5 seconds
        while (true)
        {
            userService.RegisterUser();
            Thread.Sleep(5000);
        }
    }
}
