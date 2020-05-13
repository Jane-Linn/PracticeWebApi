using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplication3.Model;

namespace WebApplication3.Service
{
    public class SearchStockInfo : ISearchStockInfo
    {
        //用DI
        private string ConnectionString = "Persist Security Info = False; User ID = sa; Password = 7319; Initial Catalog = AdventureWorks; database=StockDb;Server = CMONEYTEST99";
        private string queryStockId = $@"SELECT  [RecordID]
                                                          ,[日期]
                                                          ,[股票代號]
                                                          ,[股票名稱]
                                                          ,[參考價]
                                                          ,[開盤價]
                                                          ,[最高價]
                                                          ,[最低價]
                                                          ,[收盤價]
                                                      FROM[StockDb].[dbo].[日收盤] Where 股票代號 = @Input ";
        //用DI                                       
        
        private int SqlCount;
        
        public SearchStockInfo()
        {
           

        }
        public async Task<StockInfo[]> SearchDbByStockId(string stockId)
        {
            SqlCount = Interlocked.Increment(ref SqlCount);
            Console.WriteLine("查sql次數: " + SqlCount);
            await Task.Delay(10000);
            return DoSql().ToArray();
            //區域方法
            IEnumerable<StockInfo> DoSql()
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryStockId, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Input", stockId);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    StockInfo stockInfo = new StockInfo();
                                    stockInfo.RecordId = reader.IsDBNull(0) ? "NULL" : reader.GetInt64(0).ToString();
                                    stockInfo.Date = reader.GetString(1);
                                    stockInfo.StockId = reader.GetString(2);
                                    stockInfo.StockName = reader.IsDBNull(3) ? "NULL" : reader.GetString(3);
                                    stockInfo.RefPrice = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4);
                                    stockInfo.OpenPrice = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                                    stockInfo.HighestPrice = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                                    stockInfo.LowestPrice = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);
                                    stockInfo.ClosePrice = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                                    yield return stockInfo;
                                }
                            }
                        }
                    }
                }
            }
        }

    

    
    }
}
