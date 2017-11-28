using FlyerMe.Controls;
using System;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Subscribe : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/subscribe";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Subscribe | Start Receiving the Best Email Flyers | {0}", clsUtility.ProjectName)
                                 .SetKeywords("{0}, real estate marketing, find real estate email flyers, best real estate email  marketing, real estate flyers, real estate email flyers, flyer real estate, real estate advertising, realtor marketing", clsUtility.SiteBrandName.ToLower())
                                 .SetDescription("Subscribe to {0} and receive beautiful real estate email flyers from agents with properties for your buyers. Get started by selecting your market area.", clsUtility.ProjectName);
            }
        }

        protected string RootURL;
        protected void Page_Load(Object sender, EventArgs e)
        {
            Request.RedirectToHttpIfRequired(Response);
            RootURL = clsUtility.GetRootHost;

            if (Request.IsGet())
            {
                inputEmail.Attributes["type"] = "email";
                ddlStates.Attributes.Add("data-clientname", "State");

                if (Request["email"].HasText())
                {
                    inputEmail.Value = Request["email"];
                }

                if (Request["success"].HasText())
                {
                    divContentSubscribe.Visible = false;
                    divSuccess.Visible = true;
                }
            }
        }

        protected void btnSubscribe_Click(Object sender, EventArgs e)
        {
            if (inputEmail.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Email is required.");
            }
            else if (inputFirstName.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "First Name is required.");
            }
            else if (inputLastName.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Last Name is required.");
            }
            else if (ddlStates.SelectedValue.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Select your state.");
            }

            var objData = new clsData();
            var ht = new Hashtable();

            ht.Add("email", inputEmail.Value.Trim());
            ht.Add("first_name", inputFirstName.Value);

            if (inputMiddleName.Value.HasText())
            {
                ht.Add("middle_name", inputMiddleName.Value);
            }

            ht.Add("last_name", inputLastName.Value);
            ht.Add("state", ddlStates.SelectedValue);

            if (ddlCounty.SelectedValue.HasText())
            {
                ht.Add("county", ddlCounty.SelectedValue.ToString());
            }
            if (ddlAssociationName.SelectedValue.HasText())
            {
                ht.Add("association_name", ddlAssociationName.SelectedValue);
            }
            if (ddlMsaName.SelectedValue.HasText())
            {
                ht.Add("msa_name", ddlMsaName.SelectedValue);
            }

            var dt = objData.GetDataTable("usp_InsertSubscriptionFrontEnd", ht);

            if (Convert.ToInt32(dt.Rows[0]["subscriber_id"].ToString()) > 0)
            {
                var verificationURL = Helper.GetEncodedUrlParameter(RootURL + "verify.aspx?vcode=" + GetConfirmationCode(Convert.ToInt32(dt.Rows[0]["subscriber_id"].ToString())));
                var markup = Helper.GetPageMarkup("~/emailtemplates/verifysubscriptionemail.aspx?verificationlink=" + verificationURL);

                Helper.SendEmail(inputEmail.Value.Trim(), clsUtility.SiteBrandName + " - Verify Email", markup);
                
                var url = String.Format("~/subscribe.aspx?email={0}&success=true", inputEmail.Value);

                Response.Redirect(url, true);
            }
            else if (dt.Rows[0]["subscriber_id"].ToString().Trim() == "-1")
            {
                Helper.SetErrorResponse(System.Net.HttpStatusCode.Conflict, "Specified email has already been subscribed.");
            }
            else
            {
                Helper.SetErrorResponse(HttpStatusCode.InternalServerError, "Subscription failed. Unknown error occured. Please try again or contact us.");
            }
        }

        protected void ddlStates_SelectedIndexChanged(Object sender, EventArgs e)
        {
            ddlCounty.Items.Clear();
            ddlCounty.DataBind();
            ddlCounty.Items.Insert(0, new ListItem("Select County...", String.Empty));
            ddlAssociationName.Items.Clear();
            ddlAssociationName.DataBind();
            ddlAssociationName.Items.Insert(0, new ListItem("Select Association...", String.Empty));
            ddlMsaName.Items.Clear();
            ddlMsaName.DataBind();
            ddlMsaName.Items.Insert(0, new ListItem("Select MSA...", String.Empty));
        }

        protected void ddlStates_DataBound(Object sender, EventArgs e)
        {
            ddlStates.Items.Insert(0, new ListItem("Select State...", String.Empty));
        }

        protected string GetConfirmationCode(int id)
        {
            //Format: 7ny1kk6th9999 =  7ny + 1kk + 6th + psubscriber_id

            Random RandString = new Random();

            string[] str1 = { "asa", "wed", "7ny", "gr4", "d8s" };
            string[] str2 = { "q1w", "44e", "86d", "1kk", "iu2" };
            string[] str3 = { "2we", "8tw", "5ds", "6th", "op4" };

            string varificationcode = str1[RandString.Next(0, str1.Length - 1)] + str2[RandString.Next(0, str2.Length - 1)] + str3[RandString.Next(0, str3.Length - 1)] + id.ToString();
            return varificationcode;
        }
    }
}
