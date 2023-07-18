using System;

namespace AggregatedSearch.BusinessLogic.Models.ProviderTwoSearch
{
    /// <summary>
    /// Точка маршрута
    /// </summary>
    public class ProviderTwoPoint
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Point { get; set; } = null!;

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
    }
}