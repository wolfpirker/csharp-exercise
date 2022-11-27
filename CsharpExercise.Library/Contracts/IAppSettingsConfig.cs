namespace CsharpExercise.Contracts
{
    // this interface is used to decouple the functionality of conversions 
    // from the settings, otherwise we cannot have (independent) unit tests

    public interface IAppSettingsConfig
    {
        Dictionary<string, Dictionary<string, string>> GetSerilogSettings();
        Dictionary<string, string> GetConversionSource();
        Dictionary<string, string> GetConversionTarget();

    }
}