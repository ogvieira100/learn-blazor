using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BlazorAppTreino.Domain.Utils
{
    public class Commons
    {

        public static T ParseQueryStringToObject<T>(string queryString) where T : new()
        {
            if (string.IsNullOrWhiteSpace(queryString))
                throw new ArgumentException("Query string cannot be null or empty.");

            var obj = new T();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propDict = properties.ToDictionary(p => p.Name.ToLower(), p => p);
            var parsedQuery = HttpUtility.ParseQueryString(queryString);
            var valuesDict = new Dictionary<string, object>();

            foreach (string key in parsedQuery)
            {
                if (key == null) continue;
                var normalizedKey = key.ToLower();
                var value = parsedQuery.GetValues(key);

                if (propDict.ContainsKey(normalizedKey))
                {
                    var prop = propDict[normalizedKey];

                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listType = prop.PropertyType.GetGenericArguments()[0];
                        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));

                        foreach (var val in value)
                        {
                            list.Add(ConvertValue(listType, val));
                        }
                        valuesDict[normalizedKey] = list;
                    }
                    else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                    {
                        valuesDict[normalizedKey] = ParseNestedObject(prop.PropertyType, parsedQuery, normalizedKey);
                    }
                    else
                    {
                        valuesDict[normalizedKey] = Convert.ChangeType(value[0], prop.PropertyType);
                    }
                }
            }

            foreach (var kvp in valuesDict)
            {
                var prop = propDict[kvp.Key];
                prop.SetValue(obj, kvp.Value);
            }

            return obj;
        }

        private static object ConvertValue(Type targetType, string value)
        {
            if (targetType.IsClass && targetType != typeof(string))
            {
                return ParseNestedObject(targetType, HttpUtility.ParseQueryString(value), string.Empty);
            }
            return Convert.ChangeType(value, targetType);
        }

        private static object ParseNestedObject(Type type, System.Collections.Specialized.NameValueCollection parsedQuery, string prefix)
        {
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propDict = properties.ToDictionary(p => p.Name.ToLower(), p => p);

            foreach (string key in parsedQuery)
            {
                if (key == null || !key.StartsWith(prefix + ".")) continue;
                var normalizedKey = key.Substring(prefix.Length + 1).ToLower();
                var value = parsedQuery.GetValues(key);

                if (propDict.ContainsKey(normalizedKey))
                {
                    var prop = propDict[normalizedKey];
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listType = prop.PropertyType.GetGenericArguments()[0];
                        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));
                        foreach (var val in value)
                        {
                            list.Add(ConvertValue(listType, val));
                        }
                        prop.SetValue(obj, list);
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(value[0], prop.PropertyType));
                    }
                }
            }

            return obj;
        }
    }
}
