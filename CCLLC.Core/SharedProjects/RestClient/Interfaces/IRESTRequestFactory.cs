namespace CCLLC.Core.RESTClient
{
    public interface IRESTRequestFactory
    {
        T CreateRequest<T>(IRESTEndpointConfiguration configuration) where T : class, IRESTRequest;
    }
}
