using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolpAbp.ResourceManagement.Core;
using PolpAbp.ResourceManagement.Domain.Entities;
using PolpAbp.ResourceManagement.Services.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace PolpAbp.ResourceManagement.Services
{
    public partial class SubscriptionPlanServiceTests : ResourceManagementApplicationTestBase
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
                        Family = "Initial launch v1",
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

        [Fact]
        public async Task CanFilterTerminatedPlansAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var plan = new Plan(Guid.NewGuid())
                    {
                        Name = "Passed Plan",
                        Family = "Initial launch v1",
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
                        BillingCycleOn = DateTime.UtcNow.AddMonths(-1),
                        TerminatedOn = DateTime.UtcNow.AddDays(-1),
                        EffectiveOn = DateTime.UtcNow.AddMonths(-1),
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

                    Assert.Empty(lst);
                });
            }
        }

        [Fact]
        public async Task CanLoadFuturePlansAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var plan = new Plan(Guid.NewGuid())
                    {
                        Name = "Future Plan",
                        Family = "Initial launch v1",
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
                        BillingCycleOn = DateTime.UtcNow.AddMonths(1),
                        EffectiveOn = DateTime.UtcNow.AddMonths(1),
                        PlanId = plan.Id,
                        Quantity = 1,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var lst = await _subscriptionPlanService
                    .LoadEffectivePlansAsync(DateTime.UtcNow.AddMonths(1).AddDays(1), default);

                    Assert.Single(lst);
                    var first = lst.First();
                    Assert.NotNull(first.CurrentBillingEndDate);
                });
            }
        }

        [Fact]
        public async Task CanUpdatePlansAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var firstPlan = new Plan(ResourceManagementTestConsts.FirstPlanId)
                    {
                        Name = "Lite Plan",
                        Family = "Initial launch v1",
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
                    await _planRepository.InsertAsync(firstPlan);

                    var secondPlan = new Plan(ResourceManagementTestConsts.SecondPlanId)
                    {
                        Name = "Profession Plan",
                        Family = "Initial launch v1",
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
                    await _planRepository.InsertAsync(secondPlan);

                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddMonths(-1),
                        EffectiveOn = DateTime.UtcNow.AddMonths(-1),
                        PlanId = ResourceManagementTestConsts.FirstPlanId,
                        Quantity = 1,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var now = DateTime.UtcNow;

                    var newPlans = new List<SubscriptionPlanInputDto>();
                    newPlans.Add(new SubscriptionPlanInputDto()
                    {
                        BillingCycleOn = now.AddDays(-1),
                        EffectiveOn = now.AddDays(-1),
                        PlanId = ResourceManagementTestConsts.SecondPlanId,
                        Quantity = 1
                    });

                    await _subscriptionPlanService
                    .UpdateSubscriptionsAsync(newPlans, default);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var lst = await _subscriptionPlanService
                    .LoadEffectivePlansAsync(DateTime.UtcNow, default);

                    Assert.Single(lst);
                    var first = lst.First();
                    Assert.Equal(first.PlanId, ResourceManagementTestConsts.SecondPlanId);
                });
            }
        }

        [Fact]
        public async Task CanCancelPlansAsync()
        {
            using (_currentTenant.Change(ResourceManagementTestConsts.TenantId))
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    var resource = await _resourceRepository
                    .GetAsync(x => x.Name == ResourceManagementTestConsts.SmsResourceName);

                    var firstPlan = new Plan(ResourceManagementTestConsts.FirstPlanId)
                    {
                        Name = "Lite Plan",
                        Family = "Initial launch v1",
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
                    await _planRepository.InsertAsync(firstPlan);
                    var sub = new TenantSubscription(Guid.NewGuid())
                    {
                        BillingCycleOn = DateTime.UtcNow.AddMonths(-1),
                        EffectiveOn = DateTime.UtcNow.AddMonths(-1),
                        PlanId = ResourceManagementTestConsts.FirstPlanId,
                        Quantity = 1,
                        TenantId = ResourceManagementTestConsts.TenantId
                    };

                    await _tenantSubscriptionRepository.InsertAsync(sub);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var now = DateTime.UtcNow;
                    var lst = await _subscriptionPlanService
                                      .LoadEffectivePlansAsync(now, default);

                    Assert.Single(lst);

                    await _subscriptionPlanService
                    .CancelSubscriptionsAsync(now, default);
                });

                await WithUnitOfWorkAsync(async () =>
                {
                    var lst = await _subscriptionPlanService
                    .LoadEffectivePlansAsync(DateTime.UtcNow, default);

                    Assert.Empty(lst);
                });
            }


        }
    }
}
