namespace EcoMeal.client.Models;

public class BusinessDetailsModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string Adress { get; set; } = "";
    public string? Description { get; set; }
    public string Contact { get; set; } = "";
    public string BusinessTypeName { get; set; } = "";
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<PackageModel> Packages { get; set; } = new();
}