using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IlunchShift:IGenericReposistory<LunchShift>
    {
       Task<bool> Add(DTOAddLunchShift model);
        Task<bool> Remove(int lunchshiftId);
        Task<IEnumerable<DTOLunchShift>> GetAllShiftByDay(DateTime date);
        Task<Dictionary<DateOnly, List<DTOLunchShift>>> GetbyNhanVienId(int id);
        Task<Dictionary<DateOnly,List<DTOLunchShift>>> GetAllGroupByDate();
       
        
    }
}
