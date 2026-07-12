using EcoMeal.api.Application.Models;
using EcoMeal.api.Entities;
using EcoMeal.api.Infrastructure;
using EcoMeal.api.Application.Constants;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = UserRoles.Admin)]
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
                Packages = b.Packages.Select(p => new PackageDTO
                {
                    PackageId = p.PackageId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StartPickUp = p.StartPickUp,
                    EndPickUp = p.EndPickUp,
                    PackageTypeName = p.PackageType != null ? p.PackageType.Name : string.Empty
                }).ToList()
            })
            .FirstOrDefaultAsync(b => b.BusinessId == id);
        if (business is null)
        {
            return NotFound();
        }

        return Ok(business);
    }

    [HttpGet("{id}/packages")]
    public async Task<ActionResult<IEnumerable<PackageDTO>>> GetPackages(int id)
    {
        var packages = await _context.Packages
            .Where(p => p.BusinessId == id)
            .Include(p => p.PackageType)
            .Select(p => new PackageDTO
            {
                PackageId = p.PackageId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StartPickUp = p.StartPickUp,
                EndPickUp = p.EndPickUp,
                PackageTypeName = p.PackageType != null ? p.PackageType.Name : string.Empty
            })
            .ToListAsync();

        return Ok(packages);
    }

    [HttpGet("packageTypes")]
    public async Task<ActionResult<IEnumerable<PackageType>>> GetPackageTypes()
    {
        var packageTypes = await _context.PackageTypes
            .OrderBy(pt => pt.Name)
            .ToListAsync();

        return Ok(packageTypes);
    }

    [HttpGet("businessTypes")]
    public async Task<ActionResult<IEnumerable<BusinessType>>> GetBusinessTypes()
    {
        var businessTypes = await _context.BusinessTypes
            .OrderBy(bt => bt.Name)
            .ToListAsync();

        return Ok(businessTypes);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BusinessAddDTO dto)
    {
        var businessType = await _context.BusinessTypes.FindAsync(dto.BusinessTypeId);
        if (businessType is null)
            return BadRequest("Invalid business type.");

        var business = new Business
        {
            Name = dto.Name,
            Address = dto.Address,
            Contact = dto.Contact,
            Description = dto.Description,
            BusinessTypeId = dto.BusinessTypeId,
            BusinessType = businessType
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BusinessAddDTO dto)
    {
        var business = await _context.Businesses.FindAsync(id);
        if (business is null)
            return NotFound();

        business.Name = dto.Name;
        business.Address = dto.Address;
        business.Contact = dto.Contact;
        business.Description = dto.Description;
        business.BusinessTypeId = dto.BusinessTypeId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{businessId}/packages/{packageId}")]
    public async Task<IActionResult> DeletePackage(int businessId, int packageId)
    {
        var package = await _context.Packages
            .FirstOrDefaultAsync(p => p.PackageId == packageId && p.BusinessId == businessId);

        if (package is null)
            return NotFound();

        _context.Packages.Remove(package);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("{id}/addPackage")]
    public async Task<IActionResult> AddPackageToBusiness(int id, [FromBody] PackageAddDTO package)
    {
        var business = await _context.Businesses.FindAsync(id);
        if (business is null)
        {
            return NotFound();
        }

        var packageType = await _context.PackageTypes.FindAsync(package.PackageTypeId);
        if (packageType is null)
        {
            return BadRequest("Invalid package type.");
        }

        _context.Packages.Add(new Package
        {
            Name = package.Name,
            Description = package.Description,
            Price = package.Price,
            StartPickUp = package.StartPickup,
            EndPickUp = package.EndPickup,
            NumberOfPackages = 1,
            PackageTypeId = package.PackageTypeId,
            BusinessId = id,
            Business = business,
            PackageType = packageType
        });

        await _context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("{businessId}/packages/{packageId}")]
    public async Task<IActionResult> UpdatePackage(int businessId, int packageId, [FromBody] PackageAddDTO package)
    {
        var existingPackage = await _context.Packages
            .FirstOrDefaultAsync(p => p.PackageId == packageId && p.BusinessId == businessId);

        if (existingPackage is null)
        {
            return NotFound();
        }

        existingPackage.Name = package.Name;
        existingPackage.Description = package.Description;
        existingPackage.Price = package.Price;
        existingPackage.StartPickUp = package.StartPickup;
        existingPackage.EndPickUp = package.EndPickup;
        existingPackage.PackageTypeId = package.PackageTypeId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

}