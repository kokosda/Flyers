using FlyerMe.Admin.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public abstract class BodyControlBase : UserControl
    {
        public GridDefault Grid
        { 
            get
            {
                return GridControl.Grid;
            }
        }

        public abstract Repeater RptBodyRows { get; }

        public abstract GridControlBase GridControl { get; set; }

        public abstract CellsListControlBase GetDataCellsList(Int32 bodyRowIndex);
    }
}