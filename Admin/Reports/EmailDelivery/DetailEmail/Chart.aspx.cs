using FlyerMe.Admin.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FlyerMe.Admin.Reports.EmailDelivery.DetailEmail
{
    public partial class Chart : AdminPageBase
    {
        protected PieSliceModel[] PieSlices { get; set; }

        protected void Page_Load(Object sender, EventArgs args)
        {
            PieSlices = GetDataSource();
        }

        protected String GetJsArrayElements()
        {
            return String.Join(", ", PieSlices.Select(ps => String.Format("[\"{0}\", {1}]", ps.Caption, ps.DataValue)));
        }

        #region private

        private PieSliceModel[] GetDataSource()
        {
            var pieSlices = new List<PieSliceModel>();
            Int64 orderId;

            if (Int64.TryParse(Request["orderid"], out orderId) && orderId > 0)
            {
                using (var dataLayerObj = new clsData())
                {
                    dataLayerObj.objCon.Dispose();
                    dataLayerObj.objCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["fdeliveryDBConnectionString"].ConnectionString);
                    PieSliceModel pieSlice;
                    var ht = new Hashtable();

                    ht.Add("order_id", (Int32)orderId);

                    try
                    {
                        var dt = dataLayerObj.GetDataTable("usp_GetOrderOverview", ht);

                        if (dt.Rows.Count > 0)
                        {
                            pieSlice = new PieSliceModel("Sent (" + dt.Rows[0]["Email_Sent"].ToString() + ")", (Single)(Int32)dt.Rows[0]["Email_Sent"]);
                            pieSlices.Add(pieSlice);
                            pieSlice = new PieSliceModel("Opened (" + dt.Rows[0]["Email_Opened"].ToString() + ")", (Single)(Int32)dt.Rows[0]["Email_Opened"]);
                            pieSlices.Add(pieSlice);
                            pieSlice = new PieSliceModel("Bounce Back (" + dt.Rows[0]["Email_Bounce_Back"].ToString() + ")", (Single)(Int32)dt.Rows[0]["Email_Bounce_Back"]);
                            pieSlices.Add(pieSlice);

                            message.MessageText = String.Format("Chart data successfully generated for flyer ID={0}.", orderId.ToString());
                            message.MessageClass = Admin.Controls.MessageClassesEnum.Ok;
                        }
                        else
                        {
                            message.MessageText = String.Format("Data for flyer ID={0} not found.", orderId.ToString());
                            message.MessageClass = Admin.Controls.MessageClassesEnum.System;
                        }
                    }
                    catch (Exception ex)
                    {
                        message.MessageText = ex.Message;
                        message.MessageClass = Admin.Controls.MessageClassesEnum.Error;
                    }
                }
            }
            else
            {
                message.MessageText = String.Format("Flyer ID should be a positive integral number.");
                message.MessageClass = Admin.Controls.MessageClassesEnum.System;
            }

            message.ShowMessage();

            var result = pieSlices.ToArray();

            return result;
        }

        #endregion
    }
}
