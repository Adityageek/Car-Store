using AutoMapper;
using CarStore.Core.Models;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Services.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() 
        {
            CreateMap<Car, CarCreated>();
            CreateMap<Car, CarUpdated>();
        }
        
    }
}
