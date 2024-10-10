using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;
using EmployeeAPI.Services;

namespace EmployeeAPI.Mapper
{
    public static class NhanVienMapper
    {
      
        public static NhanVien toDTOAddNhanVien(this DTOAddNhanVien model)
        {
            return new NhanVien
            {
                ChucVuId = model.ChucVuId,
                DiaChi = model.DiaChi,
                CMND = model.CMND,
                SDT = model.SDT,
                GioiTinh = model.GioiTinh,
                LuongId = model.LuongId,
                PhongBanId = model.PhongBanId,
                TenNV = model.TenNV,
                NgaySinh = Convert.ToDateTime(model.NgaySinh),
                Anh = model?.Anh?.Name ?? " "
            };
        }
        public static DTONhanVien toDTONhanVien(this NhanVien model)
        {
            return new DTONhanVien
            {
                TenChucVu = model.ChucVu?.TenChucVu,
                DiaChi = model.DiaChi,
                CMND = model.CMND,
                SDT = model.SDT,
                GioiTinh = model.GioiTinh,
                LuongId = model.LuongId,
                TenPhongBan = model.PhongBan?.TenPhongBan,
                TenNV = model.TenNV,
                NgaySinh =DateOnly.FromDateTime(model.NgaySinh),
                Id = model.Id,
                Anh=model.Anh
            };
        }
    }
}
