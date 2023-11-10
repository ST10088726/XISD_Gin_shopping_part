using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace XISD_Gin_shopping.Controllers
{
      //[Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(string stockId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(stockId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(string stockId)
        {
            var cartCount = await _cartRepo.RemoveItem(stockId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _cartRepo.DoCheckout();
            if (!isCheckedOut)
                throw new Exception("Something happened on the server side");
            return RedirectToAction("Index", "Home");
        }
    }
}
