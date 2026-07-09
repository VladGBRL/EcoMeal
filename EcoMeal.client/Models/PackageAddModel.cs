using System.ComponentModel.DataAnnotations;
namespace EcoMeal.client.Models;

public class PackageAddModel
{
    [Required(ErrorMessage = "The name is mandatory!")]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required(ErrorMessage = "The description is mandatory!")]
    public required string Description { get; set; }
    [Required]
    [Range(0, 1000)]
    public double Price { get; set; }
    [Required]
    public DateTime StartPickup { get; set; }
    [Required]
    public DateTime EndPickup { get; set; }
    [Required]
    public int PackageTypeId { get; set; }
}