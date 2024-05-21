using AutoMapper;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Mappings;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
    }

}




/** 
* If the two models has not the same property we have to map member to from model
* Below is the example
*/

/* public AutoMapperProfiles()
    {
        CreateMap<UserDto, UserDomain>()
        .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
        // ReverseMap (Inter-change of TSource and TDestination Generic Model)
        .ReverseMap();
    }
public class UserDto 
{
    public string FullName { get; set; }  
}
public class UserDomain {
    public string Name { get; set; }
} */