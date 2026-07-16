using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.Pages;

public partial class AddBusiness
{
    [Parameter]
    public int? Id { get; set; }

    [Inject]
    public required BusinessService BusinessService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required ToastService ToastService { get; set; }
    public const string MapboxToken = "My Token";
    public BusinessAddModel Model { get; set; } = new();
    public List<BusinessTypeModel> BusinessTypes { get; set; } = new();
    public string StatusMessage { get; set; } = string.Empty;

    public string PageTitle => Id.HasValue ? "Edit business" : "Add business";
    public string ButtonText => Id.HasValue ? "Save changes" : "Add business";
    protected override async Task OnInitializedAsync()
    {
        BusinessTypes = await BusinessService.GetBusinessTypesAsync();

        if (Id.HasValue)
        {
            var existing = await BusinessService.GetOneById(Id.Value);
            if (existing is not null)
            {
                Model = new BusinessAddModel
                {
                    Name = existing.Name,
                    Address = existing.Adress,   
                    Contact = existing.Contact,
                    Description = existing.Description ?? string.Empty,
                    BusinessTypeId = BusinessTypes
                        .FirstOrDefault(t => t.Name == existing.BusinessTypeName)?.BusinessTypeId ?? 0,
                    Latitude = existing.Latitude,
                    Longitude = existing.Longitude
                };
            }
        }
    }

    public void HandleLocationChanged((double Lat, double Lng) location)
    {
        Model.Latitude = location.Lat;
        Model.Longitude = location.Lng;
    }

    public void HandleLocationCleared()
    {
        Model.Latitude = null;
        Model.Longitude = null;
    }
    public async Task HandleSubmit()
    {
        bool success;
        if (Id.HasValue)
        {
            success = await BusinessService.UpdateBusinessAsync(Id.Value, Model);
            if (success)
            {
                ToastService.ShowSuccess("Business updated successfully!");
            }
            else
            {
                ToastService.ShowError("Failed to update business.");
            }
        }
        else
        {
            success = await BusinessService.CreateBusinessAsync(Model);
            if (success)
            {
                ToastService.ShowSuccess("Business added successfully!");
            }
            else
            {
                ToastService.ShowError("Failed to add business.");
            }
        }

        if (success)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
