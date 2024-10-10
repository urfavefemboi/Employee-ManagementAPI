namespace EmployeeAPI.Model.DTOAdd
{
    public class DTOAddChamCong
    {
        public int NhanVienId { get; set; }
        public DateTime NgayChamCong { get; set; }
        public TimeSpan? Clockin { get; set; }

        public TimeSpan? ClockOut { get; set; }


    }
}
