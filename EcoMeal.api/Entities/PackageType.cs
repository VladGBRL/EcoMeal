using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class PackageType
    {
        [Key]
        public int PackageTypeId { get; set; }
        public required string Name { get; set; } = string.Empty;
    } 
}