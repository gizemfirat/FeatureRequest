using FeatureRequestProject.FeatureRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace FeatureRequestProject
{
    public class RequestFeatureProjectDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<FeatureRequest, Guid> _featureRequestRepository;
        private readonly IGuidGenerator _guidGenerator;

        public RequestFeatureProjectDataSeederContributor(IRepository<FeatureRequest, Guid> featureRequestRepository, IGuidGenerator guidGenerator)
        {
            _featureRequestRepository = featureRequestRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _featureRequestRepository.GetCountAsync() <= 0)
            {
                var request = new FeatureRequest(
                    _guidGenerator.Create(),
                    "Seed Feature Request",
                    "This is for testing.",
                    Category.UI
                    );

                await _featureRequestRepository.InsertAsync(request, autoSave: true);
            }
        }
    }
}
