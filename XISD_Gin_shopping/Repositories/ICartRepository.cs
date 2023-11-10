namespace XISD_Gin_shopping.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(string stockId, int qty);
        Task<int> RemoveItem(string stockId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}
