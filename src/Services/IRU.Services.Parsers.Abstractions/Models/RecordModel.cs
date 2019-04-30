using CsvHelper.Configuration.Attributes;

namespace IRU.Services.Parsers.Models
{
    public class RecordModel
    {
        [Name("Key")]
        public string Key { get; set; }

        [Name("ArtikelCode")]
        public string ArticleCode { get; set; }

        [Name("ColorCode")]
        public string ColorCode { get; set; }

        [Name("Description")]
        public string Description { get; set; }

        [Name("Price")]
        public decimal Price { get; set; }

        [Name("DiscountPrice")]
        public string DiscountPrice { get; set; }

        [Name("DeliveredIn")]
        public string DeliveredIn { get; set; }

        [Name("Q1")]
        public string Q1 { get; set; }

        [Name("Size")]
        public int Size { get; set; }

        [Name("Color")]
        public string Color { get; set; }
    }
}
