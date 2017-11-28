using FlyerMe.Admin.Models;
using System;
using System.Data;

namespace FlyerMe.Admin.Controls.Grid.Events
{
    public sealed class RowDataBindingEventArgs : EventArgs
    {
        public RowDataBindingEventArgs(Int32 bodyRowIndex, DataRow dataRow, GridDefault grid)
        {
            BodyRowIndex = bodyRowIndex;
            DataRow = dataRow;
            Grid = grid;
        }

        public RowDataBindingEventArgs(Int32 bodyRowIndex, Object data, GridDefault grid)
        {
            BodyRowIndex = bodyRowIndex;
            Data = data;
            Grid = grid;
        }

        public Int32 BodyRowIndex { get; private set; }

        public DataRow DataRow { get; private set; }

        public Object Data { get; private set; }

        public GridDefault Grid { get; private set; }
    }
}