
using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Interface.Common
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly EmployeeDBcontext _context;
        public UnitOfWork(EmployeeDBcontext context)
        {
            _context = context;
        }
        public async Task<bool> CommitAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _context.SaveChangesAsync();
                    await transaction.CommitAsync(); // Commit transaction nếu thành công
                    return result > 0 ;
                }
                catch
                {
                    await transaction.RollbackAsync(); // Rollback transaction nếu có lỗi
                    throw;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
