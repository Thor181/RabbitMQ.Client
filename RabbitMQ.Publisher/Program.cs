


using RabbitMQ.Client;
using System.ComponentModel.Design;
using System.Text;

namespace RabbitMQ.Publisher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "myQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            const string messageLeft = "Hello_";

            while (true)
            {
                await Task.Delay(2000);
                var messageRight = messageLeft + Random.Shared.Next().ToString();
                var body = Encoding.UTF8.GetBytes(messageRight);

                channel.BasicPublish(exchange: string.Empty, routingKey: "myQueue", basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {messageRight}");
            }


        }
    }
}
