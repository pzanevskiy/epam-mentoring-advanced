using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Constants = MessageQueues.Models.Constants;

namespace MessageQueues.ConsumerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(Constants.ConnectionString)
            };

            var connection = factory.CreateConnection();
            var model = connection.CreateModel();

            model.QueueDeclare(Constants.QueueName, true, false, false, null);
            model.QueueBind(Constants.QueueName, Constants.ExchangeName, Constants.RoutingKey);

            var consumer = new EventingBasicConsumer(model);

            using var fileReaderService = new FileReaderService(consumer);

            model.BasicConsume(Constants.QueueName, true, consumer);

            Console.ReadLine();
            model.Close();
            connection.Close();
        }
    }
}