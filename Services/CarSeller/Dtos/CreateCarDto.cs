using System.ComponentModel.DataAnnotations;

namespace CarSeller.Dtos
{
    public class CreateCarDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ModelYear { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public double Kilometeres { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
