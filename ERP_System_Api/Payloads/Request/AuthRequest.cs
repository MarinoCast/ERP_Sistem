namespace ERP_System_Api.Payloads.Request
{
    public class AuthRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreater { get; set; }
        public DateTime TokenExpire { get; set; }
    }
}
