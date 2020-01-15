﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using CCLLC.Telemetry.Implementation;

namespace CCLLC.Telemetry.Client
{ 
    public class OperationTelemetryClient<T> : TelemetryClientBase, IOperationalTelemetryClient<T> where T : IOperationalTelemetry
    {    
        private Stopwatch stopwatch;        
        private bool completed = false;
        private T telemetryItem;

        public ITelemetryClient ParentClient { get; private set; }

        public IDictionary<string, string> Properties { get; set; }

        internal OperationTelemetryClient(ITelemetryClient parentClient, T telemetryItem)
            : base()
        {
            this.ParentClient = parentClient;
            this.Properties = new ConcurrentDictionary<string, string>();
            this.telemetryItem = telemetryItem;                      
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }        

        public void CompleteOperation(bool? success)
        {
            stopwatch.Stop();
            telemetryItem.Duration = stopwatch.Elapsed;
            if (success.HasValue)
            {
                telemetryItem.Success = success;
            }
            Track(telemetryItem as ITelemetry);
            completed = true;           
        }

        public override void Dispose()
        {
            if (!completed)
            {
                CompleteOperation(null);
            }
            stopwatch = null;
            ParentClient = null;
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        public override void Track(ITelemetry telemetry)
        {
            //initialize the telemetry based on the context of this client and then push it to 
            //the next immediate ancestor to complete processing.
            this.Initialize(telemetry);
            ParentClient.Track(telemetry);
        }

        public override void Initialize(ITelemetry telemetry)
        {
            //copy any properties from the context if the telemetry support properties.
            var telemetryWithProperties = telemetry as ISupportProperties;
            if (telemetryWithProperties != null)
            {
                Utils.CopyDictionary(this.Properties, telemetryWithProperties.Properties);
            }
        }
    }
}
