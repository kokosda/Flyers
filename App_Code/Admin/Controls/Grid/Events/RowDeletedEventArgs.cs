using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowDeletedEventArgs : EventArgs
    {
        public RowDeletedEventArgs(Int32 bodyRowIndex, CellsListControlBase dataCellsList)
        {
            BodyRowIndex = bodyRowIndex;
            DataCellsList = dataCellsList;
        }

        public Int32 BodyRowIndex { get; private set; }

        public CellsListControlBase DataCellsList { get; private set; }
    }
}