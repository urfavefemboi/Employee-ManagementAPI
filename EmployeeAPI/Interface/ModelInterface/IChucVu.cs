using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IChucVu:IGenericReposistory<ChucVu>
    {

       Task<bool> Add(DTOAddChucVu model);
       
        Task<bool> Update(int id, DTOAddChucVu model);
        Task<bool> Delete(int id);
        Task<IEnumerable<DTOChucVu>> GetallChucVu();
       
        Task<IEnumerable<DTONhanVien>> GetNhanVienByChucVuId(int id);
        Task<DTOChucVu> GetbyId(int id);
    }
}
