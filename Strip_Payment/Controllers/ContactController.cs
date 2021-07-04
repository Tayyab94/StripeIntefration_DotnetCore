using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strip_Payment.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            //var customer = customers.Create(new CustomerCreateOptions
            //{
            //    Email = stripeEmail,
            //    Description = "Gesahsjdh"

            //});


            var options = new ChargeCreateOptions
            {
                Amount = 300,
                Currency = "usd",
                Source = stripeToken, // obtained with Stripe.js
                Description = "My First Test Charge (created for API docs)",
            };


            var service = new ChargeService();
            service.Create(options, new RequestOptions
            {
                IdempotencyKey = "AdHrWJehYLlRuhTp",
            });


            //var charge = charges.Create(new ChargeCreateOptions
            //{
            //    Amount = 500,
            //    Description = "Sample Charge",
            //    Currency = "usd",
            //    Customer = customer.Id
            //});

            return View();
        }
    }
}
