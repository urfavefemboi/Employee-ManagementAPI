using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;


namespace EmployeeAPI.Model.Domain
{
    public class ChamCong:BaseEntity<int>
    {

       
        [ForeignKey("NhanVien")]
        public int NhanVienId { get; set;}
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; } 
        
        
        public TimeSpan Clockin { get; set; }
        
        public TimeSpan? ClockOut { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime NgayChamCong { get; set; }

        
    }
}