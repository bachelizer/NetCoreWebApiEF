using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _dbContext;
    public RegionsController(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        /*  var regions = new List<Region>
         {
             new Region { Id = Guid.NewGuid(),
             Name = "Auckland Region",
             Code = "AKL",
             RegionImageUrl = ""},
              new Region { Id = Guid.NewGuid(),
             Name = "Wellington Region",
             Code = "WLG",
             RegionImageUrl = ""}
         }; */

        // Get Data from database - domain model
        var regions = _dbContext.Regions.ToList();

        // Map Domain Models to DTOs
        var regionsDto = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            });
        }
        // return DTOs
        return Ok(regionsDto);
    }

    // if you want to use not from route [FromRoute]
    // [HttpGet("{id}")]
    // public IActionResult GetById(Guid Id)
    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        // var region = _dbContext.Regions.Find(id); // EF method

        // Get Region Doamin Model from Database
        var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id); // LinQ method.
        if (region == null) return NotFound();

        // Map Region Domain Model to RegionDTO

        var regionDto = new RegionDto
        {

            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        // return
        // return Ok(region);
        // return DTOs
        return Ok(regionDto);
    }
}