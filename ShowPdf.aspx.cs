using SelectPdf;
using System;
using System.IO;
using System.Web.UI;

namespace FlyerMe
{
    public partial class ShowPdf : PageBase
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            var orderId = !String.IsNullOrEmpty(Request["orderid"]) ? Request["orderid"] : Request["oid"];

            if (!String.IsNullOrEmpty(orderId))
            {
                orderId = orderId.Replace("3D", ""); //Group Mail sometime inserts 3D literal so replace it

                var urlToGeneratePdfFrom = clsUtility.GetRootHost + "preview.aspx?orderid=" + orderId;
                var order = Helper.GetOrder(orderId, Response);
                var pdfDirectory = Server.MapPath("~/pdf/");
                var pdfOrderFilePath = pdfDirectory + orderId + ".pdf";
                var textOrderFilePath = pdfDirectory + orderId + ".txt";
                var pdfOrderRelativeFilePath = "~/pdf/" + orderId + ".pdf";

                if (File.Exists(pdfOrderFilePath) && (String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) != 0))
                {
                    if (File.Exists(textOrderFilePath))
                    {
                        try
                        {
                            using (var fs = File.Open(textOrderFilePath, FileMode.Append))
                            using (var bw = new BinaryWriter(fs))
                            {
                                bw.Write(DateTime.Now.ToString());
                            }
                        }
                        catch
                        {
                        }

                        Response.Redirect(pdfOrderFilePath);
                    }
                    else
                    {
                        using (var fs = File.Create(textOrderFilePath))
                        using (var bw = new BinaryWriter(fs))
                        {
                            bw.Write(DateTime.Now.ToString());
                        }

                        new HtmlToPdf().ConvertHtmlString(order.markup).Save(pdfOrderFilePath);
                        Response.Redirect(pdfOrderRelativeFilePath);
                    }
                }
                else
                {
                    new HtmlToPdf().ConvertHtmlString(order.markup).Save(pdfOrderFilePath);
                    Response.Redirect(pdfOrderRelativeFilePath);
                }
            }
        }
    }
}
