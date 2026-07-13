using EcoMeal.client.Models;
using EcoMeal.client.Services;
using Microsoft.AspNetCore.Components;

namespace EcoMeal.client.Components.Pages.Orders;

public partial class Orders
{
    [Inject]
    public required OrderService OrderService { get; set; }
    private List<OrderGetModel>? MyOrders { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            MyOrders = await OrderService.GetMyOrdersAsync();
            StateHasChanged();
        }
        
    }
}