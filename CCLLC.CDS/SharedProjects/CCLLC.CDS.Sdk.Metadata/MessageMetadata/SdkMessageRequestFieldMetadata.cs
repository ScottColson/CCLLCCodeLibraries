
namespace CCLLC.CDS.Sdk.Metadata
{
    public sealed class SdkMessageRequestFieldMetadata
    {
        public SdkMessageRequestFieldMetadata(int index, string name, string clrFormatter, bool isOptional)
        {
            this.Index = index;
            this.Name = name;
            this.CLRFormatter = clrFormatter;
            this.IsOptional = isOptional;
        }
    
        public int Index { get; }
        public string Name { get; }
        public string CLRFormatter { get; }
        public bool IsOptional { get; }
        public bool IsGeneric { get; }
    }
}
