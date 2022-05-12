namespace WebApplication1.Models
{
    public class TotalPaymentResponse
    {
        public decimal Total { get; set; }
        public string CustomerName { get; set; }
        public int BasicPackage { get; set; }
        public int StandardPackage { get; set; }
        public int AdvancedPackage { get; set; }
    }
}