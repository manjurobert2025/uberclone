using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uber.Application.DTOs.Payment;
using Uber.Application.Interfaces;

namespace Uber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // ============================================================
        // CREATE RAZORPAY ORDER
        // POST: api/Payment/create-order
        // ============================================================

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreatePaymentOrderDto dto)
        {
            try
            {
                var result =
                    await _paymentService.CreateOrderAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // ============================================================
        // VERIFY RAZORPAY PAYMENT
        // POST: api/Payment/verify
        // ============================================================

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyPayment(
            [FromBody] VerifyPaymentDto dto)
        {
            try
            {
                var result =
                    await _paymentService.VerifyPaymentAsync(dto);

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Payment verification failed."
                    });
                }

                return Ok(new
                {
                    message = "Payment verified successfully.",
                    success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}