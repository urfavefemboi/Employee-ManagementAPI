using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IPhongBan:IGenericReposistory<PhongBan>
    {
       Task <bool> Add(DTOAddPhongBan model);
        Task<bool> Save();
        Task<bool> Update(int id, DTOAddPhongBan model);
        Task<bool> Delete(int id);
        Task<IEnumerable<DTOPhongBan>> GetAllPhongBan();
        Task<IEnumerable<DTONhanVien>> GetAllNhanVienById(int id);
        Task<Dictionary<string, int>> GetSoLuongNhanVienTungPhongBan();
        Task<DTOPhongBan> GetbyId(int id);
    }
}
