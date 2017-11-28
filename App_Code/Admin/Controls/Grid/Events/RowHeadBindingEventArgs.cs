using FlyerMe.Admin.Models;
using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowHeadBindingEventArgs : EventArgs
    {
        public RowHeadBindingEventArgs(GridDefault grid)
        {
            Grid = grid;
        }

        public GridDefault Grid { get; private set; }
    }
}