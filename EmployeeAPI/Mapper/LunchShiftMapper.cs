using EmployeeAPI.Model.Domain;
using EmployeeAPI.Model.DTO;
using EmployeeAPI.Model.DTOAdd;

namespace EmployeeAPI.Mapper
{
    public static class LunchShiftMapper
    {
        public static DTOLunchShift toDTOLunchShift(this LunchShift model)
        {
            return new DTOLunchShift
            {
                Date =DateOnly.FromDateTime(model.Date),
                EndTime =TimeSpan.FromTicks(model.EndTime.Ticks),
                NhanvienId = model.NhanvienId,
                StartTime = TimeSpan.FromTicks(model.StartTime.Ticks),
                Id = model.Id
            };
        }
        public static LunchShift toDTOAddLunchShift(this DTOAddLunchShift model)
        {
            return new LunchShift
            {
                Date =model.Date,
                EndTime =TimeSpan.FromTicks(model.EndTime),
                NhanvienId = model.NhanvienId,
                StartTime =TimeSpan.FromTicks(model.StartTime)
            };
        }
    }
}
