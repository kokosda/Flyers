using FlyerMe.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class EditProfile : PageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/profile";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Profile | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            Request.RedirectToHttpsIfRequired(Response);
            divSummaryError.Visible = false;

            if (!IsPostBack)
            {
                SetDdlTitles();
                SetDdlBusinessState();
                SetDdlBrokerageState();
                SetInputs();
                SetPhotos();

                if (!String.IsNullOrEmpty(Request["successmessage"]))
                {
                    divSummarySuccess.Visible = true;
                    ltlSuccessMessage.Text = Request["successmessage"];
                }

                labelNewletter.InnerText = clsUtility.ProjectName + " Newsletter";
            }
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

        protected void ddlAssociation_DataBound(Object sender, EventArgs e)
        {
            var li = new ListItem("Select Association...", String.Empty);
            ddlAssociation.Items.Insert(0, li);
        }

        protected void ddlBrokerageState_DataBound(Object sender, EventArgs e)
        {
            ListItem li = new ListItem("Select State...", String.Empty);
            ddlBrokerageState.Items.Insert(0, li);
        }

        protected void ddlBusinessState_SelectedIndexChanged(Object sender, EventArgs e)
        {
            SetDdlAssociation();
        }

        protected void btnSave_Click(Object sender, EventArgs e)
        {
            Save();
        }

        protected void btnChangePassword_Click(Object sender, EventArgs e)
        {
            String message = null;

            if (String.IsNullOrEmpty(inputCurrentPassword.Value))
            {
                message = "Current Password is required.";
            }
            else if (String.IsNullOrEmpty(inputNewPassword.Value))
            {
                message = "New Password is required.";
            }
            else if (String.IsNullOrEmpty(inputConfirmNewPassword.Value))
            {
                message = "Confirm New Password is required.";
            }
            else if (String.Compare(inputNewPassword.Value, inputConfirmNewPassword.Value, false) != 0)
            {
                message = "Passwords do not match.";
            }

            if (String.IsNullOrEmpty(message))
            {
                var user = Membership.GetUser();

                if (String.Compare(user.GetPassword(), inputCurrentPassword.Value, false) != 0)
                {
                    message = "Current Password is incorrect";
                }
                else
                {
                    try
                    {
                        user.ChangePassword(inputCurrentPassword.Value, inputNewPassword.Value);
                        message = "Password successfully changed.";
                        Response.Redirect(String.Format("~/profile.aspx?successmessage={0}", message), true);
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                    }
                }
            }

            if (!String.IsNullOrEmpty(message))
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = message;
                divSummarySuccess.Visible = false;
            }
        }

        #region private

        private void SetDdlTitles()
        {
            var objectDataSource = new ObjectDataSource("FlyerMe.TitlesBLL", "GetTitles")
                                        {
                                            OldValuesParameterFormatString = "original_{0}"
                                        };

            form.Controls.Add(objectDataSource);
            ddlTitle.DataSource = objectDataSource;
            ddlTitle.DataBind();
        }

        private void SetDdlBusinessState()
        {
            var objectDataSource = new ObjectDataSource("FlyerMe.StatesBLL", "GetStates")
                                        {
                                            OldValuesParameterFormatString = "original_{0}"
                                        };

            form.Controls.Add(objectDataSource);
            ddlBusinessState.DataSource = objectDataSource;
            ddlBusinessState.DataBind();
        }

        private void SetDdlAssociation()
        {
            var objectDataSource = new ObjectDataSource("FlyerMe.AssociationsBLL", "GetAssociationsByState")
                                        {
                                            OldValuesParameterFormatString = "original_{0}"
                                        };
            var controlParameter = new ControlParameter
                                        {
                                            ControlID = "ddlBusinessState",
                                            Name = "state",
                                            PropertyName = "SelectedValue",
                                            Type = TypeCode.String
                                        };

            objectDataSource.SelectParameters.Add(controlParameter);
            form.Controls.Add(objectDataSource);
            ddlAssociation.DataSource = objectDataSource;
            ddlAssociation.DataBind();
        }

        private void SetDdlBrokerageState()
        {
            ddlBrokerageState.DataSource = ddlBusinessState.DataSource;
            ddlBrokerageState.DataBind();
        }

        private void SetInputs()
        {
            ddlTitle.Attributes.Add("data-clientname", "Title");
            ddlBusinessState.Attributes.Add("data-clientname", "BusinessState");
            ddlBrokerageState.Attributes.Add("data-clientname", "BrokerageState");
            inputEmail.Value = User.Identity.Name;

            var profile = Profile.GetProfile(User.Identity.Name);

            SetDdlSelectedIndex(ddlBrokerageState, profile.Brokerage.BrokerageState);
            SetDdlSelectedIndex(ddlBusinessState, profile.BusinessAddress.BusinessState);
            SetDdlSelectedIndex(ddlNewsletter, profile.Preferences.Newsletter);
            SetDdlSelectedIndex(ddlTitle, profile.Title);
            SetDdlAssociation();
            SetDdlSelectedIndex(ddlAssociation, profile.Association);
            inputBrokerageAddress1.Value = profile.Brokerage.BrokerageAddress1;
            inputBrokerageAddress2.Value = profile.Brokerage.BrokerageAddress2;
            inputBrokerageCity.Value = profile.Brokerage.BrokerageCity;
            inputBrokerageName.Value = profile.Brokerage.BrokerageName;
            inputBrokerageZipCode.Value = profile.Brokerage.BrokerageZipcode;
            inputBusinessAddress1.Value = profile.BusinessAddress.BusinessAddress1;
            inputBusinessAddress2.Value = profile.BusinessAddress.BusinessAddress2;
            inputBusinessCity.Value = profile.BusinessAddress.BusinessCity;
            inputBusinessZipCode.Value = profile.BusinessAddress.BusinessZipcode;
            inputSecondaryEmail.Value = profile.Contact.EmailSecondary;
            inputBusinessFax.Value = profile.Contact.Fax;
            inputFirstName.Value = profile.FirstName;
            inputLastName.Value = profile.LastName;
            inputMiddleName.Value = profile.MiddleInitial;
            inputBusinessPhone.Value = profile.Contact.PhoneBusiness;
            inputBusinessPhoneExt.Value = profile.Contact.PhoneBusinessExt;
            inputCellPhone.Value = profile.Contact.PhoneCell;
            inputHomePhone.Value = profile.Contact.PhoneHome;
            SetReferredByInputs(profile.ReferredBy);
            inputTeamName.Value = profile.TeamName;
            inputWebsite.Value = profile.Website;
            inputBre.Value = profile.DRE;

            if (!String.IsNullOrEmpty(inputReferredBy.Value))
            {
                inputReferredBy.Attributes.Add("disabled", "disabled");
                divReferredBy.Attributes["class"] += " email";
            }
            if (!String.IsNullOrEmpty(ddlReferredSource.SelectedValue))
            {
                ddlReferredSource.Enabled = false;
                divReferredSource.Attributes["class"] += " email";
            }
        }

        private void Save()
        {
            try
            {
                SaveProfile();

                var message = "Profile successfully saved.";

                Response.Redirect(String.Format("~/profile.aspx?successmessage={0}", message), true);
            }
            catch(Exception ex)
            {
                divSummaryError.Visible = true;
                ltlMessage.Text = ex.Message;
                divSummarySuccess.Visible = false;
            }
        }

        private ProfileCommon SaveProfile()
        {
            var profile = Profile.GetProfile(User.Identity.Name);

            profile.Brokerage.BrokerageState = ddlBrokerageState.SelectedValue.Trim();
            profile.BusinessAddress.BusinessState = ddlBusinessState.SelectedValue.Trim();
            profile.Preferences.Newsletter = ddlNewsletter.SelectedValue.Trim();
            profile.Title = ddlTitle.SelectedValue.Trim();
            profile.Association = ddlAssociation.SelectedValue.Trim();
            profile.Brokerage.BrokerageAddress1 = inputBrokerageAddress1.Value.Trim();
            profile.Brokerage.BrokerageAddress2 = inputBrokerageAddress2.Value.Trim();
            profile.Brokerage.BrokerageCity = inputBrokerageCity.Value.Trim();
            profile.Brokerage.BrokerageName = inputBrokerageName.Value.Trim();
            profile.Brokerage.BrokerageZipcode = inputBrokerageZipCode.Value.Trim();
            profile.BusinessAddress.BusinessAddress1 = inputBusinessAddress1.Value.Trim();
            profile.BusinessAddress.BusinessAddress2 = inputBusinessAddress2.Value.Trim();
            profile.BusinessAddress.BusinessCity = inputBusinessCity.Value.Trim();
            profile.BusinessAddress.BusinessZipcode = inputBusinessZipCode.Value.Trim();
            profile.Contact.EmailSecondary = inputSecondaryEmail.Value.Trim();
            profile.Contact.Fax = inputBusinessFax.Value.Trim();
            profile.FirstName = inputFirstName.Value.Trim();
            profile.LastName = inputLastName.Value.Trim();
            profile.MiddleInitial = inputMiddleName.Value.Trim();
            profile.Contact.PhoneBusiness = inputBusinessPhone.Value.Trim();
            profile.Contact.PhoneBusinessExt = inputBusinessPhoneExt.Value.Trim();
            profile.Contact.PhoneCell = inputCellPhone.Value.Trim();
            profile.Contact.PhoneHome = inputHomePhone.Value.Trim();
            profile.TeamName = inputTeamName.Value.Trim();
            profile.Website = inputWebsite.Value.Trim();
            profile.DRE = inputBre.Value.Trim();
            profile.Save();
            SavePhotos(profile);

            var result = profile;

            return result;
        }

        private void SetDdlSelectedIndex(DropDownList ddl, String value)
        {
            var item = ddl.Items.FindByValue(value);

            if (item != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(item);
            }
        }

        private void SetReferredByInputs(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var split = value.Split('|');

                if (split.Length > 0)
                {
                    SetDdlSelectedIndex(ddlReferredSource, split[0]);

                    if (split.Length > 1)
                    {
                        inputReferredBy.Value = split[1];
                    }
                }
            }
        }

        private void SetPhotos()
        {
            var profile = Profile.GetProfile(User.Identity.Name);

            if (!String.IsNullOrEmpty(profile.ImageURL))
            {
                imageMyPhoto.ImageUrl = profile.ImageURL;
                divMyPhoto.Attributes["class"] += " change";
            }
            else
            {
                imageMyPhoto.Visible = false;
            }

            if (!(String.IsNullOrEmpty(profile.LogoURL)))
            {
                imageMyLogo.ImageUrl = profile.LogoURL;
                divMyLogo.Attributes["class"] += " change";
            }
            else
            {
                imageMyLogo.Visible = false;
            }
        }

        private void SavePhotos(ProfileCommon profile)
        {
            var customerMediaDirectories = Helper.CreateCustomerMediaDirectoriesIfNeeded(profile.UserName);
            var random = new Random();
            var bytes = new Byte[16];
            var fileInputs = new[] { inputFileMyPhoto, inputFile2MyPhoto, inputFileMyLogo, inputFile2MyLogo };
            var deleteRestrictedInputs = new List<String>();

            foreach(var fi in fileInputs)
            {
                var file = fi.PostedFile;

                random.NextBytes(bytes);

                var fileName = Helper.GetHexademicalString(bytes);
                var canSaveFile = file != null && file.ContentLength > 0;
                var imageName = fi.ID.Replace("inputFile", String.Empty).Replace("2", String.Empty);
                var canDeleteFile = Request.ParseCheckboxValue("delete" + imageName) ?? false;

                if (canSaveFile)
                {
                    deleteRestrictedInputs.Add(imageName);
                }

                if (deleteRestrictedInputs.Any(p => String.Compare(p, imageName, true) == 0))
                {
                    canDeleteFile = false;
                }

                if (fi.ID.EndsWith("MyPhoto", StringComparison.OrdinalIgnoreCase))
                {
                    if (customerMediaDirectories.Count > 1)
                    {
                        Helper.HandleCustomerFileImage(fileName, customerMediaDirectories[0], customerMediaDirectories[1], canSaveFile, canDeleteFile, file.InputStream, Profile);
                    }
                }
                else if (fi.ID.EndsWith("MyLogo", StringComparison.OrdinalIgnoreCase))
                {
                    if (customerMediaDirectories.Count > 3)
                    {
                        Helper.HandleCustomerFileImage(fileName, customerMediaDirectories[2], customerMediaDirectories[3], canSaveFile, canDeleteFile, file.InputStream, Profile);
                    }
                }
            }
        }

        #endregion
    }
}