using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Discounts
{
    public partial class Offer : AdminPageBase
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

        protected void btnAddOffer_Command(Object sender, CommandEventArgs e)
        {
            try
            {
                if (inputCode.Value.HasNoText())
                {
                    message.MessageText = "Code is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                if (inputDiscount.Value.HasNoText())
                {
                    message.MessageText = "Discount is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                Decimal @decimal;

                if (!Decimal.TryParse(inputDiscount.Value, out @decimal))
                {
                    message.MessageText = "Discount should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var ht = new Hashtable();

                    ht.Add("fkDiscountTypeID", Int32.Parse(ddlDiscountType.SelectedValue));
                    ht.Add("Code", inputCode.Value.Trim());
                    ht.Add("InValue", !(Boolean.Parse(ddlIn.SelectedValue)));
                    ht.Add("InPercentage", Boolean.Parse(ddlIn.SelectedValue));
                    ht.Add("Discount", Decimal.Parse(inputDiscount.Value));
                    ht.Add("Active", inputActive.Checked);

                    var dt = new clsData().GetDataTable("usp_InsertOfferDiscount", ht);

                    if (String.Compare(dt.Rows[0]["result"] as String, "Updated", true) == 0)
                    {
                        message.MessageText = "Offer has been updated successfully!";
                    }
                    else if (String.Compare(dt.Rows[0]["result"] as String, "Inserted", true) == 0)
                    {
                        message.MessageText = "Offer has been added successfully!";
                    }

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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Offer Discount ID" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Type" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Code" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Discount" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "In" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Active" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["pkOfferDiscountID"].ToString()
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["DiscountType"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Code"] as String
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Discount"].GetDecimalStringFromObject("0.00")
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = ((Boolean)e.DataRow["InPercentage"]) ? "%" : "$"
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Active"].GetBooleanStringFromObject(),
                                                                    InputType = DataInputTypes.Checkbox,
                                                                    DisplayAsDisabledInput = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].ActionCells.Add(new ActionCell(e.Grid.Body.Rows[e.BodyRowIndex], ActionTypes.Delete));
        }

        protected void grid_RowDeleted(Object sender, RowDeletedEventArgs e)
        {
            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                Int32 @int32;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "pkOfferDiscountID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var objData = new clsData();

                    objData.strSql = "delete from fly_tblOfferDiscount where pkOfferDiscountID=" + id;
                    objData.ExecuteSql();

                    message.MessageText = "Specified discount has been deleted successfully!";
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
            grid.GridDataSource.SqlDataSourceSelectCommand = "SELECT [pkOfferDiscountID], [fkDiscountTypeID], [DiscountType], [Code], [InValue], [InPercentage], [Discount], [Active] FROM [fly_tblOfferDiscount] inner join [fly_tblDiscountType] on pkDiscountTypeID=fkDiscountTypeID";
            grid.GridDataSource.PagingDisabled = grid.PagingDisabled;
        }

        #endregion
    }
}
