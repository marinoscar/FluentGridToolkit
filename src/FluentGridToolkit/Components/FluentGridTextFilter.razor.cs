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
        /// Gets or sets the value to determined if the text search will igonre casing or it would be case sensitive
        /// </summary>
        [Parameter]
        public bool IgnoreCase { get; set; } = true;

        /// <summary>
        /// Handles the click event of the search button.
        /// </summary>
        private async Task HandleSearch()
        {

            FilterManager.AddOrUpdateFilter(ColumnName, new List<FilterExpression>() {
                new FilterExpression(){ 
                    PropertyName = Property.GetPropertyName(),
                    Value = SearchText,
                    BinaryExpression = BinaryExpression.And,
                    MethodName = nameof(string.Contains),
                    IgnoreCase = IgnoreCase
                }
            });

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
    }
}
