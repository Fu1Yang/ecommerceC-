namespace ecommerce.Models
{
    
    public class Profiles
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public int Age { get; set; }
        public string PathPhoto { get; set; }
        public string Adresse { get; set; }

        public Profiles() { }

        public Profiles(int id, int idUser, string name, string firstname, int age, string pathPhoto, string adresse)
        {
            Id = id;
            IdUser = idUser;
            Name = name;
            Firstname = firstname;
            Age = age;
            PathPhoto = pathPhoto;
            Adresse = adresse;
        }
    }
}
