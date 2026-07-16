using EcoMeal.api.Entities;
using EcoMeal.api.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.api.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly EcoMealDbContext _context;
    private readonly UserManager<User> _userManager;

    public OrderController(EcoMealDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var package = await _context.Packages.FindAsync(request.PackageId);
        if (package == null)
            return NotFound("Package not found.");

        if (package.NumberOfPackages <= 0)
            return BadRequest("No packages available left.");

        package.NumberOfPackages -= 1;

        var order = new Order
        {
            Status = "0", 
            OrderDate = DateTime.UtcNow,
            PackageId = request.PackageId,
            Package = package,
            UserId = user.Id
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOrders()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var orders = await _context.Orders
            .Include(o => o.Package)
                .ThenInclude(p => p.Business)
            .Where(o => o.UserId == user.Id)
            .Select(o => new
            {
                Id = o.OrderId,
                PackageName = o.Package.Name,
                Status = 0,
                Price = o.Package.Price,
                BusinessId = o.Package.BusinessId,
                BusinessName = o.Package.Business != null ? o.Package.Business.Name : string.Empty,
                Date = o.OrderDate,
                UserName = user.Name,
                UserContact = user.Contact
            })
            .ToListAsync();

        return Ok(orders);
    }
}

public class PlaceOrderRequest
{
    public int PackageId { get; set; }
}
