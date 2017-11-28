using FlyerMe.Admin.Controls;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Agents
{
    public partial class AddAgentEmail : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetLinks();
                SetInputs();
            }
        }

        protected void btnAddAgentEmail_Command(Object sender, CommandEventArgs e)
        {
            if (inputEmail.Value.HasNoText())
            {
                message.MessageText = "Email is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (inputFirstName.Value.HasNoText())
            {
                message.MessageText = "First Name is required.";
                message.MessageClass = MessageClassesEnum.System;
            }
            else if (ddlState.SelectedValue.HasNoText() || ddlState.Items.FindByValue(ddlState.SelectedValue) == null)
            {
                message.MessageText = "Select State.";
                message.MessageClass = MessageClassesEnum.System;
            }

            if (message.MessageText.HasNoText())
            {
                try
                {
                    var objData = new clsData();
                    var ht = new Hashtable();

                    ht.Add("email", inputEmail.Value.Trim());
                    ht.Add("first_name", inputFirstName.Value.Trim());

                    if (inputMiddleName.Value.HasText())
                    {
                        ht.Add("middle_name", inputMiddleName.Value.Trim());
                    }
                    if (inputLastName.Value.HasText())
                    {
                        ht.Add("last_name", inputLastName.Value.Trim());
                    }

                    if (ddlState.SelectedValue.HasText() && ddlState.Items.FindByValue(ddlState.SelectedValue) != null)
                    {
                        ht.Add("state", ddlState.SelectedValue);
                    }
                    if (ddlCounty.SelectedValue.HasText() && ddlCounty.Items.FindByValue(ddlCounty.SelectedValue) != null)
                    {
                        ht.Add("county", ddlCounty.SelectedValue);
                    }
                    if (ddlAssociation.SelectedValue.HasText() && ddlAssociation.Items.FindByValue(ddlAssociation.SelectedValue) != null)
                    {
                        ht.Add("association_name", ddlAssociation.SelectedValue);
                    }
                    if (ddlMsa.SelectedValue.HasText() && ddlMsa.Items.FindByValue(ddlMsa.SelectedValue) != null)
                    {
                        ht.Add("msa_name", ddlMsa.SelectedValue);
                    }

                    ht.Add("rcode", inputRcode.Value.Trim());
                    ht.Add("vresult", inputVresult.Value.Trim());
                    ht.Add("unsubscribed", inputUnsubscribed.Checked);
                    ht.Add("skip", inputSkip.Checked);

                    new clsData().ExecuteSql("usp_InsertSubscription", ht);

                    message.MessageText = "Agent email has been added successfully.";
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

        protected void ddlState_SelectedIndexChanged(Object sender, EventArgs e)
        {
            SetInputs(excludeDdlState: true);
        }

        #region private

        private void SetLinks()
        {
            hlBackToAgents.NavigateUrl = ResolveUrl("~/admin/agents.aspx");

            if (Request.UrlReferrer != null && Request.UrlReferrer.AbsolutePath.EndsWith("admin/agents.aspx", StringComparison.OrdinalIgnoreCase))
            {
                hlBackToAgents.NavigateUrl = Request.UrlReferrer.ToString();
            }
        }

        private void SetInputs(Boolean excludeDdlState = false)
        {
            if (!excludeDdlState)
            {
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("[Select State]", String.Empty));
            }

            ddlState.Attributes.Add("required", null);

            if (ddlState.SelectedValue.HasText())
            {
                ddlCounty.DataBind();
                ddlAssociation.DataBind();
                ddlMsa.DataBind();
            }
            else
            {
                ddlCounty.Items.Clear();
                ddlAssociation.Items.Clear();
                ddlMsa.Items.Clear();
            }

            ddlCounty.Items.Insert(0, new ListItem("[Select County]", String.Empty));
            ddlAssociation.Items.Insert(0, new ListItem("[Select Association]", String.Empty));
            ddlMsa.Items.Insert(0, new ListItem("[Select MSA]", String.Empty));
        }

        #endregion
    }   
}
