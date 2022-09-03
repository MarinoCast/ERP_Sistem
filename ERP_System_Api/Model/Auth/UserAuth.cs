using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class UserAuth 
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
