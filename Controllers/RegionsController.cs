using System;
using System.Reflection;
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

    // GET: https:localhost:port/api/regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        // Get Data from database - domain model
        var regions = await _dbContext.Regions.ToListAsync();

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
    // GET: https://localhost:port/api/regions/{id}
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // var region = _dbContext.Regions.Find(id); // EF method

        // Get Region Doamin Model from Database
        var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); // LinQ method.
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

    // POST: https://localhost:port/api/regions
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Map or Convert DTO to Domain Model
        var regionDomainModel = new Region
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };

        // Use Domain Model to create Region
       await  _dbContext.Regions.AddAsync(regionDomainModel);

        // Save the data to database
        await _dbContext.SaveChangesAsync();

        // Map Domain model back to DTO
        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // PUT: https://localhost:port/api/regions/{id}
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // Check if the region exists
        var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if(regionDomainModel == null) return NotFound();

        // Map DTO to domain model
        regionDomainModel.Code = updateRegionRequestDto.Code;
        regionDomainModel.Name = updateRegionRequestDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

        await _dbContext.SaveChangesAsync();

        // Convert Domain model to DTO
        var regionDto = new RegionDto{
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };

        return Ok(regionDto);
    }

    // DELETE: https://localhost:port/api/regions/{id}
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (regionDomainModel == null) return NotFound();

        // Delete region
        _dbContext.Regions.Remove(regionDomainModel);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}