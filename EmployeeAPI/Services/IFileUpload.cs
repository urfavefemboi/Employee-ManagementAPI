namespace EmployeeAPI.Services
{
    public interface IFileUpload
    {
        Task<string> Upload(IFormFile file);
    }
}
