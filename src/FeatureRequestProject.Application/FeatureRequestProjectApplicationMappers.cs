using FeatureRequestProject.FeatureRequests;
using Riok.Mapperly.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Mapperly;

namespace FeatureRequestProject;

[Mapper]
[MapExtraProperties]
public partial class FeatureRequestProjectApplicationMappers : 
    MapperBase<FeatureRequest, FeatureRequestDto>,
    IAbpMapperlyMapper<CreateUpdateFeatureRequestDto, FeatureRequest>, 
    ITransientDependency
{
    public override partial FeatureRequestDto Map(FeatureRequest source);
    public override partial void Map(FeatureRequest source, FeatureRequestDto destination);

    public partial FeatureRequest Map(CreateUpdateFeatureRequestDto source);
    public partial void Map(CreateUpdateFeatureRequestDto source, FeatureRequest destination);

    public void BeforeMap(CreateUpdateFeatureRequestDto source) { }
    public void AfterMap(CreateUpdateFeatureRequestDto source, FeatureRequest destination) { }
}

