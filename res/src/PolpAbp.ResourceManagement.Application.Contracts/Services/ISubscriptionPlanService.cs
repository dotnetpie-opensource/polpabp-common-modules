﻿using PolpAbp.ResourceManagement.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PolpAbp.ResourceManagement.Services
{
    public interface ISubscriptionPlanService
    {
        Task<Tuple<DateTime, DateTime?>> GetCurrentBillingPeriodAsync(string resourceName, CancellationToken cancellationToken);
        Task<long> GetQuotaByResourceNameAsync(string resourceName, bool isTenantLevel, CancellationToken cancellationToken);
        Task<long> GetQuotaByCategoryNameAsync(string categoryName, bool isTenantLevel, CancellationToken cancellationToken);
        /// <summary>
        /// Computes the effective plans for now or any future time
        /// </summary>
        /// <param name="referenceTime">The reference time when the plans will be effective</param>
        /// <param name="withBreakdown">A flag to include the breakdown detail or not.</param>
        /// <param name="cancellationToken">Token</param>
        /// <returns>List of plans</returns>
        Task<List<SubscriptionPlanOutputDto>> LoadEffectivePlansAsync(DateTime referenceTime, bool withBreakdown, CancellationToken cancellationToken);
        Task UpdateSubscriptionsAsync(List<SubscriptionPlanInputDto> input, CancellationToken cancellationToken);
        Task CancelSubscriptionsAsync(DateTime cancelledOn, CancellationToken cancellationToken);
    }
}
