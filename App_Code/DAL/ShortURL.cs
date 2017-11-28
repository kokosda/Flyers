using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ShortURL
/// </summary>
/// 
namespace FlyerMe.DAL
{
    public class ShortURL
    {
        public ShortURL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SaveURLDetail(string OriginalURL, string Directory)
        {
            int result = -1;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_SaveURL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OriginalURL", OriginalURL);
                cmd.Parameters.AddWithValue("@Directory", Directory);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable getURL(string OriginalURL)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
            SqlDataReader dr = null;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetURL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OriginalURL", OriginalURL);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable getURLByID(string pk_URLID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
            SqlDataReader dr = null;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_GetURLByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pk_URLID", pk_URLID);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
    
    }
}
