﻿using PolpAbp.ResourceManagement.Domain.Entities;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace PolpAbp.ResourceManagement;

public class ResourceManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IRepository<Resource> _resourceRepository;

    public ResourceManagementDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, 
        IRepository<Resource> resourceRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _resourceRepository = resourceRepository;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        await _resourceRepository.InsertAsync(new Resource(_guidGenerator.Create()) {
            Name = ResourceManagementTestConsts.SmsResourceName,
            Description = "some",
            Category = "Sms"
        });

        await _resourceRepository.InsertAsync(new Resource(_guidGenerator.Create())
        {
            Name = ResourceManagementTestConsts.FormPublication,
            Description = "Form Publications",
            Category = "Publication"
        });


        await _resourceRepository.InsertAsync(new Resource(_guidGenerator.Create())
        {
            Name = ResourceManagementTestConsts.FormFlowPublication,
            Description = "Form Flow Publications",
            Category = "Publication"
        });
    }
}
