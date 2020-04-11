using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Please enter any message...");
            Thread.Sleep(2000);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false,
                        autoDelete: false,
                        arguments: null);


                    while (true)
                    {
                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: string.Empty, routingKey: "hello",
                        basicProperties: null,
                        body: body);
                        Console.WriteLine($"Sender Sent {message}");
                        Console.WriteLine("Press any key to send another message.");
                    }


                }

            }
        }
    }
}
