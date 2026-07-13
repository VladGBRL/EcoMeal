using EcoMeal.client.Models;
using System.Net.Http.Json;

namespace EcoMeal.client.Services;

public class PackageTypeService
{
    private readonly HttpClient _http;

    public PackageTypeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PackageTypeModel>> GetPackageTypesAsync()
    {
        var packageTypes = await _http.GetFromJsonAsync<List<PackageTypeModel>>("api/business/packageTypes");
        return packageTypes ?? new List<PackageTypeModel>();
    }
}
