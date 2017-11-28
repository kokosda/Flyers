using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class L23 : PreviewFlyerWizardControlBase
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

        protected String GetSecondaryPhotoUrl(Int32 index)
        {
            String result = null;
            var photoIndex = index + 1;

            if (photoIndex == 1)
            {
                result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width324_Height197];
            }
            else
            {
                var lastPhotoIndex = Photos.Length - 1;
                var useFullWidth = ((lastPhotoIndex - 1) % 2) != 0;

                if (useFullWidth && photoIndex == lastPhotoIndex)
                {
                    result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_FullWidth];
                }
                else
                {
                    result = SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width324_Height197];
                }
            }

            return result;
        }

        protected Boolean ShouldColspan(Int32 index)
        {
            var photoIndex = index + 1;
            var lastPhotoIndex = Photos.Length - 1;
            var useFullWidth = (Photos.Length - 2 % 2) != 0;

            return useFullWidth && photoIndex == lastPhotoIndex;
        }
    }
}