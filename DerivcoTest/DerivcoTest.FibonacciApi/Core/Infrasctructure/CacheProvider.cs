﻿using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace DerivcoTest.FibonacciApi.Core.Infrasctructure
{
    public class CacheProvider : ICacheProvider
    {
        private const int CacheSeconds = 100; // 100 Seconds

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetFromCache<T>(int key) where T : class
        {
            var cachedResponse = _cache.Get(key);
            return cachedResponse as T;
        }

        public void SetCache<T>(int key, T value) where T : class
        {
            SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
        }

        public void SetCache<T>(int key, T value, DateTimeOffset duration) where T : class
        {
            _cache.Set(key, value, duration);
        }

        public void ClearCache(int key)
        {
            _cache.Remove(key);
        }
    }
}