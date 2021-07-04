using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Strip_Payment.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Strip_Payment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var stripePublishKey = ConfigurationManager.AppSettings["Stripe:PublishableKey"];
            ViewBag.StripePublishKey = stripePublishKey;
            return View();

        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Description="Gesahsjdh"
              
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            return View();
        }


        //public IActionResult Index()
        //{
        //    StripeConfiguration.SetApiKey(configuration["Stripe:SecretKey"]);

        //    //Create Card Object to create Token  
        //    Stripe.CreditCardOptions card = new Stripe.CreditCardOptions();
        //    card.Name = tParams.CardOwnerFirstName + " " + tParams.CardOwnerLastName;
        //    card.Number = tParams.CardNumber;
        //    card.ExpYear = tParams.ExpirationYear;
        //    card.ExpMonth = tParams.ExpirationMonth;
        //    card.Cvc = tParams.CVV2;
        //    //Assign Card to Token Object and create Token  
        //    Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
        //    token.Card = card;
        //    Stripe.TokenService serviceToken = new Stripe.TokenService();
        //    Stripe.Token newToken = serviceToken.Create(token);



        //    //Create Customer Object and Register it on Stripe  
        //    Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions();
        //    myCustomer.Email = tParams.Buyer_Email;
        //    myCustomer.SourceToken = newToken.Id;
        //    var customerService = new Stripe.CustomerService();
        //    Stripe.Customer stripeCustomer = customerService.Create(myCustomer);


        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
