using FlyerMe.Admin.Models;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class DataCellsList : CellsListControlBase
    {
        public override Repeater RptCells
        {
            get
            {
                return rptCells;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                foreach (RepeaterItem i in RptCells.Items)
                {
                    (i.FindControl("cell") as CellControlBase).CellsList = this;
                }
            }
        }

        public void SetDefaultDisplayMode()
        {
            foreach (RepeaterItem i in RptCells.Items)
            {
                var dataCellControl = i.FindControl("cell") as DataCellControl;

                dataCellControl.SetDefaultDisplayMode();
            }
        }

        public void RecoverInputState()
        {
            DataCellControl dcc;

            for (var i = 0; i < RptCells.Items.Count; i++)
            {
                dcc = (GetCell(i) as DataCellControl);

                if (dcc.DataInputType != DataInputTypes.None)
                {
                    dcc.Value = dcc.PrevValue;
                }
            }
        }

        protected void rptCells_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var cellControl = e.Item.FindControl("cell") as DataCellControl;
            var dc = e.Item.DataItem as DataCell;

            cellControl.BindDataCell(dc);
        }
    }
}
