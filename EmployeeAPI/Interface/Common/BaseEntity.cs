namespace EmployeeAPI.Interface.Common
{
    public abstract class BaseEntity
    {
    }
    public abstract class BaseEntity<T> : BaseEntity, IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
