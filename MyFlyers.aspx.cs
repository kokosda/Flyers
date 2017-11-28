using FlyerMe.Controls;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class MyFlyers : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/myflyers";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("My Flyers | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Request.RedirectToHttpIfRequired(Response);

            if (Request.IsGet())
            {
                filter = Filter.Bind(Request);
                pager.Filter = filter;
                pager.PageSize = 5;
                SetRptOrdersDatasource();
                SetFlyerMenuActivity();
            }
        }

        protected void rptOrders_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var order = (e.Item.DataItem as DataRowView).Row as FlyerMeDS.fly_orderRow;
            var literal = e.Item.FindControl("ltlType") as Literal;

            literal.Text = order.type.Capitalize();

            HtmlContainerControl div;
            HyperLink hyperLink;

            if (!String.IsNullOrEmpty(order.title.Trim()))
            {
                hyperLink = e.Item.FindControl("hlTitle") as HyperLink;
                hyperLink.Text = order.title;

                if (!String.IsNullOrEmpty(order.markup))
                {
                    hyperLink.NavigateUrl = String.Format("~/preview.aspx?orderid={0}", order.order_id.ToString());
                }
            }
            else
            {
                div = e.Item.FindControl("divTitle") as HtmlContainerControl;
                div.Visible = false;
            }

            div = e.Item.FindControl("divPrice") as HtmlContainerControl;
            literal = div.FindControl("ltlPrice") as Literal;
            
            if (!String.IsNullOrEmpty(order.prop_price))
            {
                literal.Text = order.prop_price.FormatPrice();
            }
            else if ((!String.IsNullOrEmpty(order.price_range_min)) || (!String.IsNullOrEmpty(order.price_range_max)))
            {
                Decimal @decimal;

                if (Decimal.TryParse(order.price_range_min, out @decimal))
                {
                    literal.Text = @decimal.FormatLongPrice();
                }
                if (Decimal.TryParse(order.price_range_max, out @decimal))
                {
                    literal.Text += " - " + @decimal.FormatLongPrice();
                }
            }
            
            if (String.IsNullOrEmpty(literal.Text))
            {
                div.Visible = false;
            }

            div = e.Item.FindControl("divStatus") as HtmlContainerControl;
            div.Attributes["class"] = String.Format("{0} {1}", div.Attributes["class"], order.status.ToLower());
            literal = div.FindControl("ltlStatus") as Literal;
            literal.Text = order.status.Capitalize();

            var image = e.Item.FindControl("imgPhoto1") as Image;

            if (!String.IsNullOrEmpty(order.photo1))
            {
                image.ImageUrl = Helper.GetPhotoPath(order.order_id, 1, order.photo1);
            }
            else
            {
                image.ImageUrl = "~/images/no-photo.jpg";
            }

            if (!String.IsNullOrEmpty(order.prop_address1.Trim()))
            {
                literal = e.Item.FindControl("ltlAddress") as Literal;
                literal.Text = Helper.GetFullAddress(order);
            }
            else
            {
                div = e.Item.FindControl("divAddress") as HtmlContainerControl;
                div.Visible = false;
            }

            literal = e.Item.FindControl("ltlFlyerId") as Literal;
            literal.Text = order.order_id.ToString();

            if (!order.delivery_date.Equals(default(DateTime)))
            {
                literal = e.Item.FindControl("ltlDeliveryDate") as Literal;
                literal.Text = order.delivery_date.FormatDate();
            }
            else
            {
                div = e.Item.FindControl("divDeliveryDate") as HtmlGenericControl;
                div.Visible = false;
            }

            if (!String.IsNullOrEmpty(order.mls_number))
            {
                literal = e.Item.FindControl("ltlMls") as Literal;
                literal.Text = order.mls_number;
            }
            else
            {
                div = e.Item.FindControl("divMls") as HtmlContainerControl;
                div.Visible = false;
            }

            literal = e.Item.FindControl("ltlMarket") as Literal;
            literal.Text = GetMarket(order);

            if (String.IsNullOrEmpty(literal.Text))
            {
                div = e.Item.FindControl("divMarket") as HtmlContainerControl;
                div.Visible = false;
            }

            if (!String.IsNullOrEmpty(order.markup))
            {
                literal = e.Item.FindControl("ltlShowFlyerUrl") as Literal;
                literal.Text = clsUtility.GetRootHost + "showflyer.aspx?oid=" + order.order_id.ToString();
            }
            else
            {
                div = e.Item.FindControl("divShowFlyerUrl") as HtmlContainerControl;
                div.Visible = false;
            }

            SetLinks(order, e);
        }

        protected void lbEditAndResend_Click(Object sender, EventArgs e)
        {
            var lb = sender as LinkButton;
            var order = GetOrderFromCommandArgument(lb);

            if (!CanEditAndResend(order))
            {
                Helper.SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Edit and resend operation is not allowed for flyer (ID={0}) in status {1}. Please wait flyer status change to proceed.", order.order_id.ToString(), order.status.ToUpper()));
            }

            order.status = Order.flyerstatus.Incomplete.ToString();
            order.invoice_transaction_id = String.Empty;
            order.delivery_date = DateTime.Now;
            order.created_on = DateTime.Now;
            order.updated_on = DateTime.Now;
            order.sent_on = DateTime.Now;
            order.field4 = String.Empty;

            var originalOrderID = order.order_id.ToString();

            order.order_id = 0;
            order.Save();

            if (String.Compare(order.type, FlyerTypes.Seller.ToString(), true) == 0 || 
                String.Compare(order.type, FlyerTypes.Custom.ToString(), true) == 0)
            {
                var origingalOrderMediaPath = Helper.GetOrderMediaRelativePath() + originalOrderID + "/";

                if (Directory.Exists(Server.MapPath(origingalOrderMediaPath)))
                {
                    var copier = new xDirectory();

                    copier.Source = new DirectoryInfo(Server.MapPath(origingalOrderMediaPath));
                    copier.Destination = new DirectoryInfo(Server.MapPath(Helper.GetOrderMediaRelativePath() + order.order_id));
                    copier.Overwrite = true;
                    copier.FolderFilter = "*";
                    copier.FileFilters.Add("*.*");
                    copier.StartCopy();
                }
            }

            var redirectionUrl = GetCreateFlyerUrl(order.type, order.order_id.ToString());

            if (!String.IsNullOrEmpty(redirectionUrl))
            {
                Response.Redirect(redirectionUrl, true);
            }
        }

        protected void lbTurnSyndication_Click(Object sender, EventArgs e)
        {
            var lb = sender as LinkButton;
            var order = GetOrderFromCommandArgument(lb);

            if (!CanTurnSyndication(order))
            {
                Helper.SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Turn syndication ON/OFF operation is not allowed for flyer (ID={0}) in status {1}. Please wait flyer status change to proceed.", order.order_id.ToString(), order.status.ToUpper()));
            }

            using(var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString))
            using(var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;

                if (order.IsSyndicat)
                {
                    cmd.CommandText = "update fly_order set IsSyndicat=0 where order_id=" + order.order_id;
                }
                else
                {
                    cmd.CommandText = "update fly_order set IsSyndicat=1 where order_id=" + order.order_id;
                }

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

            Response.Redirect(Request.Url.ToString(), true);
        }

        protected void lbCreateCopy_Click(Object sender, EventArgs e)
        {
            var lb = sender as LinkButton;
            var order = GetOrderFromCommandArgument(lb);

            if (!CanCopyOrder(order))
            {
                Helper.SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Copy operation is not allowed for flyer (ID={0}) in status {1}. Please wait flyer status change to proceed.", order.order_id.ToString(), order.status.ToUpper()));
            }

            order.status = Order.flyerstatus.Incomplete.ToString();
            order.invoice_transaction_id = String.Empty;
            order.delivery_date = DateTime.Now;
            order.created_on = DateTime.Now;
            order.updated_on = DateTime.Now;
            order.sent_on = DateTime.Now;
            order.field4 = String.Empty;

            var originalOrderID = order.order_id.ToString();

            order.order_id = 0;
            order.Save();

            if (String.Compare(order.type, FlyerTypes.Seller.ToString(), true) == 0 || 
                String.Compare(order.type, FlyerTypes.Custom.ToString(), true) == 0)
            {
                var origingalOrderMediaPath = Helper.GetOrderMediaRelativePath() + originalOrderID + "/";

                if (Directory.Exists(Server.MapPath(origingalOrderMediaPath)))
                {
                    var copier = new xDirectory();

                    copier.Source = new DirectoryInfo(Server.MapPath(origingalOrderMediaPath));
                    copier.Destination = new DirectoryInfo(Server.MapPath(Helper.GetOrderMediaRelativePath() + order.order_id));
                    copier.Overwrite = true;
                    copier.FolderFilter = "*";
                    copier.FileFilters.Add("*.*");
                    copier.StartCopy();
                }
            }

            Response.Redirect("~/myflyers.aspx", true);
        }

        protected void lbDeleteThisFlyer_Click(Object sender, EventArgs e)
        {
            var lb = sender as LinkButton;
            var order = GetOrderFromCommandArgument(lb);

            if (!CanDeleteOrder(order))
            {
                Helper.SetErrorResponse(HttpStatusCode.Forbidden, String.Format("Delete operation is not allowed for flyer (ID={0}) in status {1}. Please wait flyer status change to proceed.", order.order_id.ToString(), order.status.ToUpper()));
            }

            new OrdersBLL().Delete((Int32)order.order_id);
            Response.Redirect(Request.Url.ToString(), true);
        }

        #region private

        private Filter filter;

        private void SetRptOrdersDatasource()
        {
            var profile = Profile.GetProfile(Page.User.Identity.Name);

            var orderBLL = new OrdersBLL();
            var dtOrders = orderBLL.Adapter.GetOrdersByCustomerIdPaged(Profile.UserName, pager.PageNumber, pager.PageSize, filter.Type);

            if (dtOrders.Count > 0)
            {
                pager.ItemsCount = (Int32)dtOrders.Rows[0]["TotalRecords"];
            }

            rptOrders.DataSource = dtOrders;
            rptOrders.DataBind();
        }

        private void SetFlyerMenuActivity()
        {
            HyperLink hl;

            if (String.Compare(filter.Type, "seller", true) == 0)
            {
                hl = hlSellers;
            }
            else if (String.Compare(filter.Type, "buyer", true) == 0)
            {
                hl = hlBuyers;
            }
            else if (String.Compare(filter.Type, "custom", true) == 0)
            {
                hl = hlCustom;
            }
            else
            {
                hl = hlAll;
            }

            hl.CssClass += " active";
        }

        private String GetMarket(FlyerMeDS.fly_orderRow order)
        {
            String result = null;

            if (!String.IsNullOrEmpty(order.market_county))
            {
                result = String.Format("<strong>Market County(s):</strong> {0}", order.market_county.Replace("|", ", "));
            }
            else if (!String.IsNullOrEmpty(order.market_msa))
            {
                result = String.Format("<strong>Market MSA(s):</strong> {0}", order.market_msa.Replace("|", ", "));
            }
            else if (!String.IsNullOrEmpty(order.market_association))
            {
                result = String.Format("<strong>Market Association(s):</strong> {0}", order.market_association.Replace("|", ", "));
            }

            return result;
        }

        private void SetLinks(FlyerMeDS.fly_orderRow order, RepeaterItemEventArgs e)
        {
            var hyperLink = e.Item.FindControl("hlSendThisFlyerToClients") as HyperLink;

            if (String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0)
            {
                hyperLink.NavigateUrl = String.Format("~/sendtoclients.aspx?orderid={0}", order.order_id.ToString());
            }
            else
            {
                hyperLink.CssClass += " gray";
            }

            var linkButton = e.Item.FindControl("lbEditAndResend") as LinkButton;

            if (CanEditAndResend(order))
            {
                linkButton.CommandArgument = order.order_id.ToString();
            }
            else
            {
                linkButton.Enabled = false;
                linkButton.CssClass += " gray";
            }

            hyperLink = e.Item.FindControl("hlPostToCraigslist") as HyperLink;

            if (String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0)
            {
                hyperLink.NavigateUrl = "~/posttocraigslist.aspx?orderid=" + order.order_id.ToString();
            }
            else
            {
                hyperLink.CssClass += " gray";
            }

            hyperLink = e.Item.FindControl("hlDeliveryReport") as HyperLink;

            if (String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0)
            {
                hyperLink.NavigateUrl = "~/deliveryreport.aspx?orderid=" + order.order_id.ToString();
            }
            else
            {
                hyperLink.CssClass += "gray";
            }

            linkButton = e.Item.FindControl("lbTurnSyndication") as LinkButton;

            if (CanTurnSyndication(order))
            {
                linkButton.CommandArgument = order.order_id.ToString();
            }
            else
            {
                linkButton.Enabled = false;
                linkButton.CssClass += " gray";
            }

            var literal = e.Item.FindControl("ltlTurnSyndication") as Literal;

            if (order.IsSyndicat)
            {
                literal.Text = "OFF";
            }
            else
            {
                literal.Text = "ON";
            }

            hyperLink = e.Item.FindControl("hlViewThisFlyer") as HyperLink;

            if (!String.IsNullOrEmpty(order.markup))
            {
                hyperLink.NavigateUrl = String.Format("~/preview.aspx?orderid={0}", order.order_id.ToString());
            }
            else
            {
                hyperLink.CssClass += " gray";
            }

            hyperLink = e.Item.FindControl("hlEditThisFlyer") as HyperLink;

            if (String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) == 0)
            {
                hyperLink.NavigateUrl = GetCreateFlyerUrl(order.type, order.order_id.ToString());
                hyperLink.Text = "Complete flyer";
            }
            else
            {
                hyperLink.CssClass += " gray";
            }

            linkButton = e.Item.FindControl("lbCreateCopy") as LinkButton;

            if (CanCopyOrder(order))
            {
                linkButton.CommandArgument = order.order_id.ToString();
            }
            else
            {
                linkButton.Enabled = false;
                linkButton.CssClass += " gray";
            }

            linkButton = e.Item.FindControl("lbDeleteThisFlyer") as LinkButton;

            if (CanDeleteOrder(order))
            {
                linkButton.CommandArgument = order.order_id.ToString();
            }
            else
            {
                linkButton.Enabled = false;
                linkButton.CssClass += " gray";
            }
        }

        private String GetCreateFlyerPageName(String type)
        {
            String result = null;

            if (String.Compare(type, FlyerTypes.Seller.ToString(), true) == 0)
            {
                result = "sellersagent.aspx";
            }
            else if (String.Compare(type, FlyerTypes.Buyer.ToString(), true) == 0)
            {
                result = "buyersagent.aspx";
            }
            else if (String.Compare(type, FlyerTypes.Custom.ToString(), true) == 0)
            {
                result = "custom.aspx";
            }

            return result;
        }

        private String GetCreateFlyerUrl(String type, String orderId)
        {
            String result = null;
            var pageName = GetCreateFlyerPageName(type);

            if (!String.IsNullOrEmpty(pageName))
            {
                result = String.Format("~/createflyer/{0}?orderid={1}", pageName, orderId);
            }

            return result;
        }

        private Order GetOrderFromCommandArgument(LinkButton lb)
        {
            var result = Helper.GetOrder(lb.CommandArgument, Response, User.Identity.Name);

            return result;
        }

        private Boolean CanEditAndResend(Order order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0;
        }

        private Boolean CanEditAndResend(FlyerMeDS.fly_orderRow order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0;
        }

        private Boolean CanTurnSyndication(Order order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0 ||
                   String.Compare(order.layout, "ccc", true) == 0;
        }

        private Boolean CanTurnSyndication(FlyerMeDS.fly_orderRow order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Scheduled.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Queued.ToString(), true) == 0 ||
                   String.Compare(order.layout, "ccc", true) == 0;
        }

        private Boolean CanCopyOrder(Order order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) != 0;
        }

        private Boolean CanCopyOrder(FlyerMeDS.fly_orderRow order)
        {
            return String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) != 0;
        }

        private Boolean CanDeleteOrder(Order order)
        {
            return String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.layout, "ccc", true) == 0;
        }

        private Boolean CanDeleteOrder(FlyerMeDS.fly_orderRow order)
        {
            return String.Compare(order.status, Order.flyerstatus.Incomplete.ToString(), true) == 0 ||
                   String.Compare(order.status, Order.flyerstatus.Sent.ToString(), true) == 0 ||
                   String.Compare(order.layout, "ccc", true) == 0;
        }

        private class Filter : IFilter
        {
            private Filter(String type)
            {
                Type = type;
                entityFieldsQuery = new NameValueCollection();

                entityFieldsQuery.Add("type", Type);
            }

            public static Filter Bind(HttpRequest request)
            {
                var entityFieldsQuery = new NameValueCollection();

                entityFieldsQuery.Add(request.QueryString);

                var result = new Filter(entityFieldsQuery["type"]);

                return result;
            }

            public String Type { get; set; }

            public Boolean IsEntityFieldsEmpty
            {
                get 
                {
                    return String.IsNullOrEmpty(Type);
                }
            }

            public String EntityFieldsQueryString
            {
                get 
                {
                    if (entityFieldsQuery != null && entityFieldsQuery.Count > 0)
                    {
                        entityFieldsQueryString = entityFieldsQuery.NameValueToQueryString();
                    }

                    return entityFieldsQueryString;
                }
            }

            #region private

            private readonly NameValueCollection entityFieldsQuery;
            private String entityFieldsQueryString;

            #endregion
        }

        #endregion
    }
}