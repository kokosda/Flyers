using FlyerMe.BLL.CreateFlyer;
using Project.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Profile;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace FlyerMe
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// Helper class - contains supporting functions and routines
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class Helper
    {
        public const String ProfileImagesSatteliteSuffix_Flyer = "_FLYER";
        public const String ProfileImagesSatteliteSuffix_Flyer_Ellipse = "_FLYER_ELLIPSE";
        public const String FlyerImagesSatteliteSuffix_FullWidth = "_FULLWIDTH";
        public const String FlyerImagesSatteliteSuffix_Width408_Height394 = "_W408_H394";
        public const String FlyerImagesSatteliteSuffix_Width365_Height190 = "_W365_H190";
        public const String FlyerImagesSatteliteSuffix_Width324_Height239 = "_W324_H239";
        public const String FlyerImagesSatteliteSuffix_Width324_Height197 = "_W324_H197";
        public const String FlyerImagesSatteliteSuffix_Width276_Height168 = "_W276_H168";
        public const String FlyerImagesSatteliteSuffix_Width240_Height197 = "_W240_H197";
        public const String CustomerTestimonialImagesSatteliteSuffix_MAX = "_MAX";
        public const String CustomerTestimonialImagesSatteliteSuffix_RESIZED = "_RESIZED";

        /// <summary>
        /// Validates email address
        /// </summary>
        /// <param name="Email">Email address to validate</param>
        public static bool IsEmail(string Email)
        {
            Email = Email.Trim();
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(Email))
                return (true);
            else
                return (false);
        }

        public static Boolean IsEmail2(String email)
        {
            Boolean result;
            var strRegex =
                      @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
               + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
               + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
               + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (email != null)
            {
                result = Regex.IsMatch(email, strRegex);
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Puts current page to unsecure protocol
        /// </summary>
        public static void GoToHTTP()
        {

            if (HttpContext.Current.Request.Url.OriginalString.IndexOf("https:") != -1)
            {
                string origPath = HttpContext.Current.Request.Url.OriginalString.Replace("https:", "http:");
                HttpContext.Current.Response.Redirect(origPath);
            }
        }

        /// <summary>
        /// Validates URL
        /// </summary>
        /// <param name="Url">URL to validate</param>
        public static bool IsUrl(string Url)
        {
            string strRegex = "^(https?://)"
            + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
            + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
            + "|" // allows either IP or domain
            + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
            + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain
            + "[a-z]{2,6})" // first level domain- .com or .museum
            + "(:[0-9]{1,4})?" // port number- :80
            + "((/?)|" // a slash isn't required if there is no file name
            + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(Url))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Returns number of occurences of smallstring in bigstring
        /// </summary>
        /// <param name="SmallString">String to find</param>
        /// <param name="BigString">Big string</param>
        public int CharacterCounter(string SmallString, string BigString)
        {
            int index = 0;
            int count = 0;

            while (index < BigString.Length)
            {
                int indexOf = BigString.IndexOf(SmallString, index);
                if (indexOf != -1)
                {
                    count++; index = (indexOf + SmallString.Length);
                }
                else { index = BigString.Length; }
            }

            return count;
        }

        /// <summary>
        /// Validates if expression is numeric
        /// </summary>
        /// <param name="Expression">Expression</param>
        public static Boolean IsNumeric(Object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static void SendEmail(String recipientEmailAddress, String emailSubject, String emailBody)
        {
            SendEmail(null, recipientEmailAddress, recipientEmailAddress, emailSubject, emailBody, null, null);
        }

        public static void SendEmail(String senderName, String recipientEmailAddress, String recipientName, String emailSubject, String emailBody)
        {
            SendEmail(senderName, recipientEmailAddress, recipientEmailAddress, emailSubject, emailBody, null, null);
        }

        public static void SendEmail(String senderName, String recipientEmailAddress, String recipientName, String emailSubject, String emailBody, String[] filePathes)
        {
            SendEmail(senderName, recipientEmailAddress, recipientEmailAddress, emailSubject, emailBody, null, filePathes);
        }

        public static void SendEmail(String senderName, String recipientEmailAddress, String recipientName, String emailSubject, String emailBody, String replyToEmail, String[] filePathes = null)
        {
            EmailHelper.SendEmail(senderName, recipientEmailAddress, recipientName, emailSubject, emailBody, replyToEmail, filePathes);
        }

        public static void ClearCache()
        {
            if (HttpContext.Current.Session["NewOrder"] != null) { HttpContext.Current.Session.Remove("NewOrder"); }
            if (HttpContext.Current.Session["NewOrder2"] != null) { HttpContext.Current.Session.Remove("NewOrder2"); }
            if (HttpContext.Current.Session["CurrentOrder"] != null) { HttpContext.Current.Session.Remove("CurrentOrder"); }
            if (HttpContext.Current.Session["CurrentOrder3"] != null) { HttpContext.Current.Session.Remove("CurrentOrder3"); }
            if (HttpContext.Current.Session["CurrentMarketType"] != null) { HttpContext.Current.Session.Remove("CurrentMarketType"); }
            if (HttpContext.Current.Session["CurrentMarketState"] != null) { HttpContext.Current.Session.Remove("CurrentMarketState"); }
            if (HttpContext.Current.Session["MarketSelectedTable"] != null) { HttpContext.Current.Session.Remove("MarketSelectedTable"); }
            if (HttpContext.Current.Session["NoOfPhotos"] != null) { HttpContext.Current.Session.Remove("NoOfPhotos"); }
            if (HttpContext.Current.Session["PromoCodeCurrentOrder"] != null) { HttpContext.Current.Session.Remove("PromoCodeCurrentOrder"); }
            if (HttpContext.Current.Session["PromoCurrentOrder"] != null) { HttpContext.Current.Session.Remove("PromoCurrentOrder"); }
        }

        public static string ProperCase(string TextToFormat)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(TextToFormat.ToLower());
        }

        public string GetPropImageNamePath(string orderid, int photo)
        {
            //string siteRoot = ConfigurationManager.AppSettings["SiteRoot"];
            string siteRoot = clsUtility.GetRootHost;
            string imagePath = string.Empty;
            string imageName = string.Empty;
            string imageFullNamePath = string.Empty;
            DirectoryInfo directory = null;

            switch (photo)
            {
                case 1:
                    imagePath = "order/" + orderid + "/photo1/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Resized*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noMainPhoto.jpg"; }
                    break;
                case 2:
                    imagePath = "order/" + orderid + "/photo2/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto2.jpg"; }
                    break;
                case 3:
                    imagePath = "order/" + orderid + "/photo3/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto3.jpg"; }
                    break;
                case 4:
                    imagePath = "order/" + orderid + "/photo4/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto4.jpg"; }
                    break;
                case 5:
                    imagePath = "order/" + orderid + "/photo5/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto5.jpg"; }
                    break;
                case 6:
                    imagePath = "order/" + orderid + "/photo6/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto6.jpg"; }
                    break;
                case 7:
                    imagePath = "order/" + orderid + "/photo7/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Selected*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noSubPhoto7.jpg"; }
                    break;
            }

            return siteRoot + imageFullNamePath;

        } //End: GetPropImageNamePath


        public string GetPropImageIconNamePath(string orderid, int photo)
        {
            string imagePath = string.Empty;
            string imageName = string.Empty;
            string imageFullNamePath = string.Empty;
            DirectoryInfo directory = null;

            switch (photo)
            {
                case 1:
                    imagePath = "order/" + orderid + "/photo1/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 2:
                    imagePath = "order/" + orderid + "/photo2/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 3:
                    imagePath = "order/" + orderid + "/photo3/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 4:
                    imagePath = "order/" + orderid + "/photo4/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 5:
                    imagePath = "order/" + orderid + "/photo5/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 6:
                    imagePath = "order/" + orderid + "/photo6/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
                case 7:
                    imagePath = "order/" + orderid + "/photo7/";
                    directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(imagePath));
                    foreach (FileInfo fi in directory.GetFiles("*Icon*.*")) { imageName = fi.Name; }
                    imageFullNamePath = imagePath + imageName;
                    if (imageName.Length < 1) { imageFullNamePath = "images/noPhotoIcon.jpg"; }
                    break;
            }

            return imageFullNamePath;

        } //End: GetPropImageNamePath


        public string GetNoOfPhotos(string layout)
        {
            string noPhotos = string.Empty;

            switch (layout)
            {
                case "L01":
                    noPhotos = "5";
                    break;
                case "L02":
                    noPhotos = "5";
                    break;
                case "L03":
                    noPhotos = "4";
                    break;
                case "L04":
                    noPhotos = "4";
                    break;
                case "L05":
                    noPhotos = "3";
                    break;
                case "L06":
                    noPhotos = "1";
                    break;
                case "L07":
                    noPhotos = "6";
                    break;
                case "L08":
                    noPhotos = "7";
                    break;
            }

            return noPhotos;
        }

        
        /// <summary>
        /// Adds the onfocus and onblur attributes to all input controls found in the specified parent,
        /// to change their apperance with the control has the focus
        /// </summary>
        public static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
        {
            foreach (Control ctl in container.Controls)
            {
                if ((onlyTextBoxes && ctl is System.Web.UI.WebControls.TextBox) || ctl is System.Web.UI.WebControls.TextBox) // || ctl is DropDownList || ctl is ListBox || ctl is CheckBox || ctl is RadioButton || ctl is RadioButtonList || ctl is CheckBoxList)
                {
                    System.Web.UI.WebControls.WebControl wctl = ctl as System.Web.UI.WebControls.WebControl;
                    wctl.Attributes.Add("onfocus", string.Format("this.className = '{0}';", className));
                    wctl.Attributes.Add("onblur", "this.className = '';");
                } 
                else
                {
                    if (ctl.Controls.Count > 0)
                        SetInputControlsHighlight(ctl, className, onlyTextBoxes);
                } 
            } 
        }

        public static void TimeOut()
        {
            try { 
                
                    if (HttpContext.Current.Session["CurrentOrder"] == null)
                    {
                        HttpContext.Current.Response.Redirect("~/MyFlyers.aspx"); 
                    }
                
                
                }
            catch {
                HttpContext.Current.Response.Redirect("~/MyFlyers.aspx"); 
                    }
        }


        public string GetCustomerName(string email)
        {
            try
            {
                string customerName = "";
                if (email.Trim().Length > 0)
                {
                    MembershipUser mu = Membership.GetUser(email);

                    ProfileCommon profile = (ProfileCommon)ProfileBase.Create(mu.UserName, true);

                    if (profile.MiddleInitial.Trim().Length > 0)
                    {
                        customerName = profile.FirstName.Trim() + " " + profile.MiddleInitial.Trim() + " " + profile.LastName.Trim();
                    }
                    else
                    {
                        customerName = profile.FirstName.Trim() + " " + profile.LastName.Trim();
                    }
                }

                return customerName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string UploadUserImageFile(HtmlInputFile File)
        {
            string[] Str = HttpContext.Current.User.Identity.Name.ToString().Split('@');
          
            string Folder = Str[0];
            string myPath = HttpContext.Current.Server.MapPath("SlideShow/") + Folder;
            string FileName = clsUtility.UploadFile(myPath, File);
           
            CreateThumb("Thumbs100_" + Path.GetFileName(FileName), 100, myPath + @"\", File);
            CreateThumb("Thumbs30_" + Path.GetFileName(FileName), 30, myPath + @"\", File);
            return FileName;
        }



        public string UploadUserMusicFile(HtmlInputFile File)
        {
            string[] Str = HttpContext.Current.User.Identity.Name.ToString().Split('@');
            string Folder = Str[0];
            string myPath = HttpContext.Current.Server.MapPath("SlideShow/") + Folder;
            string FileName = clsUtility.UploadFile(myPath, File);
            return FileName;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private void CreateThumb(string filename, int h, string path,HtmlInputFile File)
        {

            //customize to current FileUpload control

            Image img = Image.FromStream(File.PostedFile.InputStream);
            decimal imageRatio = (decimal)img.Width / (decimal)img.Height;
            int baseDimension = h;
            int newH = baseDimension;
            int newW = Convert.ToInt32(baseDimension / imageRatio);
            if (img.Height > newH)
            {
                Image.GetThumbnailImageAbort dummy = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                //sometimes needed to for image to look right, i only needed it when creating larger images
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);

                Image smImg = img.GetThumbnailImage(newW, newH, dummy, IntPtr.Zero);
                smImg.Save(path+ filename);
                smImg.Dispose();
            }
            else
            {
                img.Save(path + filename);
            }

            img.Dispose();
        }

        public static string GetFormattedMsg(string SuccessMsg)
        {
            string Msg = "<table cellpadding='2' cellspacing='2' style='border:1px solid #FBEDBB;color:#000000;font-size:12px;width:100%;background-color:#fff'><tr><td align='left' style='width: 23px'><img  style='borderpx;' src='images/errormsg.jpg' /></td><td align='left' valign='middle'>" + SuccessMsg + "</td></tr></table>";
            return Msg;

        }

        public static string GetFormattedError(string ErrorMsg)
        {
            string Msg = "<table cellpadding='2' cellspacing='2' style='border:1px solid #FE8100;color:Red;font-size:12px;width:100%;background-color:#fff'><tr><td align='left' style='width: 23px'><img  style='borderpx;' src='images/errormsg.jpg' /> </td><td align='left' valign='top'><span style='font-weight:bold;color:Red;font-size:14px;'>Error :</span>" + ErrorMsg + "</td></tr></table>";
            return Msg;
        }

        public static string GetFormattedSuccess(string SuccessMsg)
        {
            string Msg = "<table cellpadding='2' cellspacing='2' style='border:1px solid #006600;color:#006600;font-size:12px;width:100%;background-color:#fff'><tr><td align='left' style='width: 27px'><img  style='borderpx;' src='images/success.gif' /> </td><td align='left' valign='middle'><span style='font-weight:bold;color:#006600;font-size:14px;'>Congratulations :</span>" + SuccessMsg + "</td></tr></table>";
            return Msg;
        }

        public static string GetUserWebSiteName(string URLName)
        {
            string WebSiteName = "";
            string Str = URLName;
            Str = Str.Replace("http://", " ").Trim();
            string[] NumberOfStr = Str.Split('.');

            string NewStr = NumberOfStr[0];

            if (NumberOfStr[0] != "www")
            {
                Str = "www." + Str;
            }
            NumberOfStr = Str.Split('.');
            WebSiteName = NumberOfStr[0] + "." + NumberOfStr[1] + "." + NumberOfStr[2] + ".com";
            return WebSiteName;
        }

        public static String GetCustomerNameByEmail(String email)
        {
            try
            {
                var result = String.Empty;
                var profile = GetProfileByEmail(email);

                if (profile != null)
                {
                    if (profile.MiddleInitial.Trim().Length > 0)
                    {
                        result = profile.FirstName.Trim() + " " + profile.MiddleInitial.Trim() + " " + profile.LastName.Trim();
                    }
                    else
                    {
                        result = profile.FirstName.Trim() + " " + profile.LastName.Trim();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static ProfileCommon GetProfileByEmail(String email)
        {
            ProfileCommon result = null;

            if (email.Trim().Length > 0)
            {
                var mu = Membership.GetUser(email);

                if (mu != null)
                {
                    result = (ProfileCommon)ProfileBase.Create(mu.UserName, true);
                }
            }

            return result;
        }

        public static List<String> CreateCustomerMediaDirectoriesIfNeeded(String email)
        {
            var result = new List<String>();
            var profile = GetProfileByEmail(email);

            if (profile != null)
            {
                var relativePathBase = GetCustomerMediaRelativePath() + profile.UserName.Replace(".", "-");
                var customerMediaDirectoryPath = HttpContext.Current.Server.MapPath(relativePathBase);
                var myPhotoFileSystemPath = customerMediaDirectoryPath + "/myphoto";
                var myPhotoRelativePath = relativePathBase + "/myphoto";
                var myLogoFileSystemPath = customerMediaDirectoryPath + "/mylogo";
                var myLogoRelativePath = relativePathBase + "/mylogo";

                if (!Directory.Exists(customerMediaDirectoryPath))
                {
                    Directory.CreateDirectory(customerMediaDirectoryPath);

                    if (!Directory.Exists(myPhotoFileSystemPath)) 
                    { 
                        Directory.CreateDirectory(myPhotoFileSystemPath);

                    }

                    if (!Directory.Exists(myLogoFileSystemPath)) 
                    { 
                        Directory.CreateDirectory(myLogoFileSystemPath);
                    }
                }

                result.Add(myPhotoFileSystemPath);
                result.Add(myPhotoRelativePath);
                result.Add(myLogoFileSystemPath);
                result.Add(myLogoRelativePath);
            }

            return result;
        }

        public static List<String> CreateOrderMediaDirectoriesIfNeeded(Int64 orderId)
        {
            var result = new List<String>();

            var relativePathBase = GetOrderMediaRelativePath() + orderId.ToString();
            var orderMediaDirectoryPath = HttpContext.Current.Server.MapPath(relativePathBase);

            if (!Directory.Exists(orderMediaDirectoryPath))
            {
                Directory.CreateDirectory(orderMediaDirectoryPath);
            }

            for (var i = 0; i < 10; i++)
            {
                var photoDirectoryPath = orderMediaDirectoryPath + "\\photo" + (i + 1).ToString();

                if (!Directory.Exists(photoDirectoryPath))
                {
                    Directory.CreateDirectory(photoDirectoryPath);
                }

                result.Add(photoDirectoryPath);
            }

            return result;
        }

        public static String GetCustomerMediaRelativePath()
        {
            var result = ConfigurationManager.AppSettings["CustomerMediaVirtualPath"];

            return result;
        }

        public static String GetOrderMediaRelativePath()
        {
            var result = ConfigurationManager.AppSettings["OrderMediaVirtualPath"];

            return result;
        }

        public static String GetHexademicalString(Byte[] bytes)
        {
            var result = bytes != null ? BitConverter.ToString(bytes).Replace("-", String.Empty) : null;

            return result;
        }

        public static Boolean IsSupportedFileImageExtension(String extension)
        {
            var result = false;

            if (!String.IsNullOrEmpty(extension))
            {
                result = new String[] 
                             {
                                 ".jpg", 
                                 ".jpeg",
                                 ".png",
                                 ".gif" 
                             }
                             .Any(s => String.Compare(s, extension, true) == 0);
            }

            return result;
        }

        public static void ClearFileImagesFromDirectory(String directory)
        {
            if (Path.IsPathRooted(directory))
            {
                foreach(var file in new DirectoryInfo(directory).GetFiles())
                {
                    if (IsSupportedFileImageExtension(file.Extension))
                    {
                        file.Delete();
                    }
                }
            }
        }

        public static String CheckImageFileExtension(HttpPostedFile file)
        {
            return CheckImageFileExtension(file.InputStream);
        }

        public static String CheckImageFileExtension(Stream stream)
        {
            String result = null;

            using (var bm = System.Drawing.Bitmap.FromStream(stream))
            {
                var allowedFormats = new ImageFormat[]
                                         {
                                             ImageFormat.Bmp,
                                             ImageFormat.Gif,
                                             ImageFormat.Jpeg,
                                             ImageFormat.Tiff
                                         };

                if (!allowedFormats.Any(af => af != bm.RawFormat))
                {
                    throw new Exception("Image format is incorrect");
                }

                result = GetImageFileExtension(bm.RawFormat);
            }

            return result;
        }

        public static String CheckImageFileExtension(Byte[] file)
        {
            String result = null;

            using (var ms = new MemoryStream(file))
            using (var bm = System.Drawing.Bitmap.FromStream(ms))
            {
                var allowedFormats = new ImageFormat[]
                                         {
                                             ImageFormat.Bmp,
                                             ImageFormat.Gif,
                                             ImageFormat.Jpeg,
                                             ImageFormat.Tiff
                                         };

                if (!allowedFormats.Any(af => af != bm.RawFormat))
                {
                    throw new Exception("Image format is incorrect");
                }

                result = GetImageFileExtension(bm.RawFormat);
            }

            return result;
        }

        public static void HandleCustomerFileImage(String fileName, String fileSystemDirectory, String fileRelativePath, Boolean canSaveFile, Boolean canDeleteFile, Stream file, ProfileCommon profile)
        {
            var fileBytes = new Byte[file.Length];

            if (file.CanRead)
            {
                file.Read(fileBytes, 0, (Int32)file.Length);
            }

            HandleCustomerFileImage(fileName, fileSystemDirectory, fileRelativePath, canSaveFile, canDeleteFile, fileBytes, profile);
        }

        public static void HandleCustomerFileImage(String fileName, String fileSystemDirectory, String fileRelativePath, Boolean canSaveFile, Boolean canDeleteFile, Byte[] file, ProfileCommon profile)
        {
            var canHandle = (!String.IsNullOrEmpty(fileSystemDirectory)) && (!String.IsNullOrEmpty(fileRelativePath));

            if (!canHandle)
            {
                return;
            }

            if (canSaveFile)
            {
                Helper.CheckImageFileExtension(file);

                var fileExtension = ".jpg";
                var savePath = fileSystemDirectory + "/" + fileName + fileExtension;

                if (fileRelativePath.IndexOf("myphoto", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    profile.ImageURL = fileRelativePath + "/" + fileName + fileExtension;
                    SaveAvatarSatelliteImages(file, savePath);
                }
                else if (fileRelativePath.IndexOf("mylogo", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    profile.LogoURL = fileRelativePath + "/" + fileName + fileExtension;
                    SaveCompanyLogoSatelliteImages(file, savePath);
                }

                var tempFile = ResizeToWidthAndSaveImage(file, 512);

                File.WriteAllBytes(savePath, tempFile);
                profile.Save();
            }
            else if (canDeleteFile)
            {
                if (fileRelativePath.IndexOf("myphoto", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    profile.ImageURL = String.Empty;
                }
                else if (fileRelativePath.IndexOf("mylogo", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (String.Compare(fileName, Path.GetFileNameWithoutExtension(profile.LogoURL), true) == 0)
                    {
                        profile.LogoURL = String.Empty;
                    }
                }

                profile.Save();
            }
        }

        public static void HandleFlyersPhoto(String fileName, String fileSystemDirectory, Boolean canSaveFile, Boolean canDeleteFile, Stream file, String imageName, Int32 number)
        {
            var fileBytes = new Byte[file.Length];

            if (file.CanRead)
            {
                file.Read(fileBytes, 0, (Int32)file.Length);
            }

            HandleFlyersPhoto(fileName, fileSystemDirectory, canSaveFile, canDeleteFile, fileBytes, imageName, number);
        }

        public static void HandleFlyersPhoto(String fileName, String fileSystemDirectory, Boolean canSaveFile, Boolean canDeleteFile, Byte[] file, String imageName, Int32 number)
        {
            var flyer = WizardFlyer.GetFlyer();
            var index = number - 1;
            var canHandle = flyer.Photos != null && index >= 0 && index < flyer.Photos.Length;

            if (!canHandle)
            {
                return;
            }

            if (canSaveFile)
            {
                Helper.CheckImageFileExtension(file);
                Helper.ClearFileImagesFromDirectory(fileSystemDirectory);

                var fileExtension = ".jpg";
                var savePath = fileSystemDirectory + "\\" + fileName + fileExtension;
                var tempFile = ResizeToWidthAndSaveImage(file, 648);

                SaveFlyerPhotoSatelliteImages(file, savePath);
                flyer.Photos[index] = Path.GetFileName(savePath);
                File.WriteAllBytes(savePath, tempFile);
            }
            else if (canDeleteFile)
            {
                Helper.ClearFileImagesFromDirectory(fileSystemDirectory);
                flyer.Photos[index] = null;
            }
        }

        public static String[] GetAllFlyerPhotoSatelliteSuffixes()
        {
            var result = new[]
                            {
                                FlyerImagesSatteliteSuffix_FullWidth,
                                FlyerImagesSatteliteSuffix_Width408_Height394,
                                FlyerImagesSatteliteSuffix_Width365_Height190,
                                FlyerImagesSatteliteSuffix_Width324_Height239,
                                FlyerImagesSatteliteSuffix_Width324_Height197,
                                FlyerImagesSatteliteSuffix_Width276_Height168,
                                FlyerImagesSatteliteSuffix_Width240_Height197
                            };

            return result;
        }

        public static void HandleCustomerTestimonialsFileImage(String fileName, String fileSystemDirectory, Stream fileStream)
        {
            var file = new Byte[fileStream.Length];

            if (fileStream.CanRead)
            {
                fileStream.Read(file, 0, (Int32)fileStream.Length);
            }

            var canHandle = fileSystemDirectory.HasText();

            if (!Directory.Exists(fileSystemDirectory))
            {
                Directory.CreateDirectory(fileSystemDirectory);
            }

            if (!canHandle)
            {
                return;
            }

            Helper.CheckImageFileExtension(file).ToLower();

            var fileExtension = ".jpg";
            var savePath = fileSystemDirectory + "/" + fileName + fileExtension;

            SaveCustomerTestimonialSatelliteImages(file, savePath);

            var tempFile = ResizeToWidthAndSaveImage(file, 512);

            File.WriteAllBytes(savePath, tempFile);
        }

        public static String GetZillowApiId()
        {
            return ConfigurationManager.AppSettings["ZillowApiId"];
        }

        public static String GetFlyerLayout(Int32? number, FlyerTypes flyerType)
        {
            String result = null;
            String format = null;
            var innerNumber = number;

            if (flyerType == FlyerTypes.Seller)
            {
                format = "L{0}";

                if (!(innerNumber > 0))
                {
                    innerNumber = 21;
                }
            }
            else if (flyerType == FlyerTypes.Buyer)
            {
                format = "B{0}";

                if (!(innerNumber > 0))
                {
                    innerNumber = 61;
                }
            }
            else if (flyerType == FlyerTypes.Custom)
            {
                format = "C{0}";

                if (!(innerNumber > 0))
                {
                    innerNumber = 1;
                }
            }

            if (innerNumber.HasValue && (!String.IsNullOrEmpty(format)))
            {
                result = String.Format(format, innerNumber.Value.ToString("D2"));
            }

            return result;
        }

        public static String GetFlyerMarkup(String layout)
        {
            String result = null;

            if (!String.IsNullOrEmpty(layout))
            {
                var url = "~/preview.aspx?markuponly=true&l=" + layout;

                result = GetPageMarkup(url);
            }

            return result;
        }

        public static String GetPageMarkup(String url)
        {
            String result = null;
            var server = HttpContext.Current != null ? HttpContext.Current.Server : null;

            if (server != null && (!String.IsNullOrEmpty(url)))
            {
                using (var sw = new StringWriter())
                {
                    server.Execute(url, sw);
                    result = sw.ToString();
                }
            }

            return result;
        }

        public static String GetFlyerType(FlyerTypes flyerType)
        {
            return flyerType.ToString().ToLower();
        }

        public static String GetPhotoPath(Int64 orderId, Int32 imageNumber, String photoFileName)
        {
            var orderMediaRelativePath = Helper.GetOrderMediaRelativePath();
            var result = String.Format("{0}{1}/photo{2}/{3}", orderMediaRelativePath, orderId.ToString(), imageNumber.ToString(), photoFileName);

            return result;
        }

        public static String GetFullPhotoPath(Int64 orderId, Int32 imageNumber, String photoFileName)
        {
            var result = GetPhotoPath(orderId, imageNumber, photoFileName);

            result = ToFullPath(result);

            return result;
        }

        public static String ToFullPath(String path)
        {
            var result = path;

            if ((!String.IsNullOrEmpty(result)) && result.StartsWith("~/"))
            {
                result = result.Remove(0, 2);
                result = (clsUtility.GetRootHost + result).ToLower();
            }

            return result;
        }

        public static String InjectNamePartToFileName(String path, String namePart)
        {
            var result = path.Replace(Path.GetFileName(path), String.Empty) + Path.GetFileNameWithoutExtension(path) + namePart + Path.GetExtension(path);

            return result;
        }

        public static String GetGoogleMapImageLink(String googlemapLink)
        {
            String result = null;

            try
            {
                var uri = new Uri(googlemapLink);

                if (uri.Host.IndexOf("google.com", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var d = new Dictionary<String, String>();
                    String googleQParameter = null;
                    String googleZoomParameter = null;

                    var query = uri.Query; 
                    
                    if (uri.Query.Length > 1 && uri.Query[0].Equals('?'))
                    {
                        query = query.Substring(1);
                    }

                    foreach (var queryComponents in query.Split('&'))
                    {
                        var keyValuePairs = queryComponents.Split('=');

                        if (keyValuePairs.Length > 1)
                        {
                            if (String.Compare(keyValuePairs[0], "q", true) == 0)
                            {
                                googleQParameter = keyValuePairs[1];
                            }
                            else if (String.Compare(keyValuePairs[0], "z", true) == 0)
                            {
                                googleZoomParameter = keyValuePairs[1];
                            }
                        }
                    }

                    googleZoomParameter = googleZoomParameter ?? "15";

                    if (!String.IsNullOrEmpty(googleQParameter))
                    {
                        result = String.Format("http://maps.googleapis.com/maps/api/staticmap?center={0}&zoom={1}&scale=false&size=640x348&maptype=roadmap&sensor=false&format=jpg&visual_refresh=true&markers=size:mid%7Ccolor:red%7C{2}", googleQParameter, googleZoomParameter, googleQParameter);
                    }
                }
            }
            catch
            {
            }

            return result;
        }

        public static void RespondWithJsonObject(Object @object, HttpResponse response)
        {
            var serializer = new JavaScriptSerializer();
            var stringResult = UTF8Encoding.Default.GetBytes(serializer.Serialize(@object));

            response.OutputStream.Write(stringResult, 0, stringResult.Length);
            response.ContentType = "application/json; charset=utf-8";
            response.End();
        }

        public static Object DeserializeJsonToObject(String value)
        {
            Object result = null;

            if (value.HasText())
            {
                var serializer = new JavaScriptSerializer();

                result = serializer.DeserializeObject(value);
            }

            return result;
        }

        public static T DeserializeJsonToObject<T>(String value) where T: class
        {
            T result = null;

            if (value.HasText())
            {
                var serializer = new JavaScriptSerializer();

                result = serializer.Deserialize<T>(value);
            }

            return result;
        }

        public static String SerializeToJsonFromObject(Object @object)
        {
            String result = null;

            if (@object != null)
            {
                var serializer = new JavaScriptSerializer();

                result = serializer.Serialize(@object);
            }

            return result;
        }

        public static Boolean IsTest()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static Order GetOrder(HttpRequest request, HttpResponse response, String username)
        {
            return GetOrder(request, null, response, username, false);
        }

        public static Order GetOrder(HttpRequest request, HttpResponse response)
        {
            return GetOrder(request, null, response, null, true);
        }

        public static Order GetOrder(String orderIdString, HttpResponse response, String username)
        {
            return GetOrder(null, orderIdString, response, username, false);
        }

        public static Order GetOrder(String orderIdString, HttpResponse response)
        {
            return GetOrder(null, orderIdString, response, null, true);
        }

        public static String GetFullAddress(FlyerMeDS.fly_orderRow order)
        {
            String result = null;

            if (!String.IsNullOrEmpty(order.prop_address1))
            {
                result = order.prop_address1.Trim();

                if (!String.IsNullOrEmpty(order.AptSuiteBldg))
                {
                    result += ", " + order.AptSuiteBldg;
                }
                if (!String.IsNullOrEmpty(order.prop_state))
                {
                    result += ", " + order.prop_state;
                }
                if (!String.IsNullOrEmpty(order.prop_zipcode))
                {
                    result += " " + order.prop_zipcode;
                }
            }

            return result;
        }

        public static String GetFullAddress(Order order)
        {
            String result = null;

            if (!String.IsNullOrEmpty(order.prop_address1))
            {
                result = order.prop_address1.Trim();

                if (!String.IsNullOrEmpty(order.AptSuiteBldg))
                {
                    result += ", " + order.AptSuiteBldg;
                }
                if (!String.IsNullOrEmpty(order.prop_state))
                {
                    result += ", " + order.prop_state;
                }
                if (!String.IsNullOrEmpty(order.prop_zipcode))
                {
                    result += " " + order.prop_zipcode;
                }
            }

            return result;
        }
        
        public static Boolean IsEmailInSpamList(String email)
        {
            Boolean result;
            var adapter = new FlyerMeDSv1TableAdapters.tblSpamFilterTableAdapter();
            var dtSpam = adapter.GetData(email);

            if (dtSpam.Rows.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static void InsertEmailInSpamList(String email)
        {
            if (!IsEmailInSpamList(email))
            {
                var adapter = new FlyerMeDSv1TableAdapters.tblSpamFilterTableAdapter();

                adapter.Insert(email, true, DateTime.Now);
            }
        }

        public static void RemoveFromSpamList(String email)
        {
            if (IsEmailInSpamList(email))
            {
                var adapter = new FlyerMeDSv1TableAdapters.tblSpamFilterTableAdapter();
                var dt = adapter.GetData(email);

                adapter.Delete((Int32)dt.Rows[0]["pk_SpamFilterID"]);
            }
        }

        public static void ParseContacts(String contacts, Int32 maxCount, out String[] emails, out String[] names, out String message)
        {
            emails = null;
            names = null;
            message = null;

            if (String.IsNullOrEmpty(contacts) || String.IsNullOrEmpty(contacts.Trim()))
            {
                message = "No email address provided.";
                return;
            }

            var helper = new Helper();
            var innerContacts = contacts.Trim();

            var rawString = innerContacts.Replace('"', ' ');
            var arraysize = helper.CharacterCounter(",", rawString) + 1;
            var firstSet = new String[arraysize];

            names = new String[maxCount];
            emails = new String[maxCount];

            firstSet = rawString.Split(',');

            if (rawString.IndexOf('<') > 0)
            {
                for (var i = 0; i < arraysize; i++)
                {
                    if (i > maxCount - 1) 
                    { 
                        break; 
                    }

                    if (firstSet[i].IndexOf('<') > 0)
                    {
                        var secondSet = firstSet[i].Split('<');

                        names[i] = secondSet[0].Trim();
                        emails[i] = secondSet[1].Replace(">", "").Trim();
                    }
                    else
                    {
                        names[i] = firstSet[i].Trim();
                        emails[i] = firstSet[i].Trim();
                    }
                }
            }
            else
            {
                names = firstSet.Select(fs => String.IsNullOrEmpty(fs) ? null : fs.Trim()).ToArray();
                emails = names;
            }

            for (var i = 0; i < emails.Length; i++)
            {
                if ((!String.IsNullOrEmpty(emails[i])) && (!String.IsNullOrEmpty(emails[i].Trim())))
                {
                    if (!Helper.IsEmail2(emails[i]))
                    {
                        message = "Email address " + emails[i] + " is not valid.";
                        emails = null;
                        names = null;
                        return;
                    }
                }
            }
        }

        public static String GetEncodedUrlParameter(String value)
        {
            return !String.IsNullOrEmpty(value) ? HttpContext.Current.Server.UrlPathEncode(value) : null;
        }

        public static String GetUrlEncodedString(String value)
        {
            return !String.IsNullOrEmpty(value) ? HttpContext.Current.Server.UrlEncode(value) : null;
        }

        public static String DecodeUrlParameter(String value)
        {
            return value.HasText() ? HttpContext.Current.Server.UrlDecode(value) : null;
        }

        public static void SetErrorResponse(HttpStatusCode statusCode, String statusDescription)
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                var response = context.Response;

                try
                {
                    response.StatusCode = (Int32)statusCode;
                    response.StatusDescription = statusDescription;
                    response.ClearContent();
                    response.TrySkipIisCustomErrors = true;
                    response.Write(GetPageMarkup("~/error.aspx"));
                    response.End();
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    response.ClearContent();
                    response.Write(ex.Message);

                    if (ex.InnerException != null)
                    {
                        response.Write("<br /> Inner exception: " + ex.InnerException.Message);
                    }
                }
            }
        }

        public static Int64 ConvertToUnixTimestamp(DateTimeOffset dateTime)
        {
            var origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 0, 0));
            var diff = dateTime - origin;

            return (Int64)Math.Floor(diff.TotalSeconds);
        }

        public static String RequestUrlAsync(String url)
        {
            var result = RequestUrlAsync(url, "GET", null, null);

            return result;
        }

        public static String RequestUrlAsync(String url, String httpMethod, String data, Dictionary<String, String> headers)
        {
            Byte[] bytes;
            var result = RequestUrlAsync(url, httpMethod, data, headers, out bytes, false);

            return result;
        }

        public static void RequestUrlAsync(String url, out Byte[] bytes)
        {
            RequestUrlAsync(url, null, null, null, out bytes, true);
        }

        public static String RequestUrlAsync(String url, String httpMethod, String data, Dictionary<String, String> headers, out Byte[] resultBytes, Boolean modeBinary)
        {
            String result = null;
            resultBytes = null;

            if (url.HasText())
            {
                var webRequest = WebRequest.Create(url);

                if (headers != null && headers.Count > 0)
                {
                    foreach (var h in headers)
                    {
                        if(WebHeaderCollection.IsRestricted(h.Key))
                        {
                            if (String.Compare(h.Key, "Content-Type", true) == 0)
                            {
                                webRequest.ContentType = h.Value;
                            }
                        }
                        else
                        {
                            webRequest.Headers.Add(h.Key, h.Value);
                        }
                    }
                }

                if (httpMethod.HasText())
                {
                    webRequest.Method = httpMethod;
                }

                if (data.HasText())
                {
                    using (var stream = webRequest.GetRequestStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(data);

                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var manualResetEvent = new ManualResetEvent(false))
                {
                    Exception ex = null;
                    Byte[] tempResultBytes = null;

                    webRequest.BeginGetResponse(ar =>
                    {
                        try
                        {
                            var wr = ar.AsyncState as WebRequest;

                            using (var response = wr.EndGetResponse(ar))
                            using (var responseStream = response.GetResponseStream())
                            {
                                var webRequestDataHandler = new WebRequestDataHandler(responseStream, modeBinary);

                                webRequestDataHandler.IsReading = true;
                                responseStream.BeginRead(webRequestDataHandler.Buffer, 0, webRequestDataHandler.Buffer.Length, ReadStreamAsync, webRequestDataHandler);

                                while (webRequestDataHandler.IsReading);

                                if (webRequestDataHandler.ModeBinary)
                                {
                                    tempResultBytes = webRequestDataHandler.Bytes;
                                }
                                else
                                {
                                    result = webRequestDataHandler.StringBuilder.ToString();
                                }

                                ex = webRequestDataHandler.Exception;
                            }
                        }
                        catch (WebException wex)
                        {
                            try
                            {
                                using (var response = wex.Response)
                                using (var responseStream = response.GetResponseStream())
                                {
                                    var webRequestDataHandler = new WebRequestDataHandler(responseStream, false);

                                    webRequestDataHandler.IsReading = true;
                                    responseStream.BeginRead(webRequestDataHandler.Buffer, 0, webRequestDataHandler.Buffer.Length, ReadStreamAsync, webRequestDataHandler);

                                    while (webRequestDataHandler.IsReading);

                                    result = webRequestDataHandler.StringBuilder.ToString();
                                    ex = webRequestDataHandler.Exception;
                                }
                            }
                            catch (Exception innerEx)
                            {
                                ex = innerEx;
                            }
                        }
                        catch (Exception ex2)
                        {
                            ex = ex2;
                        }
                        finally
                        {
                            manualResetEvent.Set();
                        }
                    }, webRequest);
#if DEBUG
                    manualResetEvent.WaitOne(new TimeSpan(0, 0, 59));
#else
                    manualResetEvent.WaitOne(new TimeSpan(0, 0, 5));
#endif
                    resultBytes = tempResultBytes;

                    if (ex != null)
                    {
                        throw ex;
                    }
                    else if (result.HasNoText() && resultBytes == null)
                    {
                        throw new TimeoutException("Time out requesting url (" + url + "). Please repeat operation.");
                    }
                }
            }

            return result;
        }

        #region private

        private static Order GetOrder(HttpRequest request, String orderIdString, HttpResponse response, String username, Boolean ignoreUserName)
        {
            if (request == null && String.IsNullOrEmpty(orderIdString))
            {
                throw new Exception("Parameter cannot be null.");
            }

            Int64 @int;
            String innerOrderIdString = null;

            if (request != null)
            {
                innerOrderIdString = !String.IsNullOrEmpty(request["orderid"]) ? request["orderid"] : request["oid"];

                if (String.IsNullOrEmpty(innerOrderIdString))
                {
                    SetErrorResponse(HttpStatusCode.BadRequest, "Order ID is required.");
                }

                if (!Int64.TryParse(innerOrderIdString, out @int))
                {
                    SetErrorResponse(HttpStatusCode.BadRequest, "Order ID format is incorrect.");
                }
            }
            else if (!String.IsNullOrEmpty(orderIdString))
            {
                innerOrderIdString = orderIdString;

                if (!Int64.TryParse(innerOrderIdString, out @int))
                {
                    SetErrorResponse(HttpStatusCode.BadRequest, "Order ID format is incorrect.");
                }
            }
            else
            {
                @int = 0;
            }

            var orderId = (Int32)@int;
            var result = new Order(orderId);

            if (result.order_id == 0)
            {
                SetErrorResponse(HttpStatusCode.NotFound, String.Format("Order with ID {0} was not found.", orderId.ToString()));
            }

            if (String.IsNullOrEmpty(username) && (!ignoreUserName))
            {
                throw new Exception("username parameter is required.");
            }

            if ((!ignoreUserName) && String.Compare(result.customer_id, username, true) != 0)
            {
                SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Access to order with ID {0} is denied.", orderId.ToString()));
            }

            return result;
        }

        private static void ReadStreamAsync(IAsyncResult ar)
        {
            var wrdh = ar.AsyncState as WebRequestDataHandler;

            try
            {
                var readBytesCount = wrdh.ResponseStream.EndRead(ar);

                if (readBytesCount > 0)
                {
                    if (wrdh.ModeBinary)
                    {
                        wrdh.ReadBufferIntoBytes(readBytesCount);
                    }
                    else
                    {
                        wrdh.StringBuilder.Append(Encoding.UTF8.GetString(wrdh.Buffer, 0, readBytesCount));
                    }

                    wrdh.ResponseStream.BeginRead(wrdh.Buffer, 0, wrdh.Buffer.Length, ReadStreamAsync, wrdh);
                }
                else
                {
                    wrdh.IsReading = false;
                }
            }
            catch (Exception ex)
            {
                wrdh.Exception = ex;
                wrdh.IsReading = false;
            }
        }
        private static String GetImageFileExtension(ImageFormat format)
        {
            try
            {
                return ImageCodecInfo.GetImageEncoders()
                                        .First(x => x.FormatID == format.Guid)
                                        .FilenameExtension
                                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                        .First()
                                        .Trim('*')
                                        .ToLower();
            }
            catch (Exception)
            {
                return ".IDFK";
            }
        }

        private static void SaveAvatarSatelliteImages(Byte[] file, String savePath)
        {
            var resizedFile = ResizeToWidthAndSaveImage(file, 138);
            var avatarForFlyerSavePath = Helper.InjectNamePartToFileName(savePath, ProfileImagesSatteliteSuffix_Flyer);

            File.WriteAllBytes(avatarForFlyerSavePath, resizedFile);

            var croppedFile = CropToCircleAndSaveImage(file);

            resizedFile = ResizeToWidthAndSaveImage(croppedFile, 138, ImageFormat.Png);
            File.WriteAllBytes(InjectNamePartToFileName(savePath, ProfileImagesSatteliteSuffix_Flyer_Ellipse).Replace(".jpg", ".png"), resizedFile);
        }

        private static void SaveCustomerTestimonialSatelliteImages(Byte[] file, String savePath)
        {
            var resizedFile = ResizeAndSaveImage(file, 200, 200);
            var ctFlyerSavePath = Helper.InjectNamePartToFileName(savePath, CustomerTestimonialImagesSatteliteSuffix_RESIZED);

            File.WriteAllBytes(ctFlyerSavePath, resizedFile);
        }

        private static Byte[] CropToCircleAndSaveImage(Byte[] file)
        {
            Byte[] result = null;

            using (var ms = new MemoryStream(file))
            using (var srcImage = Bitmap.FromStream(ms))
            using (var dstImage = new Bitmap(srcImage.Width, srcImage.Height, PixelFormat.Format32bppArgb))
            using (var g = Graphics.FromImage(dstImage))
            {
                var backGround = Color.Transparent;

                using (var br = new SolidBrush(backGround))
                {
                    g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                }

                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
                    g.SetClip(path);
                    g.DrawImage(srcImage, 0, 0);
                    result = SaveBitmapToPngBytes(dstImage);
                }
            }

            return result;
        }

        private static void SaveCompanyLogoSatelliteImages(Byte[] file, String savePath)
        {
            var resizedFile = ResizeToWidthAndSaveImage(file, 208);
            var companyLogoForFlyerSavePath = Helper.InjectNamePartToFileName(savePath, ProfileImagesSatteliteSuffix_Flyer);

            File.WriteAllBytes(companyLogoForFlyerSavePath, resizedFile);
        }

        private static void SaveFlyerPhotoSatelliteImages(Byte[] file, String savePath)
        {
            var resizedFile = ResizeAndSaveImage(file, 648, 0);
            var resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_FullWidth);

            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 408, 394);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width408_Height394);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 365, 190);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width365_Height190);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 324, 239);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width324_Height239);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 324, 197);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width324_Height197);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 276, 168);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width276_Height168);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);

            resizedFile = ResizeAndSaveImage(file, 240, 197);
            resiedFlyerPhotoSavePath = Helper.InjectNamePartToFileName(savePath, FlyerImagesSatteliteSuffix_Width240_Height197);
            File.WriteAllBytes(resiedFlyerPhotoSavePath, resizedFile);
        }

        private static Byte[] ResizeToWidthAndSaveImage(Byte[] file, Int32 targetWidth)
        {
            return ResizeToWidthAndSaveImage(file, targetWidth, ImageFormat.Jpeg);
        }

        private static Byte[] ResizeToWidthAndSaveImage(Byte[] file, Int32 targetWidth, ImageFormat imageFormat)
        {
            var result = file;

            using (var ms = new MemoryStream(result))
            using (var bm = Bitmap.FromStream(ms))
            {
                if (bm.Width > targetWidth)
                {
                    var targetHeight = (Int32)((Double)bm.Height / ((Double)bm.Width / (Double)targetWidth));

                    using (var rbm = new Bitmap(bm, targetWidth, targetHeight))
                    {
                        if (imageFormat == ImageFormat.Jpeg)
                        {
                            result = SaveBitmapToJpegBytes(rbm);
                        }
                        else if (imageFormat == ImageFormat.Png)
                        {
                            result = SaveBitmapToPngBytes(rbm);
                        }
                    }
                }
                else
                {
                    using (var rbm = new Bitmap(bm))
                    {
                        if (imageFormat == ImageFormat.Jpeg)
                        {
                            result = SaveBitmapToJpegBytes(rbm);
                        }
                        else if (imageFormat == ImageFormat.Png)
                        {
                            result = SaveBitmapToPngBytes(rbm);
                        }
                    }
                }
            }

            return result;
        }

        private static Byte[] ResizeAndSaveImage(Byte[] file, Int32 targetWidth, Int32 targetHeight)
        {
            var result = file;

            using (var ms = new MemoryStream(result))
            using (var bm = Bitmap.FromStream(ms))
            {
                var tempBm = bm as Bitmap;

                if (bm.Width != targetWidth || bm.Height != targetHeight)
                {
                    var resizedHeight = (Int32)((Double)bm.Height / ((Double)bm.Width / (Double)targetWidth));

                    if (resizedHeight == targetHeight || targetHeight == 0)
                    {
                        using (var rbm = new Bitmap(bm, targetWidth, resizedHeight))
                        {
                            result = SaveBitmapToJpegBytes(rbm);
                        }
                    }
                    else if (resizedHeight > targetHeight)
                    {
                        using (var rbm = new Bitmap(bm, targetWidth, resizedHeight))
                        {
                            var bounds = new Rectangle(new Point(0, (Int32)((resizedHeight - targetHeight) / 2D)), new Size(targetWidth, targetHeight));

                            using (var rbm2 = CropImage(rbm, bounds))
                            {
                                result = SaveBitmapToJpegBytes(rbm2);
                            }
                        }
                    }
                    else if (resizedHeight < targetHeight)
                    {
                        var factor = (Double)targetHeight / (Double)resizedHeight;
                        var resizedWidth = (Int32)(targetWidth * factor);

                        using (var rbm = new Bitmap(bm, resizedWidth, targetHeight))
                        {
                            var bounds = new Rectangle(new Point((Int32)((resizedWidth - targetWidth) / 2D), 0), new Size(targetWidth, targetHeight));

                            using (var rbm2 = CropImage(rbm, bounds))
                            {
                                result = SaveBitmapToJpegBytes(rbm2);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static Bitmap CropImage(Bitmap image, Rectangle bounds)
        {
            var result = image;            

            try
            {
                result = image.Clone(bounds, image.PixelFormat);
            }
            catch (OutOfMemoryException)
            {
                using (var targetImage = new Bitmap(bounds.Width, bounds.Height))
                using (var graphics = Graphics.FromImage(targetImage))
                {
                    graphics.DrawImage(image, new Rectangle(0, 0, targetImage.Width, targetImage.Height), bounds, GraphicsUnit.Pixel);
                    result = targetImage;
                }
            }

            return result;
        }

        private static Byte[] SaveBitmapToJpegBytes(Bitmap bm)
        {
            Byte[] result;

            using (var outputMemoryStream = new MemoryStream())
            {
                var jpegCodec = GetEncoder(ImageFormat.Jpeg);
                var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                var jpegEncoderParameters = new EncoderParameters(1);
                var qualityEncoderParameter = new EncoderParameter(qualityEncoder, 100L);

                jpegEncoderParameters.Param[0] = qualityEncoderParameter;
                bm.Save(outputMemoryStream, jpegCodec, jpegEncoderParameters);
                result = outputMemoryStream.ToArray();
            }

            return result;
        }

        private static Byte[] SaveBitmapToPngBytes(Bitmap bm)
        {
            Byte[] result;

            using (var outputMemoryStream = new MemoryStream())
            {
                bm.Save(outputMemoryStream, ImageFormat.Png);
                result = outputMemoryStream.ToArray();
            }

            return result;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            var codecs = ImageCodecInfo.GetImageDecoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        #endregion
    }
}
