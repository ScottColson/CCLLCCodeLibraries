﻿using System;

namespace CCLLC.Core
{
    public interface ICache
    {
        void Add(string key, object data, TimeSpan lifetime);

        void Add(string key, object data, int seconds);

        void Add<T>(string key, T data, TimeSpan lifetime);

        void Add<T>(string key, T data, int seconds);

        object Get(string key);

        T Get<T>(string key);

        bool Exists(string key);

        void Remove(string key);
    }
}
