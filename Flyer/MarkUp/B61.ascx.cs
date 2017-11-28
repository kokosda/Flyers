using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class B61 : PreviewFlyerWizardControlBase
    {
        protected override FlyerTypes FlyerType
        {
            get
            {
                return FlyerTypes.Buyer;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            InitFields(Profile);
        }
    }
}