using BusinessLayer.Contracts;
using BusinessLayer.ObserverDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }


        [HttpGet("GetAllPayments")]
        [Authorize(Roles = "PoliceOfficer, PostOfficeEmployee")]
        public IEnumerable<Payment> Get()
        {
            var result = new List<Payment>();
            foreach (var payment in paymentService.GetAllPayments())
            {
                result.Add(new Payment { Id = payment.Id, Fine = payment.Fine, PaymentMode = payment.PaymentMode, PaymentDate = payment.PaymentDate });
            }
            return result;
        }


        [HttpPost("PayFine")]
        [Authorize(Roles = "PostOfficeEmployee")]
        //[Auth]
        public String Post(Payment payment)
        {
            try
            {
                Console.WriteLine("Attaching Observers...");

                var messageObserver = new MessageObserver();

                paymentService.Attach(messageObserver);

                Console.WriteLine("Updating Fine Status...");

                paymentService.PayFineModel(new PaymentModel { Fine = payment.Fine, PaymentMode = payment.PaymentMode, PaymentDate = payment.PaymentDate });

                return "Fine paid successfully!";
            }
            catch (Exception)
            {
                return "Something went wrong! Verify if the fine is not already paid!";
            }
        }


    }
}
