using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class L25 : PreviewFlyerWizardControlBase
    {
        protected override FlyerTypes FlyerType
        {
            get
            {
                return FlyerTypes.Seller;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            InitFields(Profile);
        }

        protected String GetFirstPhotoUrl()
        {
            return Helper.InjectNamePartToFileName(Photos[0], Helper.FlyerImagesSatteliteSuffix_Width408_Height394);
        }

        protected String GetSecondaryPhotoUrl(Int32 index, Int32 cohortNumber)
        {
            String result = null;
            var photoIndex = index + 1;

            if (cohortNumber == 1)
            {
                result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width240_Height197];
            }
            else if (cohortNumber == 2)
            {
                result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width365_Height190];
            }

            return result;
        }
    }
}