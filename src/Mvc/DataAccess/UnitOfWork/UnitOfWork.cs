using DataAccess.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PersonAppDbContext _dbContext;
        private DatabaseFacade _fakadeDb;
        private bool _disposed;

        public UnitOfWork(PersonAppDbContext dbContext)
        {
            _dbContext = dbContext;
            _fakadeDb = _dbContext.Database;
        }

        public async Task<IDbContextTransaction> BeginAsync()
        {
            return await _fakadeDb.BeginTransactionAsync();        
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await _fakadeDb.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await _fakadeDb.RollbackTransactionAsync();
                Dispose();
                throw new Exception(e.Message);
            }
        }

        protected virtual async Task DisposeAsync(bool  disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    await _dbContext.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public async void Dispose()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IGenRepository<T>> GetGenRepositoryAsync<T>() where T : class
        {
            return await Task.FromResult(new GenericRepo<T>(_dbContext));
        }

        public Task RollBackAsync()
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
