using FlyerMe.Controls;
using System;
using System.Web.UI;

namespace FlyerMe
{    
    public partial class DeliveryReport : PageBase
    {
        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Delivery Report | {0}", clsUtility.ProjectName);
            }
        }

        protected Boolean HasStatistics { get; set; }

        protected Int32 TotalOpenEmails { get; set; }

        protected Int32 HighlyInterestedEmails { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var profile = Profile.GetProfile(Page.User.Identity.Name);

            if (Request.QueryString["orderid"] != null)
            {
                var customerReport = new CustomerReportBLL();
                var dtOrderStats = customerReport.GetEmailDeliveryCustomerReport(Request.QueryString["orderid"], Profile.UserName);

                if (dtOrderStats.Rows.Count > 0)
                {
                    var totalSent = (Int32)dtOrderStats.Rows[0]["TotalSent"];

                    HasStatistics = totalSent > 0;

                    if (HasStatistics)
                    {
                        TotalOpenEmails = (Int32)dtOrderStats.Rows[0]["TotalOpen"] * 3;
                        HighlyInterestedEmails = (Int32)dtOrderStats.Rows[0]["HighInterest"] * 3;
                        ltlFlyerId.Text = Request.QueryString["orderid"];
                        ltlFlyerDeliveryDate.Text = ((DateTime)dtOrderStats.Rows[0]["DeliveryDate"]).FormatDate();

                        var totalOpenEmailsPercentage = (((Double)TotalOpenEmails / (Double)totalSent) * 100);
                        var highlyInterestedEmailsPercentage = (((Double)HighlyInterestedEmails / (Double)totalSent) * 100);

                        ltlTotalEmailsSent.Text = totalSent.ToString();
                        ltlInterestedAgents.Text = totalOpenEmailsPercentage.ToString("0.0") + "%";
                        ltlHighlyInterestedAgents.Text = highlyInterestedEmailsPercentage.ToString("0.0") + "%";
                    }
                }
            }
        }
    }
}