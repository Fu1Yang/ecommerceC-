namespace ecommerce.Models
{
   
    public class Rdv
    {
        // clé primaire obligatoire
        public int Id { get; set; }
        public int IdUser { get; set; }

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Registrations { get; set; }
        public string Comment { get; set; }


        public Rdv() { }
        public Rdv(DateTime date,int id, int idUser, string name, string registrations,string email, string commentaire)
        {
            this.Date = date;
            this.Id = id;
            this.IdUser = idUser;
            this.Name = name;
            this.Email = email;
            this.Registrations = registrations;
            this.Comment = commentaire;
        }

    }
}
