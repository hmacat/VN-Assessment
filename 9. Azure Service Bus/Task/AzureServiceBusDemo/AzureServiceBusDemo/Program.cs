using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace AzureServiceBusDemo;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration["ServiceBus:ConnectionString"];
        var queueName = configuration["ServiceBus:QueueName"];

        await using var client = new ServiceBusClient(connectionString);

        ServiceBusSender sender = client.CreateSender(queueName);
        await SendMessageAsync(sender);
        await ReceiveMessagesAsync(client, queueName);

        Console.ReadKey();
    }

    static async Task SendMessageAsync(ServiceBusSender sender)
    {
        var messageBody = $"Message with id: {Guid.NewGuid()}";
        ServiceBusMessage message = new(messageBody);

        await sender.SendMessageAsync(message);

        Console.WriteLine($"Message sent. Message body:{messageBody}");
    }

    static async Task ReceiveMessagesAsync(ServiceBusClient client, string queueName)
    {
        ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();
    }

    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"Received message: {body}");
        await args.CompleteMessageAsync(args.Message);
    }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Exception occured:{args.Exception}");
        return Task.CompletedTask;
    }
}