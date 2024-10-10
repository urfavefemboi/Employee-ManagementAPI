namespace EmployeeAPI.Model.DTOAdd
{
    public class DTOAddNhanVien
    {
       

        public string TenNV { get; set; } = string.Empty;

        public string NgaySinh { get; set; }


        public bool GioiTinh { get; set; }

        public string DiaChi { get; set; } = string.Empty;

        public int CMND { get; set; }

        public int SDT { get; set; }

        public int? LuongId { get; set; }

       public IFormFile? Anh { get; set; }
        public int? PhongBanId { get; set; }


        public int? ChucVuId { get; set; }
    }
}
