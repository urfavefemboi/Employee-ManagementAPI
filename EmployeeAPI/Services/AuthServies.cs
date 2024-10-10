using EmployeeAPI.Model.UserDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeAPI.Services
{
    public class AuthServies
    {
        public class AuthServices : IAuthServices
        {
            private readonly UserManager<ExtendIdentity> _usermanager;
            private readonly RoleManager<IdentityRole> _rolemanager;
            private readonly IConfiguration _config;
            public AuthServices(UserManager<ExtendIdentity> usermanager,
                RoleManager<IdentityRole> rolemanager, IConfiguration config)
            {
                _usermanager = usermanager;
                _rolemanager = rolemanager;
                _config = config;
            }

            public async Task<LoginRespon> LogIn(LoginModel model)
            {
                var respon = new LoginRespon();
                //get user
                var getidentityuser = await _usermanager.FindByEmailAsync(model.UserName);
                //check password
                var passwordvalid = await _usermanager.CheckPasswordAsync(getidentityuser, model.Password);
                //get user roles
                var getuserrole = await _usermanager.GetRolesAsync(getidentityuser);
                if (getidentityuser is null || !passwordvalid )
                {
                    return respon;
                }
                //tra ve ket qua respon
                respon.IsLoggin = true;
                respon.Token = await GenerateToken(getidentityuser.Email);
                respon.RefreshToken = this.GenerateRefreshToken();
                //gan refresh token
                getidentityuser.RefreshToken = respon.RefreshToken;
                getidentityuser.RefreshTokenExpired = DateTime.UtcNow.AddHours(2);
                await _usermanager.UpdateAsync(getidentityuser);
                return respon;
            }
            private async Task<string> GenerateToken(string email)
            {
                if (email == null)
                {
                    return string.Empty;
                }
                var getidentityuser = await _usermanager.FindByEmailAsync(email);
                var getuserrole = await _usermanager.GetRolesAsync(getidentityuser);

                var claims = new List<Claim>
           {
                new Claim(ClaimTypes.Name,email),
                new Claim(ClaimTypes.Email, email)
            };
                foreach (var role in getuserrole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
                //create token 
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
                var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    issuer: _config.GetSection("Jwt:Issuer").Value,
                    audience: _config.GetSection("Jwt:Audience").Value,
                    signingCredentials: signingCred
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            private ClaimsPrincipal? GetTokenPrincipal(string JwtToken)
            {
                if (JwtToken == null)
                {
                    return null;
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
                var validation = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                return new JwtSecurityTokenHandler().ValidateToken(JwtToken, validation, out _);


            }

            private string GenerateRefreshToken()
            {
                var randomnumber = new byte[64];
                using (var numberGenerate = RandomNumberGenerator.Create())
                {
                    numberGenerate.GetBytes(randomnumber);
                }
                return Convert.ToBase64String(randomnumber);
            }

            public async Task<IdentityResult> RegisterAdmin(RegisterModel model)
            {
                
                var user = new ExtendIdentity
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _usermanager.CreateAsync(user, model.Password);

                if (!await _usermanager.CheckPasswordAsync(user, model.Password))
                {
                    return  result;
                }
                if (result.Succeeded)
                {
                    if (!await _rolemanager.RoleExistsAsync(Roles.Admin))
                    {
                        await _rolemanager.CreateAsync(new IdentityRole(Roles.Admin));
                    }
                    await _usermanager.AddToRoleAsync(user, Roles.Admin);
                }
                return result;
            }

            public async Task<IdentityResult> RegisterUser(RegisterModel model)
            {
                var user = new ExtendIdentity
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await _rolemanager.RoleExistsAsync(Roles.User))
                    {
                        await _rolemanager.CreateAsync(new IdentityRole(Roles.User));
                    }
                    await _usermanager.AddToRoleAsync(user, Roles.User);
                }
                return result;

            }

            public async Task<LoginRespon> RefreshToken(RefreshTokenModel model)
            {
                var principal = GetTokenPrincipal(model.JwtToken);
                var respon = new LoginRespon();
                if (principal?.Identity?.Name is null)
                {
                    return respon;
                }
                var identityUser = await _usermanager.FindByEmailAsync(principal.Identity.Name);
                if (identityUser is null || identityUser.RefreshToken != model.RefreshToken ||
                    identityUser.RefreshTokenExpired < DateTime.UtcNow)
                {
                    return respon;
                }
                //tra ve ket qua respon
                respon.IsLoggin = true;
                respon.Token = await GenerateToken(identityUser.Email);
                respon.RefreshToken = GenerateRefreshToken();

                //gan refresh token
                identityUser.RefreshToken = respon.RefreshToken;
                identityUser.RefreshTokenExpired = DateTime.UtcNow.AddHours(2);
                await _usermanager.UpdateAsync(identityUser);

                return respon;
            }

          
        }
    }
}
