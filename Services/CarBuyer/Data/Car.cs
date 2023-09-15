﻿using MongoDB.Entities;

namespace CarBuyer.Data
{
    public class Car : Entity
    {
        public string Name { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Kilometeres { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
