using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface INghiPhep:IGenericReposistory<NghiPhep>
    {
      Task <bool> Add(DTOAddNghiPhep model);
       
        Task<bool> Update(int id, DTOUpdateNghiPhep model);
        Task<bool> Delete(int id);
        Task<Dictionary<int, List<DTONghiPhep>>> GetAllNghiPhep();
        Task<Dictionary<int, List<DTONghiPhep>>> GetbyId(int id);
        Task<IEnumerable<DTONghiPhep>> GetNgayNghiPhepCoLuong(int id, bool status, int nam);
      Task<bool> NghiHuongLuongApproved(int nhanvienid, DTOUpdateHuongLuong approved);
      
        
    }
}
