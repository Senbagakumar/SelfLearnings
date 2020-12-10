using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Receive Msg World!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel=connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "HellowTask", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[x] Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine(" [x] Done");


                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); // Acknowledge after received the message
                    };

                    channel.BasicConsume(queue: "HellowTask", autoAck: false, consumer: consumer); // set autoAck: false
                    Console.WriteLine(" Press [enter] to exist");
                    Console.ReadLine();
                }
            }
        }
    }
}
