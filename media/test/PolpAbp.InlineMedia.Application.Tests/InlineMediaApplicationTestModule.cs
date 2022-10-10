using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Polpware.AspNetCore.Framework.IO;
using Polpware.NetStd.Framework.IO;
using Volo.Abp.Modularity;

namespace PolpAbp.InlineMedia;

[DependsOn(
    typeof(InlineMediaApplicationModule),
    typeof(InlineMediaDomainTestModule)
    )]
public class InlineMediaApplicationTestModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddScoped<INopFileProvider, NopFileProvider>(_ => new NopFileProvider(new WebHost()));
    }


    public class WebHost : IWebHostEnvironment
    {
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider
        {
            get; set;
        }
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
    }
}
