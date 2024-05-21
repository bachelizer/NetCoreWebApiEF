using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    public RegionsController(NZWalksDbContext dbContext, IRegionRepository region, IMapper mapper)
    {
        _dbContext = dbContext;
        _regionRepository = region;
        _mapper = mapper;
    }

    // GET: https:localhost:port/api/regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get Data from database - domain model
        var regions = await _regionRepository.GetAllAsync();

        // Map Domain Models to Dto
        var regionsDto = _mapper.Map<List<RegionDto>>(regions);

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
        var region = await _regionRepository.GetByIdAsync(id); // LinQ method.
        if (region == null) return NotFound();

        // Map Region Domain Model to RegionDTO
        var regionDto = _mapper.Map<RegionDto>(region);

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
        var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

        // Use Domain Model to create Region
        regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

        // Map Domain model back to DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // PUT: https://localhost:port/api/regions/{id}
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // Map update region Dto to Region
        var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

        regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

        if (regionDomainModel == null) return NotFound();

        // Convert Domain model to DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return Ok(regionDto);
    }

    // DELETE: https://localhost:port/api/regions/{id}
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var regionDomainModel = await _regionRepository.DeleteAsync(id);

        if (regionDomainModel == null) return NotFound();

        // return deleted region back
        // map Domain Model to Dto
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return Ok(regionDto);
    }
}