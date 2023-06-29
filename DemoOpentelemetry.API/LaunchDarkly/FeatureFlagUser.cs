namespace DemoOpentelemetry.API.LaunchDarkly
{
    public class FeatureFlagUser
    {
        /// <summary>
        /// Organization Id - mandatory
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// UserId - optional
        /// </summary>
        public string UserId { get; set; }
    }
}
