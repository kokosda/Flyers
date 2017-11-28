using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Discounts
{
    public partial class OfferDetails : AdminPageBase
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

        protected void btnAddOfferDiscountDetails_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                Decimal @decimal;

                if (inputEmail.Value.HasNoText())
                {
                    message.MessageText = "Email is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputCode.Value.HasNoText())
                {
                    message.MessageText = "Code is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputDiscount.Value.HasNoText())
                {
                    message.MessageText = "Discount is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Decimal.TryParse(inputDiscount.Value, out @decimal))
                {
                    message.MessageText = "Discount should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.ID = "sds";
                    Form.Controls.Add(sds);
                    sds.InsertParameters["InValue"].DefaultValue = (!(Boolean.Parse(ddlIn.SelectedValue))).ToString();
                    sds.Insert();
                    message.MessageText = "Offer discount details has been added successfully.";
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Offer Discount ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Email" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Code" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Created Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "In" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Discount" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Active" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["pkOfferDiscountDetailID"].ToString(),
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["fkOfferDiscountID"].ToString(),
                                                                    InputType = DataInputTypes.Text,
                                                                    IsInlineEditable =  true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Email"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Code"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["CreatedDate"].GetDateTimeStringFromObject()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["InPercentage"]) ? "%" : "$",
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Discount"].GetDecimalStringFromObject("0.00"),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Active"].GetBooleanStringFromObject(),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Delete));
        }

        protected void grid_RowEditing(Object sender, RowEditingEventArgs e)
        {
            var cell = e.DataCellsList.GetCell(5);
            var ddl = cell.FindControl("gridDdl") as DropDownList;
            var selectedValue = cell.GetPropertyValue("Value");

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(ddlIn.Items.FindByValue("False").Text, "$"));
            ddl.Items.Add(new ListItem(ddlIn.Items.FindByValue("True").Text, "%"));

            var li = ddl.Items.FindByValue(selectedValue);

            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            else
            {
                ddl.SelectedIndex = 0;
            }
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                var offerDiscountId = e.DataCellsList.GetCell(1).GetPropertyValue("Value");
                var email = e.DataCellsList.GetCell(2).GetPropertyValue("Value");
                var code = e.DataCellsList.GetCell(3).GetPropertyValue("Value");
                var inPercentage = String.Compare(e.DataCellsList.GetCell(5).GetPropertyValue("Value"), "%", true) == 0 ? Boolean.TrueString : Boolean.FalseString;
                var discount = e.DataCellsList.GetCell(6).GetPropertyValue("Value");
                var active = e.DataCellsList.GetCell(7).GetPropertyValue("Value");
                Int32 @int32;
                Decimal @decimal;
                Boolean @boolean;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "pkOfferDiscountDetailID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Int32.TryParse(offerDiscountId, out @int32))
                {
                    message.MessageText = "fkOfferDiscountID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (email.HasNoText())
                {
                    message.MessageText = "Email is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (code.HasNoText())
                {
                    message.MessageText = "Code is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (discount.HasNoText())
                {
                    message.MessageText = "Discount is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Decimal.TryParse(discount, out @decimal))
                {
                    message.MessageText = "Discount should be a number";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Boolean.TryParse(inPercentage, out @boolean))
                {
                    message.MessageText = "In should be a flag.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Boolean.TryParse(active, out @boolean))
                {
                    message.MessageText = "Active should be a flag.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.UpdateParameters["pkOfferDiscountDetailID"].DefaultValue = id;
                    sds.UpdateParameters["fkOfferDiscountID"].DefaultValue = offerDiscountId;
                    sds.UpdateParameters["Email"].DefaultValue = email;
                    sds.UpdateParameters["Code"].DefaultValue = code;
                    sds.UpdateParameters["InValue"].DefaultValue = (!(Boolean.Parse(inPercentage))).ToString();
                    sds.UpdateParameters["InPercentage"].DefaultValue = inPercentage;
                    sds.UpdateParameters["Discount"].DefaultValue = discount;
                    sds.UpdateParameters["Active"].DefaultValue = active;
                    sds.Update();

                    message.MessageText = "Offer discount details has been updated successfully.";
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

        protected void grid_RowEditCanceled(Object sender, RowEditCanceledEventArgs e)
        {
            var value = e.DataCellsList.GetCell(5).GetPropertyValue("PrevValue");

            e.DataCellsList.GetCell(5).GetType().GetProperty("Value").SetValue(e.DataCellsList.GetCell(5), value);
        }

        protected void grid_RowDeleted(Object sender, RowDeletedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                Int32 @int32;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "pkOfferDiscountDetailID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.DeleteParameters["pkOfferDiscountDetailID"].DefaultValue = id;
                    sds.Delete();

                    message.MessageText = "Offer discount details has been deleted successfully.";
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT [fkOfferDiscountID], [Email], [pkOfferDiscountDetailID], [Code], [CreatedDate], [InValue], [InPercentage], [Discount], [Active]";
            grid.GridDataSource.SqlDataSourceFromCommand = "FROM [fly_tblOfferDiscountDetail]";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "ORDER BY [pkOfferDiscountDetailID] DESC";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;

            grid.GridDataSource.SqlDataSourceInsertCommand = "INSERT INTO [fly_tblOfferDiscountDetail] ([fkOfferDiscountID], [Email], [Code], [CreatedDate], [InValue], [InPercentage], [Discount], [Active]) VALUES (@fkOfferDiscountID, @Email, @Code,  getdate(), @InValue, @InPercentage, @Discount, @Active)";

            var objData = new clsData();

            objData.strSql = "SELECT TOP (1) [pkOfferDiscountID] FROM [fly_tblOfferDiscount] ORDER BY pkOfferDiscountID DESC";

            var dt = objData.GetDataTable();
            var offerDiscountId = dt.Rows.Count == 1 ? dt.Rows[0]["pkOfferDiscountID"].ToString() : "0";

            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new Parameter("fkOfferDiscountID", TypeCode.Int32, offerDiscountId));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Email", TypeCode.String, "inputEmail", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Code", TypeCode.String, "inputCode", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new Parameter("InValue", TypeCode.Boolean));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("InPercentage", TypeCode.Boolean, "ddlIn", "SelectedValue"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Discount", TypeCode.Decimal, "inputDiscount", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new Parameter("Active", TypeCode.Boolean, Boolean.TrueString));

            grid.GridDataSource.SqlDataSourceUpdateCommand = "UPDATE [fly_tblOfferDiscountDetail] SET [fkOfferDiscountID] = @fkOfferDiscountID, [Email] = @Email, [Code] = @Code, [InValue] = @InValue, [InPercentage] = @InPercentage, [Discount] = @Discount, [Active] = @Active WHERE [pkOfferDiscountDetailID] = @pkOfferDiscountDetailID";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("pkOfferDiscountDetailID", TypeCode.Int32));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("fkOfferDiscountID", TypeCode.Int32));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Email", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Code", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("InValue", TypeCode.Boolean));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("InPercentage", TypeCode.Boolean));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Discount", TypeCode.Decimal));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Active", TypeCode.Boolean));

            grid.GridDataSource.SqlDataSourceDeleteCommand = "DELETE FROM [fly_tblOfferDiscountDetail] WHERE [pkOfferDiscountDetailID] = @pkOfferDiscountDetailID";
            grid.GridDataSource.SqlDataSourceDeleteParameters.Add(new Parameter("pkOfferDiscountDetailID", TypeCode.Int32));
        }

        #endregion
    }
}
