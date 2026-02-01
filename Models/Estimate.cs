namespace ecommerce.Models
{
    public class Estimate
    {
        public int Id { get; set; }

        public string Nom { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telephone { get; set; } = "";
        public string Adresse { get; set; } = "";

        public decimal TarifTotal { get; set; }
        public DateTime Date { get; set; }

        public string Intervention { get; set; } = "";
    }
}
