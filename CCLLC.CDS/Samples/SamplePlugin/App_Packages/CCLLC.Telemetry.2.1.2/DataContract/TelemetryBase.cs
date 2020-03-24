﻿using System;
using System.Collections.Generic;

namespace CCLLC.Telemetry.DataContract
{
    public abstract class TelemetryBase<TData> : ITelemetry, IDataModelTelemetry<TData> where TData : IDataModel
    {
        public string TelemetryName { get; private set; }
        public string BaseType { get; protected set; }
        public TData Data { get; private set; }
        
        public DateTimeOffset Timestamp { get; set; }
        public string Sequence { get; set; }
        public string InstrumentationKey
        {
            get { return this.Context.InstrumentationKey; }
            set { this.Context.InstrumentationKey = value; }
        }

        public ITelemetryContext Context { get; private set; }
                
        protected TelemetryBase(string telememtryName, ITelemetryContext context, TData data)
        {
            this.TelemetryName = telememtryName;
            this.BaseType = data.DataType;
            this.Data = data;
            this.Context = context;
        }

        public abstract IDataModelTelemetry<TData> DeepClone();

        public abstract void Sanitize();

        public abstract void SerializeData(ITelemetrySerializer serializer, IJsonWriter writer);

        public abstract IDictionary<string, string> GetTaggedData();
        
    }
}
