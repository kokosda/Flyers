using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public sealed class GridDefault : GridBase
    {
        public GridDefault()
        {
            Head = new Head();
            Body = new Body();
        }
    }
}