using Api.Models;
using Api.Services;
using Laborator4.Models;
using Laborator4.Services;
using System;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var service = new CategoryService();

            while (true)
            {
                Console.WriteLine("1. List all categories");
                Console.WriteLine("2. Category details");
                Console.WriteLine("3. Create category");
                Console.WriteLine("4. Delete category");
                Console.WriteLine("5. Update category title");
                Console.WriteLine("6. Add product to category");
                Console.WriteLine("7. View products in category");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var opt = Console.ReadLine();

                switch (opt)
                {
                    case "1":
                        var categories = await service.GetAllCategoriesAsync();
                        foreach (var c in categories)
                            Console.WriteLine($"[{c.Id}] {c.Name}");
                        break;

                    case "2":
                        Console.Write("Category ID: ");
                        int idDet = int.Parse(Console.ReadLine() ?? "0");
                        var cat = await service.GetCategoryByIdAsync(idDet);
                        Console.WriteLine(cat != null
                            ? $"ID: {cat.Id}, Name: {cat.Name}, Product Count: {cat.ItemsCount}"
                            : "Category does not exist.");
                        break;

                    case "3":
                        Console.Write("New name: ");
                        string newName = Console.ReadLine() ?? "";
                        await service.CreateCategoryAsync(newName);
                        Console.WriteLine("Category created.");
                        break;

                    case "4":
                        Console.Write("Category ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine() ?? "0");
                        await service.DeleteCategoryAsync(deleteId);
                        Console.WriteLine("Category deleted.");
                        break;

                    case "5":
                        Console.Write("Category ID: ");
                        int updateId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("New name: ");
                        string updatedName = Console.ReadLine() ?? "";
                        await service.UpdateCategoryTitleAsync(updateId, updatedName);
                        Console.WriteLine("Title updated.");
                        break;

                    case "6":
                        Console.Write("Category ID: ");
                        int categoryId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Product name: ");
                        string productName = Console.ReadLine() ?? "";
                        Console.Write("Price: ");
                        decimal price = decimal.Parse(Console.ReadLine() ?? "0");
                        await service.AddProductToCategoryAsync(categoryId, new Product { Title = productName, Price = price });
                        Console.WriteLine("Product added.");
                        break;

                    case "7":
                        Console.Write("Category ID: ");
                        int viewCatId = int.Parse(Console.ReadLine() ?? "0");
                        var products = await service.GetProductsByCategoryAsync(viewCatId);
                        foreach (var p in products)
                            Console.WriteLine($"- {p.Title} ({p.Price} lei)");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
