using System.Collections.Generic;
namespace ShoppingList_Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Color { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }

    public static class Categories
    {
        public static List<Category> All = new List<Category>
        {
            new Category { Id = 1, Name = "Продукты", Color = "#4CAF50" },
            new Category { Id = 2, Name = "Бытовое", Color = "#2196F3" },
            new Category { Id = 3, Name = "Другое", Color = "#9E9E9E" }
        };
    }
}

