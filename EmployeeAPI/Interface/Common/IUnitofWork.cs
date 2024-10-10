namespace EmployeeAPI.Interface.Common
{
    public interface IUnitofWork:IDisposable
    {
        Task<bool> CommitAsync();
    }
}
