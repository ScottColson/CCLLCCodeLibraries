namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    public interface IRESTRequest<T> where T : class, IRESTResponse
    {
        IAPIEndpoint ApiEndpoint { get; }
        T Execute(IWebRequest webRequest);
    }
}
