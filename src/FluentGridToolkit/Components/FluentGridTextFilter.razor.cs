using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Components
{
    public partial class FluentGridTextFilter<TGridItem, TProp> : FluentGridFilterBase<TGridItem, TProp>
    {
        /// <summary>
        /// Gets or sets the placeholder text for the input field.
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "Enter text...";

        /// <summary>
        /// Gets or sets the text entered in the search box.
        /// </summary>
        [Parameter]
        public string SearchText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the CSS style for the input box.
        /// </summary>
        [Parameter]
        public string InputStyle { get; set; } = "flex: 1;";

        /// <summary>
        /// Event callback triggered when the search button is clicked.
        /// </summary>
        [Parameter]
        public EventCallback<string> OnSearchClicked { get; set; }

        /// <summary>
        /// Handles the click event of the search button.
        /// </summary>
        private async Task HandleSearch()
        {
            var expression = CreateExpression();
            FilterManager.AddOrUpdateFilter(ColumnName, expression);

            if (OnSearchClicked.HasDelegate)
            {
                await OnSearchClicked.InvokeAsync(SearchText);
            }
            await ValueChanged();
        }

        /// <summary>
        /// Handles the click event of the clear button, clearing the search text.
        /// </summary>
        private async Task OnClear()
        {
            SearchText = string.Empty; // Automatically clear the search text
            FilterManager.RemoveFilter(ColumnName);
            if (OnClearClicked.HasDelegate)
                await OnClearClicked.InvokeAsync();
            await ValueChanged();
        }

        /// <summary>
        /// Handles when the search value changed
        /// </summary>
        /// <returns></returns>
        private async Task ValueChanged()
        {
            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync(SearchText);
        }

        /// <summary>
        /// Builds a filter expression of type Expression<Func<TGridItem, bool>> based on the property and value.
        /// </summary>
        /// <returns>The filter expression.</returns>
        public Expression<Func<TGridItem, bool>> CreateExpression()
        {
            if (Property == null)
                throw new InvalidOperationException("Property expression must be set before building the filter.");

            if (typeof(TProp) != typeof(string))
                throw new InvalidOperationException("This filter only supports string properties.");

            // Parameter for the lambda expression (TGridItem entity)
            var parameter = Expression.Parameter(typeof(TGridItem));

            // Access the property on the entity
            var propertyAccess = Expression.Invoke(Property, parameter);

            // Convert property to ToLower()
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var propertyToLower = Expression.Call(propertyAccess, toLowerMethod);

            // Convert PropertyValue to ToLower()
            var propertyValueConstant = Expression.Constant(SearchText, typeof(TProp));
            var propertyValueToLower = Expression.Call(propertyValueConstant, toLowerMethod);

            // Build the Contains expression
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExpression = Expression.Call(propertyToLower, containsMethod, propertyValueToLower);

            // Return the complete lambda expression
            return Expression.Lambda<Func<TGridItem, bool>>(containsExpression, parameter);
        }
    }
}
