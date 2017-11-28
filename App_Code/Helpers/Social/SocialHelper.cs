using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FlyerMe
{
    public static class SocialHelper
    {
        public static Boolean CanAuthenticateAsync()
        {
            var result = false;
            var authenticationModel = BindAuthenticationModel();

            if (authenticationModel != null)
            {
                var userModel = BindUserModel();

                if (userModel == null)
                {
                    throw new Exception("Cannot bind user model.");
                }
                else if (String.Compare(userModel.Email, authenticationModel.UserName) != 0)
                {
                    throw new Exception("User email and user name must match.");
                }

                if (authenticationModel.AuthenticationProvider == SocialAuthenticationProvidersEnum.Facebook)
                {
                    result = CanAuthenticateFacebookAsync(authenticationModel);
                }
                else if (authenticationModel.AuthenticationProvider == SocialAuthenticationProvidersEnum.LinkedIn)
                {
                    result = CanAuthenticateLinkedIn(authenticationModel);
                }
                else
                {
                    throw new Exception("Authentication provider " + HttpContext.Current.Request["authenticationprovider"] + " is not supported");
                }
            }

            return result;
        }

        public static SocialAuthenticationModel BindAuthenticationModel()
        {
            SocialAuthenticationModel result = null;

            if (HttpContext.Current != null)
            {
                var request = HttpContext.Current.Request;

                if (IsLinkedInCodeRedirect())
                {
                    result = GetLinkedInAuthenticationModelAsync();
                }
                else if (ShouldUseSocialProfileFromSession)
                {
                    result = GetSocialProfileFormSession().AuthenticationModel;
                }
                else
                {
                    result = new SocialAuthenticationModel(Helper.DecodeUrlParameter(request["userid"]), Helper.DecodeUrlParameter(request["username"]), Helper.DecodeUrlParameter(request["token"]), Helper.DecodeUrlParameter(request["authenticationprovider"]));
                }
            }

            return result;
        }

        public static void SetSocialProfileToSession(SocialProfile profile)
        {
            HttpContext.Current.Session.Add("SocialProfile", profile);
        }

        public static void RemoveSocialProfileFromSessionIfNeeded()
        {
            if (!ShouldUseSocialProfileFromSession)
            {
                HttpContext.Current.Session.Remove("SocialProfile");
            }
        }

        public static Boolean ShouldUseSocialProfileFromSession
        {
            get
            {
                var socialProfile = HttpContext.Current.Request["socialprofile"];

                return socialProfile.HasText() && String.Compare(socialProfile, "keep", true) == 0;
            }
        }

        public static SocialProfile GetSocialProfileFormSession()
        {
            return HttpContext.Current.Session["SocialProfile"] as SocialProfile;
        }

        public static SocialUserModel BindUserModel()
        {
            SocialUserModel result = null;

            if (IsLinkedInCodeRedirect())
            {
                result = GetSocialProfileFormSession().UserModel;
            }
            else if (ShouldUseSocialProfileFromSession)
            {
                result = GetSocialProfileFormSession().UserModel;
            }
            else if (HttpContext.Current != null)
            {
                var request = HttpContext.Current.Request;

                result = new SocialUserModel(Helper.DecodeUrlParameter(request["firstname"]), Helper.DecodeUrlParameter(request["middlename"]), Helper.DecodeUrlParameter(request["lastname"]),  Helper.DecodeUrlParameter(request["email"]), Helper.DecodeUrlParameter(request["avatarurl"]));
            }

            return result;
        }

        public static SocialProfile CreateSocialProfileIfNeeded()
        {
            SocialProfile result;

            if (ShouldUseSocialProfileFromSession)
            {
                result = GetSocialProfileFormSession();
            }
            else
            {
                result = new SocialProfile(BindAuthenticationModel(), BindUserModel());
            }

            return result;
        }

        public static void SetLinkedInAuthenticationState()
        {
            var bytes = new Byte[16];
            new Random().NextBytes(bytes);

            HttpContext.Current.Session.Add("LinkedInState", Convert.ToBase64String(bytes));
        }

        public static String GetLinkedInAuthenticationState()
        {
            return HttpContext.Current.Session["LinkedInState"] as String;
        }

        public static Boolean IsLinkedInCodeRedirect()
        {
            return HttpContext.Current.Request.QueryString.Count == 2 && 
                   HttpContext.Current.Request.QueryString["code"].HasText() && 
                   HttpContext.Current.Request.QueryString["state"].HasText();
        }

        public static Boolean IsLinkedInError()
        {
            return HttpContext.Current.Request.QueryString.Count <= 3 &&
                   HttpContext.Current.Request.QueryString["error"].HasText() &&
                   HttpContext.Current.Request.QueryString["error_description"].HasText();
        }

        #region private

        private static Boolean CanAuthenticateFacebookAsync(SocialAuthenticationModel model)
        {
            var result = false;
            var url = String.Format("{0}{1}?fields=email&access_token={2}", ConfigurationManager.AppSettings["FacebookApiUrl"], model.UserId, model.Token);
            var requestResult = Helper.RequestUrlAsync(url);
            var userObject = Helper.DeserializeJsonToObject(requestResult);

            if (userObject != null)
            {
                var userFields = userObject as Dictionary<String, Object>;

                if (userFields.ContainsKey("error"))
                {
                    throw new Exception("Facebook api error: " + (userFields["error"] as Dictionary<String, Object>)["message"]);
                }
                else
                {
                    if (userFields.ContainsKey("email"))
                    {
                        var email = userFields["email"] as String;

                        if (email.HasText())
                        {
                            result = String.Compare(email, model.UserName, true) == 0;
                        }
                        else
                        {
                            throw new Exception("User email address is empty or access to the email address is not allowed by user.");
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private static Boolean CanAuthenticateLinkedIn(SocialAuthenticationModel model)
        {
            Boolean result = false;

            if (IsLinkedInCodeRedirect())
            {
                result = GetSocialProfileFormSession() != null;
            }
            else if (ShouldUseSocialProfileFromSession)
            {
                var socialProfile = GetSocialProfileFormSession();

                result = String.Compare(socialProfile.UserModel.Email, socialProfile.AuthenticationModel.UserName) == 0;
            }

            if (!result)
            {
                var cookieKey = model.Token + "_" + ConfigurationManager.AppSettings["LinkedInAppId"];
                var cookie = HttpContext.Current.Request.Cookies[cookieKey];

                if (cookie != null)
                {
                    var authenticationInfo = Helper.DeserializeJsonToObject(Helper.DecodeUrlParameter(cookie.Value)) as Dictionary<String, Object>;

                    if (authenticationInfo != null && authenticationInfo.ContainsKey("access_token"))
                    {
                        if (!authenticationInfo.ContainsKey("signature_version"))
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_version is not provided.");
                        }

                        if (String.Compare(authenticationInfo["signature_version"].ToString(), "1", true) != 0)
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_version " + authenticationInfo["signature_version"] + " is not supported.");
                        }

                        if (!authenticationInfo.ContainsKey("signature_order"))
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_order is not provided.");
                        }

                        var signatureOrders = authenticationInfo["signature_order"] as Object[];

                        if (signatureOrders == null || signatureOrders.Length == 0)
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_order values is empty.");
                        }

                        if (!authenticationInfo.ContainsKey("signature"))
                        {
                            throw new Exception("LinkedIn authentication cookie: signature is not provided.");
                        }

                        var concatenatedFields = String.Empty;

                        foreach (var so in signatureOrders)
                        {
                            var signatureOrderValue = so as String;

                            if (signatureOrderValue.HasNoText())
                            {
                                throw new Exception("LinkedIn authentication cookie: signature_order contains empty value.");
                            }
                            else if (!authenticationInfo.ContainsKey(signatureOrderValue))
                            {
                                throw new Exception("LinkedIn authentication cookie: signature_order contains value " + signatureOrderValue + " that is not the part of the cookie.");
                            }
                            else
                            {
                                concatenatedFields += authenticationInfo[signatureOrderValue];
                            }
                        }

                        if (!authenticationInfo.ContainsKey("signature_method"))
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_method is not provided.");
                        }

                        if (String.Compare(authenticationInfo["signature_method"] as String, "HMAC-SHA1", true) != 0)
                        {
                            throw new Exception("LinkedIn authentication cookie: signature_method " + authenticationInfo["signature_method"] + " is not supported.");
                        }

                        var keyBytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["LinkedInAppSecret"]);
                        String signature;

                        using (var hmacsha1 = new HMACSHA1(keyBytes))
                        {
                            hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(concatenatedFields));
                            signature = Convert.ToBase64String(hmacsha1.Hash);
                        }

                        if (String.Compare(signature, authenticationInfo["signature"] as String, false) != 0)
                        {
                            throw new Exception("LinkedIn authentication cookie: invalid signature.");
                        }

                        result = String.Compare(model.UserId, authenticationInfo["member_id"] as String, false) == 0;
                    }
                    else
                    {
                        throw new Exception("LinkedIn authentication cookie does not contain access token.");
                    }
                }
                else
                {
                    throw new Exception("LinkedIn authentication cookie is not present.");
                }
            }

            return result;
        }

        private static SocialAuthenticationModel GetLinkedInAuthenticationModelAsync()
        {
            var result = GetSocialProfileFormSession() != null ? GetSocialProfileFormSession().AuthenticationModel : null;

            if (result == null)
            {
                var url = ConfigurationManager.AppSettings["LinkedInAccessTokenUrl"];
                var headers = new Dictionary<String, String> { { "Content-Type", "application/x-www-form-urlencoded" } };
                var nvc = new NameValueCollection();

                nvc.Add("grant_type", "authorization_code");
                nvc.Add("code", HttpContext.Current.Request.QueryString["code"]);
                nvc.Add("redirect_uri", Helper.GetUrlEncodedString((clsUtility.GetRootHost + "login.aspx").ToUri().UrlToHttps().ToString().ToLower()));
                nvc.Add("client_id", ConfigurationManager.AppSettings["LinkedInAppId"]);
                nvc.Add("client_secret", ConfigurationManager.AppSettings["LinkedInAppSecret"]);

                var data = nvc.NameValueToQueryString().Substring(1);
                var response = Helper.DeserializeJsonToObject(Helper.RequestUrlAsync(url, "POST", data, headers)) as Dictionary<String, Object>;

                if (response.ContainsKey("error") || response.ContainsKey("error_description"))
                {
                    throw new Exception(String.Format("LinkedIn authentication error: \"{0}\"", response["error_description"]));
                }

                var accessToken = response["access_token"] as String;

                url = ConfigurationManager.AppSettings["LinkedInApiUrl"] + "people/~:(id,first-name,last-name,email-address,picture-url)";
                headers = new Dictionary<String, String> { 
                                                            { "Authorization", "Bearer " + accessToken },
                                                            { "x-li-format", "json" }
                                                         };
                response = Helper.DeserializeJsonToObject(Helper.RequestUrlAsync(url, "GET", null, headers)) as Dictionary<String, Object>;

                if (response.ContainsKey("error") || response.ContainsKey("error_description"))
                {
                    throw new Exception(String.Format("LinkedIn api error: \"{0}\"", response["error_description"]));
                }
                else if (response.ContainsKey("emailAddress"))
                {
                    if ((response["emailAddress"] as String).HasText())
                    {
                        result = new SocialAuthenticationModel(response["id"] as String, response["emailAddress"] as String, accessToken, SocialAuthenticationProvidersEnum.LinkedIn.ToString());
                        url = ConfigurationManager.AppSettings["LinkedInApiUrl"] + "people/" + response["id"] + "/picture-urls::(original)";

                        var avatarUrl = response["pictureUrl"] as String;

                        try
                        {
                            var pictureUrlsResponse = Helper.DeserializeJsonToObject(Helper.RequestUrlAsync(url, "GET", null, headers)) as Dictionary<String, Object>;

                            if (pictureUrlsResponse.ContainsKey("values"))
                            {
                                var pus = pictureUrlsResponse["values"] as Object[];

                                if (pus != null && pus.Length > 0)
                                {
                                    avatarUrl = pus[0] as String;
                                }
                            }
                        }
                        catch
                        {
                        }

                        var userModel = new SocialUserModel(response["firstName"] as String, null, response["lastName"] as String, response["emailAddress"] as String, avatarUrl);

                        SetSocialProfileToSession(new SocialProfile(result, userModel));
                    }
                    else
                    {
                        throw new Exception("LinkedIn profile: email address is empty.");
                    }
                }
                else
                {
                    throw new Exception("LinkedIn api: profile does not contain email-address field which is required.");
                }
            }

            return result;
        }

        #endregion
    }
}