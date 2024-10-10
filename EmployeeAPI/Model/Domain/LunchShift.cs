using EmployeeAPI.Interface.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Model.Domain
{
    public class LunchShift:BaseEntity<int>
    {
        
        [ForeignKey("NhanVien")]
        [Column(TypeName = "int")]
        public int NhanvienId { get; set; }
        [JsonIgnore]
        public NhanVien Nhanvien { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
