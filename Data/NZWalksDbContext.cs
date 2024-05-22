using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    // seeding data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for difficulties
        // Easy, Medium, Hard

        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950f"),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                Name = "Hard"
            },
        };

        // Seed difficulties to database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);
    
        // Seed data for regions
        var regions = new List<Region>
        {
            new Region{
                Id = Guid.Parse("7c9e6679-7325-40de-944b-e07fc1f90ae7"),
                Name = "Auckland",
                Code = "AKL",
                RegionImageUrl = "AKL.png"
            },
            new Region{
                Id = Guid.Parse("7c9e6679-7425-40df-944b-e07fc1f90ae7"),
                Name = "SouthLand",
                Code = "STL",
                RegionImageUrl = "STL.png"
            },
        };

        // Seed regions to database
        modelBuilder.Entity<Region>().HasData(regions);
    }
}