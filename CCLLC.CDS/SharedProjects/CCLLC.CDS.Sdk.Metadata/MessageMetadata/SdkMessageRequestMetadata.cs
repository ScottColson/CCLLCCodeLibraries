using System;
using System.Collections.Generic;

namespace CCLLC.CDS.Sdk.Metadata
{
    public sealed class SdkMessageRequestMetadata
    {
        public SdkMessageRequestMetadata(Guid id, string name, IEnumerable<SdkMessageRequestFieldMetadata> fields)
        {
            this.Id = id;
            this.Name = name;
            this.FieldMetadata = fields ?? new List<SdkMessageRequestFieldMetadata>();
        }

        public Guid Id { get; }
        public string Name { get; }
        public IEnumerable<SdkMessageRequestFieldMetadata> FieldMetadata { get; }
    }
}
