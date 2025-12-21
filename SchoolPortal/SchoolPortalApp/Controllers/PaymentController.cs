// File: c:\SchoolManagementSystem\SchoolPortal\SchoolPortalApp\Controllers\PaymentController.cs
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using SchoolPortalApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

//https://code2night.com/Blog/MyBlog/Implement-RazorPay-Payment-Gateway-in-Asp.net-MVC
namespace SchoolPortalApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly string _key;
        private readonly string _secret;

        public PaymentController(IConfiguration configuration, ILogger<PaymentController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _key = _configuration["RazorPay:Key"] ?? throw new ArgumentNullException("RazorPay:Key configuration is missing");
            _secret = _configuration["RazorPay:Secret"] ?? throw new ArgumentNullException("RazorPay:Secret configuration is missing");

            if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_secret))
            {
                throw new ApplicationException("RazorPay API credentials are not configured.");
            }
        }

        // GET: Payment/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Payment/Payment
        public IActionResult Payment()
        {
            try
            {
                // Create order and pass order ID to the view
                Dictionary<string, object> input = new Dictionary<string, object>
                {
                    { "amount", 1000 }, // Amount in paise (e.g., 1000 = ₹10)
                    { "currency", "INR" },
                    { "receipt", "order_" + DateTime.Now.Ticks }
                };

                RazorpayClient client = new RazorpayClient(_key, _secret);
                var order = client.Order.Create(input);

                // Get the order ID from the response
                string orderId = order["id"].ToString();
                ViewBag.orderId = orderId;
                ViewBag.key = _key;
                ViewBag.amount = 1000; // Same as above
                ViewBag.currency = "INR";
                ViewBag.name = "School Portal";
                ViewBag.description = "Fee Payment";
                ViewBag.image = Url.Content("~/Content/Images/school-logo.png");
                ViewBag.prefill = new
                {
                    name = "Student Name", // You can get this from the logged-in user
                    email = "student@school.com", // You can get this from the logged-in user
                    contact = "9876543210" // You can get this from the logged-in user
                };

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment order");
                return RedirectToAction("PaymentFailed", new { message = "Error creating payment order: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Complete()
        {
            try
            {
                string? paymentId = Request.Form["razorpay_payment_id"].FirstOrDefault();
                string? orderId = Request.Form["razorpay_order_id"].FirstOrDefault();
                string? signature = Request.Form["razorpay_signature"].FirstOrDefault();

                if (string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(signature))
                {
                    return RedirectToAction("PaymentFailed", new { message = "Invalid payment response received." });
                }

                var client = new RazorpayClient(_key, _secret);
                var attributes = new Dictionary<string, string>
                {
                    { "razorpay_payment_id", paymentId },
                    { "razorpay_order_id", orderId },
                    { "razorpay_signature", signature }
                };

                // Verify the payment signature
                try
                {
                    Utils.verifyWebhookSignature(
                        $"{paymentId}|{orderId}",
                        signature,
                        _secret
                    );
                }
                catch (Razorpay.Api.Errors.SignatureVerificationError)
                {
                    throw new Exception("Invalid payment signature");
                }

                // Payment successful - Save to database
                // TODO: Implement your payment success logic here
                // Example: Update fee status, send confirmation email, etc.

                // You can pass additional data to the success view
                ViewBag.TransactionId = paymentId;
                ViewBag.Amount = Request.Form["razorpay_amount"]; // The amount in paise

                return View("PaymentSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment completion");
                return RedirectToAction("PaymentFailed", new { message = ex.Message });
            }
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }

        public ActionResult PaymentFailed(string message)
        {
            ViewBag.ErrorMessage = message ?? "An error occurred while processing your payment.";
            return View();
        }
    }
}