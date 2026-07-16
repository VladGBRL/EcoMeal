using EcoMeal.client.Models;
using System.Net.Http.Json;

namespace EcoMeal.client.Services;

public class PackageService
{
    private readonly HttpClient _http;

    public PackageService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> AddPackageToBusiness(int businessId, PackageAddModel package)
    {
        var response = await _http.PostAsJsonAsync($"api/business/{businessId}/addPackage", package);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<PackageModel>> GetPackagesAsync(int businessId)
    {
        var packages = await _http.GetFromJsonAsync<List<PackageModel>>($"api/business/{businessId}/packages");
        return packages ?? new List<PackageModel>();
    }

    public async Task<bool> UpdatePackageAsync(int businessId, int packageId, PackageAddModel package)
    {
        var response = await _http.PutAsJsonAsync($"api/business/{businessId}/packages/{packageId}", package);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletePackageAsync(int businessId, int packageId)
    {
        var response = await _http.DeleteAsync($"api/business/{businessId}/packages/{packageId}");
        return response.IsSuccessStatusCode;
    }
}