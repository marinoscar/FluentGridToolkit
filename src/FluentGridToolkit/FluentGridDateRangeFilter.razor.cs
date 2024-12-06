using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.DesignTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// A Blazor component that provides a user interface for filtering data by a date range.
    /// </summary>
    public partial class FluentGridDateRangeFilter<TGridItem, TProp> : FluentGridFilterBase<TGridItem, TProp>
    {

        /// <summary>
        /// Gets or sets the start date of the date range filter.
        /// </summary>
        [Parameter]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the date range filter.
        /// </summary>
        [Parameter]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the label for the start date picker.
        /// </summary>
        [Parameter]
        public string StartDateLabel { get; set; } = "Start Date";

        /// <summary>
        /// Gets or sets the label for the end date picker.
        /// </summary>
        [Parameter]
        public string EndDateLabel { get; set; } = "End Date";

        /// <summary>
        /// Gets or sets the ARIA label for the start date picker for accessibility purposes.
        /// </summary>
        [Parameter]
        public string StartDateAriaLabel { get; set; } = "Start Date Picker";

        /// <summary>
        /// Gets or sets the ARIA label for the end date picker for accessibility purposes.
        /// </summary>
        [Parameter]
        public string EndDateAriaLabel { get; set; } = "End Date Picker";

        /// <summary>
        /// Gets or sets the appearance style for the search button.
        /// </summary>
        [Parameter]
        public Appearance? SearchButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the appearance style for the clear button.
        /// </summary>
        [Parameter]
        public Appearance? ClearButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the icon used for the search button.
        /// </summary>
        [Parameter]
        public Icon SearchIcon { get; set; } = new ToolkitIcons.SearchIcon();

        /// <summary>
        /// Gets or sets the icon used for the clear button.
        /// </summary>
        [Parameter]
        public Icon ClearIcon { get; set; } = new ToolkitIcons.XMarkIcon();

        /// <summary>
        /// Gets or sets the tooltip text for the search button.
        /// </summary>
        [Parameter]
        public string SearchButtonTooltip { get; set; } = "Apply Filters";

        /// <summary>
        /// Gets or sets the tooltip text for the clear button.
        /// </summary>
        [Parameter]
        public string ClearButtonTooltip { get; set; } = "Clear Filters";

        /// <summary>
        /// Handles the search button click event.
        /// Validates the date range and invokes the <see cref="OnSearchClicked"/> callback.
        /// </summary>
        private async Task HandleSearch()
        {
            if (StartDate > EndDate)
            {
                HasError = true;
                StateHasChanged();
                return;
            }
            HasError = false;
            var expression = CreateExpression();
            FilterManager.AddOrUpdateFilter(ColumnName, expression);

            if (OnSearchClicked.HasDelegate)
                await OnSearchClicked.InvokeAsync((StartDate, EndDate));

            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync();
        }

        /// <summary>
        /// Handles the clear button click event.
        /// Clears the <see cref="StartDate"/> and <see cref="EndDate"/> values and invokes the <see cref="OnClearClicked"/> callback.
        /// </summary>
        public async Task OnClear()
        {
            StartDate = null;
            EndDate = null;

            //Clears the filter
            FilterManager.RemoveFilter(ColumnName);

            if (OnClearClicked.HasDelegate)
                await OnClearClicked.InvokeAsync();

            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync();
        }

        /// <summary>
        /// Generates the expression: this.StartDate > AccountCreatedDate && this.EndDate <= AccountCreatedDate.
        /// </summary>
        /// <returns>The generated expression.</returns>
        /// <exception cref="InvalidOperationException">Thrown if Property is not set.</exception>
        protected virtual Expression<Func<TGridItem, bool>> CreateExpression()
        {
            if (Property == null)
                throw new InvalidOperationException("The Property expression must be set before creating a comparison expression.");

            // Access the property on the entity
            var parameter = Expression.Parameter(typeof(TGridItem));
            var propertyAccess = Expression.Invoke(Property, parameter);

            // Convert Nullable<DateTime> to DateTime for comparisons if necessary
            //var propertyValue = typeof(TProp) == typeof(DateTime?)
            //    ? Expression.Property(propertyAccess, "Value")
            //    : propertyAccess;
            var propertyValue = propertyAccess;

            // Build the expressions for StartDate and EndDate comparisons
            var startComparison = Expression.GreaterThan(propertyValue, Expression.Constant(StartDate));
            var endComparison = Expression.LessThanOrEqual(propertyValue, Expression.Constant(EndDate));

            // Combine expressions with && (AND)
            var body = Expression.AndAlso(startComparison, endComparison);

            // Return the complete lambda expression
            return Expression.Lambda<Func<TGridItem, bool>>(body, parameter);
        }

    }

}
