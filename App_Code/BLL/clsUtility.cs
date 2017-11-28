using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
/// <summary>
/// Summary description for clsData
/// </summary>
public class clsUtility
{
    private static string RootURL = HttpContext.Current.Request.ApplicationPath;
    //private static string RootHost = "http://" + HttpContext.Current.Request.Url.Host + RootURL + "/";
    private static string RootHost = ConfigurationManager.AppSettings["SiteRoot"].ToString();

    private static CultureInfo culture;
    private static CultureInfo originalCulture;

    static clsUtility()
    {
        culture = new CultureInfo("en-US", true);
        culture.NumberFormat.CurrencyGroupSeparator = " ";
        culture.NumberFormat.CurrencySymbol = "$ ";
        culture.NumberFormat.NumberGroupSeparator = ",";

        originalCulture = CultureInfo.GetCultureInfo("en-US");
    }

    public clsUtility()
    {

    }

    public static string GetRootUrl
    {
        get
        {
            return RootHost;
        }
    }

    public static string GetRootHost
    {
        get
        {
            return RootHost;
        }
    }
    public static string UploadFile(string myPath, HtmlInputFile fileControl)
    {
        if (!Directory.Exists(myPath))
        {
            Directory.CreateDirectory(myPath + "\\");
        }
        string strFileName = fileControl.PostedFile.FileName;
        string fileExist = System.IO.Path.GetFileName(strFileName);
        try
        {
            fileControl.PostedFile.SaveAs(myPath + "\\" + fileExist);
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
        return (myPath + "\\" + fileExist);
    }

    public void Mail(string subject, string body, string MailTO)
    {
        System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        System.Net.Configuration.MailSettingsSectionGroup settings = ((System.Net.Configuration.MailSettingsSectionGroup)(config.GetSectionGroup("system.net/mailSettings")));
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(settings.Smtp.Network.UserName, MailTO);
        mail.Body = body;
        mail.Subject = subject;
        mail.IsBodyHtml = true;
        System.Net.Mail.SmtpClient o = new System.Net.Mail.SmtpClient(settings.Smtp.Network.Host);
        o.Credentials = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
        try
        {
            o.Send(mail);
        }
        catch (Exception error)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + error.Message + "!</td></tr></table>");

        }
    }

    public static CultureInfo Culture
    {
        get
        {
            return culture;
        }
    }

    public static CultureInfo OriginalCulture
    {
        get
        {
            return originalCulture;
        }
    }

    public static String SiteHttpWwwRootUrl
    {
        get
        {
            return ConfigurationManager.AppSettings["SiteHttpWwwRootUrl"];
        }
    }

    public static String SiteTopLevelDomain
    {
        get
        {
            return ConfigurationManager.AppSettings["SiteTopLevelDomain"];
        }
    }

    public static String SiteWwwDomain
    {
        get
        {
            return ConfigurationManager.AppSettings["SiteWwwDomain"];
        }
    }

    public static String SiteBrandName
    {
        get
        {
            return ConfigurationManager.AppSettings["SiteBrandName"];
        }
    }

    public static String ProjectName
    {
        get
        {
            return ConfigurationManager.AppSettings["ProjectName"];
        }
    }

    public static String ProjectNameInLowerCase
    {
        get
        {
            return ConfigurationManager.AppSettings["ProjectNameInLowerCase"];
        }
    }

    public static String ContactUsEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["ContactUsEmail"];
        }
    }

    public static String DeliveryServiceFromName
    {
        get
        {
            return ConfigurationManager.AppSettings["DeliveryServiceFromName"];
        }
    }

    public static String AbuseEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["AbuseEmail"];
        }
    }

    public static String StripePublishableApiKey
    {
        get
        {
            return ConfigurationManager.AppSettings["StripePublishableApiKey"];
        }
    }

    public static String IntercomIoApiKey
    {
        get
        {
            return ConfigurationManager.AppSettings["IntercomIoApiKey"];
        }
    }
}
