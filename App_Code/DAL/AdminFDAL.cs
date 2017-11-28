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
public class AdminFDAL : BaseDataAccess
{
    public AdminFDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected override string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ToString();
        }
    }

    public int AddPriority(string order_id, string priority, string delivery_app)
    {
        SqlCommand cmd = null;
        int result = 0;
        try
        {
            cmd = this.CreateCommand(CreateAddPriority(order_id, priority, delivery_app), "usp_AddPriority");
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

    private SqlParameter[] CreateAddPriority(string order_id, string priority, string delivery_app)
    {
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@order_id", order_id);
        parameters[1] = new SqlParameter("@priority", priority);
        parameters[2] = new SqlParameter("@delivery_app", delivery_app);
        return parameters;
    }

}
