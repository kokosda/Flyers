using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Users
{
    public partial class UserReferrals : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitGrid();

            if (!IsPostBack)
            {
                DataBind();
                UpdateUserReferrals();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            var cell = e.DataCellsList.RptCells.Items[0].FindControl("cell");

            cell = e.DataCellsList.RptCells.Items[0].FindControl("cell");
            grid.GridDataSource.SqlDataSourceUpdateParameters["pk_ReferralID"].DefaultValue = cell.GetType().GetProperty("Value").GetValue(cell) as String;
            cell = e.DataCellsList.RptCells.Items[3].FindControl("cell");
            grid.GridDataSource.SqlDataSourceUpdateParameters["ReferredBy"].DefaultValue = cell.GetType().GetProperty("Value").GetValue(cell) as String;

            var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

            sds.Update();
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { HideCell = true });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Customer Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Referred Source" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Referred By" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Created Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                    {
                                                                        Text = e.DataRow["pk_ReferralID"].ToString(),
                                                                        InputType = DataInputTypes.Hidden,
                                                                        HideCell = true
                                                                    });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                    {
                                                                        Text = e.DataRow["email"] as String
                                                                    });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                    {
                                                                        Text = e.DataRow["ReferredSource"] as String
                                                                    });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                    {
                                                                        Text = e.DataRow["ReferredBy"] as String,
                                                                        InputType = DataInputTypes.Text,
                                                                        IsInlineEditable = true
                                                                    });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                    {
                                                                        Text = e.DataRow.GetDateTimeStringFromDataColumn("CreatedDate")
                                                                    });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "select * ";
            grid.GridDataSource.SqlDataSourceFromCommand = "from tblReferrals";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by CreatedDate desc";
            grid.GridDataSource.SqlDataSourceUpdateCommand = "update tblReferrals set ReferredBy=@ReferredBy where pk_ReferralID=@pk_ReferralID";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("ReferredBy", DbType.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("pk_ReferralID", DbType.Int32));
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;
        }

        private void UpdateUserReferrals()
        {
            var objData = new clsData();
            var dtUsers = objData.GetDataTable("usp_GetNewProfilesForReferrals");
            ProfileCommon profile = null;

            foreach (DataRow dr in dtUsers.Rows)
            {
                profile = Profile.GetProfile(dr["Email"] as String);

                var ht = new Hashtable();
                var RefferedBy = profile.ReferredBy.Split('|');

                ht.Add("Email", dr["Email"] as String);

                if (RefferedBy.Length > 1)
                {
                    ht.Add("ReferredSource", RefferedBy[0]);
                    ht.Add("ReferredBy", RefferedBy[1]);
                }
                else
                {
                    ht.Add("ReferredSource", String.Empty);
                    ht.Add("ReferredBy", RefferedBy[0]);
                }

                ht.Add("CreatedDate", dr["createdate"].ToString());
                objData.ExecuteSql("usp_InsertNewReferralProfile", ht);
            }
        }

        #endregion
    }
}
