using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class Business
    {
        [Key]
        public int BusinessId { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty;

        public required string Contact { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required int BusinessTypeId { get; set; }
        public required BusinessType BusinessType { get; set; } = null!;
        public ICollection<Package> Packages { get; set; } = new List<Package>();
    }
}