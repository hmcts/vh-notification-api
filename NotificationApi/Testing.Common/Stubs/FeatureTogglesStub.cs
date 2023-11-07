using NotificationApi.Common.Util;

namespace Testing.Common.Stubs;

public class FeatureTogglesStub : IFeatureToggles
{
    public bool UseNew2023Templates { get; set; } = false;

    public bool UsePostMay2023Template()
    {
        return UseNew2023Templates;
    }
}
