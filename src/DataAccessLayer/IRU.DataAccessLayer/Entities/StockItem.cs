using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRU.DataAccessLayer.Entities
{
    [Table("StockItems")]
    public class StockItem
    {
        [Key]
        public string Key { get; set; }

        public int Size { get; set; }

        [ForeignKey("Color")]
        public int ColorId { get; set; }

        public string DeliveredInterval { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Article")]
        public string ArticleCode { get; set; }

        public Article Article { get; set; }

        public Category Category { get; set; }

        public Color Color { get; set; }
    }
}
