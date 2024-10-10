using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IHopDong:IGenericReposistory<HopDong>
    {
        Task<bool> AddHopDong(DTOAddHopDOng model);
        Task<bool> UpdateHopDong(int id, DTOUpdateHopdong model);
        Task<bool> DeleteHopDong(int id);
        Task<IEnumerable<DTOHopdong>> GetAllByStatus(bool status);
        Task<Dictionary<int, List<DTOHopdong>>> GetHopDongByNhanVien();
        Task<DTOHopdong> GetbyId(int id);
    }
}
