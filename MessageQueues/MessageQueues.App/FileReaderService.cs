﻿using RabbitMQ.Client.Events;
using MessageQueues.Models;
using Constants = MessageQueues.Models.Constants;

namespace MessageQueues.ConsumerApp;

public class FileReaderService : IDisposable
{
    private readonly IDictionary<string, List<FileChunk>> _chunks = new Dictionary<string, List<FileChunk>>();
    private readonly EventingBasicConsumer _consumer;

    public FileReaderService(EventingBasicConsumer consumer)
    {
        _consumer = consumer;

        consumer.Received += OnReceived;
    }

    private void OnReceived(object sender, BasicDeliverEventArgs e)
    {
        var fileName = e.BasicProperties.MessageId;
        var position = (int)e.BasicProperties.Headers[nameof(FileChunk.Position)];
        var size = (int)e.BasicProperties.Headers[nameof(FileChunk.Size)];
        var body = e.Body.ToArray();
        
        Console.WriteLine("Processing '{0}' {1} of {2}", fileName, position, size);

        _chunks.TryAdd(fileName, new List<FileChunk>());

        var chunk = new FileChunk
        {
            Id = fileName,
            Position = position,
            Size = size,
            Body = body
        };

        _chunks[fileName].Add(chunk);

        if (_chunks[fileName].Count == size)
        {
            var ordered = _chunks[fileName].OrderBy(x => x.Position);
            var bytes = new byte[_chunks[fileName].Sum(x => x.Body.Length)];
            var offset = 0;
            foreach (var fileChunk in ordered)
            {
                Array.ConstrainedCopy(fileChunk.Body, 0, bytes, offset, fileChunk.Body.Length);
                offset += fileChunk.Body.Length;
            }

            _chunks.Remove(fileName);
            Console.WriteLine("Saving '{0}'", fileName);
            File.WriteAllBytes($"{Constants.OutputFolder}\\{fileName}", bytes);
        }
    }

    public void Dispose()
    {
        _chunks.Clear();
        _consumer.Received -= OnReceived;
    }
}