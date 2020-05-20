using System;


namespace CCLLC.CDS.Sdk.MessageMetadata
{
    public class SdkMessageFilter
    {
        public SdkMessageFilter(Guid id, string primaryObjectTypeCode, string secondaryObjectTypeCode, bool isVisible)
        {
            this.Id = id;
            this.PrimaryObjectTypeCode = primaryObjectTypeCode;
            this.SecondaryObjectTypeCode = secondaryObjectTypeCode;
            this.IsVisible = isVisible;
        }

        public Guid Id { get; }
        public string PrimaryObjectTypeCode { get; }
        public string SecondaryObjectTypeCode { get; }
        public bool IsVisible { get; }
    }

   
}
