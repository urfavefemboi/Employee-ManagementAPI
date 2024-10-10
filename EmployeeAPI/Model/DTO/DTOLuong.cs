using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Model.DTO
{
    public class DTOLuong
    {
        
        public int Id { get; set; }

        public string MucLuong { get; set; }
    }
}
