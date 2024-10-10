namespace EmployeeAPI.Interface.Common
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; } 
    }
}
