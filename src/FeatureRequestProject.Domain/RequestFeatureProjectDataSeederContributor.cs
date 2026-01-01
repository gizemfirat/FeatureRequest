using FeatureRequestProject.FeatureRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace FeatureRequestProject
{
    public class RequestFeatureProjectDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<FeatureRequest, Guid> _featureRequestRepository;

        public RequestFeatureProjectDataSeederContributor(IRepository<FeatureRequest, Guid> featureRequestRepository)
        {
            _featureRequestRepository = featureRequestRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _featureRequestRepository.GetCountAsync() <= 0)
            {
                await _featureRequestRepository.InsertAsync(
                    new FeatureRequest
                    {
                        Title = "Seed Feature Request",
                        Description = "This is for testing purposes.",
                        CreationTime = DateTime.UtcNow,
                        Status = Status.Draft,
                        CategoryId = Category.UI
                    }, 
                    
                    autoSave: true
                );
            }
        }
    }
}
