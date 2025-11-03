namespace ecommerce.Models
{
   
    public class Rdv
    {
        public int Id { get; set; } // clé primaire obligatoire
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Registrations { get; set; }
        public string Comment { get; set; }


        public Rdv() { }
        public Rdv(DateTime date,string name, string registrations,string email, string commentaire)
        {
            this.Date = date;
            this.Name = name;
            this.Email = email;
            this.Registrations = registrations;
            this.Comment = commentaire;
        }

    }
}
