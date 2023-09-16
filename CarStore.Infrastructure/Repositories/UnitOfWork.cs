using CarStore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContextClass;
        public ICarRepository Cars { get; }

        public UnitOfWork(DbContextClass dbContextClass, ICarRepository cars)
        {
            _dbContextClass = dbContextClass;
            Cars = cars;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _dbContextClass.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContextClass.Dispose();
            }
        }
    }
}
