using CarStore.Core.Interfaces;
using CarStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Infrastructure.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(DbContextClass dbContext) : base(dbContext)
        {
        }
    }
}
