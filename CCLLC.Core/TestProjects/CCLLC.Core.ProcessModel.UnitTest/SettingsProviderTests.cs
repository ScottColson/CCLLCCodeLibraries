using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CCLLC.Core.ProcessModel.UnitTest
{
   

    [TestClass]
    public class SettingsProviderTests
    {
        class FakeSettingConnector : ISettingsProviderDataConnector
        {
            public IReadOnlyDictionary<string, string> LoadSettings(IDataService dataService)
            {
                var entries = new Dictionary<string, string>();

                entries.Add("ENV1", "VALUE1");
                entries.Add("ENV2", "VALUE2");

                return entries;
            }
        }


        class FakeDataService : IDataService
        {

        }

        
        class FakeExecutionContext : IProcessExecutionContext
        {
            public FakeExecutionContext(IDataService dataService, ICache cache)
            {
                DataService = dataService;
                Cache = cache;
            }

            public IDataService DataService { get; }

            public ICache Cache { get; }

            public Core.IReadOnlyIocContainer Container { get; }

            public ISettingsProvider Settings { get; }

            public void Trace(string message, params object[] args)
            {
                throw new NotImplementedException();
            }

            public void Trace(eSeverityLevel severityLevel, string message, params object[] args)
            {
                throw new NotImplementedException();
            }

            public void TrackEvent(string name)
            {
                throw new NotImplementedException();
            }

            public void TrackException(Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void Test_SettingsProvider_Should_LoadSettings()
        {
            var executionContext = new FakeExecutionContext(new FakeDataService(), new DefaultCache());

            var factory = new SettingsProviderFactory(new FakeSettingConnector());

            var settings = factory.CreateSettingsProvider(executionContext);

            Assert.AreEqual(2, settings.Count);
            Assert.AreEqual("VALUE1", settings.GetValue<string>("ENV1"));
            Assert.AreEqual("VALUE1", settings.GetValue<string>("env1"));
           

        }
    }
}
