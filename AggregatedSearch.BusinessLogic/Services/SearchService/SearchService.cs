using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Clients;
using AggregatedSearch.BusinessLogic.Models.ProviderOneSearch;
using AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using AggregatedSearch.BusinessLogic.Services.RouteCacheService;
using AggregatedSearch.BusinessLogic.Utils;
using AutoMapper;
using FluentValidation;

namespace AggregatedSearch.BusinessLogic.Services
{
    /// <summary>
    /// Сервис агрегированного поиска
    /// </summary>
    public class SearchService : ISearchService
    {
        /// <summary>
        /// Клиент первого провайдера поиска
        /// </summary>
        private readonly IProviderOneSearchClient _providerOneSearchClient;

        /// <summary>
        /// Клиент второго провайдера поиска
        /// </summary>
        private readonly IProviderTwoSearchClient _providerTwoSearchClient;

        /// <summary>
        /// Валидатор фильтра для получения маршрутов
        /// </summary>
        private readonly IValidator<SearchRequest> _searchRequestValidator;

        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Сервис кэширования маршрутов
        /// </summary>
        private readonly IRouteCacheService _routeCacheService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="providerOneSearchClient">Клиент первого провайдера поиска</param>
        /// <param name="providerTwoSearchClient">Клиент второго провайдера поиска</param>
        /// <param name="mapper">Маппер</param>
        /// <param name="searchRequestValidator">Валидатор фильтра для получения маршрутов</param>
        /// <param name="routeCacheService">Сервис кэширования маршрутов</param>
        public SearchService(
            IProviderOneSearchClient providerOneSearchClient,
            IProviderTwoSearchClient providerTwoSearchClient,
            IMapper mapper,
            IValidator<SearchRequest> searchRequestValidator,
            IRouteCacheService routeCacheService
        )
        {
            _providerOneSearchClient = providerOneSearchClient;
            _providerTwoSearchClient = providerTwoSearchClient;
            _mapper = mapper;
            _searchRequestValidator = searchRequestValidator;
            _routeCacheService = routeCacheService;
        }

        /// <inheritdoc />
        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            await _searchRequestValidator.ValidateAndThrowAsync(
                instance: request,
                cancellationToken: cancellationToken
            );

            Route[] routes;
            if (request.Filters?.OnlyCached == true)
            {
                routes = _routeCacheService.GetBy(request);
            }
            else
            {
                var providerOneSearchRequest = _mapper.Map<ProviderOneSearchRequest>(request);
                var providerOneSearchResponse = _providerOneSearchClient.SearchAsync(
                    request: providerOneSearchRequest,
                    cancellationToken: cancellationToken
                );

                var providerTwoSearchRequest = _mapper.Map<ProviderTwoSearchRequest>(request);
                var providerTwoSearchResponse = _providerTwoSearchClient.SearchAsync(
                    request: providerTwoSearchRequest,
                    cancellationToken: cancellationToken
                );

                await Task.WhenAll(new List<Task> { providerOneSearchResponse, providerTwoSearchResponse });

                // Добавляем фильтры, которые отсутствуют в поисковых провайдерах
                var routesFromProviderOne = _mapper.Map<IEnumerable<Route>>(
                    providerOneSearchResponse.Result.Routes
                        .WhereIf(
                            condition: request.Filters?.MinTimeLimit is not null,
                            predicate: x => x.TimeLimit >= request.Filters!.MinTimeLimit
                        )
                );
                var routesFromProviderTwo = _mapper.Map<IEnumerable<Route>>(
                    providerTwoSearchResponse.Result.Routes
                        .WhereIf(
                            condition: request.Filters?.DestinationDateTime is not null,
                            predicate: x => x.Arrival.Date == request.Filters!.DestinationDateTime
                        )
                        .WhereIf(
                            condition: request.Filters?.MaxPrice is not null,
                            predicate: x => x.Price <= request.Filters!.MaxPrice
                        )
                );

                routes = routesFromProviderOne.Union(routesFromProviderTwo).ToArray();
            }

            decimal minPrice = decimal.MaxValue;
            decimal maxPrice = 0;
            int minMinutesRoute = int.MaxValue;
            int maxMinutesRoute = 0;

            // Не используем Linq Max/Min для поиска каждого из значений, т.о. сокращаем проходы по массиву с 4 до 1
            foreach (var route in routes)
            {
                if (route.Price > maxPrice)
                {
                    maxPrice = route.Price;
                }

                if (route.Price < minPrice)
                {
                    minPrice = route.Price;
                }

                var diffDates = (int) route.DestinationDateTime.Subtract(route.OriginDateTime).TotalMinutes;
                if (diffDates > maxMinutesRoute)
                {
                    maxMinutesRoute = diffDates;
                }

                if (diffDates < minMinutesRoute)
                {
                    minMinutesRoute = diffDates;
                }

                if (request.Filters?.OnlyCached != true)
                {
                    _routeCacheService.AddOrUpdate(route);
                }
            }

            if (routes.Length == 0)
            {
                throw new ObjectNotFoundException();
            }

            var searchResponse = new SearchResponse
            {
                Routes = routes,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinMinutesRoute = minMinutesRoute,
                MaxMinutesRoute = maxMinutesRoute
            };

            return searchResponse;
        }

        /// <inheritdoc />
        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            var isOneProviderAvailable = _providerOneSearchClient.IsAvailableAsync(cancellationToken);
            var isTwoProviderAvailable = _providerTwoSearchClient.IsAvailableAsync(cancellationToken);
            var result = await Task.WhenAll(new List<Task<bool>>
            {
                isOneProviderAvailable, isTwoProviderAvailable
            });

            return result.All(x => x);
        }

        /// <inheritdoc />
        public Route GetBy(Guid id)
        {
            var route = _routeCacheService.GetBy(id);

            if (route is null)
            {
                throw new ObjectNotFoundException();
            }

            return route;
        }
    }
}