using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PolpAbp.ResourceManagement.Core;
using PolpAbp.ResourceManagement.Domain.Entities;
using Xunit;

namespace PolpAbp.ResourceManagement.Services
{
    public partial class SubscriptionPlanServiceTests
    {
        [Fact]
        public async Task CanGetQuotaByResourceName4NonNumberedPlanAsync()
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
                        Family = "Initial launch v1",
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
                    .GetQuotaByResourceNameAsync(ResourceManagementTestConsts.SmsResourceName, true, default);

                    Assert.Equal(10000, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaByResourceNameAsync(ResourceManagementTestConsts.SmsResourceName, false, default);

                    Assert.Equal(1000, userQuota);

                });
            }
        }

        [Fact]
        public async Task CanGetQuotaByResourceName4NumberedPlanAsync()
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
                        Family = "Initial launch v1",
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
                    .GetQuotaByResourceNameAsync(ResourceManagementTestConsts.SmsResourceName, true, default);

                    Assert.Equal(10000 * 10, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaByResourceNameAsync(ResourceManagementTestConsts.SmsResourceName, false, default);

                    Assert.Equal(1000, userQuota);

                });
            }
        }

        [Fact]
        public async Task CanGetQuotaByCategoryName4NonNumberedPlanAsync()
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
                        Family = "Initial launch v1",
                        Description = "Description",
                        BillingCycleId = (int)BillingCycleEnum.Month,
                        CategoryQuotas = new List<PlanCategoryQuota>
                        {
                            new PlanCategoryQuota(Guid.NewGuid())
                            {
                               Category = "Sms",
                               LimitPerUser = 1000,
                               LimitAcrossTenant = 10000
                            }
                        }
                    };

                    await _planRepository.InsertAsync(plan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddDays(-1),
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
                    .GetQuotaByCategoryNameAsync("Sms", true, default);

                    Assert.Equal(10000, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaByCategoryNameAsync("Sms", false, default);

                    Assert.Equal(1000, userQuota);

                });
            }
        }

        [Fact]
        public async Task CanGetQuotaByCategoryName4NumberedPlanAsync()
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
                        Family = "Initial launch v1",
                        Description = "Description",
                        BillingCycleId = (int)BillingCycleEnum.Month,
                        CategoryQuotas = new List<PlanCategoryQuota>
                        {
                            new PlanCategoryQuota(Guid.NewGuid())
                            {
                                Category = "Sms",
                                LimitPerUser = 2000,
                                LimitAcrossTenant = 20000
                            }
                        }
                    };

                    await _planRepository.InsertAsync(plan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddDays(-1),
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
                    .GetQuotaByCategoryNameAsync("Sms", true, default);

                    Assert.Equal(20000 * 10, tenantQuota);

                    var userQuota = await _subscriptionPlanService
                    .GetQuotaByCategoryNameAsync("Sms", false, default);

                    Assert.Equal(2000, userQuota);

                });
            }
        }

    }
}

