using BenchmarkDotNet.Attributes;
using CarSeller.Dtos;
using CarStore.Core.Models;
using CarStore.Services.Services;

namespace CarSeller.Extension
{
    [MemoryDiagnoser]
    public class CarServiceBenchmark
    {
        private readonly CarService _carService;
        private readonly Guid _carId;

        public CarServiceBenchmark(CarService carService, Guid carId)
        {
            _carService = carService;
            _carId = carId;
        }

        public async Task<Car> BenchMarkGetCar()
        {
            return await _carService.GetCarById(_carId);
        }
    }
}
