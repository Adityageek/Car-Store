using AutoMapper;
using CarBuyer.Data;
using Events;

namespace CarBuyer.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<CarCreated, Car>();
            CreateMap<CarUpdated, Car>();
        }
    }
}
