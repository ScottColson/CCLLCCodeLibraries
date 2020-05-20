using System;
using System.Collections.Generic;


namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public sealed class SdkMessage
    {
        public SdkMessage(Guid id, string name, bool isPrivate, bool isCustomAction) 
        {
            this.Id = id;
            this.Name = name;
            this.IsPrivate = isPrivate;
            this.IsCustomAction = isCustomAction;
            this.MessageFilters = new Dictionary<Guid, SdkMessageFilter>();
            this.MessagePairs = new Dictionary<Guid, SdkMessagePair>();

        }

        public string Name { get; }
        public Guid Id { get;  }
        public bool IsPrivate { get; }
        public bool IsCustomAction { get; }
        public Dictionary<Guid, SdkMessagePair> MessagePairs { get;  }
        public Dictionary<Guid, SdkMessageFilter> MessageFilters { get;  }
    }
}
