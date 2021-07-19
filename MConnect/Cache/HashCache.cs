using System;
using System.Collections.Generic;
using MConnect.Common;

namespace MConnect.Cache
{
    public static class HashCache<T>
    {
        private const string KeyCode = "mcconnect_20210320";

        private static readonly Dictionary<string, Dictionary<string, CacheValueType<T>>> HashCacheValue = default;

        // TODO: methods
        static CacheResponse<T> SetCache(string key, string hash, T value, long expire = Config.Expire)
        {
            try
            {
                // TODO: validate if non-expired key-value exists
                // encrypt key
                var cacheKey = key + KeyCode;
                var hashKey = hash + KeyCode;
                var currentTimeMilliSeconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var expireMilliseconds = currentTimeMilliSeconds + (expire * 1000);
                // validate if non-expired key-value exists
                var getOk = HashCacheValue.TryGetValue(hashKey, out var hashCacheValueObj);
                if (getOk)
                {
                    var getHashOk = hashCacheValueObj.TryGetValue(cacheKey, out var cacheValueObj);
                    if (getHashOk && (!cacheValueObj.Value.Equals(default) || !cacheValueObj.Value.Equals(null)) &&
                        cacheValueObj.Expire > currentTimeMilliSeconds)
                    {
                        // return current value
                        var cacheResult = new CacheResponse<T>{
                            Ok = true,
                            Message = "task completed successfully",
                            Value = cacheValueObj.Value
                        };
                        return cacheResult;
                    }
                }

                // set new cache-value-object {expire, value}
                var cacheValueObject = new CacheValueType<T>{Value = value, Expire = expireMilliseconds};
                if (!HashCacheValue.ContainsKey(hashKey))
                {
                    hashCacheValueObj = new Dictionary<string, CacheValueType<T>>();
                    HashCacheValue.Add(hashKey, hashCacheValueObj);
                }

                var setOk = HashCacheValue[hashKey].TryAdd(cacheKey, cacheValueObject);
                if (setOk)
                {
                    return new CacheResponse<T>{
                        Ok = true,
                        Message = "task completed successfully",
                        Value = cacheValueObject.Value
                    };
                }

                return new CacheResponse<T>{
                    Ok = false,
                    Message = "unable to set cache value",
                    Value = value
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>{
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error creating/setting cache information",
                    Value = default
                };
            }
        }

        static CacheResponse<T> GetCache(string key, string hash)
        {
            try
            {
                // encrypt key
                var cacheKey = key + KeyCode;
                var hashKey = hash + KeyCode;
                var currentTimeMilliSeconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var getOk = HashCacheValue.TryGetValue(hashKey, out var hashCacheValueObj);

                if (!getOk || hashCacheValueObj.Equals(default) || hashCacheValueObj.Equals(null))
                {
                    return new CacheResponse<T>{
                        Ok = false,
                        Message = "cache info does not exist",
                        Value = default
                    };
                }

                var getHashOk = hashCacheValueObj.TryGetValue(cacheKey, out var cacheValueObj);
                if (!getHashOk || cacheValueObj.Value.Equals(default) || cacheValueObj.Value.Equals(null))
                {
                    return new CacheResponse<T>{
                        Ok = false,
                        Message = "cache info does not exist",
                        Value = default
                    };
                }

                if (cacheValueObj.Expire > currentTimeMilliSeconds)
                {
                    // return current value
                    return new CacheResponse<T>{
                        Ok = true,
                        Message = "task completed successfully",
                        Value = cacheValueObj.Value
                    };
                }

                // delete the cacheValue by cacheKey
                HashCacheValue[hashKey].Remove(cacheKey);
                return new CacheResponse<T>{
                    Ok = false,
                    Message = "cache expired and deleted",
                    Value = default
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>{
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error fetching cache information",
                    Value = default
                };
            }
        }

        static CacheResponse<T> DeleteCache(string key, string hash, string by = "key")
        {
            try
            {
                // encrypt key
                var cacheKey = key + KeyCode;
                var hashKey = hash + KeyCode;
                // delete cache-value record by cacheKey(default) or hashKey
                if (by == "key")
                {
                    if (!HashCacheValue.ContainsKey(hashKey) || !HashCacheValue[hashKey].ContainsKey(cacheKey))
                    {
                        return new CacheResponse<T>{
                            Ok = false,
                            Message = "cache key not found",
                            Value = default
                        };
                    }

                    var delOk = HashCacheValue[hashKey].Remove(cacheKey);
                    if (delOk)
                    {
                        return new CacheResponse<T>{
                            Ok = true,
                            Message = "task completed successfully",
                            Value = default
                        };
                    }

                    return new CacheResponse<T>{
                        Ok = false,
                        Message = "unable to delete cache record or no record exists to remove",
                        Value = default
                    };
                }

                // else remove by hash
                if (!HashCacheValue.ContainsKey(hashKey))
                {
                    {
                        return new CacheResponse<T>{
                            Ok = false,
                            Message = "cache key not found",
                            Value = default
                        };
                    }
                }

                if (!HashCacheValue.ContainsKey(hashKey))
                {
                    return new CacheResponse<T>{
                        Ok = false,
                        Message = "cache key not found",
                        Value = default
                    };
                }

                var delHashOk = HashCacheValue.Remove(hashKey);
                if (delHashOk)
                {
                    return new CacheResponse<T>{
                        Ok = true,
                        Message = "task completed successfully",
                        Value = default
                    };
                }

                return new CacheResponse<T>{
                    Ok = false,
                    Message = "unable to delete cache record or no record exists to remove",
                    Value = default
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var result = new CacheResponse<T>{
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error deleting cache information",
                    Value = default
                };
                return result;
            }
        }

        static CacheResponse<T> ClearCache()
        {
            try
            {
                HashCacheValue.Clear();
                return new CacheResponse<T>{
                    Ok = true,
                    Message = "task completed successfully",
                    Value = default
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CacheResponse<T>{
                    Ok = false,
                    Message = e.Message != "" ? e.Message : "error clearing cache",
                    Value = default
                };
            }
        }
    }
}