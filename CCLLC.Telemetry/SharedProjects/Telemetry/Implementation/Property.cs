﻿// <copyright file="Property.cs" company="Microsoft">
// Copyright © Microsoft. All Rights Reserved.
// </copyright>
using System;
using System.Collections.Generic;

namespace CCLLC.Telemetry.Implementation
{
    /// <summary>
    /// A helper class for implementing properties of telemetry and context classes.
    /// </summary>
    internal static class Property
    {
        public const int MaxDictionaryNameLength = 150;
        public const int MaxDependencyTypeLength = 1024;
        public const int MaxValueLength = 8 * 1024;
        public const int MaxResultCodeLength = 1024;
        //public const int MaxEventNameLength = 512;
        public const int MaxNameLength = 1024;
        //public const int MaxMessageLength = 32768;
        public const int MaxUrlLength = 2048;
        public const int MaxDataLength = 8 * 1024;
        public const int MaxTestNameLength = 1024;
        public const int MaxRunLocationLength = 2024;
        public const int MaxAvailabilityMessageLength = 8192;

       

        public static void Set<T>(ref T property, T value) where T : class
        {
            if (value == default(T))
            {
                throw new ArgumentNullException("value");
            }

            property = value;
        }

        public static void Initialize<T>(ref T? property, T? value) where T : struct
        {
            if (!property.HasValue)
            {
                property = value;
            }
        }

        public static void Initialize(ref string property, string value)
        {
            if (string.IsNullOrEmpty(property))
            {
                property = value;
            }
        }

        //public static string SanitizeEventName(this string name)
        //{
        //    return TrimAndTruncate(name, Property.MaxEventNameLength);
        //}

       

        //public static string SanitizeDependencyType(this string value)
        //{
        //    return TrimAndTruncate(value, Property.MaxDependencyTypeLength);
        //}

        //public static string SanitizeResultCode(this string value)
        //{
        //    return TrimAndTruncate(value, Property.MaxResultCodeLength);
        //}

        public static string SanitizeValue(this string value)
        {
            return TrimAndTruncate(value, Property.MaxValueLength);
        }

        public static string SanitizeData(this string message)
        {
            return TrimAndTruncate(message, Property.MaxDataLength);
        }

        public static Uri SanitizeUri(this Uri uri)
        {
            if (uri != null)
            {
                string url = uri.ToString();

                if (url.Length > MaxUrlLength)
                {
                    url = url.Substring(0, MaxUrlLength);

                    // in case that the truncated string is invalid
                    // URI we will not do nothing and let the Endpoint to drop the property
                    Uri temp;
                    if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out temp) == true)
                    {
                        uri = temp;
                    }
                }
            }

            return uri;
        }

        //public static string SanitizeTestName(this string value)
        //{
        //    return TrimAndTruncate(value, Property.MaxTestNameLength);
        //}

        //public static string SanitizeRunLocation(this string value)
        //{
        //    return TrimAndTruncate(value, Property.MaxRunLocationLength);
        //}

        //public static string SanitizeAvailabilityMessage(this string value)
        //{
        //    return TrimAndTruncate(value, Property.MaxAvailabilityMessageLength);
        //}

        public static void SanitizeProperties(this IDictionary<string, string> dictionary)
        {
            if (dictionary != null)
            {
                var sanitizedEntries = new Dictionary<string, KeyValuePair<string, string>>(dictionary.Count);

                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    string sanitizedKey = SanitizeKey(entry.Key);
                    string sanitizedValue = SanitizeValue(entry.Value);

                    if (string.IsNullOrEmpty(sanitizedValue) || (string.CompareOrdinal(sanitizedKey, entry.Key) != 0) || (string.CompareOrdinal(sanitizedValue, entry.Value) != 0))
                    {
                        sanitizedEntries.Add(entry.Key, new KeyValuePair<string, string>(sanitizedKey, sanitizedValue));
                    }
                }

                foreach (KeyValuePair<string, KeyValuePair<string, string>> entry in sanitizedEntries)
                {
                    dictionary.Remove(entry.Key);

                    if (!string.IsNullOrEmpty(entry.Value.Value))
                    {
                        string uniqueKey = MakeKeyUnique(entry.Value.Key, dictionary);
                        dictionary.Add(uniqueKey, entry.Value.Value);
                    }
                }
            }
        }

        public static void SanitizeMeasurements(this IDictionary<string, double> dictionary)
        {
            if (dictionary != null)
            {
                var sanitizedEntries = new Dictionary<string, KeyValuePair<string, double>>(dictionary.Count);

                foreach (KeyValuePair<string, double> entry in dictionary)
                {
                    string sanitizedKey = SanitizeKey(entry.Key);

                    bool valueChanged;
                    double sanitizedValue = Utils.SanitizeNanAndInfinity(entry.Value, out valueChanged);

                    if ((string.CompareOrdinal(sanitizedKey, entry.Key) != 0) || valueChanged)
                    {
                        sanitizedEntries.Add(entry.Key, new KeyValuePair<string, double>(sanitizedKey, sanitizedValue));
                    }
                }

                foreach (KeyValuePair<string, KeyValuePair<string, double>> entry in sanitizedEntries)
                {
                    dictionary.Remove(entry.Key);
                    string uniqueKey = MakeKeyUnique(entry.Value.Key, dictionary);
                    dictionary.Add(uniqueKey, entry.Value.Value);
                }
            }
        }

        public static string TrimAndTruncate(string value, int maxLength)
        {
            if (value != null)
            {
                value = value.Trim();
                value = Truncate(value, maxLength);
            }

            return value;
        }

        private static string Truncate(string value, int maxLength)
        {
            return value.Length > maxLength ? value.Substring(0, maxLength) : value;
        }

        private static string SanitizeKey(string key)
        {
            string sanitizedKey = TrimAndTruncate(key, Property.MaxDictionaryNameLength);
            return MakeKeyNonEmpty(sanitizedKey);
        }

        private static string MakeKeyNonEmpty(string key)
        {
            return string.IsNullOrEmpty(key) ? "required" : key;
        }

        private static string MakeKeyUnique<TValue>(string key, IDictionary<string, TValue> dictionary)
        {
            if (dictionary.ContainsKey(key))
            {
                const int UniqueNumberLength = 3;
                string truncatedKey = Truncate(key, MaxDictionaryNameLength - UniqueNumberLength);
                int candidate = 1;
                do
                {
                    key = truncatedKey + candidate;
                    ++candidate;
                }
                while (dictionary.ContainsKey(key));
            }

            return key;
        }
    }
}
