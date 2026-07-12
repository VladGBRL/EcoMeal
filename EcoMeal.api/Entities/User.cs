using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EcoMeal.api.Entities
{
    public class User : IdentityUser<int>
    {
       
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
       
    }
}