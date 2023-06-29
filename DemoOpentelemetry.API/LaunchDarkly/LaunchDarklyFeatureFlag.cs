using LaunchDarkly.Sdk.Server;
using LaunchDarkly.Sdk;

namespace DemoOpentelemetry.API.LaunchDarkly
{
    public class LaunchDarklyFeatureFlag : IFeatureFlag
    {
        private LdClient _featureFlagClient;

        public LaunchDarklyFeatureFlag(string sdkKey)
        {
            _featureFlagClient = new LdClient(sdkKey);
        }

        /// <summary>
        /// Determines whether [is feature enabled] [the specified SDK key].
        /// </summary>
        /// <param name="featureFlag">The feature flag.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public bool IsEnabled(string featureFlag, FeatureFlagUser user, bool defaultValue)
        {

            var userObject = User.WithKey(user.TenantId);

            return _featureFlagClient.BoolVariation(featureFlag, userObject, defaultValue);
        }

    }
}
