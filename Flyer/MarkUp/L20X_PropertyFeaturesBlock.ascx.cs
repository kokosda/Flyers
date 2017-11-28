using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class L20X_PropertyFeaturesBlock : PreviewFlyerWizardBlockControlBase
    {
        public String[] PropertyFeatures
        {
            get
            {
                return Preview.PropertyFeatures;
            }
        }
    }
}