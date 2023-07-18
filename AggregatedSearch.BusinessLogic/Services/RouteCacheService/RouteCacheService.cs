using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;
using AggregatedSearch.BusinessLogic.Utils;

namespace AggregatedSearch.BusinessLogic.Services.RouteCacheService
{
    /// <summary>
    /// Сервис кэширования маршрутов
    /// </summary>
    public class RouteCacheService : IRouteCacheService
    {
        /// <summary>
        /// Соответствие ключа Guid-у
        /// </summary>
        private readonly HashSet<Route> _cache = new();

        /// <summary>
        /// Блокировка
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new();

        /// <inheritdoc />
        public Guid AddOrUpdate(Route route)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (_cache.TryGetValue(route, out var routeFromCache))
                {
                    route.Id = routeFromCache.Id;
                    return routeFromCache!.Id;
                }

                route.Id = Guid.NewGuid();
                
                _lock.EnterWriteLock();
                try
                {
                    _cache.Add(route);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                return route.Id;
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        /// <inheritdoc />
        public Route? GetBy(Guid id)
        {
            _lock.EnterReadLock();
            try
            {
                return _cache.FirstOrDefault(x => x.Id == id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <inheritdoc />
        public Route[] GetBy(SearchRequest request)
        {
            _lock.EnterReadLock();
            try
            {
                return _cache
                    .Where(x => x.Origin == request.Origin)
                    .Where(x => x.Destination == request.Destination)
                    .Where(x => x.OriginDateTime == request.OriginDateTime)
                    .WhereIf(
                        condition: request.Filters?.DestinationDateTime is not null,
                        predicate: x => x.DestinationDateTime == request.Filters!.DestinationDateTime
                    )
                    .WhereIf(
                        condition: request.Filters?.MaxPrice is not null,
                        predicate: x => x.Price <= request.Filters!.MaxPrice
                    )
                    .WhereIf(
                        condition: request.Filters?.MinTimeLimit is not null,
                        predicate: x => x.TimeLimit >= request.Filters!.MinTimeLimit
                    )
                    .Select(x => x)
                    .ToArray();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}