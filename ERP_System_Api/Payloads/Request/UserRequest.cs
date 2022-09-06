using ERP_System_Api.Model;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Payloads.Request
{
    public class UserRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }



    }
}
