using FlyerMe.Controls;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Contacts : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/contacts";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Contact us | Here for Real Estate Agents 24/7 365 Days a Year | {0}", clsUtility.ProjectName)
                                 .SetTitle("Contact {0} and Our Customer Service", clsUtility.ProjectName)
                                 .SetKeywords("contact {0}, {0} customer service, real estate marketing help, real estate email flyer help, real estate email marketing customer service, real estate flyer service, real estate email flyer help, flyer real estate, real estate advertising, realtor marketing", clsUtility.SiteBrandName.ToLower())
                                 .SetDescription("Contact team {0} for any assistance regarding real estate marketing, real estate email flyers, listing syndication, email flyer marketing tips any time. Available to you 24/7 and 365 days of the year, you can design an effective, beautiful flyer in minutes. ", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlStates.Attributes.Add("data-clientname", "State");
                inputEmail.Attributes["type"] = "email";

                if (!String.IsNullOrEmpty(Request["successmessage"]))
                {
                    divSummary.Visible = true;
                    divSummary.Attributes["class"] = "message saved";
                    ltlMessage.Text = Request["successmessage"];
                }
            }
        }

        protected void ddlStates_DataBound(Object sender, EventArgs e)
        {
            ddlStates.Items.Insert(0, new ListItem("Select State...", String.Empty));
        }

        protected void btnSubmit_Click(Object sender, System.EventArgs e)
        {
            String message = null;

            if (String.IsNullOrEmpty(inputName.Value))
            {
                message = "Name is required.";
            }
            else if (String.IsNullOrEmpty(ddlStates.SelectedValue))
            {
                message = "Select your state.";
            }
            else if (String.IsNullOrEmpty(inputEmail.Value))
            {
                message = "Email is required.";
            }
            else if (String.IsNullOrEmpty(textareaMessage.Value))
            {
                message = "Enter your question or comment.";
            }
            else if (textareaMessage.Value.Length > 3000)
            {
                message = "Please use no more than 3000 symbols.";
            }

            if (String.IsNullOrEmpty(message))
            {
                var helper = new Helper();
                var contactUsEmail = ConfigurationManager.AppSettings["ContactUsEmail"];
                var msgSubject = "Client Assistance Request or Feedback";

                try
                {
                    var sb = new StringBuilder(File.ReadAllText(Server.MapPath("~/flyer/markup/Mail-ContactUs.txt")));

                    sb.Replace("~@Name@~", inputName.Value);
                    sb.Replace("~@Company@~", inputCompany.Value);
                    sb.Replace("~@State@~", ddlStates.SelectedValue);
                    sb.Replace("~@Email@~", inputEmail.Value);
                    sb.Replace("~@Telephone@~", inputPhone.Value);
                    sb.Replace("~@FlyerID@~", inputFlyerId.Value);
                    sb.Replace("~@Message@~", textareaMessage.Value.Replace("\n", "<br />"));

                    Helper.SendEmail(null, contactUsEmail, contactUsEmail, msgSubject, sb.ToString(), inputEmail.Value); 
                }
                catch (Exception ex)
                {
                    message = "Message delivery failed. Exception: " + ex.Message;
                }
            }

            if (String.IsNullOrEmpty(message))
            {
                message = "Your message has been sent successfully.";
                Response.Redirect("~/contacts.aspx?successmessage=" + message, true);
            }
            else
            {
                divSummary.Visible = true;
                divSummary.Attributes["class"] = "message error";
                ltlMessage.Text = message;
            }
        }
    }
}