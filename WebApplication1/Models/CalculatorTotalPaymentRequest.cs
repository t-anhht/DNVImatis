namespace WebApplication1.Models
{
    public class CalculatorTotalPaymentRequest
    {
        public string CustomerCode { get; set; } = "IMT";
        public string CustomerName { get; set; } = "IMT Company";
        public int BasicPackage { get; set; } = 12;
        public int StandardPackage { get; set; } = 0;
        public int AdvancedPackage { get; set; } = 0;
    }
}
