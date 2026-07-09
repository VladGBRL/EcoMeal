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
                    Address = existing.Adress,   // client model uses "Adress" spelling
                    Contact = existing.Contact,
                    Description = existing.Description ?? string.Empty,
                    BusinessTypeId = BusinessTypes
                        .FirstOrDefault(t => t.Name == existing.BusinessTypeName)?.BusinessTypeId ?? 0
                };
            }
        }
    }

    public async Task HandleSubmit()
    {
        if (Id.HasValue)
        {
            await BusinessService.UpdateBusinessAsync(Id.Value, Model);
        }
        else
        {
            await BusinessService.CreateBusinessAsync(Model);
        }

        NavigationManager.NavigateTo("/");
    }
}
