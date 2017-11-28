using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
/// <summary>
/// Summary description for clsMblog
/// </summary>
public class clsFlyers
{
    public clsFlyers()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int BindFlyers(DataList DL, int PageNo, string SortBy, string SearchBy, string AddressSearch, string City, string State)
    {
        int intTotalRows = 0;
        PageNo = PageNo + 1;
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("TableName", "viewOrders");
            ht.Add("TableIDField", "order_id");
            ht.Add("SortByField", SortBy);
            ht.Add("SearchIn", "invoice_transaction_id <> '' and type='seller' and prop_address1 like '%" + AddressSearch + "%' and prop_city like '" + City + "%' and prop_state like '" + State + "%' and (prop_zipcode)");
            ht.Add("SearchBy", SearchBy.Replace("'",""));
            ht.Add("PageSize", 8);
            ht.Add("PageNo", PageNo);
            DataSet ds = dataLayerObj.GetDataSetWithoutReplacingQute("usp_Paging", ht);
            if (ds.Tables.Count > 1)
            {
                DataTable DT = ds.Tables[1];
                intTotalRows = (int)ds.Tables[0].Rows[0]["TotalRows"];
                clsUI.BindData(DL, DT);
            }
            else
            {
                //HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>No Record found!</td></tr></table>");
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
        return intTotalRows;
    }

    public static int BindFlyers(Repeater rpt, Int32 PageNo, String SortBy, String SearchBy, String AddressSearch, String City, String State)
    {
        int intTotalRows = 0;
        PageNo = PageNo + 1;
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("TableName", "viewOrders");
            ht.Add("TableIDField", "order_id");
            ht.Add("SortByField", SortBy);
            ht.Add("SearchIn", "invoice_transaction_id <> '' and type='seller' and prop_address1 like '%" + AddressSearch + "%' and prop_city like '" + City + "%' and prop_state like '" + State + "%' and (prop_zipcode)");
            ht.Add("SearchBy", SearchBy.Replace("'", ""));
            ht.Add("PageSize", 12);
            ht.Add("PageNo", PageNo);
            using (DataSet ds = dataLayerObj.GetDataSetWithoutReplacingQute("usp_Paging", ht))
            {
                if (ds.Tables.Count > 1)
                {
                    using (DataTable DT = ds.Tables[1])
                    {
                        intTotalRows = (int)ds.Tables[0].Rows[0]["TotalRows"];
                        clsUI.BindData(rpt, DT);
                    }
                }
            }
        }
        catch
        {
            throw;
        }

        return intTotalRows;
    }
}
