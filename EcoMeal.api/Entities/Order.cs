using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public required String Status { get; set; } = string.Empty;
        public required DateTime OrderDate { get; set; }
        public required int PackageId { get; set; }
        public required Package Package { get; set; } = null!;
        public required int UserId { get; set; }
        public required User User { get; set; } = null!;
    }
}