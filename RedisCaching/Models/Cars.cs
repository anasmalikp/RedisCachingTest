using System.ComponentModel.DataAnnotations;

namespace RedisCaching.Models
{
    public class Cars
    {
        [Key]
        public int? Id { get; set; }
        public string CarName { get; set; }
        public string Manufacturer { get; set; }
    }
}
