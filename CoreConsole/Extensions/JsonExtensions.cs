using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CoreConsole.Extensions
{
    public static class JsonExtensions
    {
        public static T GetValueOrDefault<T>(this JsonElement element, string path, T defaultValue)
        {
            var parts = path.Split('.', StringSplitOptions.RemoveEmptyEntries);
            JsonElement currentElement;
            for (int i = 0; i < parts.Length; i++)
            {
                if (element.TryGetProperty(parts[i], out currentElement))
                {
                    element = currentElement;
                    if (i == parts.Length - 1)
                    {
                        return JsonSerializer.Deserialize<T>(element.GetRawText());
                    }
                }
                else
                {
                    return defaultValue;
                }
            }

            //still here..
            return defaultValue;
        }

        public static List<JsonElement> GetList(this JsonElement element, string path = "")
        {
            if (path == "")
            {
                return element.EnumerateArray().ToList();
            }

            var result = new List<JsonElement>();
            var parts = path.Split('.', StringSplitOptions.RemoveEmptyEntries);
            JsonElement currentElement;
            for (int i = 0; i < parts.Length; i++)
            {
                if (element.TryGetProperty(parts[i], out currentElement))
                {
                    element = currentElement;
                    if (i == parts.Length - 1)
                    {
                        return element.EnumerateArray().ToList();
                    }
                }
                else
                {
                    return result;
                }
            }
            return result;
        }
    }
}
