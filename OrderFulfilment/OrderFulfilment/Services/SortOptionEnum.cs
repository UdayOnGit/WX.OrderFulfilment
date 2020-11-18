using System.ComponentModel;

namespace WX.OrderFulfilment.Services
{
    public enum SortOptionEnum
    {
        [Description("Low to High Price")]
        Low,
        [Description("High to Low Price")]
        High,
        [Description("A - Z sort on the Name")]
        Ascending,
        [Description(" Z - A sort on the Name")]
        Descending,
        [Description("Recommended")]
        Recommended
    }
}
