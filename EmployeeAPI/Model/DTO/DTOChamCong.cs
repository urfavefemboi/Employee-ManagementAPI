using EmployeeAPI.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Model.DTO
{
    public class DTOChamCong
    {

        public int Id { get; set; }
      
        public int NhanVienId { get; set; }

        public DateOnly NgayChamCong { get; set; }

        public TimeSpan Clockin { get; set; }

        public TimeSpan ClockOut { get; set; }

    }
   
}
