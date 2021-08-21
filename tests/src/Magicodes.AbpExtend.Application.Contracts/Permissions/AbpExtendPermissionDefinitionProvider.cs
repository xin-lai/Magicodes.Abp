using Magicodes.AbpExtend.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Magicodes.AbpExtend.Permissions
{
    public class AbpExtendPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(AbpExtendPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(AbpExtendPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpExtendResource>(name);
        }
    }
}
