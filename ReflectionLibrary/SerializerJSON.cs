using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLibrary
{
    public class SerializerJSON<T> : Serializer<T>
    {
        private readonly List<PropertyInfo> _properties;
        private readonly T _value;

        public SerializerJSON(T value)
        {
            _value = value;
            _properties = Parse(value);
        }

        public override void Serialize(string filePath)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter writer = new StreamWriter(filePath))
                using (JsonWriter json = new JsonTextWriter(writer))
                {
                    serializer.Serialize(json, _value);
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
                JsonSerializer serializer = new JsonSerializer();

                using (StreamReader reader = new StreamReader(filePath))
                using (JsonTextReader json = new JsonTextReader(reader))
                {
                    return serializer.Deserialize<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
