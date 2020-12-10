using System;
using System.Text;
using RabbitMQ.Client;

namespace NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Send Msg World!");

            var message = GetMessage(args);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "HellowTask", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "HellowTask", basicProperties: properties, body: body);

                    Console.WriteLine(" [x] Sent {0}", message);

                }
            }


            Console.WriteLine("Please [enter] to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" " , args) : "Hellow World");
        }
    }
}
