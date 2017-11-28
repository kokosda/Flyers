using System;
using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public abstract class RowBase
    {
        public RowBase(Int32 index)
        {
            Index = index;
            Cells = new List<CellBase>();
        }

        public List<CellBase> Cells { get; set; }

        public Int32 Index { get; set; }
    }
}