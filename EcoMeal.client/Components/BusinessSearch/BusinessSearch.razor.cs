using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.BusinessSearch;

public partial class BusinessSearch
{
    [Inject]
    public required BusinessService BusinessService { get; set; }

    private string searchQuery = string.Empty;
    private string lastQuery = string.Empty;
    private List<BusinessModel>? results;
    private bool isLoading;
    private bool isSearchActive;

    private async Task ExecuteSearch()
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return;

        lastQuery = searchQuery;
        isSearchActive = true;
        isLoading = true;
        results = null;

        results = await BusinessService.SearchAsync(searchQuery);
        isLoading = false;
    }

    private async Task HandleKeyUp(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
            await ExecuteSearch();
    }

    private void ClearSearch()
    {
        searchQuery = string.Empty;
        lastQuery = string.Empty;
        results = null;
        isSearchActive = false;
    }
}
