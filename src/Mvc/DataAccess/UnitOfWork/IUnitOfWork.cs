using DataAccess.Repositories.Generics;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginAsync();
        Task CommitAsync();
        Task RollBackAsync();
        Task<IGenRepository<T>> GetGenRepositoryAsync<T>() where T : class;
    }
}
