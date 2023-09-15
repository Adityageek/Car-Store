namespace Events
{
    public class CarCreated
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Kilometeres { get; set; }

        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
