using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Cache;
using WebApplication3.Model;
using WebApplication3.Service;

namespace WebApplication3.Controller
{

    [Route("api/")]
    [ApiController]
    public class StockInfoController : ControllerBase
    {

        private readonly ICache MyCache;
      
        public StockInfoController(ICache cache)
        {

            MyCache = cache;
           
        }
        /// <summary>
        /// 依照指定的股票代碼取得資料
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        [HttpGet("get/{stockId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockInfo(string stockId)
        {
            var cacheInfo =await  MyCache.GetStockIdInfo(stockId.ToString());
            return Ok(cacheInfo);
        }

    }
}

