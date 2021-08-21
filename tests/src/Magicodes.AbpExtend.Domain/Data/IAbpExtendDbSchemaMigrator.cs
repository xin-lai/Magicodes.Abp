using System.Threading.Tasks;

namespace Magicodes.AbpExtend.Data
{
    public interface IAbpExtendDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
