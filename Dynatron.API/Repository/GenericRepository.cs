using Dynatron.API.Context;
using Dynatron.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dynatron.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CustomerContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(CustomerContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity                    
                    .AsNoTracking()
                    .ToListAsync();

            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity.FindAsync(id);
            return getById!;
        }        

        public async Task<bool> RegisterAsync(T entity)
        {
            await _context.AddAsync(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
        public async Task<bool> EditAsync(T entity)
        {            
            _context.Entry(entity).State = EntityState.Modified;

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            _context.Remove(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await _entity.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
            if (entity != null)
            {
                return true;
            }
                
            return false;

        }
    }
}
