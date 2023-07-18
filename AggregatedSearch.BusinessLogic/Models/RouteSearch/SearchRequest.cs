using System;

namespace AggregatedSearch.BusinessLogic.Models.RouteSearch
{
    /// <summary>
    /// Фильтр для получения маршрутов
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// Наименование начальной точки
        /// </summary>
        public string? Origin { get; set; }

        /// <summary>
        /// Наименование конечной точки
        /// </summary>
        public string? Destination { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? OriginDateTime { get; set; }

        /// <summary>
        /// Дополнительные фильтры для получения маршрутов
        /// </summary>
        public SearchFilters? Filters { get; set; }
    }
}