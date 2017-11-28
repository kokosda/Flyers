using System;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowEditCanceledEventArgs : EventArgs
    {
        public RowEditCanceledEventArgs(Int32 bodyRowIndex, CellsListControlBase dataCellsList)
        {
            BodyRowIndex = bodyRowIndex;
            DataCellsList = dataCellsList;
        }

        public Int32 BodyRowIndex { get; private set; }

        public CellsListControlBase DataCellsList { get; private set; }
    }
}