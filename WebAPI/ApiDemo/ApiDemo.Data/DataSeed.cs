using ApiDemo.Data.Models;

namespace ApiDemo.Data;

public class DataSeed
{
    public static void Seed(DemoContext context)
    {
        if (!context.Categories.Any())
        {
            CreateCatalogs(context);
            context.SaveChanges();
        }

        if (!context.Items.Any())
        {
            CreateItems(context);
            context.SaveChanges();
        }
    }

    private static void CreateItems(DemoContext context)
    {
        var items = new Item[]
        {
            new() { Name = "iPhone X", Description = "Apple iPhone X, 64GB, Space Gray", Price = 999.99m, CategoryId = 1 },
            new() { Name = "Samsung Galaxy S10", Description = "Samsung Galaxy S10, 128GB, Prism Black", Price = 899.99m, CategoryId = 1 },
            new() { Name = "MacBook Pro", Description = "Apple MacBook Pro, 13-inch, 256GB, Space Gray", Price = 1499.99m, CategoryId = 1 },
            new() { Name = "Harry Potter and the Philosopher's Stone", Description = "J.K. Rowling's first novel in the Harry Potter series", Price = 12.99m, CategoryId = 2 },
            new() { Name = "The Lord of the Rings: The Fellowship of the Ring", Description = "J.R.R. Tolkien's first novel in The Lord of the Rings series", Price = 14.99m, CategoryId = 2 },
            new() { Name = "Men's T-Shirt", Description = "100% cotton, black, size L", Price = 19.99m, CategoryId = 3 },
            new() { Name = "Women's Dress", Description = "Polyester, red, size M", Price = 29.99m, CategoryId = 3 },
            new() { Name = "Running Shoes", Description = "Nike Air Zoom Pegasus 38, black/white, size 10", Price = 119.99m, CategoryId = 3 },
            new() { Name = "Smart Watch", Description = "Apple Watch Series 6, 44mm, Space Gray Aluminum Case with Black Sport Band", Price = 399.99m, CategoryId = 1 },
            new() { Name = "Wireless Headphones", Description = "Bose QuietComfort 35 II, black", Price = 299.99m, CategoryId = 1 }
        };
        context.Items.AddRange(items);
    }

    private static void CreateCatalogs(DemoContext context)
    {
        var categories = new Category[]
        {
            new() {Name = "Electronics"},
            new() {Name = "Books"},
            new() {Name = "Clothing"}
        };
        context.Categories.AddRange(categories);
    }
}