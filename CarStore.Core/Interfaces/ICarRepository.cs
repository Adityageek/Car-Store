using CarStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Core.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
    }
}
