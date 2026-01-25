namespace ecommerce.Models
{
    public class Panier
    {
        public int Id { get; set; }
        public int UserId { get; set; }       
        public int ProductId { get; set; }   
        public string NameProduct { get; set; } 
        public int Quantity { get; set; }    
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
