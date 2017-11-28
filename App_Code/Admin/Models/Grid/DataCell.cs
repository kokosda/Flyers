using System;

namespace FlyerMe.Admin.Models
{
    public sealed class DataCell : CellBase
    {
        public DataCell(RowBase row) : base(row)
        {
        }

        public Object Data { get; set; }

        public Boolean IsInlineEditable { get; set; }

        public DataInputTypes InputType { get; set; }

        public Boolean DisplayAsDisabledInput { get; set; }

        public Boolean? TextIsInvisible { get; set; }
    }
}