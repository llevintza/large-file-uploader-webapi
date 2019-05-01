using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRU.DataAccessLayer.Entities
{
    [Table("dbo.Categories")]

    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
