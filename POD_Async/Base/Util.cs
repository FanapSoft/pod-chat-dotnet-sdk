using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using POD_Async.Core;

namespace POD_Async.Base
{
    public static class Util
    {
        public static List<KeyValuePair<string, string>> ValidateByAttribute(this object builder)
        {
            var hasErrorFields = new List<KeyValuePair<string, string>>();
            var type = builder.GetType();
            var context = new ValidationContext(builder, null, null);
            var fields = type.GetRuntimeFields()
                .Where(_ => _.GetCustomAttributes(typeof(ValidationAttribute), false).Any());
            foreach (var field in fields)
            {
                var results = new List<ValidationResult>();
                var attributes = field
                    .GetCustomAttributes(false)
                    .OfType<ValidationAttribute>()
                    .ToArray();
                Validator.TryValidateValue(field.GetValue(builder), context, results, attributes);
                foreach (var result in results)
                {
                    hasErrorFields.Add(new KeyValuePair<string, string>(field.Name, result.ErrorMessage));
                }
            }

            return hasErrorFields;
        }

        public static List<KeyValuePair<string, string>> ValidateFieldAndPropertyByAttribute(this object builder)
        {
            var hasErrorFields = new List<KeyValuePair<string, string>>();
            var type = builder.GetType();
            var context = new ValidationContext(builder, null, null);
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance |
                               BindingFlags.FlattenHierarchy;
            var members = GetFieldsAndProperties(type, bindingFlags)
                .Where(_ => _.GetCustomAttributes(typeof(ValidationAttribute), false).Any());
            foreach (var member in members)
            {
                var results = new List<ValidationResult>();
                var attributes = member
                    .GetCustomAttributes(false)
                    .OfType<ValidationAttribute>()
                    .ToArray();

                if (member is FieldInfo field)
                {
                    Validator.TryValidateValue(field.GetValue(builder), context, results, attributes);
                }
                else if (member is PropertyInfo property)
                {
                    Validator.TryValidateValue(property.GetValue(builder), context, results, attributes);
                }

                foreach (var result in results)
                {
                    hasErrorFields.Add(new KeyValuePair<string, string>(member.Name, result.ErrorMessage));
                }
            }

            return hasErrorFields;
        }

        public static List<KeyValuePair<string, string>> FilterNotNull<T>(this T obj, Dictionary<string, string> podParameterName, bool ignoreNullValue = true)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            var properties = obj.GetType().GetProperties(bindingFlags);
            var notNullProperties = new List<KeyValuePair<string, string>>();
            foreach (var pr in properties)
            {
                var val = pr.GetValue(obj);
                if (ignoreNullValue && val == null) continue;
                if (val is IList valueList)
                {
                    if (val is Array)
                    {
                        podParameterName.TryGetValue(pr.Name, out var podName);
                        foreach (var value in valueList)
                        {
                            notNullProperties.Add(new KeyValuePair<string, string>(podName, value.ToString(CultureInfo.InvariantCulture)));
                        }
                    }
                    else
                    {
                        var objects = valueList.Cast<object>().ToList();
                        foreach (var ob in objects)
                        {
                            notNullProperties.AddRange(ob.FilterNotNull(podParameterName, ignoreNullValue));
                        }
                    }
                }
                else if (pr.PropertyType.IsClass && pr.PropertyType.Assembly.FullName.StartsWith("POD_"))
                {
                    notNullProperties.AddRange(val.FilterNotNull(podParameterName, ignoreNullValue));
                }
                else
                {
                    podParameterName.TryGetValue(pr.Name, out var podName);
                    notNullProperties.Add(new KeyValuePair<string, string>(podName, val.ToString(CultureInfo.InvariantCulture)));
                }
            }

            return notNullProperties;
        }
        public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
        {
            var targetMembers = new List<MemberInfo>();

            targetMembers.AddRange(type.GetRuntimeFields());
            targetMembers.AddRange(type.GetProperties(bindingAttr));

            return targetMembers;
        }

        public static string ToJsonWithNotNullProperties(this object obj)
        {
            var serializeObject = JsonConvert.SerializeObject(obj,Formatting.None,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            return serializeObject;
        }

        public static string ToString(this object value, CultureInfo cultureInfo)
        {
            return string.Format(cultureInfo, "{0}", value);
        }

        public static string ToJson(this object obj)
        {
            var serializeObject = JsonConvert.SerializeObject(obj,Formatting.None);
            return serializeObject;
        }

        public static string GetImageUrl(long imageId, string hashCode, bool downloadable)
        {
            var url = BaseUrl.FileServerAddress;

            url += "/nzh/image/" + "?imageId=" + imageId;

            if (downloadable)
            {
                url += "&downloadable=" + downloadable + "&hashCode=" + hashCode;
            }
            else
            {
                url += "&hashCode=" + hashCode;
            }

            return url;
        }
        public static string GetFile(long fileId, string hashCode, bool downloadable)
        {
            var url = BaseUrl.FileServerAddress;

            return url + "/nzh/file/" + "?fileId=" + fileId + "&downloadable=" + downloadable + "&hashCode=" + hashCode;
        }
    }
}
