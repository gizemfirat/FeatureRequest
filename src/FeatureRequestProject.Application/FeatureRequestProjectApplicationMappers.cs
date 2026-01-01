using FeatureRequestProject.FeatureRequestComments;
using FeatureRequestProject.FeatureRequests;
using Riok.Mapperly.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Mapperly;

namespace FeatureRequestProject;

[Mapper]
[MapExtraProperties]

public partial class FeatureRequestProjectApplicationMappers : 
    MapperBase<FeatureRequest, FeatureRequestDto>,
    IAbpMapperlyMapper<CreateUpdateFeatureRequestDto, FeatureRequest>,
    ITransientDependency
{
    protected IGuidGenerator GuidGenerator { get; }

    public FeatureRequestProjectApplicationMappers(IGuidGenerator guidGenerator)
    {
        GuidGenerator = guidGenerator;
    }

    public override partial FeatureRequestDto Map(FeatureRequest source);
    public override partial void Map(FeatureRequest source, FeatureRequestDto destination);

    public FeatureRequest Map(CreateUpdateFeatureRequestDto source) 
    {
        var entity = new FeatureRequest(
            GuidGenerator.Create(),
            source.Title,
            source.Description,
            source.CategoryId
            );

        entity.Status = source.Status;

        return entity;
    }
    public void Map(CreateUpdateFeatureRequestDto source, FeatureRequest destination) 
    {
        destination.Status = source.Status;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.CategoryId = source.CategoryId;
    }

    public partial FeatureRequestCommentDto Map(FeatureRequestComment source);
    public void BeforeMap(CreateUpdateFeatureRequestDto source) { }
    public void AfterMap(CreateUpdateFeatureRequestDto source, FeatureRequest destination) { }
}

