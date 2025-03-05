using BlazorAppTreino.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BlazorAppTreino.Domain.Utils
{
    public enum EventNotification
    {
        Insert = 1,
        Update = 2,
        Delete = 3

    }

    public enum Priority
    {
        High = 1,
        Average = 2,
        Low = 3
    }

    public enum Layer
    {
        App = 1,
        Domain = 2,
        Repository = 3,
        Others = 4
    }
    public enum TypeNotificationNoty
    {
        Alert = 1,
        Error = 2,
        Sucess = 3,
        Information = 4,
        BreakSystem = 5
    }

    public enum NotyIntention
    {

    }
    public class Notys {

        public Priority Priority { get; set; }

        public Layer? Layer { get; set; }

        public TypeNotificationNoty TypeNotificationNoty { get; set; }

        public string Message { get; set; }

        public NotyIntention? NotyIntention { get; set; }

        public List<string> PropertsErrors { get; set; }

        public Notys()
        {
            Priority = Priority.Average;
            TypeNotificationNoty = TypeNotificationNoty.Error;
            PropertsErrors = new List<string>();
        }

    }
    public class ResponseApi<T> where T:class
    {

        public T Obj { get; set; }

        public List<Notys> Notys { get; set; }

        public ResponseApi()
        {
            Notys = new List<Notys>();
            Obj =  Activator.CreateInstance<T>();
        }
    }
    public class Commons
    {


        public static string ToQueryString<T>(T obj, string prefix = "")
        {
            if (obj == null) return string.Empty;
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var queryString = new StringBuilder();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;
                string key = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";

                if (value is IList list)
                {
                    foreach (var item in list)
                    {
                        queryString.AppendFormat("{0}={1}&", key, Uri.EscapeDataString(item.ToString()));
                    }
                }
                else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
                {
                    queryString.Append(ToQueryString(value, key));
                }
                else
                {
                    queryString.AppendFormat("{0}={1}&", key, Uri.EscapeDataString(value.ToString()));
                }
            }

            return queryString.ToString().TrimEnd('&');
        }



        public static async Task<ResponseApi<T>> TreatResponse<T>(Func<Task<T>> func) 
            where T: class {

            var obj = Activator.CreateInstance<ResponseApi<T>>();
            try
            {
                var objResp =  await func.Invoke();
                obj.Obj = objResp ?? Activator.CreateInstance<T>();
                return obj;

            }
            catch (Exception ex)
            {
                obj.Notys.Add(new Notys {
                    Message  = ex.Message
                });
            }
            return obj;
        }

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
