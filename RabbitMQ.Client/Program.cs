using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "myQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "myQueue", autoAck: true, consumerTag: "Consumer_" + Path.GetRandomFileName(), consumer: consumer);

            await Task.Delay(Timeout.Infinite);
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var messageRaw = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(messageRaw);

            Console.WriteLine($" [x] Received: {message} ");
        }
    }
}
