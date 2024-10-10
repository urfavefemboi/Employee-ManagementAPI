using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Model.Domain
{
    public class Luong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string MucLuong { get; set; }
        [JsonIgnore]
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}