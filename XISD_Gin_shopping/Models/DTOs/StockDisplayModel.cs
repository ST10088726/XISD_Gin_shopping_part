using Humanizer.Localisation;

namespace XISD_Gin_shopping.Models.DTOs
{
    public class StockDisplayModel
    {
        public IEnumerable<Stock> Stocks { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
