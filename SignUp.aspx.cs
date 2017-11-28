using FlyerMe.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class SignUp : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/signup";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Login or Sign Up | {0}", clsUtility.ProjectName)
                                 .SetTitle("Sign Up Free for The Best Effective Email Marketing Flyer Services | {0}", clsUtility.SiteBrandName)
                                 .SetKeywords("Real estate marketing services, find real estate email flyers, best real estate email marketing, real estate flyer services, real estate email flyer software, software for flyer real estate, real estate advertising providers, realtor marketing services")
                                 .SetDescription("Subscribe to {0} and start receiving our email flyers campaigns by selecting your marketing area with your needs and requirement. Send flyers to other agents. | {0}", clsUtility.ProjectName);
            }
        }

        protected String RootURL;

        protected String ReturnUrl
        {
            get
            {
                return Request["ReturnUrl"];
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.Redirect("~/createflyer.aspx", true);
            }

            Request.RedirectToHttpsIfRequired(Response);
            RootURL = clsUtility.GetRootHost;
            divSummaryError.Visible = false;

            if (Request["errormessage"].HasText())
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = Request["errormessage"];
            }

            if (!IsPostBack)
            {
                SetDdlTitles();
                SetDdlBusinessState();
                SetDdlBrokerageState();
                labelNewletter.InnerText = clsUtility.ProjectName + " Newsletter";
            }

            SetInputs();
            SetMode();
            Form.Attributes["data-marketsurl"] = ResolveUrl("~/signup.aspx/getmarkets");
        }

        protected void ddlTitle_DataBound(Object sender, EventArgs e)
        {
            var li = new ListItem("Select Title...", String.Empty);
            ddlTitle.Items.Insert(0, li);
        }

        protected void ddlBusinessState_DataBound(Object sender, EventArgs e)
        {
            var li = new ListItem("Select State...", String.Empty);
            ddlBusinessState.Items.Insert(0, li);
        }

        protected void ddlBrokerageState_DataBound(Object sender, EventArgs e)
        {
            ListItem li = new ListItem("Select State...", String.Empty);
            ddlBrokerageState.Items.Insert(0, li);
        }

        protected void btnSignUp_Click(Object sender, EventArgs e)
        {
            Save();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static Object GetMarkets(String state)
        {
            var result = new
                            {
                                Associations = new List<Tuple<String, String>>(),
                            };
            String message = null;

            if (state.HasText())
            {
                Action<List<Tuple<String, String>>, DataRowCollection> fillData = (l, drc) =>
                {
                    foreach (DataRow r in drc)
                    {
                        l.Add(Tuple.Create(r["marketid"] as String, r["market"] as String));
                    }
                };

                try
                {
                    DataTable dt;

                    using (var dataObj = new clsData())
                    {
                        dataObj.strSql = String.Format("SELECT DISTINCT [market], [marketid] FROM [fly_association_listsize] where state='{0}' order by market asc", state);
                        dt = dataObj.GetDataTable();
                        fillData(result.Associations, dt.Rows);
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }

            if (message.HasText())
            {
                return new { Message = message };
            }

            return result;
        }

        #region private

        private void SetDdlTitles()
        {
            var objectDataSource = new ObjectDataSource("FlyerMe.TitlesBLL", "GetTitles")
                                    {
                                        OldValuesParameterFormatString = "original_{0}"
                                    };

            formSignUp.Controls.Add(objectDataSource);

            ddlTitle.DataSource = objectDataSource;
            ddlTitle.DataBind();
        }

        private void SetDdlBusinessState()
        {
            var objectDataSource = new ObjectDataSource("FlyerMe.StatesBLL", "GetStates")
                                        {
                                            OldValuesParameterFormatString = "original_{0}"
                                        };

            formSignUp.Controls.Add(objectDataSource);

            ddlBusinessState.DataSource = objectDataSource;
            ddlBusinessState.DataBind();
        }

        private void SetDdlBrokerageState()
        {
            ddlBrokerageState.DataSource = ddlBusinessState.DataSource;
            ddlBrokerageState.DataBind();
        }

        private void SetInputs()
        {
            inputEmail.Attributes["type"] = "email";
            inputEmailConfirm.Attributes["type"] = "email";
            inputSecondaryEmail.Attributes["type"] = "email";
            ddlTitle.Attributes.Add("data-clientname", "Title");
            ddlBusinessState.Attributes.Add("data-clientname", "BusinessState");
            ddlBrokerageState.Attributes.Add("data-clientname", "BrokerageState");

            if (GetCurrentMode() == ModesEnum.Mini)
            {
                SetInputsInModeMini();
            }
        }

        private void SetInputsInModeMini()
        {
            if (IsPostBack)
            {
                return;
            }

            SocialHelper.RemoveSocialProfileFromSessionIfNeeded();

            var socialUserModel = SocialHelper.GetSocialProfileFormSession() != null ? SocialHelper.GetSocialProfileFormSession().UserModel : SocialHelper.BindUserModel();

            if (socialUserModel != null)
            {
                if (socialUserModel.Email.HasText())
                {
                    try
                    {
                        if (SocialHelper.CanAuthenticateAsync())
                        {
                            SocialHelper.SetSocialProfileToSession(SocialHelper.CreateSocialProfileIfNeeded());
                            divEmail.Visible = false;
                            divConfirmEmail.Visible = false;
                            divPassword.Visible = false;
                            divConfirmPassword.Visible = false;
                            divFirstName.Visible = socialUserModel.FirstName.HasNoText();
                            divLastName.Visible = socialUserModel.LastName.HasNoText();

                            if (!(inputFirstName.Visible || inputLastName.Visible))
                            {
                                divMiddleName.Visible = false;
                            }
                        }
                        else
                        {
                            divSummaryError.Visible = true;
                            ltlMessage.Text = "Cannot validate email " + socialUserModel.Email + ". Please complete registration manually. Possible reasons: <br />1) access to email address is not allowed by user<br />2) email from social network profile is not equal to email obtained with given access token.";
                        }
                    }
                    catch (Exception ex)
                    {
                        divSummaryError.Visible = true;
                        ltlMessage.Text = ex.Message;
                    }
                }

                inputFirstName.Value = socialUserModel.FirstName;
                inputMiddleName.Value = socialUserModel.MiddleName;
                inputLastName.Value = socialUserModel.LastName;

                if (socialUserModel.AvatarUrl.HasText())
                {
                    divSocImage.Visible = true;
                    imageAvatar.ImageUrl = socialUserModel.AvatarUrl;
                }
            }
        }

        private void ValidateOnSave()
        {
            if (GetCurrentMode() == ModesEnum.Main || (GetCurrentMode() == ModesEnum.Mini && SocialHelper.CreateSocialProfileIfNeeded() == null))
            {
                if (inputEmail.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Email is required.");
                }
                else if (String.Compare(inputEmail.Value, inputEmailConfirm.Value, true) != 0)
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Emails do not match.");
                }
                else if (String.Compare(inputEmail.Value, inputEmailConfirm.Value, true) != 0)
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Emails do not match.");
                }
                else if (inputPassword.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Password is required.");
                }
                else if (inputConfirmPassword.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Confirm Password is required.");
                }
                else if (String.Compare(inputPassword.Value, inputConfirmPassword.Value, false) != 0)
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Passwords do not match.");
                }
                else if (inputFirstName.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "First Name is required.");
                }
                else if (inputLastName.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Last Name is required.");
                }
            }
            else if (GetCurrentMode() == ModesEnum.Mini && SocialHelper.CreateSocialProfileIfNeeded() != null)
            {
                var socialProfile = SocialHelper.CreateSocialProfileIfNeeded();
                var userModel = socialProfile.UserModel;

                if (userModel.FirstName.HasNoText() && inputFirstName.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "First Name is required.");
                }
                else if (userModel.LastName.HasNoText() && inputLastName.Value.HasNoText())
                {
                    Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Last Name is required.");
                }
            }

            if (ddlTitle.SelectedValue.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Select Title.");
            }
            else if (ddlTitle.Items.FindByValue(ddlTitle.SelectedValue) == null)
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Provide valid Title.");
            }
            else if (inputBusinessAddress1.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Business Address 1 is required.");
            }
            else if (inputBusinessCity.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Business City is required.");
            }
            else if (ddlBusinessState.SelectedValue.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Select your Business State.");
            }
            else if (ddlBusinessState.Items.FindByValue(ddlBusinessState.SelectedValue) == null)
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Provide valid Business State.");
            }
            else if (inputBusinessZipCode.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Business Zip Code is required.");
            }
            else if (inputBusinessPhone.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Business Phone is required.");
            }
            else if (inputBrokerageName.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Brokerage Name is required.");
            }
            else if (inputBrokerageAddress1.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Brokerage Address 1 is required.");
            }
            else if (inputBrokerageCity.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Brokerage City is required.");
            }
            else if (ddlBusinessState.SelectedValue.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Select Brokerage State.");
            }
            else if (ddlBusinessState.Items.FindByValue(ddlBusinessState.SelectedValue) == null)
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Provide valid Brokerage State.");
            }
            else if (inputBrokerageZipCode.Value.HasNoText())
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Zip Code is required.");
            }
            else if (!cbTerm.Checked)
            {
                Helper.SetErrorResponse(HttpStatusCode.BadRequest, "Accept Terms and Conditions.");
            }
        }

        private void Save()
        {
            try
            {
                ValidateOnSave();

                var email = inputEmail.Value.Trim();
                var password = inputPassword.Value;

                if (GetCurrentMode() == ModesEnum.Mini)
                {
                    var socialProfile = SocialHelper.CreateSocialProfileIfNeeded();

                    if (email.HasNoText())
                    {
                        email = socialProfile.UserModel.Email;
                    }

                    if (password.HasNoText())
                    {
                        password = GeneratePassword();
                    }
                }

                var user = Membership.CreateUser(email, password, email);
                var profile = SaveProfile();

                InsertSubscriber(profile);

                if (Membership.ValidateUser(user.UserName, user.GetPassword()))
                {
                    try
                    {
                        SendEmail(user, profile);
                    }
                    catch
                    {
                    }

                    if (Request.QueryString["ReturnUrl"] != null)
                    {
                        FormsAuthentication.RedirectFromLoginPage(user.Email, false);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        Response.Redirect("~/createflyer.aspx");
                    }
                }
            }
            catch (MembershipCreateUserException ex)
            {
                divSummaryError.Visible = true;

                switch (ex.StatusCode)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                        ltlMessage.Text = "You have supplied a duplicate email address.";
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        ltlMessage.Text = "You have supplied a duplicate username.";
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        ltlMessage.Text = "You have not supplied a proper email address.";
                        break;
                    default:
                        ltlMessage.Text = "Error: " + ex.Message;
                        break;
                }
            }
            catch (Exception ex)
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = String.Format("Unhandled error: {0}", ex.Message);
            }
        }

        private ProfileCommon SaveProfile()
        {
            var result = Profile;
            var email = inputEmail.Value.Trim();
            var firstName = inputFirstName.Value.Trim();
            var middleName = inputMiddleName.Value.Trim();
            var lastName = inputLastName.Value.Trim();
            var avatarUrl = String.Empty;

            if (GetCurrentMode() == ModesEnum.Mini)
            {
                var socialProfile = SocialHelper.CreateSocialProfileIfNeeded();

                if (socialProfile != null)
                {
                    var userModel = socialProfile.UserModel;

                    if (email.HasNoText() && userModel.Email.HasText())
                    {
                        email = userModel.Email;
                    }
                    if (firstName.HasNoText() && userModel.FirstName.HasText())
                    {
                        firstName = userModel.FirstName;
                    }
                    if (middleName.HasNoText() && userModel.MiddleName.HasText())
                    {
                        middleName = userModel.MiddleName;
                    }
                    if (lastName.HasNoText() && userModel.LastName.HasText())
                    {
                        lastName = userModel.LastName;
                    }
                    if (userModel.AvatarUrl.HasText())
                    {
                        avatarUrl = userModel.AvatarUrl;
                    }
                }
            }

            if (email.Length > 0)
            {
                result = Profile.GetProfile(email);
            }
            else
            {
                result = Profile.GetProfile(User.Identity.Name);
            }

            result.Association = Request.Form["association"];
            result.Brokerage.BrokerageState = ddlBrokerageState.SelectedValue.Trim();
            result.BusinessAddress.BusinessState = ddlBusinessState.SelectedValue.Trim();
            result.Preferences.Newsletter = ddlNewsletter.SelectedValue.Trim();
            result.Title = ddlTitle.SelectedValue.Trim();
            result.Brokerage.BrokerageAddress1 = inputBrokerageAddress1.Value.Trim();
            result.Brokerage.BrokerageAddress2 = inputBrokerageAddress2.Value.Trim();
            result.Brokerage.BrokerageCity = inputBrokerageCity.Value.Trim();
            result.Brokerage.BrokerageName = inputBrokerageName.Value.Trim();
            result.Brokerage.BrokerageZipcode = inputBrokerageZipCode.Value.Trim();
            result.BusinessAddress.BusinessAddress1 = inputBusinessAddress1.Value.Trim();
            result.BusinessAddress.BusinessAddress2 = inputBusinessAddress2.Value.Trim();
            result.BusinessAddress.BusinessCity = inputBusinessCity.Value.Trim();
            result.BusinessAddress.BusinessZipcode = inputBusinessZipCode.Value.Trim();
            result.Contact.EmailSecondary = inputSecondaryEmail.Value.Trim();
            result.Contact.Fax = inputBusinessFax.Value.Trim();
            result.FirstName = firstName;
            result.LastName = lastName;
            result.MiddleInitial = middleName;
            result.Contact.PhoneBusiness = inputBusinessPhone.Value.Trim();
            result.Contact.PhoneBusinessExt = inputBusinessPhoneExt.Value.Trim();
            result.Contact.PhoneCell = inputCellPhone.Value.Trim();
            result.Contact.PhoneHome = inputHomePhone.Value.Trim();
            result.ReferredBy = ddlReferredSource.SelectedValue + "|" + inputReferredBy.Value.Trim();
            result.TeamName = inputTeamName.Value.Trim();
            result.Website = inputWebsite.Value.Trim();
            result.DRE = inputBre.Value.Trim();
            result.ImageURL = avatarUrl;

            result.Save();

            if (result.ImageURL.HasText())
            {
                SaveAvatar(result.ImageURL, result);
            }

            return result;
        }

        private void InsertSubscriber(ProfileCommon profile)
        {
            var objData = new clsData();
            var dtCityInfo = objData.GetData("select * from tblCityCountyMSAInfo where city='" + profile.BusinessAddress.BusinessAddress1.Replace("'", "") + "' and state='" + profile.BusinessAddress.BusinessState + "'");
            var ht = new Hashtable();

            ht.Add("first_name", profile.FirstName);
            ht.Add("middle_name", profile.MiddleInitial);
            ht.Add("last_name", profile.LastName);

            if (dtCityInfo.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtCityInfo.Rows[0]["CountyName"].ToString()))
                {
                    ht.Add("county", dtCityInfo.Rows[0]["CountyName"].ToString());
                }
                if (!String.IsNullOrEmpty(dtCityInfo.Rows[0]["MSA_Name"].ToString()))
                {
                    ht.Add("msa_name", dtCityInfo.Rows[0]["MSA_Name"].ToString());
                }
            }

            var associationName = String.Empty;

            try
            {
                dynamic markets = GetMarkets(profile.BusinessAddress.BusinessState);
                var associations = markets.Associations as List<Tuple<String, String>>;

                if (associations != null)
                {
                    var association = associations.FirstOrDefault(a => String.Compare(a.Item1, profile.Association, true) == 0);

                    if (association != null)
                    {
                        associationName = association.Item2;
                    }
                }
            }
            catch
            {
            }

            ht.Add("state", profile.BusinessAddress.BusinessState);
            ht.Add("email", inputEmail.Value.Trim());
            ht.Add("association_name", associationName);

            objData.ExecuteSql("usp_InsertSubscriberViaRegistration", ht);
        }

        private void SendEmail(MembershipUser user, ProfileCommon profile)
        {
            var promoCode = GetPromoCode(user);
            var userFullname = String.Format("{0} {1}", profile.FirstName, profile.LastName);

            var nv = new NameValueCollection();

            nv.Add("fullname", Helper.GetUrlEncodedString(userFullname));
            nv.Add("login", Helper.GetUrlEncodedString(user.Email));
            nv.Add("password", Helper.GetUrlEncodedString(user.GetPassword()));
            nv.Add("promocode", Helper.GetUrlEncodedString(promoCode));

            var url = String.Format("~/emailtemplates/welcomeemail.aspx?{0}", nv.NameValueToQueryString());
            var markup = Helper.GetPageMarkup(url);

            Helper.SendEmail(user.Email, "Welcome to " + clsUtility.SiteBrandName, markup);
        }

        private String GetPromoCode(MembershipUser user)
        {
            var result = String.Empty;
            var objData = new clsData();
            var dtDiscount = new DataTable();

            dtDiscount = objData.GetData("select fly_tblOfferDiscount.*, fly_tblDiscountType.DiscountType from fly_tblOfferDiscount inner join fly_tblDiscountType on fly_tblOfferDiscount.fkDiscountTypeID=fly_tblDiscountType.pkDiscountTypeID where DiscountType='New Registration Offer' and Active=1");

            if (dtDiscount.Rows.Count > 0)
            {
                var htDiscount = new Hashtable();

                htDiscount.Add("fkOfferDiscountID", Convert.ToInt32(dtDiscount.Rows[0]["pkOfferDiscountID"].ToString()));
                htDiscount.Add("Email", user.Email);
                htDiscount.Add("Code", dtDiscount.Rows[0]["Code"].ToString());
                htDiscount.Add("InValue", dtDiscount.Rows[0]["InValue"].ToString());
                htDiscount.Add("InPercentage", dtDiscount.Rows[0]["InPercentage"].ToString());
                htDiscount.Add("Discount", dtDiscount.Rows[0]["Discount"].ToString());
                htDiscount.Add("Active", dtDiscount.Rows[0]["Active"].ToString());

                var dtResult = new DataTable();

                dtResult = objData.GetDataTable("usp_InsertOfferDiscountDetails", htDiscount);

                if (String.Compare(dtResult.Rows[0]["result"].ToString(), "Inserted", true) == 0)
                {
                    result = dtDiscount.Rows[0]["Code"].ToString();
                }
            }

            return result;
        }

        private ModesEnum GetCurrentMode()
        {
            var result = ModesEnum.Main;

            if (Request["mode"].HasText())
            {
                if (String.Compare(Request["mode"], "mini", true) == 0)
                {
                    result = ModesEnum.Mini;
                }
            }

            return result;
        }

        private void SetMode()
        {
            if (GetCurrentMode() == ModesEnum.Mini)
            {
                h1Header.InnerText = null;

                var literal = new Literal();

                h1Header.Controls.Add(literal);

                if (inputFirstName.Value.HasText())
                {
                    literal.Text = inputFirstName.Value + ", you are almost there!<br /> Complete your registration to continue.";
                }
                else
                {
                    literal.Text = "Almost there!<br /> Complete your registration to continue.";
                }

                divSocial.Visible = false;
                divLogin.Visible = false;
                divOr.Visible = false;
                formSignUp.Action = "signup.aspx" + Request.Url.Query;
            }
        }

        private String GeneratePassword()
        {
            var random = new Random();
            var bytes = new Byte[12];

            random.NextBytes(bytes);

            var result = Convert.ToBase64String(bytes);

            return result;
        }

        private void SaveAvatar(String avatarUrl, ProfileCommon profile)
        {
            try
            {
                Byte[] avatarBytes;

                Helper.RequestUrlAsync(avatarUrl, out avatarBytes);

                var pathes = Helper.CreateCustomerMediaDirectoriesIfNeeded(profile.UserName);
                var bytes = new Byte[16];

                new Random().NextBytes(bytes);

                var fileName = Helper.GetHexademicalString(bytes);

                Helper.HandleCustomerFileImage(fileName, pathes[0], pathes[1], true, false, avatarBytes, profile);
            }
            catch
            {
            }
        }

        private enum ModesEnum
        {
            Main = 0,
            Mini = 10
        }

        #endregion
    }
}