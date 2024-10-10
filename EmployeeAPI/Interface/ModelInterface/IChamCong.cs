using EmployeeAPI.Interface.Common;
using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Model.DTOUpdate;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IChamCong:IGenericReposistory<ChamCong>
    {
      Task <bool> Create(DTOAddChamCong model);
       //Task <bool> Save();
       Task <bool> Edit(int id, DTOUpdateChamCong model);
       Task <bool> Delete(int id);
        Task<Dictionary<DateOnly, List<DTOChamCong>>> GetAllLichChamCong();

        Task<Dictionary<DateOnly, List<DTOChamCong>>> GetbyId(int id);
    }
}
