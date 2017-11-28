using System;
using System.Net;
using System.Web.UI;

namespace FlyerMe
{
    public partial class Error : Page
    {
        public String Description { get; set; }

        protected void Page_Load(Object sender, EventArgs args)
        {
            var ex = Server.GetLastError();

            Description = Response.StatusDescription;

            if (ex != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.InternalServerError;
                Description = "Exception: " + ex.Message.Replace(Environment.NewLine, "<br />");

                String stacktrace = null;
                
                if (ex.StackTrace.HasText())
                {
                    stacktrace = "Stacktrace:<br /><br />" + ex.StackTrace.Replace("\n", "<br />");
                }

                if (ex.InnerException != null)
                {
                    Description += "<br /><br /> Inner exception: " + ex.InnerException.Message.Replace(Environment.NewLine, "<br />");

                    if (ex.InnerException.StackTrace.HasText())
                    {
                        stacktrace += "<br /><br />Inner stacktrace:<br /><br />" + ex.InnerException.StackTrace.Replace("\n", "<br />");
                    }
                }

                if (Helper.IsTest() && (!String.IsNullOrEmpty(stacktrace)))
                {
                    divStacktrace.Visible = true;
                    ltlStackTrace.Text = stacktrace;
                }
            }
        }
    }
}