using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorTotalAmountController : ControllerBase
    {
        private readonly ILogger<CalculatorTotalAmountController> _logger;
        private readonly ICalculatorTotalAmountServices _calculatorTotalAmountServices;

        public CalculatorTotalAmountController(ILogger<CalculatorTotalAmountController> logger,
            ICalculatorTotalAmountServices calculatorTotalAmountServices)
        {
            _logger = logger;
            _calculatorTotalAmountServices = calculatorTotalAmountServices;
        }


        // POST: CalculatorTotalAmountController/CalculatorPayment
        [HttpPost]
        public async Task<IActionResult> CalActionResultculatorPayment(CalculatorTotalPaymentRequest request)
        {
            TotalPaymentResponse result = new TotalPaymentResponse();

            try
            {
                result = await _calculatorTotalAmountServices.CalculatorTotalPayment(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(result);
            }
        }

    }
}
