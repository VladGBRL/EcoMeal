namespace EcoMeal.api.Application.Models;

public class BusinessAddDTO
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Contact { get; set; }
    public string Description { get; set; } = string.Empty;
    public required int BusinessTypeId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
