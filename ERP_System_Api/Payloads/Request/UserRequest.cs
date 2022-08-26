using ERP_System_Api.Model;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Payloads.Request
{
    public class UserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public Role roles { get; set; }

    }
}
