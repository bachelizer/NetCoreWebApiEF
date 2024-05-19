namespace NZWalks.Api.Models.DTO;

public class UpdateRegionRequestDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? RegionImageUrl { get; set; }
}