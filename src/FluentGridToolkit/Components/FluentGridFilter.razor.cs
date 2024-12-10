using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Components
{
    public partial class FluentGridFilter<TGridItem, TProp> : FluentGridFilterBase<TGridItem, TProp>
    {
        /// <summary>
        /// Gets or sets the placeholder text for the input field.
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "Enter text...";

        /// <summary>
        /// Gets or sets the CSS style for the input box.
        /// </summary>
        [Parameter]
        public string InputStyle { get; set; } = "flex: 1;width:200px";

        /// <summary>
        /// Gets or sets the data type that will be subject to filter
        /// </summary>
        [Parameter]
        public required FilterDataType DataType { get; set; } = FilterDataType.Text;

        /// <summary>
        /// Gets or sets the icon for the plus button.
        /// </summary>
        [Parameter]
        public Icon PlusIcon { get; set; } = new ToolkitIcons.PlusIcon();


        private List<FilterValue> FilterValues { get; set; } = new();
        private List<Option<BinaryExpression>> BinaryOptions { get; set; } = new() {
            new Option<BinaryExpression>() { Text = "And", Value = BinaryExpression.And },
            new Option<BinaryExpression>() { Text = "Or", Value = BinaryExpression.Or },
        };

        private List<Option<ComparisonOperator>> Operator { get; set; } = new();


        private Dictionary<string, string> items = new Dictionary<string, string>()
    {
        {"And","And"},
        {"Or","Or"},
    };


        protected override void OnInitialized()
        {
            AddOnMore(); //add the first item
            foreach (var op in Enum.GetNames(typeof(ComparisonOperator)))
            {
                Operator.Add(new Option<ComparisonOperator>()
                {
                    Text = op,
                    Value = (ComparisonOperator)Enum.Parse(typeof(ComparisonOperator), op)
                });
            }
        }

        private async Task HandleSearch(MouseEventArgs e)
        {
            if (FilterValues.Any(i => i == null))
            {
                HasError = true;
                StateHasChanged();
            }
            else HasError = false;

            var propName = Property.GetPropertyName();
            var filters = FilterValues.Select(i => i.ToFilterExpression(propName, DataType)).ToList();


            FilterManager.AddOrUpdateFilter(propName, filters);

            if (OnSearchClicked.HasDelegate)
                await OnSearchClicked.InvokeAsync();

            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync();

        }

        private void AddOnMore()
        {
            if (!FilterValues.Any())
            {
                FilterValues.Add(new FilterValue());
                return;
            }
            if (FilterValues.Last().IsNullValue(DataType)) return;
            FilterValues.Add(new FilterValue());
            StateHasChanged();
        }

        private void HandleOnMenuChanged(MenuChangeEventArgs args)
        {
            if (FilterValues.Any())
            {
                FilterValues.Last().BinaryExpression = args.Value == "And" ? "And" : "Or";
            }
            AddOnMore();
        }
    }
}
