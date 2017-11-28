using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowCommandedEventArgs : EventArgs
    {
        public RowCommandedEventArgs(Int32 bodyRowIndex, CellsListControlBase dataCellsList, Object sender, CommandEventArgs commandEventArgs)
        {
            BodyRowIndex = bodyRowIndex;
            DataCellsList = dataCellsList;
            Sender = sender;
            CommandEventArgs = commandEventArgs;
        }

        public Int32 BodyRowIndex { get; private set; }

        public CellsListControlBase DataCellsList { get; private set; }

        public Object Sender { get; private set; }

        public CommandEventArgs CommandEventArgs { get; private set; }
    }
}