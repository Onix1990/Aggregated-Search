using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AggregatedSearch.HealthChecks
{
    public class SearchServiceHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Сервсис агрегированного поиска
        /// </summary>
        private readonly ISearchService _searchService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="searchService">Сервсис агрегированного поиска</param>
        public SearchServiceHealthCheck(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken
        )
        {
            var isHealthy = await _searchService.IsAvailableAsync(cancellationToken);

            return isHealthy
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }
    }
}