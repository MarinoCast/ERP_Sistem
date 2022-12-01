namespace ERP_System_Api.Payloads.Response
{
    public class ClientResponse
    {
        public string Message { get; set; }
        public string ClientName { get; set; }

        public bool Create { get; set; }
        public DateTime DayCreate { get; set; }
    }
}
