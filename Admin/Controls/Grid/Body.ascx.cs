using FlyerMe.Admin.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class Body : BodyControlBase
    {
        public override Repeater RptBodyRows
        {
            get
            {
                return rptBodyRows;
            }
        }

        public override GridControlBase GridControl { get; set; }

        public override CellsListControlBase GetDataCellsList(Int32 bodyRowIndex)
        {
            return RptBodyRows.Items[bodyRowIndex].FindControl("dataCellsList") as CellsListControlBase;
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                foreach (RepeaterItem i in rptBodyRows.Items)
                {
                    (i.FindControl("dataCellsList") as CellsListControlBase).Body = this;
                    (i.FindControl("actionCellsList") as CellsListControlBase).Body = this;
                }
            }
            else
            {
                if (Parent.Visible)
                {
                    DataBind();
                }
            }
        }

        protected void rptBodyRows_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var bodyRow = e.Item.DataItem as BodyRow;
            var cellsList = e.Item.FindControl("dataCellsList") as CellsListControlBase;

            cellsList.Body = this;
            cellsList.RptCells.DataSource = bodyRow.DataCells;
            cellsList.RptCells.DataBind();

            cellsList = e.Item.FindControl("actionCellsList") as CellsListControlBase;
            cellsList.Body = this;
            cellsList.RptCells.DataSource = bodyRow.ActionCells;
            cellsList.RptCells.DataBind();
        }

        public override void DataBind()
        {
            rptBodyRows.DataSource = Grid.Body.Rows;
            rptBodyRows.DataBind();
        }
    }
}
