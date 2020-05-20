namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public sealed class SdkMessageResponseField
    {
        public SdkMessageResponseField(int index, string name, string clrFormatter, string value)
        {
            this.Index = index;
            this.Name = name;
            this.CLRFormatter = clrFormatter;
            this.Value = value;
        }

        public int Index { get; }
        public string Name { get; }
        public string CLRFormatter { get; }
        public string Value { get; }
    }
}
