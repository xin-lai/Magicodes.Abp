using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Magicodes.AbpExtend.Pages
{
    public class Index_Tests : AbpExtendWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
