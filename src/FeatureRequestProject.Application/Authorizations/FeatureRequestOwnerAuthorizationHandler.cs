using System.Threading.Tasks;
using FeatureRequestProject.FeatureRequests;
using FeatureRequestProject.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;

namespace FeatureRequestProject.Authorization
{
    public class FeatureRequestOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, FeatureRequest>
    {
        private readonly IPermissionChecker _permissionChecker;

        public FeatureRequestOwnerAuthorizationHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            FeatureRequest resource)
        {
            if (resource.CreatorId.HasValue && resource.CreatorId.ToString() == context.User.FindFirst("sub")?.Value)
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == "Update" && await _permissionChecker.IsGrantedAsync(FeatureRequestProjectPermissions.FeatureRequests.Edit))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == "Delete" && await _permissionChecker.IsGrantedAsync(FeatureRequestProjectPermissions.FeatureRequests.Delete))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}