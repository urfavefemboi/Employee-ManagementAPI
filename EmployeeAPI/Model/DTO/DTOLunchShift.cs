namespace EmployeeAPI.Model.DTO
{
    public class DTOLunchShift
    {
        public int Id { get; set; }
      
        public int NhanvienId { get; set; }

        
        public DateOnly? Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
