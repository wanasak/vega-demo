using System;
using vega_demo.Extensions;

namespace vega_demo.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int? MakeId { get; set; }
        public string SortBy { get ; set ; }
        public bool IsSortAscending { get; set; }
    }
}