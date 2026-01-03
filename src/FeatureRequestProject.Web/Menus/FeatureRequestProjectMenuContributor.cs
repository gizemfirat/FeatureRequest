using FeatureRequestProject.Localization;
using FeatureRequestProject.MultiTenancy;
using FeatureRequestProject.Permissions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace FeatureRequestProject.Web.Menus;

public class FeatureRequestProjectMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<FeatureRequestProjectResource>();

        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                FeatureRequestProjectMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "FeaturesRequest",
                l["Menu:FeatureRequest"],
                icon: "fa fa-lightbulb"
            ).AddItem(
                new ApplicationMenuItem(
                    "FeaturesRequest.FeatureRequests",
                    l["Menu:FeatureRequests"],
                    url: "/FeatureRequests"
                )
            )
        );

        if (currentUser.IsAuthenticated)
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    "FeatureRequestProject.MyRequests",
                    l["MyFeatureRequests"],
                    url: "/FeatureRequests/MyFeatureRequests",
                    icon: "fa fa-user"
                )
            );
        }

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
