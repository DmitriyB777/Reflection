using ReflectionLibrary;

namespace ReflectionConsoleApp
{
    public class F 
    { 
        public int i1 { get; init; }
        public int i2 { get; init; }
        public int i3 { get; init; }
        public int i4 { get; init; }
        public int i5 { get; init; }    
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            F f = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

            Console.WriteLine("CSV");

            Console.WriteLine($"Время на сериализацию = {TimeSerialize(new SerializerCSV<F>(f), "data.csv", 100000)} мс");
            Console.WriteLine($"Время на десериализацию = {TimeDeserialize(new SerializerCSV<F>(f), "data.csv", 100000)} мс");

            Console.WriteLine("JSON");

            Console.WriteLine($"Время на сериализацию = {TimeSerialize(new SerializerJSON<F>(f), "data.json", 100000)} мс");
            Console.WriteLine($"Время на десериализацию = {TimeDeserialize(new SerializerJSON<F>(f), "data.json", 100000)} мс");

            Console.ReadKey();
        }

        static double TimeSerialize(Serializer<F> serializer, string filePath, int iter)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < iter; i++)
            {
                serializer.Serialize(filePath);
            }

            DateTime end = DateTime.Now;

            return (end - start).TotalMilliseconds;
        }

        static double TimeDeserialize(Serializer<F> serializer, string filePath, int iter)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < iter; i++)
            {
                F res = serializer.Deserialize(filePath);
            }

            DateTime end = DateTime.Now;

            return (end - start).TotalMilliseconds;
        }
    }
}
