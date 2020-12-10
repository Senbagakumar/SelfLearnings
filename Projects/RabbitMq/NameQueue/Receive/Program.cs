using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Hellow", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += Consumer_Received;
                    channel.BasicConsume(queue: "Hellow", autoAck: true, consumer: consumer);
                    Console.WriteLine(" Press [enter] to exist");
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);

            int dots = message.Split('.').Length - 1;
            Thread.Sleep(dots * 1000);


        }
    }
}
