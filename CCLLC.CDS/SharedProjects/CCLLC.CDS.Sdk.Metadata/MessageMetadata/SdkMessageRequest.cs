using System;
using System.Collections.Generic;

namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public sealed class SdkMessageRequest
    {
        public SdkMessageRequest(SdkMessagePair messagePair, Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.RequestFields = new Dictionary<int, SdkMessageRequestField>();
        }

        public SdkMessagePair MessagePair { get; }
        public Guid Id { get; }
        public string Name { get; }
        public Dictionary<int, SdkMessageRequestField> RequestFields { get; }
    }
}
