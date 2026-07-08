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
        [HttpGet("{id}")]
    public async Task<ActionResult<BusinessDetailsDTO>> GetOneById(int id)
    {
        var business = await _context.Businesses
            .Include(b => b.Packages)
            .ThenInclude(p => p.PackageType)
            .Select(b => new BusinessDetailsDTO
            {
                BusinessId = b.BusinessId,
                Name = b.Name,
                Address = b.Address,
                Description = b.Description,
                Contact = b.Contact,
                BusinessTypeName = b.BusinessType.Name,
            })
            .FirstOrDefaultAsync(b => b.BusinessId == id);
        if (business is null)
        {
            return NotFound();
        }

        return Ok(business);
    }
     [HttpPost]
    [Route("{id}/addPackage")]
    public async Task<IActionResult> AddPackageToBusiness(int id, [FromBody] PackageAddDTO package)
    {
        _context.Packages.Add(new Package
        {
            Name = package.Name,
            Description = package.Description,
            Price = (double)package.Price,
            StartPickUp = package.StartPickup,
            EndPickUp = package.EndPickup,
            NumberOfPackages = 1,
            PackageTypeId = package.PackageTypeId,
            BusinessId = id,
        });
        await _context.SaveChangesAsync();
        return Created();
    }

}