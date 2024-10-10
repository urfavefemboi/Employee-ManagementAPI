using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Mapper
{
    public static class HopDongMapper
    {
        public static DTOHopdong toDTOHopDong (this HopDong model)
        {
            return new DTOHopdong
            {
                HanHD = model.HanHD,
                NgayKyHD = model.NgayKyHD,
                HieuLuc = model.HieuLuc,
                NhanVienId = model.NhanVienId,
                Id = model.Id
            };
        }
        public static HopDong toDTOAddHopDong(this DTOAddHopDOng model)
        {
            return new HopDong
            {
                HanHD = Convert.ToDateTime(model.HanHD),
                NgayKyHD = Convert.ToDateTime(model.NgayKyHD),
                HieuLuc = model.HieuLuc,
                NhanVienId = model.NhanVienId,
               
            };
        }
    }
}
