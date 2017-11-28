using FlyerMe.Admin.Models;
using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class ActionCellCustomBoundEventArgs : EventArgs
    {
        public ActionCellCustomBoundEventArgs(ActionCell ac, CellControlBase actionCellControl)
        {
            ActionCell = ac;
            ActionCellControl = actionCellControl;
        }

        public ActionCell ActionCell { get; private set; }

        public CellControlBase ActionCellControl { get; private set; }
    }
}