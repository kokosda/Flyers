using System;
using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public sealed class Body
    {
        public Body()
        {
            Rows = new List<BodyRow>();
        }

        public List<BodyRow> Rows { get; set; }

        public Int32 TotalRecords { get; set; }
    }
}