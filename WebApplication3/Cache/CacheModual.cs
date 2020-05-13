using Microsoft.AspNetCore.Mvc;
using System.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Service;
using System.Threading;

namespace WebApplication3.Cache
{
    public class CacheModual : ICache
    {
        private int CallCount;
        private readonly ISearchStockInfo SearchStockInfo;
        private MemoryCache StockIdInfoCache = MemoryCache.Default;
        private CacheItemPolicy CacheItemPolicy = new CacheItemPolicy()
        {
            SlidingExpiration = new TimeSpan(0, 0, 30)
        };
        public CacheModual(ISearchStockInfo searchStockInfo)
        {
            SearchStockInfo = searchStockInfo;
            CallCount = 0;
        }

        public async Task<StockInfo[]> GetStockIdInfo(string stockId)
        {
            Console.WriteLine("呼叫服務");
            CallCount = Interlocked.Increment(ref CallCount);
            Console.WriteLine($"服務被呼叫次數: { CallCount}  時間: {DateTime.Now}");

            Lazy<Task<StockInfo[]>> stockInfoTaskLazy = new Lazy<Task<StockInfo[]>>(() => SearchStockInfo.SearchDbByStockId(stockId));
            
            var cacheInfo = StockIdInfoCache.AddOrGetExisting(stockId, stockInfoTaskLazy, CacheItemPolicy);
            if (cacheInfo == null)
            {
                Console.WriteLine("建立快取");
                return await stockInfoTaskLazy.Value;
            }
            else
            {
                Console.WriteLine("拿快取");
                return await(cacheInfo as Lazy<Task<StockInfo[]>>).Value;
            }
           
        }

       
    }
}
