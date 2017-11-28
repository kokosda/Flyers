using System;
using System.Collections.Specialized;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls
{
    public abstract class AdminControlBase : UserControl
    {
        public virtual HtmlContainerControl DivMessage
        {
            get
            {
                return FindControl("divMessage") as HtmlContainerControl;
            }
        }

        public virtual Literal LtlMessage 
        { 
            get
            {
                return FindControl("ltlMessage") as Literal;
            }
        }

        public String MessageText { get; set; }

        public MessageClassesEnum MessageClass { get; set; }

        public virtual void Page_Load(Object sender, EventArgs e)
        {
            SetMessage();
        }

        public void RedirectToAbsolutPathToShowMessage()
        {
            RedirectToShowMessage(MessageText, MessageClass, true);
        }

        public void RedirectToShowMessage()
        {
            RedirectToShowMessage(MessageText, MessageClass, false);
        }

        public void RedirectToShowMessage(String message, MessageClassesEnum messageClass, Boolean useAbsoultePath = false)
        {
            var canRedirect = (!IsPostBack) && (Request.QueryString["message"].HasNoText()) || IsPostBack;
            
            if (canRedirect)
            {
                var url = Request.Url.ToString();
                var innerMessage = Helper.GetUrlEncodedString(message);
                var innerMessageClass = messageClass.ToString().ToLower();
                var nvc = useAbsoultePath ? new NameValueCollection() : new NameValueCollection(Request.QueryString);

                nvc["message"] = innerMessage;
                nvc["messageclass"] = innerMessageClass;

                if (useAbsoultePath)
                {
                    url = Request.Url.AbsolutePath + "?" + nvc.NameValueToQueryString(false);
                }
                else if (Request.Url.Query.HasText())
                {
                    url = Request.Url.AbsolutePath + "?" + nvc.NameValueToQueryString(false);
                }
                else
                {
                    url += "?" + nvc.NameValueToQueryString(false);
                }

                try
                {
                    Response.Redirect(url, true);
                }
                catch (ThreadAbortException)
                {
                }
            }
        }

        public void ShowMessage()
        {
            DivMessage.Visible = true;
            DivMessage.Attributes["class"] = "message " + MessageClass.ToString().ToLower();
            LtlMessage.Text = MessageText;
        }

        public void HideMessage()
        {
            DivMessage.Visible = false;
        }

        #region private

        private void SetMessage()
        {
            if (!IsPostBack)
            {
                if (Request["message"].HasText())
                {
                    DivMessage.Visible = true;
                    LtlMessage.Text = Request["message"];

                    if (Request["messageclass"].HasText())
                    {
                        DivMessage.Attributes["class"] += " " + Request["messageclass"];
                    }
                    else
                    {
                        DivMessage.Attributes["class"] += " " + MessageClassesEnum.System.ToString().ToLower();
                    }
                }
            }
        }

        #endregion
    }
}