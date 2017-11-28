using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class L26 : PreviewFlyerWizardControlBase
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

        protected String GetSecondaryPhotoUrl(Int32 index, Int32 cohortNumber)
        {
            String result = null;
            var photoIndex = index + 1;
            var lastPhotoIndex = Photos.Length - 1 >= (cohortNumber * 2) ? (cohortNumber * 2) : Photos.Length - 1;
            var useFullWidth = (lastPhotoIndex % 2) != 0;

            if (useFullWidth && photoIndex == lastPhotoIndex)
            {
                result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_FullWidth];
            }
            else
            {
                result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width324_Height239];
            }

            return result;
        }

        protected Boolean ShouldColspan(Int32 index, Int32 cohortNumber)
        {
            var photoIndex = index + 1;
            var lastPhotoIndex = Photos.Length - 1 >= (cohortNumber * 2) ? (cohortNumber * 2) : Photos.Length - 1;
            var useFullWidth = (lastPhotoIndex % 2) != 0;

            return useFullWidth && photoIndex == lastPhotoIndex;
        }
    }
}