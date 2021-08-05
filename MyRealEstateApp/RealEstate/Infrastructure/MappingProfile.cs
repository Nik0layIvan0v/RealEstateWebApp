using AutoMapper;
using RealEstate.Services.Models;
using RealEstate.Models.Estates;
using RealEstate.Models;

namespace RealEstate.Infrastructure
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<EstateFormModel, EstateServiceModel>().ReverseMap();
            this.CreateMap<City, CityModel>().ReverseMap();
            this.CreateMap<Area, AreaModel>().ReverseMap();
        }
    }
}
