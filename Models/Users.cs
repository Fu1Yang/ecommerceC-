namespace ecommerce.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string Role { get; set; }


        // Constructeur vide (requis par EF Core)
        public Users() { }

        // Constructeur avec paramètres
        public Users(string name, string email, string password, string phone, bool emailConfirmed, string role = "User")
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            EmailConfirmed = emailConfirmed;
            Role = role;
        }

    }
}
