using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.ProviderOneSearch;
using AggregatedSearch.BusinessLogic.Utils;

namespace AggregatedSearch.BusinessLogic.Clients
{
    /// <summary>
    /// Клиент первого провайдера поиска
    /// </summary>
    public class ProviderOneSearchClient : IProviderOneSearchClient
    {
        /// <inheritdoc />
        public Task<ProviderOneSearchResponse> SearchAsync(
            ProviderOneSearchRequest request,
            CancellationToken cancellationToken
        )
        {
            // Todo: Fake данные, должна быть работа с HttpClient-ом
            var routes = new ProviderOneRoute[]
            {
                new()
                {
                    From = "Москва",
                    To = "Сочи",
                    DateFrom = new DateTime(2023, 07, 15, 19, 00, 00),
                    DateTo = new DateTime(2023, 07, 15, 19, 50, 00),
                    Price = 150,
                    TimeLimit = new DateTime(2023, 07, 15, 20, 30, 00)
                },
                new()
                {
                    From = "Пермь",
                    To = "Сочи",
                    DateFrom = new DateTime(2023, 07, 14, 18, 15, 00),
                    DateTo = new DateTime(2023, 07, 14, 18, 45, 00),
                    Price = 120,
                    TimeLimit = new DateTime(2023, 07, 14, 18, 58, 00)
                }
            };

            return Task.FromResult(new ProviderOneSearchResponse
            {
                Routes = routes
                    .Where(x => x.From == request.From)
                    .Where(x => x.To == request.To)
                    .Where(x => x.DateFrom == request.DateFrom)
                    .WhereIf(request.DateTo is not null, x => x.DateTo == request.DateTo)
                    .WhereIf(request.MaxPrice is not null, x => x.Price <= request.MaxPrice)
                    .ToArray()
            });
        }

        /// <inheritdoc />
        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            // Todo: Fake данные, должна быть работа с HttpClient-ом
            return Task.FromResult(true);
        }
    }
}