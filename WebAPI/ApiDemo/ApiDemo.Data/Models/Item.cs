using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDemo.Data.Models;

public class Item
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
}