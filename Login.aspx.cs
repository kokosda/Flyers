using FlyerMe.Controls;
using System;
using System.Net;
using System.Threading;
using System.Web.Security;
using System.Web.UI;

namespace FlyerMe
{
    public partial class LogIn : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/login";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Log In | {0}", clsUtility.ProjectName);
            }
        }

        protected String RootURL;

        protected String UserName
        {
            get
            {
                return Request.Form["username"];
            }
        }

        protected Boolean RememberIsChecked
        {
            get
            {
                var result = true;

                if (!String.IsNullOrEmpty(Request["remember"]))
                {
                    var checkboxValue = Request.ParseCheckboxValue("remember");

                    if (checkboxValue.HasValue)
                    {
                        result = checkboxValue.Value;
                    }
                }
                else
                {
                    result = false;
                }

                return result;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Redirect("~/createflyer.aspx", true);
            }

            if (Request.IsGet())
            {
                Request.RedirectToHttpsIfRequired(Response);

                if (Request["errormessage"].HasText())
                {
                    divSummaryError.Visible = true;
                    ltlMessage.Text = Request["errormessage"];
                }
                else if (SocialHelper.IsLinkedInError())
                {
                    if (String.Compare(Request["error"], "access_denied", true) == 0)
                    {
                        Response.Redirect("~/signup.aspx", true);
                    }

                    divSummaryError.Visible = true;
                    ltlMessage.Text = String.Format("LinkedIn: {0} (code {1})", Request["error_description"], Request["error"]);
                }
            }

            RootURL = clsUtility.GetRootHost;

            if (CanAuthenticate())
            {
                Authenticate(Request.Form["username"], Request.Form["password"]);
            }
        }

        private void Authenticate(String username, String password)
        {
            if (IsSocialAuthenticationProvider())
            {
                AuthenticateThroughSocialProvider();
            }
            else if (Membership.ValidateUser(username, password))
            {
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(username, CreatePersistentCookie());
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, CreatePersistentCookie());
                    Response.Redirect("~/createflyer.aspx");
                }
            }
            else
            {
                var message = "Your login attempt was not successful. Please try again.";
                var url = String.Format("~/login.aspx?errormessage={0}", message);

                if (CreatePersistentCookie())
                {
                    url += "&remember=on";
                }

                Response.Redirect(url, true);
            }
        }

        private void AuthenticateThroughSocialProvider()
        {
            try
            {
                var socialAuthenticationModel = SocialHelper.BindAuthenticationModel();

                if (SocialHelper.CanAuthenticateAsync())
                {
                    var user = Membership.GetUser(socialAuthenticationModel.UserName);

                    if (user != null)
                    {
                        var redirectionUrl = RootURL + "createflyer.aspx";

                        if (Request["returnurl"].HasText())
                        {
                            redirectionUrl = new Uri(RootURL.ToUri(), Request["returnurl"]).ToString();
                        }

                        HandleSocialAuthenticationResponse(socialAuthenticationModel, true, false, redirectionUrl, null);
                    }
                    else
                    {
                        HandleSocialAuthenticationResponse(socialAuthenticationModel, false, true, null, null);
                    }
                }
                else
                {
                    HandleSocialAuthenticationResponse(socialAuthenticationModel, false, true, null, null);
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                var message = "Unable to verify your social account. Please try relogin to social network or continue registration manually. Error: " + ex.Message;

                HandleSocialAuthenticationResponse(null, false, false, null, message);
            }
        }

        private Boolean CreatePersistentCookie()
        {
            return Request.ParseCheckboxValue("remember") ?? false;
        }

        private Boolean IsSocialAuthenticationProvider()
        {
            var result = Request["authenticationProvider"].HasText();

            if (!result)
            {
                result = SocialHelper.IsLinkedInCodeRedirect();

                if (result)
                {
                    var state = SocialHelper.GetLinkedInAuthenticationState();

                    if (state.HasNoText())
                    {
                        Response.Redirect("~/login.aspx", true);
                    }
                    else if (String.Compare(state, Request["state"], false) != 0)
                    {
                        Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Cross Site Request Forgery attempt detected.");
                    }
                }
            }

            return result;
        }

        private Boolean CanAuthenticate()
        {
            return Request.IsPost() || SocialHelper.IsLinkedInCodeRedirect();
        }

        private void HandleSocialAuthenticationResponse(SocialAuthenticationModel authenticationModel, Boolean isAuthenticated, Boolean canContinueRegistration, String redirectionUrl, String message)
        {
            if (SocialHelper.IsLinkedInCodeRedirect())
            {
                HandleLinkedInAuthenticationResponse(authenticationModel, isAuthenticated, canContinueRegistration, redirectionUrl, message);
            }
            else
            {
                if (isAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(authenticationModel.UserName, CreatePersistentCookie());
                }

                var result = new
                                {
                                    Result = isAuthenticated,
                                    CanContinueRegistration = canContinueRegistration,
                                    RedirectionUrl = redirectionUrl,
                                    Message = message
                                };

                Helper.RespondWithJsonObject(result, Response);
            }
        }

        private void HandleLinkedInAuthenticationResponse(SocialAuthenticationModel authenticationModel, Boolean isAuthenticated, Boolean canContinueRegistration, String redirectionUrl, String message)
        {
            if (isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(authenticationModel.UserName, CreatePersistentCookie());

                if (redirectionUrl.HasText())
                {
                    Response.Redirect(redirectionUrl, true);
                }
                else
                {
                    Response.Redirect("~/createflyer.aspx", true);
                }
            }
            else
            {
                if (canContinueRegistration)
                {
                    var url = String.Format("~/signup.aspx?mode=mini&authenticationprovider={0}&socialprofile=keep", authenticationModel.AuthenticationProvider.ToString().ToLower());

                    if (message.HasText())
                    {
                        url += "&errormessage=" + message;
                    }

                    Response.Redirect(url, true);
                }
                else
                {
                    var url = String.Format("~/login.aspx");

                    if (message.HasText())
                    {
                        url += "?errormessage=" + message;
                    }

                    Response.Redirect(url, true);
                }
            }
        }
    }
}
