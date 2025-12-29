using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace FeatureRequestProject.Pages;

public class Index_Tests : FeatureRequestProjectWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
