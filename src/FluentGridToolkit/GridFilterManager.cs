﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Manages dynamic filtering of an <see cref="IQueryable{T}"/> data source by applying and managing multiple filters.
    /// </summary>
    /// <typeparam name="T">The type of the entity in the data source.</typeparam>
    public class GridFilterManager<T>
    {
        private readonly IQueryable<T> _baseQuery;
        private readonly Dictionary<string, Expression<Func<T, bool>>> _filters = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GridFilterManager{T}"/> class with the provided base query.
        /// </summary>
        /// <param name="baseQuery">The base query on which filters will be applied.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="baseQuery"/> is null.</exception>
        public GridFilterManager(IQueryable<T> baseQuery)
        {
            _baseQuery = baseQuery ?? throw new ArgumentNullException(nameof(baseQuery), "Base query cannot be null.");
        }

        /// <summary>
        /// Adds a new filter or updates an existing filter for a specific column or property.
        /// </summary>
        /// <param name="column">The name of the column or property to filter.</param>
        /// <param name="filter">The filtering logic as a LINQ expression.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="column"/> or <paramref name="filter"/> is null.
        /// </exception>
        /// <remarks>
        /// If a filter already exists for the specified column, it will be overwritten with the new filter.
        /// </remarks>
        public void AddOrUpdateFilter(string column, Expression<Func<T, bool>> filter)
        {
            if (column == null) throw new ArgumentNullException(nameof(column), "Column name cannot be null.");
            if (filter == null) throw new ArgumentNullException(nameof(filter), "Filter expression cannot be null.");

            _filters[column] = filter;
        }

        /// <summary>
        /// Removes an existing filter for a specific column or property.
        /// </summary>
        /// <param name="column">The name of the column or property whose filter should be removed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="column"/> is null.</exception>
        /// <remarks>
        /// If no filter exists for the specified column, the method performs no action.
        /// </remarks>
        public void RemoveFilter(string column)
        {
            if (column == null) throw new ArgumentNullException(nameof(column), "Column name cannot be null.");

            _filters.Remove(column);
        }

        /// <summary>
        /// Clears all filters, effectively resetting the query to its unfiltered state.
        /// </summary>
        public void ClearFilters()
        {
            _filters.Clear();
        }

        /// <summary>
        /// Applies all the currently defined filters to the base query and returns the filtered result.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{T}"/> that represents the filtered query.
        /// </returns>
        /// <remarks>
        /// Filters are applied in the order they were added. This method preserves deferred execution, so the query
        /// is not executed until enumeration or other terminal operations are performed.
        /// </remarks>
        public IQueryable<T> ApplyFilters()
        {
            IQueryable<T> query = _baseQuery;

            foreach (var filter in _filters.Values)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }

}