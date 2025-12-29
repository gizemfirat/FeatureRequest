using FeatureRequestProject.Samples;
using Xunit;

namespace FeatureRequestProject.EntityFrameworkCore.Applications;

[Collection(FeatureRequestProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<FeatureRequestProjectEntityFrameworkCoreTestModule>
{

}
