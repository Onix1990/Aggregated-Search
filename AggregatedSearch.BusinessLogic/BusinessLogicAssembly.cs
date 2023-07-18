using System.Reflection;

namespace AggregatedSearch.BusinessLogic
{
    /// <summary>
    /// Класс для получения объекта Assembly
    /// </summary>
    public static class BusinessLogicAssembly
    {
        /// <summary>
        /// Значение Assembly
        /// </summary>
        public static readonly Assembly Value = typeof(BusinessLogicAssembly).Assembly;
    }
}