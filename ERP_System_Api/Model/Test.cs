using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class Test
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; }

        public string description { get; set; }
    }
}
