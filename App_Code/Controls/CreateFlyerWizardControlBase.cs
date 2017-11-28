using FlyerMe.BLL.CreateFlyer;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls
{
    public abstract class CreateFlyerWizardControlBase : UserControl
    {
        public String FormAction { get; set; }

        public WizardFlyer Flyer
        {
            get
            {
                if (flyer == null)
                {
                    flyer = WizardFlyer.GetFlyer();
                }

                return flyer;
            }
        }

        public ProfileCommon ProfileCommon
        {
            get
            {
                return profile;
            }
        }

        public String ParentPageName { get; set; }

        public String Step { get; set; }

        public virtual void SetSessionData(Boolean isInitial)
        {
        }

        protected virtual void InitFields(ProfileCommon profile)
        {
            flyer = Flyer;
            this.profile = profile.GetProfile(Page.User.Identity.Name);
        }

        protected void SetDdlSelectedIndex(DropDownList ddl, String value)
        {
            if (String.Compare(ddl.SelectedValue, value, false) == 0)
            {
                return;
            }

            var item = ddl.Items.FindByValue(value);

            if (item != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(item);
            }
        }

        #region private

        private WizardFlyer flyer;
        private ProfileCommon profile;

        #endregion
    }
}