using System;


namespace CCLLC.CDS.Sdk.Metadata
{
    public class SdkMessageFilterMetadata
    {
        public SdkMessageFilterMetadata(Guid id, string primaryObjectTypeCode, string secondaryObjectTypeCode, bool isVisible)
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
