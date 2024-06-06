using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLibrary
{
    public abstract class Serializer<T>
    {
        public abstract void Serialize(string filePath);
        public abstract T Deserialize(string filePath);
        protected List<PropertyInfo> Parse(T obj)
        {
            Type type = typeof(T);

            PropertyInfo[] properties = type.GetProperties();

            List<PropertyInfo> names = properties.Where(p => p.PropertyType.IsValueType)
                                                 .ToList();

            return names;
        }
    }
}
