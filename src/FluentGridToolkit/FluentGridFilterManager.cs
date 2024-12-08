using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Manages dynamic filtering of an <see cref="IQueryable{T}"/> data source by applying and managing multiple filters.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity in the data source.</typeparam>
    public class FluentGridFilterManager<TEntity>
    {
        private readonly IQueryable<TEntity> _baseQuery;

        private readonly Dictionary<string, List<FilterExpression>> _filters = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentGridFilterManager{T}"/> class with the provided base query.
        /// </summary>
        /// <param name="baseQuery">The base query on which filters will be applied.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="baseQuery"/> is null.</exception>
        public FluentGridFilterManager(IQueryable<TEntity> baseQuery)
        {
            _baseQuery = baseQuery ?? throw new ArgumentNullException(nameof(baseQuery), "Base query cannot be null.");
        }

        /// <summary>
        /// The current filtered data.
        /// </summary>
        public IQueryable<TEntity> Data => ApplyFilters();

        
        public void AddOrUpdateFilter(string column, List<FilterExpression> filters)
        {
            _filters[column] = filters;
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
        public IQueryable<TEntity> ApplyFilters()
        {
            IQueryable<TEntity> query = _baseQuery;
            
            var filters = _filters.Values.SelectMany(x => x.ToList()).ToList();
            if(filters.Count <= 0) return query;

            var predicate = DynamicExpressionBuilder.BuildExpression<TEntity>(filters);

            query = query.Where(predicate);
            
            return query;
        }
    }

}
