using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FlyerMe.DAL;

/// <summary>
/// Summary description for clsShortURL
/// </summary>
/// 
namespace FlyerMe.BLL
{
    public class clsShortURL
    {
        public clsShortURL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int saveURLDetail(string OriginalURL, string Directory)
        {
            try
            {
                ShortURL shortURL = new ShortURL();
                return shortURL.SaveURLDetail(OriginalURL, Directory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getURL(string OriginalURL)
        {
            DataTable dt = new DataTable();
            try
            {
                ShortURL shortURL = new ShortURL();
                dt = shortURL.getURL(OriginalURL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable getURLByID(string pk_URLID)
        {
            DataTable dt = new DataTable();
            try
            {
                ShortURL shortURL = new ShortURL();
                dt = shortURL.getURLByID(pk_URLID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

    }
}
