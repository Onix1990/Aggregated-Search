using System;

namespace AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public class ProviderTwoRoute
    {
        /// <summary>
        /// Начальная точка
        /// </summary>
        public ProviderTwoPoint Departure { get; set; } = null!;

        /// <summary>
        /// Конечная точка
        /// </summary>
        public ProviderTwoPoint Arrival { get; set; } = null!;

        /// <summary>
        /// Стоимость маршрута
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Лимит времени
        /// </summary>
        public DateTime TimeLimit { get; set; }
    }
}