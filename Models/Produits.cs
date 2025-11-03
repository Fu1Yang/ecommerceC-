using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ecommerce.Models
{
    public class Produits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string ImagePath { get; set; }

        public Produits(){ }

        public Produits(string name, string description, decimal price, int stock, string imagePath)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImagePath = imagePath;
        }

    }

}
