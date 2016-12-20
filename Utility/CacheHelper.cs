using System;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web;
using System.Collections;

namespace Utility
{
    public class CacheHelper
    {
        public static object GetCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public static void SetCache(string key,object obj,CacheDependency dependencies,DateTime absoluteExpiration,TimeSpan slidingExpiration,CacheItemPriority priority,CacheItemRemovedCallback onRemoveCallback)
        {
            HttpRuntime.Cache.Insert(key, obj, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public static void SetCache(string key,object obj)
        {
            HttpRuntime.Cache.Insert(key, obj);
        }

        public static void SetCache(string key, object obj,DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, obj,null,absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public static void SetCache(string key, object obj, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public static void AddCache(string key,object obj)
        {
            HttpRuntime.Cache.Add(key, obj, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public static void RemoveAllCache()
        {
            IDictionaryEnumerator cacheEnums = HttpRuntime.Cache.GetEnumerator();
            List<string> keys = new List<string>();
            while (cacheEnums.MoveNext())
            {
                keys.Add(cacheEnums.Key.ToString());
            }
            foreach (string key in keys)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
    }
}
