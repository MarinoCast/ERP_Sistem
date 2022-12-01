namespace ERP_System_Api.Payloads.Request
{
    public class BillingRequest
    {
        public ClientRequest clientInfo { get; set; }

        public ProcessingsRequest processingInfo { get; set; }

        public string? Nota { get; set; }

        public DateTime nextAppointment { get; set; }

    }
}
