using EcoMeal.client.Models;

namespace EcoMeal.client.Services;

public class BusinessService
{
    private readonly HttpClient _http;

    public BusinessService(HttpClient http)
    {
        _http = http;
    }
    public async Task<List<BusinessModel>> GetAllAsync()
    {
        var businesses = await _http.GetFromJsonAsync<List<BusinessModel>>("/api/business");
        return businesses ?? new List<BusinessModel>();
    }
     public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"/api/business/{id}");
        return response.IsSuccessStatusCode;
    }
    
    public async Task<BusinessDetailsModel?> GetOneById(int id)
    {
        try
        {
            var business = await _http.GetFromJsonAsync<BusinessDetailsModel>($"api/business/{id}");
            return business;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task AddPackageToBusiness(int businessId, PackageAddModel package)
    {
        await _http.PostAsJsonAsync($"api/business/{businessId}/addPackage", package);
    }

    public async Task<List<PackageModel>> GetPackagesAsync(int businessId)
    {
        var packages = await _http.GetFromJsonAsync<List<PackageModel>>($"api/business/{businessId}/packages");
        return packages ?? new List<PackageModel>();
    }

    public async Task<List<PackageTypeModel>> GetPackageTypesAsync()
    {
        var packageTypes = await _http.GetFromJsonAsync<List<PackageTypeModel>>("api/business/packageTypes");
        return packageTypes ?? new List<PackageTypeModel>();
    }

    public async Task UpdatePackageAsync(int businessId, int packageId, PackageAddModel package)
    {
        await _http.PutAsJsonAsync($"api/business/{businessId}/packages/{packageId}", package);
    }

    public async Task<List<BusinessTypeModel>> GetBusinessTypesAsync()
    {
        var types = await _http.GetFromJsonAsync<List<BusinessTypeModel>>("api/business/businessTypes");
        return types ?? new List<BusinessTypeModel>();
    }

    public async Task CreateBusinessAsync(BusinessAddModel model)
    {
        await _http.PostAsJsonAsync("api/business", model);
    }

    public async Task UpdateBusinessAsync(int id, BusinessAddModel model)
    {
        await _http.PutAsJsonAsync($"api/business/{id}", model);
    }

    public async Task<bool> DeletePackageAsync(int businessId, int packageId)
    {
        var response = await _http.DeleteAsync($"api/business/{businessId}/packages/{packageId}");
        return response.IsSuccessStatusCode;
    }
}