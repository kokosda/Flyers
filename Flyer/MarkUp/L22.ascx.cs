using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class L22 : PreviewFlyerWizardControlBase
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
            var photoIndex = index + 1;
            var lastPhotoIndex = Photos.Length - 1;
            var useFullWidth = lastPhotoIndex % 2 != 0;

            if (useFullWidth && photoIndex == lastPhotoIndex)
            {
                return SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_FullWidth];
            }
            else
            {
                return SecondaryPhotos[Photos[photoIndex]][Helper.FlyerImagesSatteliteSuffix_Width324_Height239];
            }
        }

        protected Boolean ShouldColSpanTd(Int32 index)
        {
            var photoIndex = index + 1;
            var lastPhotoIndex = Photos.Length - 1;
            var useFullWidth = lastPhotoIndex % 2 != 0;

            return useFullWidth && photoIndex == lastPhotoIndex;
        }
    }
}