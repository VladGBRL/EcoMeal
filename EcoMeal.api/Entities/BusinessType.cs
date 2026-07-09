using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class BusinessType
    {
        [Key]
        public int BusinessTypeId { get; set; }
        public required string Name { get; set; } = string.Empty;
    }
}