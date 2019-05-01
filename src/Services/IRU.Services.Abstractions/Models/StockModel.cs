namespace IRU.Services.Models
{
    public class StockModel
    {
        public string Key { get; set; }
        
        public int Size { get; set; }

        public ColorModel Color { get; set; }

        public string DeliveredInterval { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public CategoryModel Category { get; set; }

        public ArticleModel Article { get; set; }
    }
}
