using System;
using System.Collections.Generic;
using MConnect.Common;

namespace MConnect.Cache
{
    /// <summary>
    /// Set, Get and Delete Cache-record by unique cache-key, and Clear Cache
    /// </summary>
    /// <typeparam name="T">Cache-value is set by the generic value type T</typeparam>
    /// <example>
    /// <code>
    /// TODO
    /// </code>
    /// </example>
    public static class SimpleCache<T>
    {
        private const string KeyCode = "mcconnect_20210320";

        private static readonly Dictionary<string, CacheValueType<T>> CacheValue = default;

        // methods
        /// <summary>
        /// <c>SetCache</c> method sets the cache-value by key (cacheKey)
        /// </summary>
        /// <param name="key">Unique cache-key</param>
        /// <param name="value">Cache-value to be stored in memory</param>
        /// <param name="expire">Expiry time in seconds for the store cache-value</param>
        /// <returns>The set-cache-value-response: {Ok: bool, Message: string, Value: T} </returns>
        static CacheResponse<T> SetCache(string key, T value, long expire = Config.Expire)
        {
            try
            {
                // encrypt key
                var cacheKey = key + KeyCode;
                var currentTimeMilliSeconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var expireMilliseconds = currentTimeMilliSeconds + (expire * 1000);
                // validate if non-expired key-value exists
                var getOk = CacheValue.TryGetValue(cacheKey, out var cacheValueObj);
                if (getOk && (!cacheValueObj.Value.Equals(default) || !cacheValueObj.Value.Equals(null)) &&
                    cacheValueObj.Expire > currentTimeMilliSeconds)
                {
                    // return current value
                    return new CacheResponse<T>
                    {
                        Ok = true,
                        Message = "task completed successfully",
                        Value = cacheValueObj.Value
                    };
                }

                // set new cache-value-object {expire, value}
                var cacheValueObject = new CacheValueType<T> {Value = value, Expire = expireMilliseconds};
                // var cacheValueObject = new CacheValueType<T> {Value=value, Expire = expireMilliseconds};
                var setOk = CacheValue.TryAdd(cacheKey, cacheValueObject);
                if (setOk)
                {
                    return new CacheResponse<T>
                    {
                        Ok = true,
                        Message = "task completed successfully",
                        Value = cacheValueObject.Value
                    };
                }

                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = "unable to set cache value",
                    Value = value
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error creating/setting cache information",
                    Value = default
                };
            }
        }

        static CacheResponse<T> GetCache(string key)
        {
            try
            {
                // encrypt key
                var cacheKey = key + KeyCode;
                // get the current 
                var getOk = CacheValue.TryGetValue(cacheKey, out var cacheValueObj);
                var currentTimeMilliSeconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                if (!getOk || cacheValueObj.Value.Equals(default) || cacheValueObj.Value.Equals(null))
                {
                    return new CacheResponse<T>
                    {
                        Ok = false,
                        Message = "cache info does not exist",
                        Value = default
                    };
                }

                if (cacheValueObj.Expire > currentTimeMilliSeconds)
                {
                    // return current value
                    return new CacheResponse<T>
                    {
                        Ok = true,
                        Message = "task completed successfully",
                        Value = cacheValueObj.Value
                    };
                }

                // delete the cacheValue by cacheKey
                CacheValue.Remove(cacheKey);
                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = "cache expired and deleted",
                    Value = default
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error fetching cache information",
                    Value = default
                };
            }
        }

        static CacheResponse<T> DeleteCache(string key)
        {
            try
            {
                // encrypt key
                var cacheKey = key + KeyCode;
                // delete cache-value record
                if (!CacheValue.ContainsKey(cacheKey))
                {
                    return new CacheResponse<T>
                    {
                        Ok = false,
                        Message = "cache key not found",
                        Value = default
                    };
                }

                var delOk = CacheValue.Remove(cacheKey);
                if (delOk)
                {
                    return new CacheResponse<T>
                    {
                        Ok = true,
                        Message = "task completed successfully",
                        Value = default
                    };
                }

                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = "unable to delete cache record or no record exists to remove",
                    Value = default
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>
                {
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error deleting cache information",
                    Value = default
                };
            }
        }

        static CacheResponse<T> ClearCache()
        {
            try
            {
                CacheValue.Clear();
                var result = new CacheResponse<T>
                {
                    Ok = true,
                    Message = "task completed successfully",
                    Value = default
                };
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var result = new CacheResponse<T>
                {
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error clearing cache",
                    Value = default
                };
                return result;
            }
        }
    }
}