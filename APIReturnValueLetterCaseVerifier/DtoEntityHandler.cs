using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIReturnValueLetterCaseVerifier
{
    internal class DtoEntityHandler
    {
        public DtoEntityHandler(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        private Assembly Assembly { get; set; }

        public IEnumerable<Entry> GetEntities()
        {
            var types = Assembly.GetTypes();
            List<Entry> entries = new List<Entry>();

            const string typeEndName = "Dto";
            foreach (var type in types)
            {
                if (type.FullName.EndsWith(typeEndName, StringComparison.OrdinalIgnoreCase))
                {
                    IEnumerable<Entry> badEntries = HandleProperty(type.GetProperties());
                    entries.AddRange(badEntries);
                }
            }

            return entries;
        }

        private IEnumerable<Entry> HandleProperty(IEnumerable<PropertyInfo> propertyInfos)
        {
            foreach (var property in propertyInfos)
            {
                if (FilterIn(property))
                {
                    string location = string.Format("{0} -> {1} -> {2}", property.DeclaringType.FullName, property.PropertyType.FullName, property.Name);
                    string value = GetJsonPropertyName(property);
                    yield return new Entry() { Location = location, Value = value };
                }
            }
        }

        private string GetJsonPropertyName(PropertyInfo property)
        {
            string value;

            var attributes = property.GetCustomAttributes<JsonPropertyAttribute>(false);
            if (attributes != null && attributes.Count() > 0)
            {
                value = attributes.ElementAt(0).PropertyName;
            }
            else
            {
                value = property.Name;
            }

            return value;
        }

        private bool FilterIn(PropertyInfo property)
        {
            var jsonPropertyName = GetJsonPropertyName(property);
            if (string.IsNullOrEmpty(jsonPropertyName))
            {
                return false;
            }

            return jsonPropertyName.Any(charX => char.IsUpper(charX));
        }
    }
}
