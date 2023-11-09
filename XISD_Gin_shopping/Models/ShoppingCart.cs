using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace XISD_Gin_shopping.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<CartDetails> CartDetail { get; set; }
    }
}
