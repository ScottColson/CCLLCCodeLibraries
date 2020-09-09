using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace CCLLC.Core.Serialization
{   
    public class DateOnlyJSONSerializer : IJSONContractSerializer
    {
        const string DateTimeFormat = "yyyy-MM-dd";

        public T Deserialize<T>(string data) where T : class, ISerializableData
        {
            var serializer = getSerializer<T>();           

            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(data));
            stream.Position = 0;

            var item = serializer.ReadObject(stream);
            return (T)item;
        }

        public string Serialize<T>(T data) where T : ISerializableData
        {
            var serializer = getSerializer<T>();

            MemoryStream stream = new MemoryStream();
            
            serializer.WriteObject(stream, data);
            stream.Position = 0;

            StreamReader reader = new StreamReader(stream,System.Text.Encoding.ASCII);
            
            var serializedData = reader.ReadToEnd();
            return serializedData;
        }

        DataContractJsonSerializer getSerializer<T>()
        {
            return new DataContractJsonSerializer(typeof(T),
              new DataContractJsonSerializerSettings
              {
                  UseSimpleDictionaryFormat = true,
                  DateTimeFormat = new DateTimeFormat(DateTimeFormat)
              });
        }
    }
}
