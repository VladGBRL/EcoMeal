using System.ComponentModel.DataAnnotations;

namespace EcoMeal.client.Models;

public class BusinessAddModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact is required.")]
    public string Contact { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select a business type.")]
    public int BusinessTypeId { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
