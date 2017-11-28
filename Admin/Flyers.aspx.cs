using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin
{
    public partial class Flyers : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/flyers";
            }
        }

        protected void Page_Load(Object sender, EventArgs args)
        {
            InitGrid();
            InitInputs();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Flyer Id" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Customer Id" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Type" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "State" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Tax %" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Price" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Trans Id" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Promo" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Status" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Delivery" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Created" });
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = String.Format("<a href='{0}flyers/details.aspx?orderid={1}'>{1}</a>", AdminSiteRootUrl, e.DataRow[0].ToString())
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[1] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[2] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[3] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDecimalStringFromDataColumn(4, "0.00")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetPriceStringFromDataColumn(5)
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[6] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow[7] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = (e.DataRow[8] as String).Capitalize(),
                                                                    Attributes = new Dictionary<String, String> { { "class", (e.DataRow[8] as String).ToLower() } }
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateStringFromDataColumn(9)
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow.GetDateStringFromDataColumn(10)
                                                                });
        }

        protected void btnSearchOrder_Click(Object sender, EventArgs e)
        {
            var url = Request.Url.AbsolutePath;
            var query = String.Empty;

            foreach (Control c in divSearch.Controls)
            {
                var input = c as HtmlInputText;

                if (input != null && input.Value.HasText())
                {
                    query += "&" + input.ID.Replace("input", null).ToLower() + "=" + input.Value;
                    continue;
                }

                var ddl = c as DropDownList;

                if (ddl != null && ddl.SelectedValue.HasText())
                {
                    query += "&" + ddl.ID.Replace("ddl", null).ToLower() + "=" + ddl.SelectedValue;
                    continue;
                }
            }

            if (query.HasText())
            {
                url += "?" + query.Substring(1, query.Length - 1);
            }

            Response.Redirect(url, true);
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT [order_id], [customer_id], [type], [market_state], [invoice_tax], [tota_price], [invoice_transaction_id], [invoice_promocode], [status], [delivery_date], [created_on]";
            grid.GridDataSource.SqlDataSourceFromCommand = "FROM [fly_order]";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by [order_id] DESC";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
            grid.GridDataSource.SqlExcelExportCommand = 
            @"SELECT order_id , customer_id, [type], market_state ,market_county, market_association, market_msa ,tota_price, invoice_tax, invoice_transaction_id, status, theme, layout, delivery_date, mls_number, email_subject, title, sub_title, prop_address1, prop_address2, prop_city, prop_state, prop_zipcode, prop_desc, prop_price, Bedrooms, FullBaths, HalfBaths, Parking, SqFoots, YearBuilt, Floors, LotSize, Subdivision, HOA, mls_link, virtualtour_link, map_link, use_mls_logo, use_housing_logo, property_type, created_on, updated_on, sent_on, PropertyFeatures, PropertyFeaturesValues, OtherPropertyFeatures, Category, PropertyType,  IsSyndicat, LastEmailDeliveryTime, AptSuiteBldg, OpenHouses
              FROM fly_order
              left outer join fly_PropertyType on fly_order.fk_PropertyType=fly_PropertyType.PropertyTypeID
              left outer join fly_PropertyCategory on fly_order.fk_PropertyCategory=fly_PropertyCategory.CategoryID";
        }

        private void InitInputs()
        {
            Boolean containsData = true;

            if (!IsPostBack)
            {
                BindDataToInputs(out containsData);
            }

            if (containsData)
            {
                var whereCommand = "WHERE 1=1 ";

                if (inputOrderId.Value.HasText())
                {
                    whereCommand += "and [order_id] = " + inputOrderId.Value.Trim() + " ";
                }
                if (inputTransactionId.Value.HasText())
                {
                    whereCommand += "and [invoice_transaction_id] = '" + inputTransactionId.Value.Trim() + "' ";
                }
                if (inputCustomerId.Value.HasText())
                {
                    whereCommand += "and [customer_id] like '%" + inputCustomerId.Value.Trim() + "%' ";
                }
                if (ddlOrderStatus.SelectedValue.HasText())
                {
                    whereCommand += "and [status] = '" + ddlOrderStatus.SelectedValue + "' ";
                }
                if (inputDeliveryFrom.Value.HasText())
                {
                    whereCommand += " and [delivery_date] >= '" + inputDeliveryFrom.Value.Trim() + "' ";
                }
                if (inputDeliveryTo.Value.HasText())
                {
                    whereCommand += " and [delivery_date] <= '" + inputDeliveryTo.Value.Trim() + "' ";
                }
                if (inputOrderFrom.Value.HasText())
                {
                    whereCommand += " and [created_on] >= '" + inputOrderFrom.Value.Trim() + "' ";
                }
                if (inputOrderTo.Value.HasText())
                {
                    whereCommand += " and [created_on] <= '" + inputOrderTo.Value.Trim() + "' ";
                }

                grid.GridDataSource.SqlDataSourceWhereCommand = whereCommand;
            }
        }

        private void BindDataToInputs(out Boolean containsData)
        {
            containsData = false;

            foreach (String name in Request.QueryString)
            {
                var input = divSearch.FindControl("input" + name) as HtmlInputText;

                if (input != null)
                {
                    input.Value = Request.QueryString[name];
                    containsData = true;
                    continue;
                }

                var ddl = divSearch.FindControl("ddl" + name) as DropDownList;

                if (ddl != null)
                {
                    var item = ddl.Items.FindByValue(Request.QueryString[name]);

                    if (item != null)
                    {
                        ddl.SelectedIndex = ddl.Items.IndexOf(item);
                        containsData = true;
                        continue;
                    }
                }
            }
        }

        #endregion
    }
}
