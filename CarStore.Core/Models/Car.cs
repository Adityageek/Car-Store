using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Core.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Kilometeres { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
