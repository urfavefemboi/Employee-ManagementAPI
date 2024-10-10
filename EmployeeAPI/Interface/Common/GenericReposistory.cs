
using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Interface.Common
{
    public class GenericReposistory<T> : IGenericReposistory<T> where T:class
    {
        protected readonly EmployeeDBcontext _dbcontext;
        protected readonly DbSet<T> _dbSet;
        public GenericReposistory(EmployeeDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
            _dbSet= _dbcontext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public  IQueryable<T> GetAll()
        {
            return  _dbSet.AsNoTrackingWithIdentityResolution();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
             _dbSet.Update(entity);
        }
    }
}
