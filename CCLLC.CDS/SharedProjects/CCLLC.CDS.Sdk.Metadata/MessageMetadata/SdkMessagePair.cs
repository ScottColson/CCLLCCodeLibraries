using System;

namespace CCLLC.CDS.Sdk.MessageMetadata
{
    
    public sealed class SdkMessagePair
    {
        public SdkMessagePair(SdkMessage message, Guid id, string messageNamespace) 
        {
            this.Message = message;
            this.Id = id;
            this.MessageNamespace = messageNamespace;
            

        }

        public Guid Id { get; }
        public string MessageNamespace { get; }
        public SdkMessage Message { get; set; }
        public SdkMessageRequest Request { get; set; }
        public SdkMessageResponse Response { get; set; }
    }
}
