using System;
using System.Collections.Generic;


namespace CCLLC.CDS.Sdk.Metadata
{
    public sealed class SdkMessageMetadata
    {
        public SdkMessageMetadata(Guid id, string name, bool isPrivate, bool isCustomAction, IEnumerable<SdkMessageFilterMetadata> filters, IEnumerable<SdkMessagePairMetadata> messagePairs) 
        {
            this.Id = id;
            this.Name = name;
            this.IsPrivate = isPrivate;
            this.IsCustomAction = isCustomAction;
            this.MessageFilterMetadata = filters ?? new List<SdkMessageFilterMetadata>();
            this.MessagePairMetadata = messagePairs ?? new List<SdkMessagePairMetadata>();

        }

        public string Name { get; }
        public Guid Id { get;  }
        public bool IsPrivate { get; }
        public bool IsCustomAction { get; }
        public IEnumerable<SdkMessagePairMetadata> MessagePairMetadata { get;  }
        public IEnumerable<SdkMessageFilterMetadata> MessageFilterMetadata { get;  }
    }
}
