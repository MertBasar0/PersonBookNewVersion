using DataAccess.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual Task Dispose(bool  disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IGenRepository<T>> GetGenRepository<T>() where T : class
        {
            return await Task.FromResult(new GenericRepo<T>(_dbContext));
        }

        public Task RollBack()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    default:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
            return Task.CompletedTask;
        }
    }
}
