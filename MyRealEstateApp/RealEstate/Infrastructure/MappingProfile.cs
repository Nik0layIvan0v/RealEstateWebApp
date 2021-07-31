using AutoMapper;
using RealEstate.Services.Models;
using RealEstate.Models.Estates;

namespace RealEstate.Infrastructure
{
  
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<EstateFormModel, EstateServiceModel>().ReverseMap();
        }
    }
}
