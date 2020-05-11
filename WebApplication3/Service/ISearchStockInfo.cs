using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Service
{
    public interface ISearchStockInfo
    {
        Task<IActionResult> SearchByStockId(int stockId);
    }
 }
