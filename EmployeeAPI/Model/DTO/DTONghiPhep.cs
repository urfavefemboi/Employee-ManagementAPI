namespace EmployeeAPI.Model.DTO
{
    public class DTONghiPhep
    {
       
        public int Id { get; set; }
      
        public int? NhanVienId { get; set; }
       
        public DateTime NgayNghi { get; set; }

        
        public DateTime NghiDen { get; set; }

        public int? SoNgayNghi { get; set; }
        public string? LyDo { get; set; }

      
        public bool HuongLuong { get; set; }
    }
}
