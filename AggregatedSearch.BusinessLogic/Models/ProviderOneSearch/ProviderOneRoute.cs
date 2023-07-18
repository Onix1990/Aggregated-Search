using System;

namespace AggregatedSearch.BusinessLogic.Models.ProviderOneSearch
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public class ProviderOneRoute
    {
        /// <summary>
        /// Имя начальной точки
        /// </summary>
        public string From { get; set; } = null!;

        /// <summary>
        /// Имя конечной точки
        /// </summary>
        public string To { get; set; } = null!;

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime DateTo { get; set; }

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