using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Cache
{
    public class InfoCache
    {
        IMemoryCache memoryCache = new IMemoryCache(); 
    }
}
