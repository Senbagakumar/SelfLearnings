using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Send
{
    class Send
    {
        public static void main(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Hellow", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //string message = "Hellow How Are you";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "Hellow", basicProperties: null, body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
