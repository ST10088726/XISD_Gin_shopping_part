using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using XISD_Gin_shopping.Models;

namespace XISD_Gin_shopping.Controllers
{
    public class StockController : Controller
    {
        private ShoppingCart tm= new ShoppingCart();

        IFirebaseConfig config = new FirebaseConfig
        {
          AuthSecret= "de2ehPcqdy3rz32v5BPZJG3VCVF9b3ldkSsOrbhB",
          BasePath= "https://juniper-junction-distill-2b013-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public IActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Stock");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Stock>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Stock>(((JProperty)item).Value.ToString()));
            }
            return View(list);
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }    

        [HttpPost]
        public ActionResult Create(Stock stock)
        {
            try
            {
                AddStockToFirebase(stock);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }
        private void AddStockToFirebase(Stock stock)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = stock;
            PushResponse response = client.Push("Stock/", data);
            data.ID = response.Result.name;
            SetResponse setResponse = client.Set("Stock/" + data.ID, data);
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Stock/" + id);
            Stock data = JsonConvert.DeserializeObject<Stock>(response.Body);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Stock/" + id);
            Stock data = JsonConvert.DeserializeObject<Stock>(response.Body);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Stock stock)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Stock/" + stock.ID, stock);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Stock/" + id);
            return RedirectToAction("Index");
        }

    }
}
