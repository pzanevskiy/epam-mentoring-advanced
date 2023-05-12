namespace MessageQueues.Models;

public static class Constants
{
    public const string ExchangeName = "files-exchange";
    public const string QueueName = "reader-queue";
    public const string ConnectionString = "amqp://guest:guest@localhost:5672";
    public const string RoutingKey = "files.key";
    public const string InputFolder = @"C:\Users\Pavel_Zaneuski\source\ghrepos\epam-mentoring-advanced\InputFolder";
    public const string OutputFolder = @"C:\Users\Pavel_Zaneuski\source\ghrepos\epam-mentoring-advanced\OutputFolder";
}