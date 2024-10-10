using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;

namespace EmployeeAPI.Interface
{
    public interface INhanVien:IGenericReposistory<NhanVien>
    {
       Task <bool> Add(DTOAddNhanVien model);
        Task<bool> Update(int id, DTOUpdateNhanVien model);
        Task<bool> Delete(int id);

        Task<DTONhanVien> GetByNhanVienId(int id);
        Task<IEnumerable<DTONhanVien>> GetAll(CancellationToken canceltoken); 
        Task<IEnumerable<DTOHopdong>> GetHopDongByNhanVienId(int id);
        Task<IEnumerable<DTOChamCong>> GetChamCongByNhanVienId(int id);     
        Task<IEnumerable<DTONghiPhep>> GetNghiPhepByNhanVienId(int id);
       
    }
}
