using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AC4_SampleSeller : PreviewFlyerWizardControlBase
    {
        protected override FlyerTypes FlyerType
        {
            get
            {
                return FlyerTypes.Seller;
            }
        }
    }
}