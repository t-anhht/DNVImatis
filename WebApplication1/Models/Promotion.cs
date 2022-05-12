namespace WebApplication1.Models
{
    public class Promotion: BaseClass
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Description { get; set; }
        public string PackageType { get; set; }
        public int Block { get; set; }
        public decimal? DiscountByAmount { get; set; }
        public int? DiscountByPackage { get; set; }

    }
}
