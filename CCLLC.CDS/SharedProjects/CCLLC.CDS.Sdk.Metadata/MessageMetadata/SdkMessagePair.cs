using System;

namespace CCLLC.CDS.Sdk.Metadata
{
    
    public sealed class SdkMessagePairMetadata
    {
        public SdkMessagePairMetadata(Guid id, string messageNamespace, SdkMessageRequestMetadata requestMetadata, SdkMessageResponseMetadata responseMetadata) 
        {           
            this.Id = id;
            this.MessageNamespace = messageNamespace;
            this.RequestMetadata = requestMetadata;
            this.ResponseMetadata = responseMetadata;
        }

        public Guid Id { get; }
        public string MessageNamespace { get; }
        public SdkMessageRequestMetadata RequestMetadata { get; }
        public SdkMessageResponseMetadata ResponseMetadata { get; }
    }
}
