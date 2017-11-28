using FlyerMe.BLL.CreateFlyer;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace FlyerMe.Controls
{
    public abstract class PreviewFlyerWizardControlBase : UserControl
    {
        protected abstract FlyerTypes FlyerType { get; }

        public WizardFlyer Flyer
        {
            get
            {
                if (flyer == null)
                {
                    flyer = WizardFlyer.GetFlyer();

                    if (flyer == null)
                    {
                        flyer = WizardFlyer.SetFlyer(FlyerType);
                    }
                }

                return flyer;
            }
        }

        public String RootUrl
        {
            get
            {
                if (rootUrl == null)
                {
                    rootUrl = clsUtility.GetRootHost;
                }

                return rootUrl;
            }
        }

        public String EmailImageUrl
        {
            get
            {
                if (emailImageUrl == null)
                {
                    emailImageUrl = RootUrl + "email_flyer_email/";
                }

                return emailImageUrl;
            }
        }

        protected String ProfileImageUrl
        {
            get
            {
                return profile.ImageURL.HasText() ? Helper.InjectNamePartToFileName(profile.ImageURL, Helper.ProfileImagesSatteliteSuffix_Flyer_Ellipse).Replace(".jpg", ".png") : null;
            }
        }

        protected String CompanyLogoImageUrl
        {
            get
            {
                return profile.LogoURL;
            }
        }

        protected String ViewOnlineUrl
        {
            get
            {
                return Flyer.OrderId.HasValue ? RootUrl + "showflyer.aspx?orderid=" + Flyer.OrderId : null;
            }
        }

        protected String ViewPdfUrl
        {
            get
            {
                return Flyer.OrderId.HasValue ? RootUrl + "showpdf.aspx?orderid=" + Flyer.OrderId : null;
            }
        }

        protected String SendToClientsUrl
        {
            get
            {
                return Flyer.OrderId.HasValue ? RootUrl + "forwardtoclients.aspx?orderid=" + Flyer.OrderId : null;
            }
        }

        protected String FirstPhotoUrl
        {
            get
            {
                return Helper.GetFullPhotoPath(Flyer.OrderId.Value, 1, Helper.InjectNamePartToFileName(Flyer.Photos[0], Helper.FlyerImagesSatteliteSuffix_FullWidth));
            }
        }

        protected String MobilePhone
        {
            get
            {
                return profile.Contact.PhoneCell;
            }
        }

        protected String BrokerageName
        {
            get
            {
                return profile.Brokerage.BrokerageName;
            }
        }

        protected String Phone
        {
            get
            {
                var result = Flyer.Phone;

                if ((!String.IsNullOrEmpty(Flyer.Phone)) && (!String.IsNullOrEmpty(Flyer.Ext)))
                {
                    result = String.Format("{0} Ext.{1}", Flyer.Phone, Flyer.Ext);
                }

                return result;
            }
        }

        protected String[] Photos
        {
            get
            {
                if (photos == null)
                {
                    var temp = new List<String>(Flyer.Photos.Length);

                    for (var i = 0; i < Flyer.Photos.Length; i++)
                    {
                        if (Flyer.Photos[i].HasText())
                        {
                            temp.Add(Helper.GetFullPhotoPath(Flyer.OrderId.Value, i + 1, Flyer.Photos[i]));
                        }
                    }

                    photos = temp.ToArray();
                }

                return photos;
            }
        }

        protected Dictionary<String, Dictionary<String, String>> SecondaryPhotos
        {
            get
            {
                if (secondaryPhotos == null)
                {
                    var innerPhotos = Photos.Skip(1).ToArray();
                    var satelliteSuffixes = Helper.GetAllFlyerPhotoSatelliteSuffixes();
                    Dictionary<String, String> dict;

                    secondaryPhotos = new Dictionary<String, Dictionary<String, String>>(innerPhotos.Length);

                    for (var i = 0; i < innerPhotos.Length; i++)
                    {
                        dict = new Dictionary<String,String>(satelliteSuffixes.Length);
                        secondaryPhotos.Add(innerPhotos[i], dict);

                        for (var j = 0; j < satelliteSuffixes.Length; j++)
                        {
                            dict.Add(satelliteSuffixes[j], Helper.InjectNamePartToFileName(innerPhotos[i], satelliteSuffixes[j]));
                        }
                    }
                }

                return secondaryPhotos;
            }
        }

        protected String Price
        {
            get
            {
                return Flyer.Price.HasValue ? Flyer.Price.Value.FormatLongPrice() : null;
            }
        }

        protected String PriceRange
        {
            get
            {
                return Flyer.PriceMin.Value.FormatLongPrice() + " — " + Flyer.PriceMax.Value.FormatLongPrice();
            }
        }

        protected String Description
        {
            get
            {
                return Flyer.Description.HasText() ? Flyer.Description.Replace("\n", "<br />") : null;
            }
        }

        public String[] PropertyFeatures
        {
            get
            {
                if (propertyFeatures == null)
                {
                    propertyFeatures = Flyer.GetAmenities();
                }

                return propertyFeatures;
            }
        }

        protected String GoogleMapImageLink
        {
            get
            {
                return Helper.GetGoogleMapImageLink(Flyer.MapLink);
            }
        }

        protected String Address
        {
            get
            {
                return Flyer.StreetAddress + " " + (Flyer.City.HasText() ? Flyer.City + ", " : null) + Flyer.State + " " + Flyer.ZipCode;
            }
        }

        protected String CreateFlyerUrl
        {
            get
            {
                return RootUrl + "createflyer.aspx";
            }
        }

        protected String AvatarUrl
        {
            get
            {
                return profile.ImageURL.HasText() ? Helper.ToFullPath(Helper.InjectNamePartToFileName(profile.ImageURL, Helper.ProfileImagesSatteliteSuffix_Flyer_Ellipse)).Replace(".jpg", ".png") : null;
            }
        }

        protected String CompanyLogoUrl
        {
            get
            {
                return profile.LogoURL.HasText() ? Helper.ToFullPath(Helper.InjectNamePartToFileName(profile.LogoURL, Helper.ProfileImagesSatteliteSuffix_Flyer)) : null;
            }
        }

        protected Boolean IsHeaderLinksVisible
        {
            get
            {
                return Flyer.OrderId.HasValue;
            }
        }

        protected Boolean IsFooterLinksVisible
        {
            get
            {
                return Flyer.OrderId.HasValue;
            }
        }

        protected Boolean IsPropertyDescriptionBlockVisible
        {
            get
            {
                var result = (!String.IsNullOrEmpty(Flyer.Bedrooms) && String.Compare(Flyer.Bedrooms, "0") != 0) ||
                             (!String.IsNullOrEmpty(Flyer.Bathrooms) && String.Compare(Flyer.Bathrooms, "0") != 0) ||
                             (!String.IsNullOrEmpty(Flyer.LotSize)) || (!String.IsNullOrEmpty(Flyer.Subdivision)) || (!String.IsNullOrEmpty(Flyer.YearBuilt)) || (!String.IsNullOrEmpty(Flyer.Sqft));

                return result;
            }
        }

        protected Boolean IsFirstPhotoBlockVisible
        {
            get
            {
                return Photos.Length > 0;
            }
        }

        protected Boolean IsSecondPhotoBlockVisible
        {
            get
            {
                return Photos.Length > 1;
            }
        }

        protected Boolean IsThirdPhotoBlockVisible
        {
            get
            {
                return Photos.Length > 2;
            }
        }

        protected Boolean IsFourthPhotoBlockVisible
        {
            get
            {
                return Photos.Length > 3;
            }
        }

        protected Boolean IsFifthPhotoBlockVisible
        {
            get
            {
                return Photos.Length > 4;
            }
        }

        protected Boolean IsSecondaryPhotosBlockVisible
        {
            get
            {
                return Flyer.OrderId.HasValue && Photos.Length > 1;
            }
        }

        protected Boolean IsMlsPriceBlockVisible
        {
            get
            {
                return !String.IsNullOrEmpty(Flyer.MlsNumber) || Flyer.Price.HasValue;
            }
        }

        protected Boolean IsFlyerTitleBlockVisible
        {
            get
            {
                return !String.IsNullOrEmpty(Flyer.FlyerTitle);
            }
        }

        protected Boolean IsPropertyFeaturesBlockVisible
        {
            get
            {
                return PropertyFeatures.Length > 0;
            }
        }

        protected Boolean IsOpenHousesBlockVisible
        {
            get
            {
                return Flyer.OpenHouses.HasText();
            }
        }

        protected Boolean IsDescriptionBlockVisible
        {
            get
            {
                return !String.IsNullOrEmpty(Flyer.Description);
            }
        }

        protected Boolean IsPriceRangeBlockVisible
        {
            get
            {
                return Flyer.PriceMin.HasValue && Flyer.PriceMax.HasValue;
            }
        }

        protected Boolean IsAddressBlockVisible
        {
            get
            {
                return Flyer.StreetAddress.HasText();
            }
        }

        protected Boolean IsContactsBlockVisible
        {
            get
            {
                return Flyer.Name.HasText() && Phone.HasText() && BrokerageName.HasText() && Flyer.Email.HasText();
            }
        }

        protected Boolean IsLocationBlockVisible
        {
            get
            {
                return Flyer.Location.HasText();
            }
        }

        protected virtual void InitFields(ProfileCommon profile)
        {
            this.profile = profile.GetProfile(Page.User.Identity.Name);
        }

        #region private

        private String rootUrl;
        private String emailImageUrl;
        private ProfileCommon profile;
        private WizardFlyer flyer;
        private String[] photos;
        private Dictionary<String, Dictionary<String, String>> secondaryPhotos;
        private String[] propertyFeatures;

        #endregion
    }
}