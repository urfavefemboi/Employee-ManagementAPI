using EmployeeAPI.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.Model.DTO
{
    public class DTOThanhToan
    {
        public int? NhanVienId { get; set; }
     
        public int Id { get; set; }

 
        public int? LinhLuong { get; set; }
    }
}
