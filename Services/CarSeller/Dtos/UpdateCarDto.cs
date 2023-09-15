using CarSeller.Entities;

namespace CarSeller.Dtos
{
    public class UpdateCarDto
    {
        public string Name { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Kilometeres { get; set; }

        public string Status { get; set; }
    }
}
