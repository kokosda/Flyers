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
/// Summary description for AdminDAL
/// </summary>
public class AdminDAL : BaseDataAccess
{
    public AdminDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString();
        }
    }

    public DataTable GetUserNote(string UserId)
    {
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        DataTable dt = new DataTable();
        try
        {
            cmd = this.CreateCommand(CreateGetUserNote(UserId), "usp_GetUserNote");
            dr = this.ExecuteReader(cmd);
            dt.Load(dr);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Close(cmd);
        }
        return dt;
    }

    private SqlParameter[] CreateGetUserNote(string UserId)
    {
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("@UserId", UserId);
        return parameters;
    }

    public int SaveUserNote(string UserId, string Note)
    {
        SqlCommand cmd = null;
        int result = 0;
        try
        {
            cmd = this.CreateCommand(CreateSaveUserNote(UserId, Note), "usp_SaveUserNote");
            result = this.ExecuteNonQuery(cmd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Close(cmd);
        }
        return result;
    }

    private SqlParameter[] CreateSaveUserNote(string UserId, string Note)
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@UserId", UserId);
        parameters[1] = new SqlParameter("@Note", Note);
        return parameters;
    }

}
