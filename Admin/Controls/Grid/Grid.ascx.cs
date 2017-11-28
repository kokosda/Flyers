using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class GridControl : GridControlBase
    {
        public override Int32 StartRowIndex
        {
            get
            {
                return (pager.PageNumber - 1) * PageSize;
            }
        }

        public override Int32 TotalRecords
        {
            get
            {
                var result = Grid != null ? Grid.Body.TotalRecords : 0;

                if (result == 0)
                {
                    if (IsPostBack)
                    {
                        if (hfTotalRecords.Value.HasText())
                        {
                            result = Int32.Parse(hfTotalRecords.Value);
                        }
                    }
                }

                if (!IsPostBack)
                {
                    hfTotalRecords.Value = result.ToString();
                }

                return result;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            RowEditing += GridControl_RowEditing;

            if (IsPostBack)
            {
                body.GridControl = this;
            }

            SetPager();
            SetExcelExporter();
            SetHtmlElements();

            if (PreheadLiteralText.HasText())
            {
                ltlPrehead.Text = PreheadLiteralText;
                ltlPrehead.Visible = true;
            }
        }

        public override void DataBind()
        {
            Grid = BuildGridFromDataSource();
            head.Grid = Grid;
            body.GridControl = this;
            body.DataBind();
        }

        #region private

        private void GridControl_RowEditing(Object sender, RowEditingEventArgs e)
        {
            for (var i = 0; i < body.RptBodyRows.Items.Count; i++)
            {
                foreach (RepeaterItem ri in (body.RptBodyRows.Items[i].FindControl("actionCellsList") as CellsListControlBase).RptCells.Items)
                {
                    if (ri.ItemIndex == e.CellIndex)
                    {
                        var cell = ri.FindControl("cell");

                        cell.GetType().GetMethod("SetDefaultDisplayMode").Invoke(cell, new Object[] { e.BodyRowIndex });
                        break;
                    }
                }

                if (i != e.BodyRowIndex)
                {
                    var cellsList = (body.RptBodyRows.Items[i].FindControl("dataCellsList") as CellsListControlBase);

                    cellsList.GetType().GetMethod("RecoverInputState").Invoke(cellsList, null);
                }
            }
        }

        private void SetPager()
        {
            if (PagingDisabled)
            {
                pager.Visible = false;
            }
            else
            {
                var itemsCount = TotalRecords;
                var filter = Filter.Bind(Request, itemsCount.ToString(), EncodeUrlParametersForPager);

                pager.Filter = filter;
                pager.PageName = PageName;
                pager.ItemsCount = Int32.Parse(filter.ItemsCount);

                if (PageSize > 0)
                {
                    pager.PageSize = PageSize;
                }
            }
        }

        private void SetExcelExporter()
        {
            if (ExcelExporterEnabled)
            {
                excelExporterTop.Visible = true;
                excelExporterBottom.Visible = true;
            }
            else
            {
                excelExporterTop.Visible = false;
                excelExporterBottom.Visible = false;
            }

            excelExporterTop.SqlSelectCommand = GridDataSource.SqlExcelExportCommand;
            excelExporterBottom.SqlSelectCommand = GridDataSource.SqlExcelExportCommand;
        }

        private void SetHtmlElements()
        {
            if (TableFixed)
            {
                AddClassAttribute(divTable.Attributes, "tfixed");
            }

            if (TableNews)
            {
                AddClassAttribute(divTable.Attributes, "news");
            }

            if (TableSmall)
            {
                AddClassAttribute(divTable.Attributes, "small");
            }
        }

        private void AddClassAttribute(AttributeCollection ac, String value)
        {
            if (ac["class"].HasNoText())
            {
                ac["class"] = String.Empty;
            }

            if (ac["class"].IndexOf(value, StringComparison.Ordinal) < 0)
            {
                divTable.Attributes["class"] += " " + value;
            }
        }

        private class Filter : IFilter
        {
            private Filter(NameValueCollection nvc)
            {
                ItemsCount = nvc["itemscount"];
                nameValueCollection = nvc;
            }

            public static Filter Bind(HttpRequest request, String itemsCount, NameValueCollection encodeUrlParameters)
            {
                var nvc = new NameValueCollection(request.QueryString);

                if (encodeUrlParameters != null)
                {
                    foreach(String p in encodeUrlParameters)
                    {
                        if (nvc[p] != null)
                        {
                            nvc[p] = Helper.GetUrlEncodedString(nvc[p]);
                        }
                    }
                }

                nvc.Remove("page");
                nvc.Remove("message");
                nvc.Remove("messageclass");

                if (nvc["itemscount"].HasNoText())
                {
                    nvc.Add("itemscount", itemsCount);
                }

                var result = new Filter(nvc);

                return result;
            }

            public String ItemsCount { get; private set; }

            public Boolean IsEntityFieldsEmpty
            {
                get 
                {
                    return ItemsCount.HasNoText();
                }
            }

            public String EntityFieldsQueryString
            {
                get 
                {
                    return nameValueCollection.NameValueToQueryString();
                }
            }

            #region private

            private readonly NameValueCollection nameValueCollection;

            #endregion
        }

        #endregion
    }
}