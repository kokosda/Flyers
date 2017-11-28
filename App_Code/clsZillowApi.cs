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

/// <summary>
/// Summary description for clsZillowApi
/// </summary>
public class clsZillowApi
{
	public clsZillowApi()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DataTable _editedFacts;

    public DataTable editedFacts
    {
        get
        {
            return _editedFacts;
        }
        set
        {
            _editedFacts = value;

        }
    }

    private string _homeDescription;

    public string homeDescription
    {
        get
        {
            return _homeDescription;

        }
        set
        {
            _homeDescription = value;

        }
    }
    private string _Error;
    public string Error
    {

        get
        {
            return _Error;

        }
        set
        {
            _Error = value;

        }
    }
       
    

    public bool GetZpid(string ZwsId, string Address, string City, string State, string ZipCode)
    {
        DataSet Result;
        string url = "http://www.zillow.com/webservice/GetSearchResults.htm?";
        string XML = "zws-id=" + ZwsId + "&address=" + Address + "&citystatezip=" + City + "+" + State + "+" + ZipCode;
        Result = GetAPIInformation(url + XML);
        if (Result.Tables["message"] != null)
        {
            if (Result.Tables["message"].Rows[0]["code"].ToString() == "0")
            {

                return GetAddressInformation("X1-ZWz1cprct1fw23_3if65", Result.Tables["result"].Rows[0]["zpid"].ToString());
               
            }
            else
            {
                Error=Result.Tables["message"].Rows[0]["text"].ToString();
          
                return false;
            }
        }
        else
        {

        }
        return false;
    }

    public bool GetAddressInformation(string ZwsId, string zpid)
    {
        DataSet Result;
        string url = "http://www.zillow.com/webservice/GetUpdatedPropertyDetails.htm?";
        string XML = "zws-id=" + ZwsId + "&zpid=" + zpid;
        Result = GetAPIInformation(url + XML);
        if (Result.Tables["message"] != null)
        {
            if (Result.Tables["message"].Rows[0]["code"].ToString() == "0")
            {
                editedFacts = Result.Tables["editedFacts"];
                homeDescription = Result.Tables["response"].Rows[0]["homeDescription"].ToString();
                return true;

            }
            else
            {
                Error = Result.Tables["message"].Rows[0]["text"].ToString();
                return false;
            }


        }
        return false;

    }

    public bool GetPropertyAttributes(string ZwsId, string Address, string City, string State, string ZipCode)
    {
        DataSet Result;
        string url = "http://www.zillow.com/webservice/GetDeepSearchResults.htm?";
        string XML = "zws-id=" + ZwsId + "&address=" + Address + "&citystatezip=" + City + "+" + State + "+" + ZipCode;
        Result = GetAPIInformation(url + XML);
        if (Result.Tables["message"] != null)
        {
            if (Result.Tables["message"].Rows[0]["code"].ToString() == "0")
            {
                editedFacts = Result.Tables["result"];
                //homeDescription = Result.Tables["response"].Rows[0]["homeDescription"].ToString();
                return true;

            }
            else
            {
                Error = Result.Tables["message"].Rows[0]["text"].ToString();
                return false;
            }
        }
        else
        {

        }
        return false;
    }

    public DataSet GetAPIInformation(string URL)
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
