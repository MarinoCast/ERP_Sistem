using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class Test
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

    }
}
