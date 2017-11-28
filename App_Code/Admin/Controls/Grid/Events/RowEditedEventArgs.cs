using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowEditedEventArgs : EventArgs
    {
        public RowEditedEventArgs(Int32 bodyRowIndex, CellsListControlBase dataCellsList)
        {
            BodyRowIndex = bodyRowIndex;
            DataCellsList = dataCellsList;
        }

        public Int32 BodyRowIndex { get; private set; }

        public CellsListControlBase DataCellsList { get; private set; }
    }
}