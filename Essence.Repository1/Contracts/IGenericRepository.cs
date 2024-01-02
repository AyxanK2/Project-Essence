using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAll();

        public Task<T> GetId(int id);

        public Task AddAsync(T entity);

        public void Delete(T entity);

        public bool IsExits(int id);

        public Task Save();
    }

}
