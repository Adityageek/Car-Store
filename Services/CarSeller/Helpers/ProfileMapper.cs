using CarSeller.Dtos;
using AutoMapper;
using Events;
using CarStore.Core.Models;

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
            CreateMap<UpdateCarDto, Car>();
        }
    }
}
