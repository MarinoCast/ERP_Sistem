using ERP_System_Api.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class UserAuth
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;
        public Role roles { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] Pass { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
       
    }
}
