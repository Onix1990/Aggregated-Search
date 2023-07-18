using System;
using System.Collections.Generic;
using System.Linq;

namespace AggregatedSearch.BusinessLogic.Utils
{
    /// <summary>
    /// Методы расширения для IEnumerable
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Выполенение предикаты Where по условию
        /// </summary>
        /// <param name="source">Источник данных</param>
        /// <param name="condition">Условия</param>
        /// <param name="predicate">Предиката</param>
        /// <typeparam name="TSource">Тип данных в источнике</typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(
            this IEnumerable<TSource> source,
            bool condition,
            Func<TSource, bool> predicate
        ) =>
            condition ? source.Where(predicate) : source;
    }
}