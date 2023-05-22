namespace API.Models
{
    public class CreateItemModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }
    }
}
