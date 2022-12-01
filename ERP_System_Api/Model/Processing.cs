using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class Processing
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Price { get; set; }


    }
}
