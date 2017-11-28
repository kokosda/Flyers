using FlyerMe.BLL.CreateFlyer;
using System;
using System.Web.UI;

namespace FlyerMe.Controls
{
    public abstract class PreviewFlyerWizardBlockControlBase : UserControl
    {
        protected WizardFlyer Flyer
        {
            get
            {
                return Preview.Flyer;
            }
        }

        protected String RootUrl
        {
            get
            {
                return Preview.RootUrl;
            }
        }

        protected String EmailImageUrl
        {
            get
            {
                return Preview.EmailImageUrl;
            }
        }
        
        protected PreviewFlyerWizardControlBase Preview
        {
            get
            {
                if (preview == null)
                {
                    preview = Parent as PreviewFlyerWizardControlBase;
                }

                return preview;
            }
        }

        #region private

        private PreviewFlyerWizardControlBase preview;

        #endregion
    }
}