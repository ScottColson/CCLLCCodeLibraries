namespace CCLLC.Core.Serialization
{
    public interface ISerializableData
    {
        string ToString(IDataSerializer serializer);
    }
}
