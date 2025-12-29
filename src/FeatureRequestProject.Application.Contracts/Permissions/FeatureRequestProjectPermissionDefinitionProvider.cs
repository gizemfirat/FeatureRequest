using FeatureRequestProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FeatureRequestProject.Permissions;

public class FeatureRequestProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FeatureRequestProjectPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(FeatureRequestProjectPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FeatureRequestProjectResource>(name);
    }
}
