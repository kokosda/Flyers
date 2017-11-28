using System;
using System.Collections;
using System.Configuration;

namespace FlyerMe
{
    public partial class WebBeacons : PageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            var orderIdStr = Request["orderid"];
            var subscriberIdStr = Request["subscriberid"];
            Int64 orderId, subscriberId;

            if (Int64.TryParse(orderIdStr, out orderId) && Int64.TryParse(subscriberIdStr, out subscriberId))
            {
                if ((orderId > 0L) && (subscriberId > 0L))
                {
                    try
                    {
                        using (var obj = new clsData())
                        {
                            obj.objCon.Dispose();
                            obj.objCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString);

                            var ht = new Hashtable();

                            ht.Add("order_id", orderId);
                            ht.Add("subscriber_id", subscriberId);
                            ht.Add("Email_opened_IP", Request.UserHostAddress);

                            obj.ExecuteSql("fly_sp_webbeacons", ht);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            Response.ClearContent();
            Response.CacheControl = "no-cache";
            Response.ContentType = "image/png";
            Response.WriteFile(Server.MapPath("~/images/webbeacon.png"));
            Response.End();
        }
    }
}
