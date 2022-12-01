
using ERP_System_Api.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace ERP_System_Api.Model
{
    public class Client
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }

        public int age { get; set; }

        public string sex { get; set; }

        public string personalId { get; set; }

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public string fullName
        {
            get { return $"{name} {lastName}"; }
        }

    }
}
