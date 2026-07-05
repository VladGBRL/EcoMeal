using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
       
    }
}