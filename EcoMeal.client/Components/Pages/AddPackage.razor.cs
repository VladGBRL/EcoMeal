using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.Pages
{
    public partial class AddPackage
    {
        [Parameter]
        public int BusinessId { get; set; }

        [Parameter]
        public int? PackageId { get; set; }

        [Inject]
        public required BusinessService BusinessService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        public PackageAddModel PackageAddModel { get; set; } = new PackageAddModel()
        {
            Name = string.Empty,
            Description = string.Empty,
            Price = 0,
            StartPickup = DateTime.Now,
            EndPickup = DateTime.Now.AddHours(2),
            PackageTypeId = 1
        };

        public List<PackageTypeModel> PackageTypes { get; set; } = new();
        public string StatusMessage { get; set; } = string.Empty;
        public string PageTitle => PackageId.HasValue ? "Edit package" : "Add package";
        public string ButtonText => PackageId.HasValue ? "Save changes" : "Add package";

        protected override async Task OnInitializedAsync()
        {
            PackageTypes = await BusinessService.GetPackageTypesAsync();

            if (PackageId.HasValue)
            {
                var existingPackage = (await BusinessService.GetPackagesAsync(BusinessId))
                    .FirstOrDefault(p => p.PackageId == PackageId.Value);

                if (existingPackage is not null)
                {
                    PackageAddModel = new PackageAddModel
                    {
                        Name = existingPackage.Name,
                        Description = existingPackage.Description,
                        Price = existingPackage.Price,
                        StartPickup = existingPackage.StartPickUp,
                        EndPickup = existingPackage.EndPickUp,
                        PackageTypeId = PackageTypes.FirstOrDefault(pt => pt.Name == existingPackage.PackageTypeName)?.PackageTypeId ?? 1
                    };
                }
            }
        }

        public async Task HandleSubmit()
        {
            if (PackageId.HasValue)
            {
                await BusinessService.UpdatePackageAsync(BusinessId, PackageId.Value, PackageAddModel);
                StatusMessage = "Package updated successfully.";
            }
            else
            {
                await BusinessService.AddPackageToBusiness(BusinessId, PackageAddModel);
                StatusMessage = "Package added successfully.";
            }

            NavigationManager.NavigateTo($"/business/{BusinessId}");
        }
    }
}