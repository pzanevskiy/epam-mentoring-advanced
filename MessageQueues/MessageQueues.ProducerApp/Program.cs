using RabbitMQ.Client;
using Constants = MessageQueues.Models.Constants;

namespace MessageQueues.ProducerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var filter = "*.txt";
            var factory = new ConnectionFactory
            {
                Uri = new Uri(Constants.ConnectionString)
            };

            var connection = factory.CreateConnection();
            var model = connection.CreateModel();
            model.ExchangeDeclare(Constants.ExchangeName, ExchangeType.Direct);

            using var fileWatcherService = new FileWatcherService(model, Constants.InputFolder, filter);

            Console.ReadLine();
            model.Close();
            connection.Close();
        }
    }
}