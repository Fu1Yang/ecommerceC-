using System.ComponentModel.DataAnnotations;

namespace ecommerce.DTO
{
    public class EstimateDto
    {
        [Required]
        public string Nom { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Telephone { get; set; } = "";

        [Required]
        public string Adresse { get; set; } = "";

        [Required]
        public string Intervention { get; set; }

        [Range(0, 10000)]
        public decimal TarifTotal { get; set; }

        public DateTime Date { get; set; }
    }
}
