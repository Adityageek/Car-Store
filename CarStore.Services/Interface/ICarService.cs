using CarStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Services.Interface
{
    public interface ICarService
    {

        Task<bool> CreateCar(Car cars);

        Task<IEnumerable<Car>> ListCars();

        Task<Car> GetCarById(Guid id);

        Task<bool> UpdateCar(Car cars, Guid id);

        Task<bool> DeleteCar(Guid id);
    }
}
