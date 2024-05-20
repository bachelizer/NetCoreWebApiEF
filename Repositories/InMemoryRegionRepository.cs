/**
* This is for an example, it is use for if you have to change the data source to 
* in memory, you will use the same implementation interface
* this is a kind of just updating the repository but implementation is the same as is
*
*/

using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public class InMemoryRegionRepository // : IRegionRepository
{
    public async Task<List<Region>> GetAllAsync()
    {
        return new List<Region>
        {
            new Region()
            {
                Id = Guid.NewGuid(),
                Code = "SAM",
                Name = "Test Lang"
            }
        };
    }
}