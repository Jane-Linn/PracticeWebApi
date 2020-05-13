using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Cache
{
    public interface ICache
    {
        Task<StockInfo[]> GetStockIdInfo(string stockId);
    }
}
