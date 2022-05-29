using ProjectAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIdemo.Models
{
    public class Car
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        public int ReleaseYear { get; set; }

        [MaxLength(30)]
        public string Color { get; set; }

        public FuelType FuelType { get; set; }
    }
}
