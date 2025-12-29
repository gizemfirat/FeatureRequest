using FeatureRequestProject.Samples;
using Xunit;

namespace FeatureRequestProject.EntityFrameworkCore.Domains;

[Collection(FeatureRequestProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<FeatureRequestProjectEntityFrameworkCoreTestModule>
{

}
