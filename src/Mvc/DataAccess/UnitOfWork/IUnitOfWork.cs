using DataAccess.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        Task RollBack();
        Task<IGenRepository<T>> GetGenRepository<T>() where T : class;
    }
}
