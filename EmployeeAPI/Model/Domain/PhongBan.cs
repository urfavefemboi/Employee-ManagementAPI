using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;

namespace EmployeeAPI.Model.Domain
{
    public class PhongBan:BaseEntity<int>
    {

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string TenPhongBan { get; set; }
        [JsonIgnore]
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}