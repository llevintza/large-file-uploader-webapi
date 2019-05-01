using System;
using System.Collections.Generic;
using System.Text;

namespace IRU.Services.Models
{
    public class StockModel
    {
        public string Key { get; set; }

        //public string ArticleCode { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }

        public string DeliveredInterval { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string Category { get; set; }

        public ArticleModel Article { get; set; }

    }
}
