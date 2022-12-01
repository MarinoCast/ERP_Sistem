using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_System_Api.Model
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }

        public Client clientInfo { get; set; }
 
        public Processing processingInfo { get; set; }

        public string? Nota { get; set; }

        public DateTime nextAppointment { get; set; }

        
    }
}
