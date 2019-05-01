namespace IRU.Services.JsonModels
{
    public class StockModel
    {
        public string Key { get; set; }
        
        public int Size { get; set; }

        public string Color { get; set; }

        public string DeliveredInterval { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string Category { get; set; }

        public ArticleModel Article { get; set; }

    }
}
