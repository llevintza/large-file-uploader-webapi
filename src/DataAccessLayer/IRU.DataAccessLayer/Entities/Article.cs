using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRU.DataAccessLayer.Entities
{
    [Table("Articles")]

    public class Article
    {
        [Key]
        public string ArticleCode { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }
        
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
