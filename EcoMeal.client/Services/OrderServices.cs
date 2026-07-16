using EcoMeal.client.Models;
using EcoMeal.client.Services;
using System.Net.Http.Headers;
namespace EcoMeal.client.Services;

public class OrderService
{
    private readonly HttpClient _http;
    private readonly AuthService _authService;
    public OrderService(HttpClient http,AuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    public async Task<bool> PlaceOrderAsync(int packageId)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/order")
            {
                Content = JsonContent.Create(new { PackageId = packageId })
            };
            await AddAuthHeaderAsync(request);

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in PlaceOrderAsync: {ex.Message}");
            return false;
        }
    }

    public async Task<List<OrderGetModel>> GetMyOrdersAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/order/my");
            await AddAuthHeaderAsync(request);

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return new List<OrderGetModel>();
            }

            var orders = await response.Content.ReadFromJsonAsync<List<OrderGetModel>>();
            return orders ?? new List<OrderGetModel>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in GetMyOrdersAsync: {ex.Message}");
            return new List<OrderGetModel>();
        }
    }


    private async Task AddAuthHeaderAsync(HttpRequestMessage request)
    {
        if (string.IsNullOrEmpty(_authService.Token))
        {
            await _authService.LoadTokenAsync();
        }

        if (!string.IsNullOrEmpty(_authService.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authService.Token);
        }
    }
}