using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.BusinessDeleteButton;

public partial class BusinessDeleteButton
{
    [Inject]
    public required BusinessService BusinessService { get; set; }

    [Inject]
    public required ToastService ToastService { get; set; }

    [Parameter]
    public int BusinessId { get; set; }

    [Parameter]
    public EventCallback<int> OnDeleted { get; set; }

    private bool _isDeleting;
    private bool _hasError;
    private bool _hasSuccess;

    private async Task HandleDeleteAsync()
    {
        Console.WriteLine($"Delete button clicked for business {BusinessId}");
        if (_isDeleting)
        {
            return;
        }

        _isDeleting = true;
        _hasError = false;
        _hasSuccess = false;

        try
        {
            var ok = await BusinessService.DeleteAsync(BusinessId);
            Console.WriteLine($"Delete result for {BusinessId}: {ok}");
            if (ok)
            {
                Console.WriteLine($"Invoking delete callback for {BusinessId}");
                _hasSuccess = true;
                ToastService.ShowSuccess("Business deleted successfully!");
                if (OnDeleted.HasDelegate)
                {
                    await OnDeleted.InvokeAsync(BusinessId);
                }
            }
            else
            {
                _hasError = true;
                ToastService.ShowError("Failed to delete business.");
                Console.WriteLine($"Delete failed for {BusinessId}");
            }
        }
        catch (Exception ex)
        {
            _hasError = true;
            ToastService.ShowError($"Error deleting business: {ex.Message}");
            Console.WriteLine($"Delete exception for {BusinessId}: {ex.Message}");
        }
        finally
        {
            _isDeleting = false;
        }
    }
}