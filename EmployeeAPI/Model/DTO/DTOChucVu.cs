using EmployeeAPI.Model.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Model.DTO
{
    public class DTOChucVu
    {
      
        public int Id { get; set; }

    
        public string TenChucVu { get; set; }
      
    }
}
