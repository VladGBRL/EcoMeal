using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public required int NumberOfPackages { get; set; }
        public required double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public required DateTime StartPickUp { get; set; }
        public required DateTime EndPickUp { get; set; }
        public required int BusinessTypeId { get; set; }
        public required BusinessType BusinessType { get; set; } = null!;
        public required int PackageTypeId { get; set; }
        public required PackageType PackageType { get; set; } = null!;
    }
}