using CarSeller.Dtos;
using CarSeller.Entities;
using AutoMapper;
using Events;

namespace CarSeller.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<Car, CarDto>();
            CreateMap<CreateCarDto, Car>();
            CreateMap<CarDto, CarCreated>();
            CreateMap<Car, CarUpdated>();
        }
    }
}
