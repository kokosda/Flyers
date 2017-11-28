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
/// Summary description for CustomerReport
/// </summary>
public class CustomerReportDAL: BaseDataAccess
{
    public CustomerReportDAL()
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

    public DataTable GetEmailDeliveryCustomerReport(string OrderId, string Customer_ID)
    {
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        DataTable dt = new DataTable();
        try
        {
            cmd = this.CreateCommand(CreateGetEmailDeliveryCustomerReport(OrderId, Customer_ID), "usp_GetEmailDeliveryCustomerReport");
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

    private SqlParameter[] CreateGetEmailDeliveryCustomerReport(string OrderId, string Customer_ID)
    {
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@Order_ID", OrderId);
        parameters[1] = new SqlParameter("@Customer_ID", "'" + Customer_ID + "'");
        parameters[2] = new SqlParameter("@ProjectDbo", ConfigurationManager.AppSettings["ProjectDbo"] + ".");
        return parameters;
    }

}
