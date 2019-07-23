using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.Managers;

namespace Cinema.Extensions
{
    public static class CacheManagerExtensions
    {
        public static TResult CacheResult<TResult>(this ICacheManager cacheManager,
            Func<TResult> dataGetter, string cacheKey, TimeSpan? explanationSpan = null) where TResult : class
        {
            var resultModel = cacheManager.Get<TResult>(cacheKey);

            if (resultModel == null)
            {
                resultModel = dataGetter();

                if (resultModel!=null)
                {
                    if (explanationSpan.HasValue)
                    {
                        cacheManager.Insert(resultModel, cacheKey, explanationSpan.Value);
                    }
                    else
                    {
                        cacheManager.Insert(resultModel, cacheKey);
                    }
                }
            }
            return resultModel;
        }

        public static IEnumerable<TResult> CacheResult<TResult>(this ICacheManager cacheManager,
           Func <IEnumerable<TResult>> dataGetter, string cacheKey, TimeSpan? explanationSpan = null) where TResult : class
        {
            var resultModel = cacheManager.Get<IEnumerable<TResult>>(cacheKey);

            if (resultModel == null)
            {
                resultModel = dataGetter();

                if (resultModel.Any())
                {
                    if (explanationSpan.HasValue)
                    {
                        cacheManager.Insert(resultModel, cacheKey, explanationSpan.Value);
                    }
                    else
                    {
                        cacheManager.Insert(resultModel, cacheKey);
                    }
                }
            }
            return resultModel;
        }

        public static TResult CacheResult<TResult>(this ICacheManager cacheManager,
            Func<TResult> dataGetter, string cacheKey, DateTime explanationTime) where TResult : class
        {
            var resultModel = cacheManager.Get<TResult>(cacheKey);

            if (resultModel == null)
            {
                resultModel = dataGetter();

                if (resultModel != null)
                {
                   
                        cacheManager.Insert(resultModel, cacheKey, explanationTime);
                    
                }
            }
            return resultModel;
        }

        public static IEnumerable<TResult> CacheResult<TResult>(this ICacheManager cacheManager,
           Func<IEnumerable<TResult>> dataGetter, string cacheKey, DateTime explanationTime) where TResult : class
        {
            var resultModel = cacheManager.Get<IEnumerable<TResult>>(cacheKey);

            if (resultModel == null)
            {
                resultModel = dataGetter();

                if (resultModel.Any())
                {
                   cacheManager.Insert(resultModel, cacheKey, explanationTime);
                   
                }
            }
            return resultModel;
        }
    }
}