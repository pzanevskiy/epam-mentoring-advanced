using MessageQueues.Models;
using RabbitMQ.Client;
using Constants = MessageQueues.Models.Constants;

namespace MessageQueues.ProducerApp;

public class FileWatcherService : IDisposable
{
    private const int ChunkSize = 2 * 1024 * 1024;
    private readonly IModel _model;
    private readonly FileSystemWatcher _watcher;

    public FileWatcherService(IModel model, string path, string filter)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException("Path doesn't exist");

        _model = model;

        _watcher = new FileSystemWatcher(path, filter);
        _watcher.EnableRaisingEvents = true;

        _watcher.Created += OnCreated;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine("Processing '{0}'", e.FullPath);

        var fileSize = new FileInfo(e.FullPath).Length;
        var chunksCount = (int)Math.Ceiling((double)fileSize / ChunkSize);

        Console.WriteLine("Chunks count - {0}", chunksCount);

        using var fs = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read);

        var buffer = new byte[ChunkSize];
        int chunkCounter = 1, size;

        while ((size = fs.Read(buffer, 0, buffer.Length)) > 0)
        {
            var chunk = new FileChunk
            {
                Id = e.Name,
                Position = chunkCounter,
                Size = chunksCount,
                Body = size < ChunkSize ? buffer.Take(size).ToArray() : buffer
            };
            SendChunk(chunk);

            chunkCounter++;
        }
    }

    private void SendChunk(FileChunk chunk)
    {
        var props = _model.CreateBasicProperties();
        props.MessageId = chunk.Id;
        props.Headers = new Dictionary<string, object>
        {
            { nameof(FileChunk.Size), chunk.Size },
            { nameof(FileChunk.Position), chunk.Position },
        };

        Console.WriteLine("Sending chunk - {0}", chunk.Position);

        _model.BasicPublish(Constants.ExchangeName, Constants.RoutingKey, props, chunk.Body);
    }

    public void Dispose()
    {
        _watcher.Created -= OnCreated;
    }
}