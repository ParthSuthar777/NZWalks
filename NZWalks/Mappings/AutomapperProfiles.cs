using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            //CreateMap<UserDto,UserDomain>()
            //    .ForMember(x=>x.Name,opt => opt.MapFrom(x=>x.FullName))
            //    .ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();
            CreateMap<AddWalksRequestDto, Walks>().ReverseMap();
            CreateMap<Walks,WalksDto>().ForMember(x=>x.RegionName,opt=>opt.MapFrom(x=>x.Region.Name))
                .ForMember(x=>x.DifficultyName,opt=>opt.MapFrom(x=>x.Difficulty.Name)).ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalksRequestDto, Walks>().ReverseMap();
        }
    }
    //public class UserDto
    //{
    //    public string FullName { get; set; }
    //}
    //public class UserDomain
    //{
    //    public string Name { get; set; }
    //}

}
