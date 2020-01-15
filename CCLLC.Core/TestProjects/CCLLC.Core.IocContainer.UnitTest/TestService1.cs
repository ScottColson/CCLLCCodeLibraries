namespace CCLLC.Core.IoCContainer.UnitTest
{
    public interface ITestService1
    {
        string Name { get; }
    }

    public class TestService1A : ITestService1
    {
        public string Name => "1A";
    }

    public class TestService1B : ITestService1
    {
        public string Name => "1B";
    }
}
