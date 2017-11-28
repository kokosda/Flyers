using FlyerMe.Admin.Models;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class Head : UserControl
    {
        public GridDefault Grid { get; set; }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Parent.Visible)
                {
                    rptHead.DataSource = Grid.Head.HeaderCells;
                    rptHead.DataBind();
                }
            }
        }

        protected void rptHead_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var th = e.Item.FindControl("thHeaderCell") as HtmlTableCell;
            var literal = e.Item.FindControl("ltlText") as Literal;
            var headerCell = e.Item.DataItem as HeaderCell;

            if (headerCell.Html.HasText())
            {
                literal.Text = headerCell.Html;
            }
            else
            {
                literal.Text = headerCell.Text;

                if (headerCell.Attributes != null)
                {
                    foreach (var a in headerCell.Attributes)
                    {
                        th.Attributes.Add(a.Key, a.Value);
                    }
                }
            }

            if (headerCell.HideCell)
            {
                th.Style.Add("display", "none");
            }
        }
    }
}
