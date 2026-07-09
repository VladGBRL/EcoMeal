using EcoMeal.api.Application.Models;

public class BusinessDetailsDTO : BusinessDTO
{
    public List<PackageDTO> Packages { get; set; } = new();
}