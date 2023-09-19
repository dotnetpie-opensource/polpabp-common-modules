using PolpAbp.ResourceManagement.Core;
using PolpAbp.ResourceManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace PolpAbp.ResourceManagement.Services
{
    public class SubscriptionPlanServiceTests : ResourceManagementApplicationTestBase
    {
        private IRepository<Resource> _resourceRepository;
        private IRepository<Plan> _planRepository;
        private IRepository<TenantSubscription> _tenantSubscriptionRepository;
        private ISubscriptionPlanService _subscriptionPlanService;
        private ICurrentTenant _currentTenant;

        public SubscriptionPlanServiceTests()
        {
            _resourceRepository = GetRequiredService<IRepository<Resource>>();
            _planRepository = GetRequiredService<IRepository<Plan>>();
            _tenantSubscriptionRepository = GetRequiredService<IRepository<TenantSubscription>>();
            _subscriptionPlanService = GetRequiredService<ISubscriptionPlanService>();
            _currentTenant = GetRequiredService<ICurrentTenant>();
        }

        [Fact]
        public async Task CanGetQuota4NonNumberedPlanAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var plan = new Plan(Guid.NewGuid())
                    {
                        Name = "Golden Plan",
                        Description = "Description",
                        BillingCycleId = (int)BillingCycleEnum.Month,
                        Breakdowns = new List<PlanBreakdown>
                        {
                            new PlanBreakdown(Guid.NewGuid())
                            {
                               ResourceId = resource.Id,
                               LimitPerUser = 1000,
                               LimitAcrossTenant = 10000
                            }
                        }
                    };

                    await _planRepository.InsertAsync(plan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddDays(-1),
                        IsTerminated = false,
                        EffectiveOn = DateTime.UtcNow.AddDays(-1),
                        PlanId = plan.Id,
                        Quantity = 1,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var tenantQuota = await _subscriptionPlanService
                    .GetQuotaAsync(ResourceManagementTestConsts.SmsResourceName, true, default);

                    Assert.Equal(10000, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaAsync(ResourceManagementTestConsts.SmsResourceName, false, default);

                    Assert.Equal(1000, userQuota);

                });
            }
        }

        [Fact]
        public async Task CanGetQuota4NumberedPlanAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var plan = new Plan(Guid.NewGuid())
                    {
                        Name = "Per person Plan",
                        Description = "Description",
                        BillingCycleId = (int)BillingCycleEnum.Month,
                        Breakdowns = new List<PlanBreakdown>
                        {
                            new PlanBreakdown(Guid.NewGuid())
                            {
                               ResourceId = resource.Id,
                               LimitPerUser = 1000,
                               LimitAcrossTenant = 10000
                            }
                        }
                    };

                    await _planRepository.InsertAsync(plan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddDays(-1),
                        IsTerminated = false,
                        EffectiveOn = DateTime.UtcNow.AddDays(-1),
                        PlanId = plan.Id,
                        Quantity = 10,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var tenantQuota = await _subscriptionPlanService
                    .GetQuotaAsync(ResourceManagementTestConsts.SmsResourceName, true, default);

                    Assert.Equal(10000 * 10, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaAsync(ResourceManagementTestConsts.SmsResourceName, false, default);

                    Assert.Equal(1000, userQuota);

                });
            }
        }

        [Fact]
        public async Task CanLoadPlansAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var plan = new Plan(Guid.NewGuid())
                    {
                        Name = "Lite Plan",
                        Description = "Description",
                        BillingCycleId = (int)BillingCycleEnum.Month,
                        Breakdowns = new List<PlanBreakdown>
                        {
                            new PlanBreakdown(Guid.NewGuid())
                            {
                               ResourceId = resource.Id,
                               LimitPerUser = 0,
                               LimitAcrossTenant = 10000
                            }
                        }
                    };

                    await _planRepository.InsertAsync(plan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddDays(-1),
                        IsTerminated = false,
                        EffectiveOn = DateTime.UtcNow.AddDays(-1),
                        PlanId = plan.Id,
                        Quantity = 1,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var lst = await _subscriptionPlanService
                    .LoadEffectivePlansAsync(DateTime.UtcNow, default);

                    Assert.Single(lst);
                    var first = lst.First();
                    Assert.NotNull(first.CurrentBillingEndDate);

                });
            }
        }
    }
}
