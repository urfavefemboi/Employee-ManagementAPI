using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface ITHanhToan:IGenericReposistory<ThanhToan>
    {
       Task<bool> Add(DTOAddThanhToan model);
        //Task<bool> Save();
        Task<bool> Update(DTOAddThanhToan model);
        Task<bool> Delete(int id);
        Task<IEnumerable<DTOThanhToan>> GetAllThanhToan();
        Task<IEnumerable<DTOThanhToan>> GetbyNhanVienId(int id);

    }
}
