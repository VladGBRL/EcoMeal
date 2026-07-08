using EcoMeal.client.Models;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.BusinessCard;

public partial class BusinessCard
{
    [Parameter]
    public required BusinessModel Business { get; set; }

    [Parameter]
    public EventCallback<int> OnDeleted { get; set; }
}