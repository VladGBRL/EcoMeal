using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.BusinessList;

public partial class BusinessList
{
    [Inject]
    public required BusinessService BusinessService {get; set;}

    private List<BusinessModel>? Businesses {get; set;}

    protected override async Task OnInitializedAsync()
    {
        Businesses = await BusinessService.GetAllAsync();
    }

    private async Task HandleBusinessDeleted(int businessId)
    {
        Console.WriteLine($"BusinessList.HandleBusinessDeleted: {businessId}");
        Businesses = await BusinessService.GetAllAsync();
        StateHasChanged();
    }

}