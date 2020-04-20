namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Serialization;

    public interface ISerializedRESTResponse : IRESTResponse, ISerializableData
    {
    }
}
