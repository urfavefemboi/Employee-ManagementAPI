using EmployeeAPI.Interface.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Model.Domain
{
    public class NhanVien:BaseEntity<int>
    {
        
        [Required]
        [StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string TenNV { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [StringLength(1), Column(TypeName = "nvarchar(1)")]
        public bool GioiTinh { get; set; }
        [Required]
        [StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string DiaChi { get; set; } = string.Empty;
        [Required]
        [MaxLength(8), Column(TypeName = "int")]
        public int CMND { get; set; }
        [Required]
        [MaxLength(7), Column(TypeName = "int")]
        public int SDT { get; set; }

        [ForeignKey("Luong")]
        public int? LuongId { get; set; }
        [JsonIgnore]
        public Luong? Luong { get; set; }
        [ForeignKey("PhongBan")]
        public int? PhongBanId { get; set; }
        [JsonIgnore]
        public PhongBan? PhongBan { get; set; }

        [ForeignKey("ChucVu")]
        public int? ChucVuId { get; set; }
        [JsonIgnore]
        public ChucVu? ChucVu { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string? Anh { get; set; }
        [JsonIgnore]
        public ICollection<HopDong>? HopDong { get; set; }
        [JsonIgnore]
        public ICollection<ChamCong>? ChamCong { get; set; }
        [JsonIgnore]
        public ICollection<ThanhToan>? ThanhToan { get; set; }
        [JsonIgnore]
        public ICollection<NghiPhep>? NghiPhep { get; set; }
        [JsonIgnore]
        public ICollection<LunchShift>? LunchShift { get; set; }

    }
}
