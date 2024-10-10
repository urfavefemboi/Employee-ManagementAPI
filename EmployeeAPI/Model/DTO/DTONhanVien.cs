namespace EmployeeAPI.Model.DTO
{
    [Serializable]
    public class DTONhanVien
    {
       
        public int Id { get; set; }
        
        public string TenNV { get; set; } = string.Empty;
       
        public DateOnly NgaySinh { get; set; }

      
        public bool GioiTinh { get; set; }
       
        public string DiaChi { get; set; } = string.Empty;

        public int CMND { get; set; }
       
        public int SDT { get; set; }
       
        public int? LuongId { get; set; }

     
        public string? TenPhongBan { get; set; }
        public string? TenChucVu { get; set; }
        public string? Anh { get; set; }

   
    }
}
