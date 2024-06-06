using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLibrary
{
    public class SerializerCSV<T> : Serializer<T>
    {
        private readonly List<PropertyInfo> _properties;
        private readonly T _value;
        private readonly string _separator;

        public SerializerCSV(T value)
        {
            _value = value;
            _properties = Parse(value);
            _separator = ";";
        }

        public override void Serialize(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (PropertyInfo property in _properties)
                    {
                        writer.WriteLine(string.Join(_separator, property.Name, property.GetValue(_value)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override T Deserialize(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    List<string[]> rows = new List<string[]>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(_separator);

                        rows.Add(values);
                    }

                    Type type = typeof(T);
                    T obj = Activator.CreateInstance<T>();

                    foreach (var row in rows)
                    {
                        PropertyInfo propertyInfo = type.GetProperty(row[0]);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[1], propertyInfo.PropertyType));
                    }

                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
