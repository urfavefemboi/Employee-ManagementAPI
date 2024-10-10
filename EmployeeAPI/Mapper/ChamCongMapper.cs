using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Mapper
{
    public static class ChamCongMapper
    {
        public static DTOChamCong toDTOChamCong(this ChamCong model)
        {
            return new DTOChamCong
            {
                Id = model.Id,
                NhanVienId = model.NhanVienId,
                Clockin = model.Clockin,
                ClockOut = (TimeSpan)model.ClockOut,
                NgayChamCong = DateOnly.FromDateTime(model.NgayChamCong)
            };
        }
        public static ChamCong toDTOAddChamCong(this DTOAddChamCong model)
        {
            return new ChamCong
            {
                NhanVienId = model.NhanVienId,
                Clockin = (TimeSpan)model.Clockin,
                ClockOut = model.ClockOut,
                NgayChamCong =model.NgayChamCong
            };
        }
    }
}
