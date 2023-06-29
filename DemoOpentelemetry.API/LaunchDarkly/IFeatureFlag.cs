namespace DemoOpentelemetry.API.LaunchDarkly
{
    public interface IFeatureFlag
    {
        bool IsEnabled(string featureKey, FeatureFlagUser user, bool defaultValue);
    }
}
