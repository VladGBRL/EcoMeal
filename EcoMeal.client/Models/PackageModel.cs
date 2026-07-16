namespace EcoMeal.client.Models;

public class PackageModel
{
    public int PackageId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime StartPickUp { get; set; }
    public DateTime EndPickUp { get; set; }
    public string PackageTypeName { get; set; } = string.Empty;
    public int NumberOfPackages { get; set; }
}
