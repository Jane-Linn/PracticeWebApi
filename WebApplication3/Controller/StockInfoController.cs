using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Service;

namespace WebApplication3.Controller
{
    
    [Route("api/")]
    public class StockInfoController: ControllerBase
    {
        private readonly ISearchStockInfo SearchStockInfo;
        public StockInfoController(SearchStockInfo searchStockInfo)
        {
            SearchStockInfo = searchStockInfo;
        }

        [HttpGet("get/{stockId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockInfo( int stockId)
        {
            return await SearchStockInfo.SearchById(stockId);

        }
        
    }
    }

