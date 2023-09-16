using AutoMapper;
using CarStore.Core.Interfaces;
using CarStore.Core.Models;
using CarStore.Services.Interface;
using Events;
using MassTransit;
using MassTransit.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Services.Services
{
    public class CarService : ICarService
    {
        public IUnitOfWork _unitOfWork;
        public IPublishEndpoint _publishEndpoint;
        public IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }
        public async Task<bool> CreateCar(Car cars)
        {
            if (cars != null)
            {
                await _unitOfWork.Cars.Add(cars);

                await _publishEndpoint.Publish(_mapper.Map<CarCreated>(cars));

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteCar(Guid id)
        {
            if (id != null)
            {
                var cars = await _unitOfWork.Cars.GetById(id);
                if (cars != null)
                {
                    _unitOfWork.Cars.Delete(cars);

                    await _publishEndpoint.Publish<CarDeleted>(new { Id = id });

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<Car> GetCarById(Guid id)
        {
             if (id != null)
            {
                var cars = await _unitOfWork.Cars.GetById(id);
                if (cars != null)
                {
                    return cars;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Car>> ListCars()
        {
            return await _unitOfWork.Cars.List();
        }

        public async Task<bool> UpdateCar(Car updateCarDto, Guid id)
        {
            if (updateCarDto != null)
            {
                var car = await _unitOfWork.Cars.GetById(id);
                if (car != null)
                {
                    car.Description = updateCarDto.Description ?? car.Description;
                    car.Name = updateCarDto.Name ?? car.Name;
                    car.Status = updateCarDto.Status != null ? EnumHelper.EnumParse(updateCarDto.Status.ToString(), car.Status) : car.Status;
                    car.Kilometeres = updateCarDto.Kilometeres == 0 ? car.Kilometeres : updateCarDto.Kilometeres;
                    car.Price = updateCarDto.Price == 0 ? car.Price : updateCarDto.Price;
                    car.ModelYear = updateCarDto.ModelYear == 0 ? car.ModelYear : updateCarDto.ModelYear;
                    car.Color = updateCarDto.Color ?? car.Color;
                    car.Image = updateCarDto.Image ?? car.Image;
                    car.UpdatedAt = DateTime.Now;

                    _unitOfWork.Cars.Update(car);

                    await _publishEndpoint.Publish(_mapper.Map<CarUpdated>(car));

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
