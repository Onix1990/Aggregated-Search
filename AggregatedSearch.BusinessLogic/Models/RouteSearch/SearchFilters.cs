using System;

namespace AggregatedSearch.BusinessLogic.Models.RouteSearch
{
    /// <summary>
    /// Фильтр для получения маршрутов (опциональные параметры)
    /// </summary>
    public class SearchFilters
    {
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? DestinationDateTime { get; set; }

        /// <summary>
        /// Максимальная стоимость маршрута
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Минимальное значение лимита времени
        /// </summary>
        public DateTime? MinTimeLimit { get; set; }

        /// <summary>
        /// Данные только из кэша
        /// </summary>
        public bool? OnlyCached { get; set; }
    }
}