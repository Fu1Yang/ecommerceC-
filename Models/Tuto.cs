namespace ecommerce.Models
{
    public class Tuto
    {
        public int Id { get; set; }
        public string TitleTuto { get; set; }
        public string Description { get; set;}
        public string Url { get; set; }

        public Tuto() { }

        public Tuto(string title, string description, string url) 
        {
            TitleTuto = title;
            Description = description;
            Url = url;
        }
    }
}
