using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Discounts
{
    public partial class Promo : AdminPageBase
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

        protected void btnAddDiscount_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                Decimal @decimal;

                if (inputCode.Value.HasNoText())
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
                    sds.Insert();
                    message.MessageText = "Promo code has been added successfully.";
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Code" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Discount" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "In" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "One Time Use" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Disabled" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["DiscountCodeID"].ToString(),
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["DiscountDate"].GetDateStringFromObject()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["DiscountCode"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Discount"].GetDecimalStringFromObject("0.00"),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["InPercentage"]) ? "%" : "$",
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Select
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["OneTimeUse"].GetBooleanStringFromObject(),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Disable"].GetBooleanStringFromObject(),
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Edit));
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Delete));
        }

        protected void grid_RowEditing(Object sender, RowEditingEventArgs e)
        {
            var cell = e.DataCellsList.GetCell(4);
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
                var code = e.DataCellsList.GetCell(2).GetPropertyValue("Value");
                var discount = e.DataCellsList.GetCell(3).GetPropertyValue("Value");
                var inPercentage = String.Compare(e.DataCellsList.GetCell(4).GetPropertyValue("Value"), "%", true) == 0 ? Boolean.TrueString : Boolean.FalseString;
                var oneTimeUse = e.DataCellsList.GetCell(5).GetPropertyValue("Value");
                var disable = e.DataCellsList.GetCell(6).GetPropertyValue("Value");
                Int32 @int32;
                Decimal @decimal;
                Boolean @boolean;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "Discount Code ID should be a number.";
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
                else if (!Boolean.TryParse(oneTimeUse, out @boolean))
                {
                    message.MessageText = "One Time Use should be a flag.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!Boolean.TryParse(disable, out @boolean))
                {
                    message.MessageText = "Disable should be a flag.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;
                    sds.UpdateParameters["DiscountCodeID"].DefaultValue = id;
                    sds.UpdateParameters["DiscountCode"].DefaultValue = code;
                    sds.UpdateParameters["Discount"].DefaultValue = discount;
                    sds.UpdateParameters["InPercentage"].DefaultValue = inPercentage;
                    sds.UpdateParameters["OneTimeUse"].DefaultValue = oneTimeUse;
                    sds.UpdateParameters["Disable"].DefaultValue = disable;
                    sds.Update();

                    message.MessageText = "Promo code has been updated successfully.";
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
            var value = e.DataCellsList.GetCell(4).GetPropertyValue("PrevValue");

            e.DataCellsList.GetCell(4).GetType().GetProperty("Value").SetValue(e.DataCellsList.GetCell(4), value);
        }

        protected void grid_RowDeleted(Object sender, RowDeletedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                Int32 @int32;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "Discount Code ID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.DeleteParameters["DiscountCodeID"].DefaultValue = id;
                    sds.Delete();

                    message.MessageText = "Promo code has been deleted successfully.";
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT DiscountCodeID, DiscountCode, Discount, InPercentage, OneTimeUse, Disable, DiscountDate";
            grid.GridDataSource.SqlDataSourceFromCommand = "FROM fly_Discount";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by DiscountCodeID DESC";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;

            grid.GridDataSource.SqlDataSourceInsertCommand = "INSERT INTO fly_Discount(DiscountCode, Discount, InPercentage, OneTimeUse, Disable, DiscountDate) VALUES (@DiscountCode, @Discount, @InPercentage, @OneTimeUse, @Disable, GETDATE())";
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("DiscountCode", TypeCode.String, "inputCode", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Discount", TypeCode.Decimal, "inputDiscount", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("InPercentage", TypeCode.Boolean, "ddlIn", "SelectedValue"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("OneTimeUse", TypeCode.Boolean, "inputOneTimeUse", "Checked"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new Parameter("Disable", TypeCode.Boolean, Boolean.FalseString));

            grid.GridDataSource.SqlDataSourceUpdateCommand = "UPDATE [fly_Discount] SET [DiscountCode] = @DiscountCode, [Discount] = @Discount, [InPercentage]=@InPercentage, [OneTimeUse] = @OneTimeUse, [Disable] = @Disable WHERE [DiscountCodeID] = @DiscountCodeID";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("DiscountCodeID", TypeCode.Int32));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("DiscountCode", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Discount", TypeCode.Decimal));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("InPercentage", TypeCode.Boolean));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("OneTimeUse", TypeCode.Boolean));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Disable", TypeCode.Boolean));

            grid.GridDataSource.SqlDataSourceDeleteCommand = "DELETE FROM [fly_Discount] WHERE [DiscountCodeID] = @DiscountCodeID";
            grid.GridDataSource.SqlDataSourceDeleteParameters.Add(new Parameter("DiscountCodeID", TypeCode.Int32));
        }

        #endregion
    }
}
