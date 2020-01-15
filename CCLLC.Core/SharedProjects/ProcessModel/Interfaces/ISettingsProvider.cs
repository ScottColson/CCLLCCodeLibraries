namespace CCLLC.Core
{
    public interface ISettingsProvider
    {
        T Get<T>(string key, T defaultValue = default(T));
    }
}
