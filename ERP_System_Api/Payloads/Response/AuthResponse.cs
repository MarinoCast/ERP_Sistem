namespace ERP_System_Api.Payloads
{
    public class AuthResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreater { get; set; } 
        public DateTime TokenExpire { get; set; }
    }
}
