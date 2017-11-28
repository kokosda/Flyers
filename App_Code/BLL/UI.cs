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
/// Summary description for UI
/// </summary>
/// 
public class clsUI
{
    public void UI()
    {
    }

    public static void BindData(DataList Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }

    public static void BindData(DataGrid  Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }

    public static void BindData(DropDownList Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }

    public static void BindData(GridView Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }

    public static void BindData(FormView Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch
        {
            //HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }


    public static void BindData(HtmlSelect Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
                DT.Dispose();
            }
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write("<table width=100%><tr><td Class='ErrorMsg' align=center width=100%>" + e.Message + "!</td></tr></table>");
        }
    }

    public static void BindData(Repeater Cntl, DataTable DT)
    {
        try
        {
            if (DT != null)
            {
                Cntl.DataSource = DT;
                Cntl.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
}

