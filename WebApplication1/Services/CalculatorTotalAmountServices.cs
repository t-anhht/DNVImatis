using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    
    public class CalculatorTotalAmountServices : ICalculatorTotalAmountServices
    {
        List<Customers> customers = JsonConvert.DeserializeObject<List<Customers>>(File.ReadAllText(@"..\WebApplication1\JsonData\Customers.json"));
        List<Packages> packages = JsonConvert.DeserializeObject<List<Packages>>(File.ReadAllText(@"..\WebApplication1\JsonData\Packages.json"));
        List<Promotion> promotions = JsonConvert.DeserializeObject<List<Promotion>>(File.ReadAllText(@"..\WebApplication1\JsonData\Promotions.json"));
        public CalculatorTotalAmountServices()
        {

        }


        public async Task<TotalPaymentResponse> CalculatorTotalPayment(CalculatorTotalPaymentRequest request)
        {
            TotalPaymentResponse response = new TotalPaymentResponse();
            response.CustomerName = request.CustomerName;
            response.BasicPackage = request.BasicPackage;
            response.StandardPackage = request.StandardPackage;
            response.AdvancedPackage = request.AdvancedPackage;

            var promotionList = GetAllPromotionByCustomerId(request.CustomerCode);
            decimal totalPayment = 0;

            if (request.BasicPackage > 0)
            {
                totalPayment += ApplyPromotion(PackageType.Basic.ToString(), request.BasicPackage, promotionList);
            }
            if (request.StandardPackage > 0)
            {
                totalPayment += ApplyPromotion(PackageType.Standard.ToString(), request.StandardPackage, promotionList);
            }
            if (request.AdvancedPackage > 0)
            {
                totalPayment += ApplyPromotion(PackageType.Advanced.ToString(), request.AdvancedPackage, promotionList);
            }

            response.Total = totalPayment;


            return response;
        }

        // Get all customer to show in UI dropdownlist
        public async Task<List<Customers>> GetCustomers()
        {
            List<Customers> response = new List<Customers>();

            response = customers.ToList();

            return response;
        }

        private List<Promotion> GetAllPromotionByCustomerId(string customerCode)
        {
            List<Promotion> promotionList = new List<Promotion>();
            promotionList = promotions.Where(x => x.CustomerCode == customerCode).ToList();

            return promotionList;
        }

        private decimal ApplyPromotion(string packageType, int quantity, List<Promotion> promotionList)
        {
            decimal totalPaymentPackage = 0;
            var package = packages.FirstOrDefault(x => x.Code == packageType);
            decimal packagePrice = package != null ? package.Price : 0;
            var promotion = promotionList.Where(x => x.PackageType == packageType).FirstOrDefault();
            if (promotion != null)
            {
                if (quantity >= promotion.Block)
                {
                    totalPaymentPackage += quantity % promotion.Block * packagePrice;
                    decimal priceByPackage = 0;
                    if (promotion.DiscountByPackage != null)
                        priceByPackage = (decimal)(quantity / promotion.Block * (promotion.Block - promotion.DiscountByPackage) * packagePrice);
                    else if (promotion.DiscountByAmount != null)
                        priceByPackage = (decimal)(quantity * (packagePrice - promotion.DiscountByAmount));
                    totalPaymentPackage += priceByPackage;
                }
            }
            else
            {
                totalPaymentPackage = quantity * packagePrice;
            }

            return totalPaymentPackage;
        }
    }
}
