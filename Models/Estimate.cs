namespace ecommerce.Models
{
    public class Estimate
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public decimal TarifTotal { get; set; }
        public DateTime Date { get; set; }
        public Estimate() { }

        public Estimate(int id, string nom, string email, string telephone, string adresse, decimal tarifTotal, DateTime date)
        {
            Id = id;
            Nom = nom;
            Email = email;
            Telephone = telephone;
            Adresse = adresse;
            TarifTotal = tarifTotal;
            Date = date;
        }
    }

}
