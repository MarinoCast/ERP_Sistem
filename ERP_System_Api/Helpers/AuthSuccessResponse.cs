namespace ERP_System_Api.Helpers
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string RefreshToken { get; set; }
    }
}
