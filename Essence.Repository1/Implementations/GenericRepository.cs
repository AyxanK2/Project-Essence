using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            entity.DeletedAt = DateTime.Now;
        }

        public async Task<T> GetId(int id)
        {
            return await _context.Set<T>().Where(x => x.DeletedAt == null).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().Where(x => x.DeletedAt == null).ToListAsync();
        }

        public bool IsExits(int id)
        {
            return _context.Set<T>().Any(x => x.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
