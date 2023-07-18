using System;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;

namespace AggregatedSearch.BusinessLogic.Services.RouteCacheService
{
    /// <summary>
    /// Сервис кэширования маршрутов
    /// </summary>
    public interface IRouteCacheService
    {
        /// <summary>
        /// Добавить или обновить маршрут в кэшэ
        /// </summary>
        /// <param name="route">Маршрут</param>
        /// <returns></returns>
        Guid AddOrUpdate(Route route);

        /// <summary>
        /// Получить запись из кэша по Guid
        /// </summary>
        /// <param name="id">Идентификатор маршрута</param>
        /// <returns></returns>
        Route? GetBy(Guid id);

        /// <summary>
        /// Получить записи из кэша
        /// </summary>
        /// <param name="request">Фильтр для получения маршрутов</param>
        /// <returns></returns>
        Route[] GetBy(SearchRequest request);
    }
}