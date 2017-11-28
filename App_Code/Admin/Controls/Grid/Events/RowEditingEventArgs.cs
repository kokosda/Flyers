using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowEditingEventArgs : EventArgs
    {
        public RowEditingEventArgs(Int32 bodyRowIndex, Int32 cellIndex, CellsListControlBase dataCellsList)
        {
            BodyRowIndex = bodyRowIndex;
            CellIndex = cellIndex;
            DataCellsList = dataCellsList;
        }

        public Int32 BodyRowIndex { get; private set; }

        public Int32 CellIndex { get; private set; }

        public CellsListControlBase DataCellsList { get; private set; }
    }
}