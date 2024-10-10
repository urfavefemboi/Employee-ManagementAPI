using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;

namespace EmployeeAPI.Model.Domain
{
    public class HopDong:BaseEntity<int>
    {
        
        [ForeignKey("NhanVien")]
        public int? NhanVienId { get; set; }
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime NgayKyHD { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime HanHD { get; set; }     
        [Required]
        [Column(TypeName = "nvarchar(1)")]
        public bool HieuLuc { get; set; }
    }
}