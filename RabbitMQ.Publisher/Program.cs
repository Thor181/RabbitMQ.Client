


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

            
            var messageRight = @"E:\Desktop\В Красноярский краевой суд через.docx";
            PublishMessage(channel, messageRight);
            Console.WriteLine($" [x] Sent {messageRight}");
            await Task.Delay(2000);

            var messageRight2 = @"E:\Desktop\Отзыв Торгашин А.А. 2-603_2021 .pdf";
            PublishMessage(channel, messageRight2);
            Console.WriteLine($" [x] Sent {messageRight2}");
            await Task.Delay(2000);

        }

        private static void PublishMessage(IModel channel, string messageRight)
        {
            var body = Encoding.UTF8.GetBytes(messageRight);

            channel.BasicPublish(exchange: string.Empty, routingKey: "myQueue", basicProperties: null, body: body);
        }
    }
}
