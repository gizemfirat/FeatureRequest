using FeatureRequestProject.FeatureRequests;
using Riok.Mapperly.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Mapperly;

namespace FeatureRequestProject.Web;

[Mapper]
[MapExtraProperties]
public partial class FeatureRequestProjectWebMappers : MapperBase<FeatureRequestDto, CreateUpdateFeatureRequestDto>, ITransientDependency

{
    public override partial CreateUpdateFeatureRequestDto Map(FeatureRequestDto source);
    public override partial void Map(FeatureRequestDto source, CreateUpdateFeatureRequestDto destination);
}

