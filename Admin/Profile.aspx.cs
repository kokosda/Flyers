using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Models;
using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin
{
    public partial class ProfileAdmin : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var objData = new clsData();

                objData.strSql = String.Format("select * from fly_tblUser where UserEmailID = '{0}'", AdminUserModel.GetAdminUserModelFromSession().Email);

                var dt = objData.GetDataTable();
                var model = AdminUserModel.ToAdminUserModel(dt);

                inputLogin.Value = model.Email;
                hfUserId.Value = dt.Rows[0]["pk_UserID"].ToString();
            }
        }

        protected void btnSaveProfile_Command(Object sender, CommandEventArgs e)
        {
            Int32 @int32;

            if (hfUserId.Value.HasNoText())
            {
                message.MessageText = "User ID is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (!Int32.TryParse(hfUserId.Value, out @int32))
            {
                message.MessageText = "User ID should be a number.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputLogin.Value.HasNoText())
            {
                message.MessageText = "Login is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputNewPassword.Value.HasNoText())
            {
                message.MessageText = "New Password is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputConfirmPassword.Value.HasNoText())
            {
                message.MessageText = "Confirm Password is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (String.Compare(inputNewPassword.Value, inputConfirmPassword.Value, false) != 0)
            {
                message.MessageText = "New and Confirm Passwords must match.";
                message.MessageClass = MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    var objData = new clsData();

                    objData.strSql = String.Format("update fly_tblUser set UserEmailID='{0}', Password='{1}' where pk_UserID={2}", inputLogin.Value.Trim(), inputNewPassword.Value, hfUserId.Value);
                    objData.ExecuteSql();

                    var model = AdminUserModel.GetAdminUserModelFromSession();

                    model.Email = inputLogin.Value.Trim();

                    message.MessageText = "Admin profile has been saved.";
                    message.MessageClass = MessageClassesEnum.Ok;
                }
                catch (Exception ex)
                {
                    message.MessageText = ex.Message;
                    message.MessageClass = MessageClassesEnum.Error;
                }
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
    }
}
