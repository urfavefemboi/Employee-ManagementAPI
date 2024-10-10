using EmployeeAPI.Model.UserDomain;
using Microsoft.AspNetCore.Identity;

namespace EmployeeAPI.Services
{
    public interface IAuthServices
    {
        Task<LoginRespon> LogIn(LoginModel model);
        Task<IdentityResult> RegisterAdmin(RegisterModel model);
        Task<IdentityResult> RegisterUser(RegisterModel model);
        Task<LoginRespon> RefreshToken(RefreshTokenModel model);

    }
}
