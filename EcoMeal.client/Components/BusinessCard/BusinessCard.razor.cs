using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.BusinessCard;

public partial class BusinessCard
{
    [Parameter]
    public required BusinessModel Business {get; set;}
    [Inject]
    public required BusinessService BusinessService {get; set;}
}