using EcoMeal.api.Application.Models;
using EcoMeal.api.Entities;
using EcoMeal.api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.api.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessController: ControllerBase
{
    private readonly EcoMealDbContext _context;

    public BusinessController(EcoMealDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessDTO>>> GetAll()
    {
        var businessesDTOs = await _context.Businesses
            .Include(b => b.BusinessType)
            .Select(b => new BusinessDTO
            {
                BusinessId = b.BusinessId,
                Name  = b.Name,
                Address = b.Address,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name
            }).ToListAsync();

        return Ok(businessesDTOs);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var business = await _context.Businesses.FindAsync(id);
        if(business is null)
        {
            return NotFound();
        }

        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}