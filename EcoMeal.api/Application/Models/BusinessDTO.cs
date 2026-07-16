using System.ComponentModel.DataAnnotations;

namespace EcoMeal.api.Application.Models
{
    public class BusinessDTO
    {
        public int BusinessId { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Address { get; set; } = string.Empty;
        public required string Contact { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required string BusinessTypeName {get; set;} = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}