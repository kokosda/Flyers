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
/// Summary description for clsMisc
/// </summary>
public class clsMisc
{
	public clsMisc()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getNewsAndUpdates()
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_selectActiveNewsAndUpdates";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            dr.Dispose();
        }
        catch
        {
            //code here
        }
        finally 
        {
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }

    public DataTable getNewsAndUpdatesByID(string pk_NewsID)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_selectActiveNewsAndUpdatesByID";
            cmd.Parameters.AddWithValue("pk_NewsID", pk_NewsID);
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            dr.Dispose();
        }
        catch
        {

        }
        finally
        {
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }

    public DataTable getCustomerTestimonials()
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (conn.State != ConnectionState.Open) { conn.Open(); }
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_GetTestimonials";
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            dr.Close();
            dr.Dispose();
        }
        catch
        {

        }
        finally
        {
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return dt;
    }

}
