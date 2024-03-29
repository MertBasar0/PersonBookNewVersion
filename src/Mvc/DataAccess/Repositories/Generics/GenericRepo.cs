using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Generics
{
    public class GenericRepo<TEntity> : IGenRepository<TEntity> where TEntity : class
    {
        private readonly PersonAppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepo(DbContext context)
        {
            _context = (PersonAppDbContext)context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }
    }
}
