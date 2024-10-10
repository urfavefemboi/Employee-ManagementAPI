using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Mapper
{
    public static class NghiPhepMapper
    {
        public static DTONghiPhep toDTONghiPhep(this NghiPhep model)
        {
            return new DTONghiPhep
            {
                Id = model.Id,
                HuongLuong = model.HuongLuong,
                LyDo = model.LyDo,
                NgayNghi = model.NgayNghi,
                NghiDen = model.NghiDen,
                NhanVienId = model.NhanVienId,
                SoNgayNghi = (model.NghiDen-model.NgayNghi).Days
            };
        }
        public static NghiPhep toDTOAddNghiPhep(this DTOAddNghiPhep model)
        {
            return new NghiPhep
            {
                HuongLuong = false,
                LyDo = model.LyDo,
                NgayNghi =Convert.ToDateTime( model.NgayNghi),
                NghiDen = Convert.ToDateTime(model.NghiDen),
                NhanVienId = model.NhanVienId,
                SoNgayNghi=(Convert.ToDateTime(model.NghiDen)-Convert.ToDateTime(model.NgayNghi)).Days
            };
        }
    }
}
