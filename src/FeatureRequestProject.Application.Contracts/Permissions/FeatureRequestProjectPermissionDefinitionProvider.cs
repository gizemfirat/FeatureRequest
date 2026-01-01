using FeatureRequestProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FeatureRequestProject.Permissions;

public class FeatureRequestProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FeatureRequestProjectPermissions.GroupName, L("Permission:FeatureRequestProject"));
        var featureRequestsPermission = myGroup.AddPermission(FeatureRequestProjectPermissions.FeatureRequests.Default, L("Permission:FeatureRequests"));

        featureRequestsPermission.AddChild(FeatureRequestProjectPermissions.FeatureRequests.Create, L("Permission:Create"));
        featureRequestsPermission.AddChild(FeatureRequestProjectPermissions.FeatureRequests.Edit, L("Permission:Edit"));
        featureRequestsPermission.AddChild(FeatureRequestProjectPermissions.FeatureRequests.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FeatureRequestProjectResource>(name);
    }
}
