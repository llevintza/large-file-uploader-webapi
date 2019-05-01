using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRU.DataAccessLayer.Entities
{
    [Table("Colors")]

    public class Color
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
