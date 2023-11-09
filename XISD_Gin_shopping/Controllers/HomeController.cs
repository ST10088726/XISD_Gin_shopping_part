using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Diagnostics;
using XISD_Gin_shopping.Models;


namespace XISD_Gin_shopping.Controllers
{
    public class HomeController : Controller
    {
        private Stock tm = new Stock();
        private readonly ILogger<HomeController> _logger;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "de2ehPcqdy3rz32v5BPZJG3VCVF9b3ldkSsOrbhB",
            BasePath = "https://juniper-junction-distill-2b013-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            List<Stock> stock = new List<Stock>();
            stock = new List<Stock>
            {
                new Stock
                {
                Arrival="2023-05-20",
                Category="1698315704965",
                Description="BOTANICAL",
                ID="1699103224821",
                StockName="JUNIPER CASCADE RESERVE",
                Price="449",
                Quantity="10" 
                 //Edit
               }
            };
            return View(stock);
        }
        public IActionResult AddToCart(int ID)
        {
            client = new FireSharp.FirebaseClient(config);

            //// Get the product that the user wants to add to their cart.
            //    FirebaseResponse response=client.Get("Stock");
            //    Stock stock = new Stock();
            //Stock stock = stock.ID;
            //    // Add the product to the shopping cart.
            //    ShoppingCart cart = GetShoppingCart();
            //    cart.Add(new CartItem(product));

            //    // Save the shopping cart.
            //    SaveShoppingCart(cart);

            //    // Return the view.
            //    return View(cart);
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();

            Session session = service.Get(TempData["Session"].ToString());


            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();

                return View("Success");
            }
            return View("Login");

        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            List<Stock> stock = new List<Stock>();
            stock = new List<Stock>
          {
                new Stock
                {
                Arrival="2023-05-20",
                Category="1698315704965",
                Description="BOTANICAL",
                ID="1699103224821",
                StockName="JUNIPER CASCADE RESERVE",
                Price="449",
                Quantity="10" //Edit

                }
           };

            var domain = "https://localhost:7132/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Home/OrderConfirmation",
                CancelUrl = domain + "Home/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = "mahomedfaraaz@gmail.com"//Edit
            };

            foreach (var item in stock)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = Convert.ToInt32(item.Price) * Convert.ToInt32(item.Quantity),
                        Currency = "zar",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.StockName.ToString(),
                        }
                    },
                    Quantity = Convert.ToInt32(item.Quantity),
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
    }
}