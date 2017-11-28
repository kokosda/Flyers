using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AD0_SampleBuyer : PreviewFlyerWizardControlBase
    {
        protected override FlyerTypes FlyerType
        {
            get 
            {
                return FlyerTypes.Buyer;
            }
        }
    }
}