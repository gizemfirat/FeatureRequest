using Microsoft.AspNetCore.Builder;
using FeatureRequestProject;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("FeatureRequestProject.Web.csproj");
await builder.RunAbpModuleAsync<FeatureRequestProjectWebTestModule>(applicationName: "FeatureRequestProject.Web" );

public partial class Program
{
}
