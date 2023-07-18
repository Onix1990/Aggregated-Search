using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch;
using AggregatedSearch.BusinessLogic.Utils;

namespace AggregatedSearch.BusinessLogic.Clients
{
    /// <summary>
    /// Клиент второго провайдера поиска
    /// </summary>
    public class ProviderTwoSearchClient : IProviderTwoSearchClient
    {
        /// <inheritdoc />
        public Task<ProviderTwoSearchResponse> SearchAsync(
            ProviderTwoSearchRequest request,
            CancellationToken cancellationToken
        )
        {
            // Todo: Fake данные, должна быть работа с HttpClient-ом
            var routes = new ProviderTwoRoute[]
            {
                new()
                {
                    Departure = new ProviderTwoPoint
                    {
                        Point = "Москва",
                        Date = new DateTime(2023, 07, 15, 19, 00, 00),
                    },
                    Arrival = new ProviderTwoPoint
                    {
                        Point = "Сочи",
                        Date = new DateTime(2023, 07, 15, 19, 50, 00),
                    },
                    Price = 150,
                    TimeLimit = new DateTime(2023, 07, 15, 20, 30, 00)
                },
                new()
                {
                    Departure = new ProviderTwoPoint
                    {
                        Point = "Омск",
                        Date = new DateTime(2023, 07, 11, 07, 25, 00),
                    },
                    Arrival = new ProviderTwoPoint
                    {
                        Point = "Томск",
                        Date = new DateTime(2023, 07, 12, 07, 20, 00),
                    },
                    Price = 500,
                    TimeLimit = new DateTime(2023, 07, 13, 20, 00, 00)
                },
                new()
                {
                    Departure = new ProviderTwoPoint
                    {
                        Point = "Москва",
                        Date = new DateTime(2023, 07, 15, 19, 00, 00),
                    },
                    Arrival = new ProviderTwoPoint
                    {
                        Point = "Сочи",
                        Date = new DateTime(2023, 07, 15, 19, 40, 00),
                    },
                    Price = 200,
                    TimeLimit = new DateTime(2023, 07, 15, 20, 15, 00)
                }
            };

            return Task.FromResult(new ProviderTwoSearchResponse
            {
                Routes = routes
                    .Where(x => x.Departure.Point == request.Departure)
                    .Where(x => x.Arrival.Point == request.Arrival)
                    .Where(x => x.Departure.Date == request.DepartureDate)
                    .WhereIf(request.MinTimeLimit is not null, x => x.TimeLimit >= request.MinTimeLimit)
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