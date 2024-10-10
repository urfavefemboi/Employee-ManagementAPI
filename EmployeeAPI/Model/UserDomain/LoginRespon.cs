namespace EmployeeAPI.Model.UserDomain
{
    public class LoginRespon
    {
        public bool IsLoggin { get; set; }=false;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; }=string.Empty;
    }
}
