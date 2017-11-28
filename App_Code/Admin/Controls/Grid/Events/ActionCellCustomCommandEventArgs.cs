using FlyerMe.Admin.Models;
using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class ActionCellCustomCommandEventArgs : EventArgs
    {
        public ActionCellCustomCommandEventArgs(Int32 bodyRowIndex, CellControlBase actionCellControl)
        {
            BodyRowIndex = bodyRowIndex;
            ActionCellControl = actionCellControl;
        }

        public Int32 BodyRowIndex { get; private set; }

        public CellControlBase ActionCellControl { get; private set; }
    }
}