using System;
using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public abstract class CellBase
    {
        public CellBase(RowBase row)
        {
            Row = row;
        }

        public RowBase Row { get; private set; }

        public String Text { get; set; }

        public String Html { get; set; }

        public Boolean HideCell { get; set; }

        public Dictionary<String, String> Attributes { get; set; }
    }
}