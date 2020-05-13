using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Service
{
    public interface ISearchStockInfo
    {
        Task<StockInfo[]> SearchDbByStockId(string stockId);
    }
 }
