namespace EmployeeAPI.Model.DTOAdd
{
    public class DTOAddLunchShift
    {
        public int NhanvienId { get; set; }

        public DateTime Date { get; set; }

        public Int64 StartTime { get; set; }
        public Int64 EndTime { get; set; }
    }
}
