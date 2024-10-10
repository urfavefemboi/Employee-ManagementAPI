using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface ILuong:IGenericReposistory<Luong>
    {
       Task <bool> Add(DTOAddLuong model);
       
       Task <bool> Update(int id, DTOAddLuong model);
        Task<bool> Delete(int id);
        Task<IEnumerable<Luong>> GetallLuong();
        Task<Dictionary<int, List<DTONhanVien>>> GetNhanVienGrouByMucLuong();
        Task<DTOLuong?> GetbyId(int id);
    }
}
