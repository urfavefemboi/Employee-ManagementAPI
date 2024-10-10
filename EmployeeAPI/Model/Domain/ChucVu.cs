﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EmployeeAPI.Interface.Common;

namespace EmployeeAPI.Model.Domain
{
    public class ChucVu:BaseEntity<int>
    {
        
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string TenChucVu { get; set; }
        [JsonIgnore]
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}