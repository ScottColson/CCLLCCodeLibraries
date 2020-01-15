﻿using System.Collections.Generic;

namespace CCLLC.Telemetry
{
    public interface IInternalContext
    {
        string SdkVersion { get; set; }
        string AgentVersion { get; set; }
        string NodeName { get; set; }
        void CopyTo(IInternalContext target);
        void UpdateTags(IDictionary<string, string> tags, IContextTagKeys keys);
    }
}
