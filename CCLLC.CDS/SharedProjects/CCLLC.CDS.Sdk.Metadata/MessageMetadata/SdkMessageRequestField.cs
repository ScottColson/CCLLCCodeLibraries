using CCLLC.CDS.Proxy;

namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public sealed class SdkMessageRequestField
    {
        public SdkMessageRequestField(SdkMessageRequest request, int index, string name, string clrFormatter, bool isOptional)
        {
            this.Index = index;
            this.Name = name;
            this.CLRFormatter = clrFormatter;
            this.IsOptional = IsOptional;
        }
    
        public SdkMessageRequest Request { get; }
        public int Index { get; }
        public string Name { get; }
        public string CLRFormatter { get; }
        public bool IsOptional { get; }
        public bool IsGeneric { get; }
    }
}
