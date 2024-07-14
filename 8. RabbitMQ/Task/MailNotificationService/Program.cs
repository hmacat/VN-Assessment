using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MailNotificationService;

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

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var user = JsonSerializer.Deserialize<User>(message);

            SendWelcomeEmail(user);
        };
        channel.BasicConsume(queue: "user_registration",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine("Waiting for user registration messages...");
        Console.ReadLine();
    }

    private static void SendWelcomeEmail(User user)
    {
        Console.WriteLine($"Sending welcome email to {user.Id} ({user.Email})");
    }
}