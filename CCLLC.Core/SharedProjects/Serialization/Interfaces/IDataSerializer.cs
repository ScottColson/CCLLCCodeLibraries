namespace CCLLC.Core.Serialization
{
    public interface IDataSerializer
    {
        string Serialize<T>(T data) where T : ISerializableData;

        T Deserialize<T>(string data) where T : class, ISerializableData;
    }
}
