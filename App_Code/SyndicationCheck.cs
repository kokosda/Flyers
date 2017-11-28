using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for SyndicationCheck
/// </summary>
public class SyndicationCheck
{
	public SyndicationCheck()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string checkGoogleBase(string strAddress, string strCity, string strState, string strZip)
    {
        //Sorry, there are no items that match your search.
        strAddress = strAddress.Replace(" ", "+");
        strAddress = strAddress.Replace(",", "%2C");

        strCity = strCity.Replace(" ", "+");
        strCity = strCity.Replace(",", "%2C");

        strState = strState.Replace(" ", "+");
        strState = strState.Replace(",", "%2C");

        strZip = strZip.Replace(" ", "+");
        strZip = strZip.Replace(",", "%2C");

        string strQuery = strAddress + "%2C" + strCity + "%2C" + strState + "%2C" + strZip;

        string strSearchURL = "http://www.google.com/base/rss?q=" + strQuery + "&view=list&hl=en&gl=US";

        DataSet dsResult = GetXML(strSearchURL);

        if (dsResult.Tables.Count > 2)
        {
            if (dsResult.Tables["item"].Rows.Count <= 0)
            {
                strSearchURL = "";
            }
            else
            {
                strSearchURL = "http://www.google.com/base/s2?q=" + strQuery + "&view=list&hl=en&gl=US";
            }
        }
        else
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public string checkVast(string strAddress, string strCity, string strState, string strZip, string Price)
    {
        //http://www.vast.com/real_estate/location-2354-E-Karalee-Way--Sandy--UT-84092/price-269000.269000
        strAddress = strAddress.Replace(" ", "-");
        strAddress = strAddress.Replace(",", "-");

        strCity = strCity.Replace(" ", "-");
        strCity = strCity.Replace(",", "-");

        strState = strState.Replace(" ", "-");
        strState = strState.Replace(",", "-");

        strZip = strZip.Replace(" ", "-");
        strZip = strZip.Replace(",", "-");

        string strQuery = strAddress + "-" + strCity + "-" + strState + "-" + strZip;

        if (Price == "")
        {
            Price = "9999999";
        }

        string strSearchURL = "http://www.vast.com/rss/real_estate/location-" + strQuery + "/price-" + Price + "." + Price;

        DataSet dsResult = GetXML(strSearchURL);

        if (dsResult.Tables["title"] != null)
        {
            if (dsResult.Tables["title"].Rows.Count <= 1)
            {
                strSearchURL = "";
            }
            else
            {
                strSearchURL = "http://www.vast.com/real_estate/location-" + strQuery + "/price-" + Price + "." + Price;
            }
        }
        else
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public string checkOodle(string strAddress, string strCity, string strState, string strZip, string Price)
    {
        //http://www.oodle.com/housing/sale/-/price_268999_269001/-/84092/+0/?q=2354+E+Karalee+Way&oldq=2354+E.+Karalee+Way&inbs=1
        strAddress = strAddress.Replace(" ", "+");
        strAddress = strAddress.Replace(",", "+");

        strCity = strCity.Replace(" ", "+");
        strCity = strCity.Replace(",", "+");

        strState = strState.Replace(" ", "+");
        strState = strState.Replace(",", "+");

        strZip = strZip.Replace(" ", "+");
        strZip = strZip.Replace(",", "+");

        string strQuery = strAddress + "+" + strCity + "+" + strState + "+" + strZip;

        if (Price == "")
        {
            Price = "9999999";
        }
        else
        {
            Price = Price.Replace(",", "");
            Price = Price.Replace("$", "");
        }

        double dblPrice = Convert.ToDouble(Price);

        double lowPrice = dblPrice - 1;
        double highPrice = dblPrice + 1;

        string strSearchURL = "http://www.oodle.com/housing/sale/-/price_" + lowPrice + "_" + highPrice + "/-/" + strZip + "/+0/?q=" + strQuery + "&v=rss&sc=housing%2Fsale";

        DataSet dsResult = GetXML(strSearchURL);

        if (dsResult.Tables["item"] != null)
        {
            if (dsResult.Tables["item"].Rows.Count <= 0)
            {
                strSearchURL = "";
            }
            else
            {
                strSearchURL = "http://www.oodle.com/housing/sale/-/price_" + lowPrice + "_" + highPrice + "/-/" + strZip + "/+0/?q=" + strQuery + "&sc=housing%2Fsale";
            }
        }
        else
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public string checkLycos(string strAddress, string strCity, string strState, string strZip, string Price)
    {
        //http://lycos.oodle.com/housing/sale/-/price_268999_269001/-/84092/+0/?q=2354+E+Karalee+Way&oldq=2354+E.+Karalee+Way&inbs=1
        strAddress = strAddress.Replace(" ", "+");
        strAddress = strAddress.Replace(",", "+");

        strCity = strCity.Replace(" ", "+");
        strCity = strCity.Replace(",", "+");

        strState = strState.Replace(" ", "+");
        strState = strState.Replace(",", "+");

        strZip = strZip.Replace(" ", "+");
        strZip = strZip.Replace(",", "+");

        string strQuery = strAddress + "+" + strCity + "+" + strState + "+" + strZip;

        if (Price == "")
        {
            Price = "9999999";
        }
        else
        {
            Price = Price.Replace(",", "");
            Price = Price.Replace("$", "");
        }

        double dblPrice = Convert.ToDouble(Price);

        double lowPrice = dblPrice - 1;
        double highPrice = dblPrice + 1;

        string strSearchURL = "http://lycos.oodle.com/housing/sale/-/price_" + lowPrice + "_" + highPrice + "/-/" + strZip + "/+0/?q=" + strQuery + "&v=rss&sc=housing%2Fsale";

        DataSet dsResult = GetXML(strSearchURL);

        if (dsResult.Tables["item"] != null)
        {
            if (dsResult.Tables["item"].Rows.Count <= 0)
            {
                strSearchURL = "";
            }
            else
            {
                strSearchURL = "http://www.oodle.com/housing/sale/-/price_" + lowPrice + "_" + highPrice + "/-/" + strZip + "/+0/?q=" + strQuery + "&sc=housing%2Fsale";
            }
        }
        else
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public string checkZillow(string strAddress, string strCity, string strState, string strZip, string Price)
    {
        //http://www.zillow.com/homes/map/2354-E.-Karalee-Way,-Sandy,-Utah-84092_rb/
        strAddress = strAddress.Replace(" ", "-");

        strCity = strCity.Replace(" ", "-");

        strState = strState.Replace(" ", "-");

        strZip = strZip.Replace(" ", "-");

        string strQuery = strAddress + ",-" + strCity + ",-" + strState + "-" + strZip;

        string strSearchURL = "http://www.zillow.com/homes/map/" + strQuery + "_rb/";

        WebClient objWebClient = new WebClient();
        UTF8Encoding objUTF8 = new UTF8Encoding();

        string strContents = objUTF8.GetString(objWebClient.DownloadData(strSearchURL));

        if (strContents.Contains("We could not find this area."))
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public string checkTrulia(string strAddress, string strCity, string strState, string strZip, string Price)
    {
        //http://www.trulia.com/for_sale/1436_38th_Ave,Oakland,CA,94601_addr/fs,s_pt/
        //http://www.trulia.com/for_sale/37.74649,37.80432,-122.24189,-122.19618_xy/37.775407,-122.219037,1436_38th_Ave,Oakland,CA,94601_addr/fs,s_pt/
        strAddress = strAddress.Replace(" ", "_");

        strCity = strCity.Replace(" ", "_");

        strState = strState.Replace(" ", "_");

        strZip = strZip.Replace(" ", "_");

        string strQuery = strAddress + "," + strCity + "," + strState + "," + strZip;

        string strSearchURL = "http://www.trulia.com/for_sale/" + strQuery + "_rb/";

        WebClient objWebClient = new WebClient();
        UTF8Encoding objUTF8 = new UTF8Encoding();

        string strContents = objUTF8.GetString(objWebClient.DownloadData(strSearchURL));

        if (strContents.Contains("No properties for sale matched your search"))
        {
            strSearchURL = "";
        }
        else if (strContents.Contains("strAddress"))
        { 

        }

        return strSearchURL;
    }

    public string checkPropBot(string strAddress)
    {
        //http://www.propbot.com/search.php?ts=rfs&st=2354+E+Karalee
        strAddress = strAddress.Replace(" ", "+");
        strAddress = strAddress.Replace(",", "+");

        //strCity = strCity.Replace(" ", "+");
        //strCity = strCity.Replace(",", "+");

        //strState = strState.Replace(" ", "+");
        //strState = strState.Replace(",", "+");

        //strZip = strZip.Replace(" ", "+");
        //strZip = strZip.Replace(",", "+");

        string strQuery = strAddress;

        string strSearchURL = "http://www.propbot.com/out/xmlpropfeed.php?ts=rfs&st=" + strQuery;

        DataSet dsResult = GetXML(strSearchURL);

        if (dsResult.Tables["item"] != null)
        {
            if (dsResult.Tables["item"].Rows.Count <= 0)
            {
                strSearchURL = "";
            }
            else
            {
                strSearchURL = "http://www.propbot.com/search.php?ts=rfs&st=" + strQuery;
            }
        }
        else
        {
            strSearchURL = "";
        }

        return strSearchURL;
    }

    public DataSet GetXML(string URL)
    {
        DataSet RatesAPI = new DataSet();
        try
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.Method = "Get";
            WebResponse webResponse = httpWebRequest.GetResponse();
            Stream streamResponse = webResponse.GetResponseStream();
            RatesAPI.ReadXml(streamResponse);
            streamResponse.Close();
        }
        catch
        {

        }
        return RatesAPI;
    }

}
