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
using AdminDSTableAdapters;
using System.Text;
/// <summary>
/// Summary description for clsAdmin
/// </summary>
[System.ComponentModel.DataObject]
[Serializable]
public class clsAdmin
{
	public clsAdmin()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [System.ComponentModel.DataObjectMethodAttribute
    (System.ComponentModel.DataObjectMethodType.Select, true)]
    public AdminDS.fly_order_Subscribers_With_EmailDataTable GetOverViewEmailDeliveryReport()
    {
        fly_order_Subscribers_With_EmailTableAdapter TA = new fly_order_Subscribers_With_EmailTableAdapter();
        return TA.GetDataOverviewReport();
    }

    public static int BindAgents(DataGrid DL, int PageNo, int PageSize, string SortBy, string SearchQuery, string SearchBy, int TopRows)
    {
        int intTotalRows = 0;
        PageNo = PageNo + 1;
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("TableName", "viewAgents");
            ht.Add("TableIDField", "id");
            ht.Add("SortByField", SortBy);
            ht.Add("SearchIn", "1=1 " + SearchQuery + " and email");
            ht.Add("SearchBy", SearchBy);
            ht.Add("PageSize", PageSize);
            ht.Add("PageNo", PageNo);
            ht.Add("TopRows", TopRows);
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

    public static int BindNewSubscriber(DataGrid DL, int PageNo, int PageSize, string SortBy, string SearchQuery, string SearchBy, int TopRows)
    {
        int intTotalRows = 0;
        PageNo = PageNo + 1;
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("TableName", "viewNewSubscriber");
            ht.Add("TableIDField", "RealtorID");
            ht.Add("SortByField", SortBy);
            ht.Add("SearchIn", "1=1 " + SearchQuery + " and email");
            ht.Add("SearchBy", SearchBy);
            ht.Add("PageSize", PageSize);
            ht.Add("PageNo", PageNo);
            ht.Add("TopRows", TopRows);
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

    public string GetMSAForCounty(string state, string county)
    {
        string strMSA = "";
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("state", state);
            ht.Add("county", county);
            DataTable dt = dataLayerObj.GetDataTable("usp_GetMSAForCounty", ht);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (sb.ToString() != "")
                    {
                        sb.Append(", " + dr["msa_name"].ToString().Trim());
                    }
                    else
                    {
                        sb.Append(dr["msa_name"].ToString().Trim());
                    }
                }
                strMSA = sb.ToString();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
        return strMSA;
    }

    public string GetCountyForMSA(string state, string msa)
    {
        string strCounty = "";
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("state", state);
            ht.Add("msa", msa);
            DataTable dt = dataLayerObj.GetDataTable("usp_GetCountyForMSA", ht);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (sb.ToString() != "")
                    {
                        sb.Append(", " + dr["county"].ToString().Trim());
                    }
                    else
                    {
                        sb.Append(dr["county"].ToString().Trim());
                    }
                }
                strCounty = sb.ToString();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
        return strCounty;
    }

    public string GetCountyForAssociation(string state, string association)
    {
        string strCounty = "";
        try
        {
            Hashtable ht = new Hashtable();
            clsData dataLayerObj = new clsData();
            ht.Add("state", state);
            ht.Add("association", association);
            DataTable dt = dataLayerObj.GetDataTable("usp_GetCountyForAssociation", ht);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (sb.ToString() != "")
                    {
                        sb.Append(", " + dr["county"].ToString().Trim());
                    }
                    else
                    {
                        sb.Append(dr["county"].ToString().Trim());
                    }
                }
                strCounty = sb.ToString();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
        return strCounty;
    }

    public DataTable GetUserNote(string UserId)
    {
        DataTable dt = new DataTable();
        try
        {
            AdminDAL adminDAL = new AdminDAL();
            dt = adminDAL.GetUserNote(UserId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }

    public int SaveUserNote(string UserId, string Note)
    {
        int result = 0;

        AdminDAL adminDAL = new AdminDAL();
        result = adminDAL.SaveUserNote(UserId, Note);

        return result;
    }

    public int AddPriority(string order_id, string priority, string delivery_app)
    {
        int result = 0;
        try
        {
            AdminFDAL adminFDAL = new AdminFDAL();
            result = adminFDAL.AddPriority(order_id,priority,delivery_app);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }
}
