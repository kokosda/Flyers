using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Others
{
    public partial class BlockedEmails : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            InitGrid();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void btnAddBlockedEmail_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                if (inputEmail.Value.HasNoText())
                {
                    message.MessageText = "Email is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.ID = "sds";
                    Form.Controls.Add(sds);
                    sds.Insert();

                    message.MessageText = "Blocked Email has been added successfully.";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { HideCell = true });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Created Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["pk_SpamFilterID"].ToString(),
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email"] as String,
                                                                    InputType = DataInputTypes.Text,
                                                                    IsInlineEditable = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["CreatedDate"].GetDateStringFromObject()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Delete));
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                var email = e.DataCellsList.GetCell(1).GetPropertyValue("Value");

                if (email.HasNoText())
                {
                    message.MessageText = "Email is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.UpdateParameters["pk_SpamFilterID"].DefaultValue = id;
                    sds.UpdateParameters["Email"].DefaultValue = email;
                    sds.Update();

                    message.MessageText = "Blocked Email has been updated successfully.";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        protected void grid_RowDeleted(Object sender, RowDeletedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                Int32 @int32;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "pk_SpamFilterID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.DeleteParameters["pk_SpamFilterID"].DefaultValue = id;
                    sds.Delete();

                    message.MessageText = "Blocked Email has been deleted successfully.";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
            }
            catch(Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            message.RedirectToShowMessage();
        }

        #region private

        private void InitGrid()
        {
            grid.GridDataSource.SqlDataSourceSelectCommand = "select *";
            grid.GridDataSource.SqlDataSourceFromCommand = "from tblSpamFilter";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by pk_SpamFilterID desc";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;

            grid.GridDataSource.SqlDataSourceInsertCommand = "INSERT INTO tblSpamFilter(Email) VALUES (@Email)";
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Email", TypeCode.String, "inputEmail", "Value"));

            grid.GridDataSource.SqlDataSourceUpdateCommand = "UPDATE tblSpamFilter SET Email = @Email WHERE pk_SpamFilterID= @pk_SpamFilterID";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("pk_SpamFilterID", TypeCode.Int32));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Email", TypeCode.String));

            grid.GridDataSource.SqlDataSourceDeleteCommand = "DELETE FROM tblSpamFilter WHERE (pk_SpamFilterID= @pk_SpamFilterID)";
            grid.GridDataSource.SqlDataSourceDeleteParameters.Add(new Parameter("pk_SpamFilterID", TypeCode.Int32));
        }

        #endregion
    }
}
