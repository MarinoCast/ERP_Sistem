namespace ERP_System_Api.Helpers
{
    public class AuthResult
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public bool Success { get; set; }

        public string Message { get; set; }
      
        
    }
}
