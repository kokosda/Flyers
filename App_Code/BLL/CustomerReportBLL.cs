using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CustomerReportBLL
/// </summary>
public class CustomerReportBLL
{
	public CustomerReportBLL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetEmailDeliveryCustomerReport(string OrderId, string Customer_ID)
    {
        DataTable dtOrderReport = new DataTable();
        try
        {
            CustomerReportDAL customerReport = new CustomerReportDAL();
            dtOrderReport = customerReport.GetEmailDeliveryCustomerReport(OrderId, Customer_ID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dtOrderReport;
    }
}
