using EmployeeAPI.Model.DTO;

namespace EmployeeAPI.Interface.ModelInterface
{
    public interface IReportService:ITinhLuong
    {
        Task<IEnumerable<DTONhanVien>> GetNhanVienNghiBySoNgay(int ngay);
        Task<IEnumerable<DTONhanVien>> FindNhanVienByName(string name);
        Task<Dictionary<string, List<DTONhanVien>>> GetAllNhanVienGroupByChucVu();
        Task<Dictionary<string, List<DTONhanVien>>> GetNhanVIenByPhongBan();
        Task<IEnumerable<DTOLunchShift>> GetAllLunchShiftByDayOver1hour(DateTime date);
        Task<Dictionary<int, int>> GetSoNgayNghiTrongThangByNHanVienId(int id,int nam);
       
    }
}
