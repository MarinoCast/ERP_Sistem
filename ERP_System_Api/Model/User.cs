using ERP_System_Api.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class User
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        public Role roles { get; set; }
    }
}
