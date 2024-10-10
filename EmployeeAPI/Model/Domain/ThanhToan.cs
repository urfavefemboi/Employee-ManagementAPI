using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;

namespace EmployeeAPI.Model.Domain
{
    public class ThanhToan:BaseEntity<int>
    {

        
        [ForeignKey("NhanVien")]
        public int? NhanVienId { get; set; }
        [JsonIgnore]
        public NhanVien? NhanVien { get; set; }
      
        
        [Column(TypeName = "int")]
        public int? LinhLuong { get; set; }
      
    }
}