namespace MConnect.Cache
{
    /// <summary>
    /// Sets the cache-value object by cacheKey for simple (key) and hash (hash and key)
    /// </summary>
    /// <remarks>
    /// Cache-value is set by the generic value type T 
    /// </remarks>
    /// <typeparam name="T">Set cache-value by the generic value type T</typeparam>
    public struct CacheValueType<T>
    {
        /// <value>
        /// Gets the cache expiry time in milliseconds
        /// </value>
        public long Expire { get; set; }
        /// <value>
        /// Gets the cache-value of the generic type T
        /// </value>
        public T Value { get; set; }

    }

    /// <summary>
    ///  Set the cache-response for setCache, getCache, deleteCache and clearCache methods
    /// </summary>
    /// <remarks>
    /// Set or Get Cache-value is set by the generic value type T 
    /// </remarks>
    /// <typeparam name="T">Set/get cache-value by the generic value type T</typeparam>
    public struct CacheResponse<T>
    {
        /// <summary>
        /// <value> Indicates success (true) or failure (false) </value>
        /// </summary>
        public bool Ok { get; set; }
        /// <summary>
        /// <value> Descriptive response message </value>
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// <value> Return cache-value (default or actual value) </value>
        /// </summary>
        public T Value { get; set; }
    }
}
