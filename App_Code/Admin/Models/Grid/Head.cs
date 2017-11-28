using System.Collections.Generic;

namespace FlyerMe.Admin.Models
{
    public sealed class Head
    {
        public Head()
        {
            HeaderCells = new List<HeaderCell>();
        }

        public List<HeaderCell> HeaderCells { get; set; }
    }
}