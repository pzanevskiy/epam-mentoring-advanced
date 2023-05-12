namespace MessageQueues.Models
{
    public class FileChunk
    {
        // create properties Id as Guid, Position, Size and Body as byte[]
        public string Id { get; set; }
        public int Position { get; set; }
        public int Size { get; set; }
        public byte[] Body { get; set; }
    }
}