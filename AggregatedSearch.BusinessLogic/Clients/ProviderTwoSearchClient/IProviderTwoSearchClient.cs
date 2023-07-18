using System.Threading;
using System.Threading.Tasks;
using AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch;

namespace AggregatedSearch.BusinessLogic.Clients
{
    /// <summary>
    /// Клиент второго провайдера поиска
    /// </summary>
    public interface IProviderTwoSearchClient
    {
        /// <summary>
        /// Получить маршруты
        /// </summary>
        /// <param name="request">Данные для фильтра</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task<ProviderTwoSearchResponse> SearchAsync(
            ProviderTwoSearchRequest request,
            CancellationToken cancellationToken
        );

        /// <summary>
        /// Проверка доступности
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
    }
}