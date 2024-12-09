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
        /// Handles the click event of the search button.
        /// </summary>
        private async Task HandleSearch()
        {

            await HandleTextSearch(SearchText);

        }
    }
}
