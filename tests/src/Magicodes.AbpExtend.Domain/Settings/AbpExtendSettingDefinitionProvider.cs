using Volo.Abp.Settings;

namespace Magicodes.AbpExtend.Settings
{
    public class AbpExtendSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AbpExtendSettings.MySetting1));
        }
    }
}
