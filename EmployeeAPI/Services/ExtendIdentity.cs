using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeAPI.Services
{
    public class ExtendIdentity:IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpired { get; set; }
    }
}
