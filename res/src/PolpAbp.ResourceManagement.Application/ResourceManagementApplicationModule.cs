﻿using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Caching;
using PolpAbp.Framework;

namespace PolpAbp.ResourceManagement;

[DependsOn(
    typeof(ResourceManagementDomainModule),
    typeof(ResourceManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule),
    typeof(PolpAbpFrameworkAbpExtensionsModule)

    )]
public class ResourceManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<ResourceManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ResourceManagementApplicationModule>(validate: true);
        });
    }
}
