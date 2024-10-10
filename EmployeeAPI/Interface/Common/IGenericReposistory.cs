namespace EmployeeAPI.Interface.Common
{
    public interface IGenericReposistory<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);     
        void Delete(T entity);
        void Update(T entity);
        Task Add(T entity);

    }
}
