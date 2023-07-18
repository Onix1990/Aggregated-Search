using System;
using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.RouteSearch;

namespace AggregatedSearch.BusinessLogic.Services
{
    /// <summary>
    /// Сервис агрегированного поиска
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Получить маршруты
        /// </summary>
        /// <param name="request">Данные для фильтра</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка доступности
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<bool> IsAvailableAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить маршрут по Guid (из кэша)
        /// </summary>
        /// <param name="id">Идентификатор маршурта</param>
        /// <returns></returns>
        Route GetBy(Guid id);
    }
}