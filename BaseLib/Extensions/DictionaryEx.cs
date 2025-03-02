﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLib
{
    /// <summary>
    /// 字典扩展方法类
    /// </summary>
    public static class DictionaryEx
    {
        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, value);
            else
                @this[key] = value;

            return @this[key];
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <returns></returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this,
            IDictionary<TKey, TValue> that)
        {
            foreach (var item in that) AddOrUpdate(@this, item.Key, item.Value);
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <returns></returns>
        public static void AddOrUpdateTo<TKey, TValue>(this IDictionary<TKey, TValue> @this,
            IDictionary<TKey, TValue> that)
        {
            foreach (var item in @this) AddOrUpdate(that, item.Key, item.Value);
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, addValue);
            else
                @this[key] = updateValueFactory(key, @this[key]);

            return @this[key];
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static async Task<TValue> AddOrUpdateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key,
            TValue addValue, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, addValue);
            else
                @this[key] = await updateValueFactory(key, @this[key]);

            return @this[key];
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <param name="updateValue">更新时的值</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue,
            TValue updateValue)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, addValue);
            else
                @this[key] = updateValue;

            return @this[key];
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this,
            IDictionary<TKey, TValue> that, Func<TKey, TValue, TValue> updateValueFactory)
        {
            foreach (var item in that) AddOrUpdate(@this, item.Key, item.Value, updateValueFactory);
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static void AddOrUpdateTo<TKey, TValue>(this IDictionary<TKey, TValue> @this,
            IDictionary<TKey, TValue> that, Func<TKey, TValue, TValue> updateValueFactory)
        {
            foreach (var item in @this) AddOrUpdate(that, item.Key, item.Value, updateValueFactory);
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValueFactory">添加时的操作</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key,
            Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, addValueFactory(key));
            else
                @this[key] = updateValueFactory(key, @this[key]);

            return @this[key];
        }

        /// <summary>
        ///     添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValueFactory">添加时的操作</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static async Task<TValue> AddOrUpdateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key,
            Func<TKey, Task<TValue>> addValueFactory, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
                @this.Add(key, await addValueFactory(key));
            else
                @this[key] = await updateValueFactory(key, @this[key]);

            return @this[key];
        }

        /// <summary>
        ///     获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="addValueFactory"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key,
            Func<TValue> addValueFactory)
        {
            if (!@this.ContainsKey(key)) @this.Add(key, addValueFactory());

            return @this[key];
        }

        /// <summary>
        ///     获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="addValueFactory"></param>
        /// <returns></returns>
        public static async Task<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key,
            Func<Task<TValue>> addValueFactory)
        {
            if (!@this.ContainsKey(key)) @this.Add(key, await addValueFactory());

            return @this[key];
        }

        /// <summary>
        ///     获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="addValue"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue)
        {
            if (!@this.ContainsKey(key)) @this.Add(key, addValue);

            return @this[key];
        }

        /// <summary>
        ///     遍历IEnumerable
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="action">回调方法</param>
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dic, Action<TKey, TValue> action)
        {
            foreach (var item in dic) action(item.Key, item.Value);
        }

        /// <summary>
        ///     安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <returns></returns>
        public static Dictionary<TKey, TSource> ToDictionarySafety<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var dic = new Dictionary<TKey, TSource>();
            foreach (var item in source) AddOrUpdate(dic, keySelector(item), item);

            return dic;
        }

        /// <summary>
        ///     安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static Dictionary<TKey, TElement> ToDictionarySafety<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var dic = new Dictionary<TKey, TElement>();
            foreach (var item in source) AddOrUpdate(dic, keySelector(item), elementSelector(item));

            return dic;
        }

        /// <summary>
        ///     安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>

        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <returns></returns>
        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var dic = new ConcurrentDictionary<TKey, TSource>();
            foreach (var item in source) AddOrUpdate(dic, keySelector(item), item);

            return dic;
        }

        /// <summary>
        ///     安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var dic = new ConcurrentDictionary<TKey, TElement>();
            foreach (var item in source) AddOrUpdate(dic, keySelector(item), elementSelector(item));

            return dic;
        }
    }
}