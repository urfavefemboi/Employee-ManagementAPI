namespace EmployeeAPI.Model.DTO
{
    public class DTOHopdong
    {
       
        public int Id { get; set; }
       
        public int? NhanVienId { get; set; }
      
        public DateTime NgayKyHD { get; set; }
       
        public DateTime HanHD { get; set; }
       
        public bool HieuLuc { get; set; }
    }
}
