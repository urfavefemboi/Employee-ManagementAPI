using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;

namespace EmployeeAPI.Model.Domain
{
    public class NghiPhep:BaseEntity<int>
    {
       
        [ForeignKey("NhanVien")]
        public int? NhanVienId { get; set; }
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime NgayNghi { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime NghiDen { get; set; }
        [Column(TypeName = "int")]
        public int? SoNgayNghi { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? LyDo { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1)")]
        public bool HuongLuong { get; set; }
      
    }
}