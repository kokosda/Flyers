using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using FlyerMe.SpecialExtensions;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Others
{
    public partial class CustomerTestimonials : AdminPageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/admin/others/customertestimonials";
            }
        }

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

        protected void btnAddTestimonial_Command(Object sender, CommandEventArgs e)
        {
            var forceRedirect = false;

            try
            {
                DateTime @dateTime;

                if (inputFirstName.Value.HasNoText())
                {
                    message.MessageText = "First Name is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputLastName.Value.HasNoText())
                {
                    message.MessageText = "Last Name is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputCompany.Value.HasNoText())
                {
                    message.MessageText = "Company is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputPosition.Value.HasNoText())
                {
                    message.MessageText = "Position is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (textareaMessage.Value.HasNoText())
                {
                    message.MessageText = "Message is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (inputSubmittedDate.Value.HasNoText())
                {
                    message.MessageText = "Submitted date is required";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!DateTime.TryParse(inputSubmittedDate.Value, out @dateTime))
                {
                    message.MessageText = "Submitted date format is incorrect. Please use following format: " + SpecialExtensions.StringSpecialHelper.GetDateFormat();
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.ID = "sds";
                    Form.Controls.Add(sds);
                    sds.Inserted += sds_Inserted;
                    sds.Insert();

                    if (message.MessageText.HasNoText())
                    {
                        message.MessageText = "Customer testimonial has been added successfully.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        forceRedirect = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok || forceRedirect)
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
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Image" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "First Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Name" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Company" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Position" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Message" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Submitted Date" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Active" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Delete));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["pk_TestimonialID"].ToString(),
                                                                    InputType = DataInputTypes.Hidden,
                                                                    HideCell = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["ImagePath"] != DBNull.Value ? String.Format("<div class='image'><img src='{0}' style='max-width: 40px;' /></div>", ResolveUrl(e.DataRow["ImagePath"] as String)) : null,
                                                                    InputType = DataInputTypes.Text,
                                                                    IsInlineEditable = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["FirstName"] as String,
                                                                    InputType = DataInputTypes.Text,
                                                                    IsInlineEditable = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["LastName"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Company"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Position"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["Message"] as String,
                                                                    IsInlineEditable = true,
                                                                    InputType = DataInputTypes.Text
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = e.DataRow["SubmittedDate"].GetDateStringFromObject(),
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
            var value = e.DataCellsList.GetCell(1).GetPropertyValue("Value");

            if (value.HasText())
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                var obj = new clsData();

                obj.strSql = "select * from tblTestimonials where pk_TestimonialID = " + id;

                var dt = obj.GetDataTable();
                var cell = e.DataCellsList.GetCell(1);

                cell.GetType().GetProperty("Value").SetValue(cell, dt.Rows[0]["ImagePath"] as String);
            }
        }

        protected void grid_RowEditCanceled(Object sender, RowEditCanceledEventArgs e)
        {
            var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
            var obj = new clsData();

            obj.strSql = "select * from tblTestimonials where pk_TestimonialID = " + id;

            var dt = obj.GetDataTable();
            var cell = e.DataCellsList.GetCell(1);
            var value = dt.Rows[0]["ImagePath"] as String;

            if (value.HasText())
            {
                value = String.Format("<div class='image'><img src='{0}' style='max-width: 40px;' /></div>", ResolveUrl(value));
            }

            cell.GetType().GetProperty("Value").SetValue(cell, value);
        }

        protected void grid_RowEdited(Object sender, RowEditedEventArgs e)
        {
            var forceRedirect = false;

            try
            {
                var id = e.DataCellsList.GetCell(0).GetPropertyValue("Value");
                var imagePath = e.DataCellsList.GetCell(1).GetPropertyValue("Value");
                var firstName = e.DataCellsList.GetCell(2).GetPropertyValue("Value");
                var lastName = e.DataCellsList.GetCell(3).GetPropertyValue("Value");
                var company = e.DataCellsList.GetCell(4).GetPropertyValue("Value");
                var position = e.DataCellsList.GetCell(5).GetPropertyValue("Value");
                var messageText = e.DataCellsList.GetCell(6).GetPropertyValue("Value");
                var submittedDate = e.DataCellsList.GetCell(7).GetPropertyValue("Value");
                var active = e.DataCellsList.GetCell(8).GetPropertyValue("Value");
                Int32 @int32;
                DateTime @dateTime;

                if (!Int32.TryParse(id, out @int32))
                {
                    message.MessageText = "pk_TestimonialID should be a number";
                }
                else if (firstName.HasNoText())
                {
                    message.MessageText = "First Name is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (lastName.HasNoText())
                {
                    message.MessageText = "Last Name is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (company.HasNoText())
                {
                    message.MessageText = "Company is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (position.HasNoText())
                {
                    message.MessageText = "Position is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (messageText.HasNoText())
                {
                    message.MessageText = "Message is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (submittedDate.HasNoText())
                {
                    message.MessageText = "Submitted date is required";
                    message.MessageClass = MessageClassesEnum.System;
                }
                else if (!DateTime.TryParse(submittedDate, out @dateTime))
                {
                    message.MessageText = "Submitted date format is incorrect. Please use following format: " + SpecialExtensions.StringSpecialHelper.GetDateFormat();
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.UpdateParameters["pk_TestimonialID"].DefaultValue = id;
                    sds.UpdateParameters["FirstName"].DefaultValue = firstName;
                    sds.UpdateParameters["LastName"].DefaultValue = lastName;
                    sds.UpdateParameters["Position"].DefaultValue = position;
                    sds.UpdateParameters["Company"].DefaultValue = company;
                    sds.UpdateParameters["Message"].DefaultValue = messageText;
                    sds.UpdateParameters["ImagePath"].DefaultValue = imagePath;
                    sds.UpdateParameters["SubmittedDate"].DefaultValue = submittedDate;
                    sds.UpdateParameters["Active"].DefaultValue = active;
                    sds.Updated += sds_Updated;
                    sds.Update();

                    if (message.MessageText.HasNoText())
                    {
                        message.MessageText = "Customer Testimonial has been updated successfully.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                    else
                    {
                        forceRedirect = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok || forceRedirect)
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
                    message.MessageText = "pk_TestimonialID should be a number.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var sds = grid.GridDataSource.GetDataSource() as SqlDataSource;

                    sds.DeleteParameters["pk_TestimonialID"].DefaultValue = id;
                    sds.Deleted += sds_Deleted;
                    sds.Delete();

                    if (message.MessageText.HasNoText())
                    {
                        message.MessageText = "Customer Testimonial has been deleted successfully.";
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
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
            grid.GridDataSource.SqlDataSourceFromCommand = "from tblTestimonials";
            grid.GridDataSource.SqlDataSourceOrderByCommand = "order by pk_TestimonialID DESC";
            grid.GridDataSource.SqlDataSourceStartRowIndex = grid.StartRowIndex;
            grid.GridDataSource.SqlDataSourceMaximumRows = grid.PageSize;

            grid.GridDataSource.SqlDataSourceInsertCommand = "INSERT INTO tblTestimonials(FirstName, LastName, Position, Company, ImagePath, Message, SubmittedDate, Active) VALUES (@FirstName, @LastName, @Position, @Company, @ImagePath, @Message, @SubmittedDate, @Active); SET @pk_TestimonialID = Scope_Identity();";
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("FirstName", TypeCode.String, "inputFirstName", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("LastName", TypeCode.String, "inputFirstName", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Position", TypeCode.String, "inputPosition", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Company", TypeCode.String, "inputCompany", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("ImagePath", TypeCode.String, "inputImagePath", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Message", TypeCode.String, "textareaMessage", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("SubmittedDate", TypeCode.DateTime, "inputSubmittedDate", "Value"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new ControlParameter("Active", TypeCode.Boolean, "inputActive", "Checked"));
            grid.GridDataSource.SqlDataSourceInsertParameters.Add(new Parameter("pk_TestimonialID", TypeCode.Int32)
                                                                            {
                                                                                Direction = ParameterDirection.Output
                                                                            });

            grid.GridDataSource.SqlDataSourceUpdateCommand = "UPDATE tblTestimonials SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Company = @Company, Message = @Message, ImagePath = @ImagePath, SubmittedDate = @SubmittedDate, Active = @Active WHERE (pk_TestimonialID = @pk_TestimonialID)";
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("pk_TestimonialID", TypeCode.Int32));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("FirstName", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("LastName", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Position", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Company", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Message", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("ImagePath", TypeCode.String));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("SubmittedDate", TypeCode.DateTime));
            grid.GridDataSource.SqlDataSourceUpdateParameters.Add(new Parameter("Active", TypeCode.Boolean));

            grid.GridDataSource.SqlDataSourceDeleteCommand = "DELETE FROM tblTestimonials WHERE (pk_TestimonialID = @pk_TestimonialID)";
            grid.GridDataSource.SqlDataSourceDeleteParameters.Add(new Parameter("pk_TestimonialID", TypeCode.Int32));
        }

        private void sds_Inserted(Object sender, SqlDataSourceStatusEventArgs e)
        {
            var id = (Int32)e.Command.Parameters["@pk_TestimonialID"].Value;

            SaveCustomerTestimonialImage(id);
        }

        private void SaveCustomerTestimonialImage(Int32 customerTestimonialId)
        {
            try
            {
                if (inputImage.PostedFile != null && inputImage.PostedFile.ContentLength > 0)
                {
                    var bytes = new Byte[16];
                    var random = new Random();

                    random.NextBytes(bytes);

                    var fileName = Helper.GetHexademicalString(bytes);
                    var relativePath = GetCustomerTestimonialImageRelativeDirectory(customerTestimonialId);
                    var fileSystemDirectory = Server.MapPath(relativePath);

                    Helper.HandleCustomerTestimonialsFileImage(fileName, fileSystemDirectory, inputImage.PostedFile.InputStream);

                    var obj = new clsData();
                    var resizedImagePath = Helper.InjectNamePartToFileName(relativePath + "/" + fileName + ".jpg", Helper.CustomerTestimonialImagesSatteliteSuffix_RESIZED);

                    obj.strSql = "UPDATE tblTestimonials SET ImagePath = N'" + resizedImagePath + "' WHERE pk_TestimonialID = " + customerTestimonialId.ToString();
                    obj.ExecuteSql();
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }
        }

        private void sds_Updated(Object sender, SqlDataSourceStatusEventArgs e)
        {
            var id = (Int32)e.Command.Parameters["@pk_TestimonialID"].Value;
            var imagePath = e.Command.Parameters["@ImagePath"].Value as String;

            if (imagePath.HasNoText())
            {
                DeleteCustomerTestimonialImageDirectory(id);
            }
        }

        private void sds_Deleted(Object sender, SqlDataSourceStatusEventArgs e)
        {
            var id = (Int32)e.Command.Parameters["@pk_TestimonialID"].Value;

            DeleteCustomerTestimonialImageDirectory(id);
        }

        private void DeleteCustomerTestimonialImageDirectory(Int32 customerTestimonialId)
        {
            try
            {
                var directory = Server.MapPath(GetCustomerTestimonialImageRelativeDirectory(customerTestimonialId));

                if (Directory.Exists(directory))
                {
                    Directory.Delete(directory, true);
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }
        }

        private String GetCustomerTestimonialImageRelativeDirectory(Int32 customerTestimonialId)
        {
            return "~/customer/testimonials/" + customerTestimonialId.ToString();
        }

        #endregion
    }
}
