using FlyerMe.Admin.Models;
using System;
using System.Collections;
using System.Net;
using System.Threading;
using System.Web.Security;
using System.Web.UI;

namespace FlyerMe.Admin
{
    public partial class LogIn : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            if (AdminUserModel.GetAdminUserModelFromSession() != null)
            {
                Response.Redirect("~/admin/default.aspx", true);
            }

            if (Request.IsGet())
            {
                Request.RedirectToHttpsIfRequired(Response);
            }
        }

        protected void btnEnter_Click(Object sender, EventArgs e)
        {
            String message = null;

            inputLogin.Attributes.Remove("class");
            inputPassword.Attributes.Remove("class");

            if (inputLogin.Value.HasNoText())
            {
                message = "Login is required.";
                inputLogin.Attributes.Add("class", "error");
            }
            else if (inputPassword.Value.HasNoText())
            {
                message = "Password is required";
                inputPassword.Attributes.Add("class", "error");
            }

            if (message.HasNoText())
            {
                var objData = new clsData();
                var ht = new Hashtable();

                ht.Add("UserEmailID", inputLogin.Value);
                ht.Add("Password", inputPassword.Value);

                var dt = objData.GetDataTable("usp_getAdminUser", ht);

                if (dt.Rows.Count > 0)
                {
                    AdminUserModel.SetAdminUserModelToSession(dt);

                    var url = "~/admin/default.aspx";

                    if (Request["returnurl"].HasText())
                    {
                        url = Server.UrlDecode(Request["returnurl"]).Replace(Environment.NewLine, " ");
                    }

                    Response.Redirect(url, true);
                }
                else
                {
                    message = "Your login attempt was not successful. Please try again.";
                }
            }
            
            if (message.HasText())
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = message;
            }
        }

        private Boolean CreatePersistentCookie()
        {
            return false;
        }
    }
}
