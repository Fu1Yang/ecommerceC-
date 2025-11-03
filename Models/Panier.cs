namespace ecommerce.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int UserId { get; set; }       // pour relier le panier à l'utilisateur
        public int ProductId { get; set; }    // produit ajouté
        public string NameProduct { get; set; } // nom du produit 
        public int Quantity { get; set; }     // quantité
        public DateTime AddedAt { get; set; }

        public Panier() { }

        public Panier(int id, int userId, int productId,string nameProduct, int quantity, DateTime addedAt)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
            NameProduct = nameProduct;
            Quantity = quantity;
            AddedAt = addedAt;

        }
    }
}
