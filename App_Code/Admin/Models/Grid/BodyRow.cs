using System;
using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public sealed class BodyRow : RowBase
    {
        public BodyRow(Int32 index) : base(index)
        {
            DataCells = new List<DataCell>();
            ActionCells = new List<ActionCell>();
        }

        public List<DataCell> DataCells { get; set; }

        public List<ActionCell> ActionCells { get; set; }
    }
}