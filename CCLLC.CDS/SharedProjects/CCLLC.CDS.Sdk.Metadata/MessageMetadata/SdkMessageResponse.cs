using System;
using System.Collections.Generic;

namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public sealed class SdkMessageResponse
    {
        public SdkMessageResponse(Guid id)
        {
            this.Id = id;
            this.ResponseFields = new Dictionary<int, SdkMessageResponseField>();
        }

        public Guid Id { get; }
        public Dictionary<int, SdkMessageResponseField> ResponseFields { get; }
    }
}
