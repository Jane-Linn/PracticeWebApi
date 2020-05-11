using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    public class StockInfo
    {
        public string RecordId { get; set; }
        public string Date { get; set; }
        public string StockId { get; set; }
        public string StockName { get; set; }
        public decimal RefPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal ClosePrice { get; set; }
    }
}
