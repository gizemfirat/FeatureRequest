using Volo.Abp.Settings;

namespace FeatureRequestProject.Settings;

public class FeatureRequestProjectSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FeatureRequestProjectSettings.MySetting1));
    }
}
