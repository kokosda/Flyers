using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Controls.Grid
{
    public partial class ExcelExporter : UserControl
    {
        public String SqlSelectCommand { get; set; }

        protected void hlExportToExcel_Command(Object sender, CommandEventArgs e)
        {
            if (SqlSelectCommand.HasText())
            {
                using (var cmd = new SqlCommand())
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ToString()))
                {
                    cmd.CommandText = SqlSelectCommand;
                    cmd.CommandType = CommandType.Text;

                    if (conn.State != ConnectionState.Open) 
                    { 
                        conn.Open(); 
                    }

                    cmd.Connection = conn;

                    var ds = new DataSet();
                    var da = new SqlDataAdapter(cmd);

                    da.Fill(ds);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=Report.xls");
                    EnableViewState = false;
                    Response.Charset = String.Empty;

                    using (var sw = clsExportToExcel.ExportToExcelXML(ds))
                    {
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                }
            }
        }
    }
}
