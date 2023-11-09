using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace XISD_Gin_shopping.Models
{
    [Table("CartDetail")]
    public class CartDetails
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId {get; set;}
        [Required]
        public string StockId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Stock Stock  { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
