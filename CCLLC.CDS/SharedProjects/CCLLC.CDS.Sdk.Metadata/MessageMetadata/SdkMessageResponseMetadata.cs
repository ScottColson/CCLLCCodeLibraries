using System;
using System.Collections.Generic;

namespace CCLLC.CDS.Sdk.Metadata
{
    public sealed class SdkMessageResponseMetadata
    {
        public SdkMessageResponseMetadata(Guid id, IEnumerable<SdkMessageResponseFieldMetadata> fields)
        {
            this.Id = id;
            this.FieldMetadata = fields ?? new List<SdkMessageResponseFieldMetadata>();
        }

        public Guid Id { get; }
        public IEnumerable<SdkMessageResponseFieldMetadata> FieldMetadata { get; }
    }
}
