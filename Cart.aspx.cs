using FlyerMe.Controls;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Cart : PageBase
    {
        protected override String ScriptsBundleName
        {
            get
            {
                return "~/bundles/scripts/cart";
            }
        }

        protected Boolean IsEmpty { get; set; }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Cart | {0}", clsUtility.ProjectName);
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            if (Request.IsGet())
            {
                Request.RedirectToHttpsIfRequired(Response);
                SetRptTransactionHistory();
                SetDivTaxRate();

                var cphAfterFooter = Master.FindControl("cphAfterFooter") as ContentPlaceHolder;
                var control = LoadControl("~/controls/masterpageaccount/popup.ascx");

                cphAfterFooter.Controls.Add(control);
                control = LoadControl("~/controls/masterpageaccount/popupbig.ascx");
                cphAfterFooter.Controls.Add(control);
                form.Attributes.Add("data-canpayurl", ResolveUrl("~/cart.aspx/canpay"));
            }
            else if (Request.IsPost())
            {
                try
                {
                    ProcessPayment();

                    Response.Redirect("~/cart.aspx?thanks=true", true);
                }
                catch(Exception ex)
                {
                    ltlCartSummary.Text = ex.Message;
                    divCartSummary.Visible = true;
                    divCartSummary.Attributes["class"] += " error";
                }
            }
        }

        protected void RptTransactionHistory_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            var tr = e.Item.FindControl("tr") as HtmlTableRow;

            if (e.Item.ItemIndex > 4)
            {
                tr.Attributes.Add("style", "display:none;");
            }

            var literal = tr.FindControl("ltlOrder") as Literal;

            literal.Text = DataBinder.Eval(e.Item.DataItem, "order_id").ToString();
            literal = tr.FindControl("ltlFlyer") as Literal;
            literal.Text = DataBinder.Eval(e.Item.DataItem, "title") as String;
            literal = tr.FindControl("ltlTotal") as Literal;
            literal.Text = DataBinder.Eval(e.Item.DataItem, "total_price") as String;

            if (Request["orderid"].HasText() && String.Compare((e.Item.DataItem as DataRowView).Row["order_id"].ToString(), Request["orderid"], false) == 0)
            {
                var checkbox = tr.FindControl("checkbox") as HtmlInputCheckBox;

                checkbox.Checked = true;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static Object GetDiscountRate(String promoCode, Int32 ordersCount)
        {
            Object result = null;
            var username = HttpContext.Current.User.Identity.Name;
            String message = null;
            Decimal? discountRate = null;
            var percentage = false;
            var canGetDiscount = !String.IsNullOrEmpty(promoCode) && promoCode.Trim().Length > 0;

            if (!canGetDiscount)
            {
                message = "Please enter a coupon number or promotion code.";
            }

            var innerPromoCode = promoCode.Trim();
            
            if (canGetDiscount)
            {
                canGetDiscount = ordersCount > 0;

                if (!canGetDiscount)
                {
                    message = "Please select some orders.";
                }
            }

            if (canGetDiscount)
            {
                var offerDiscountAdapter = new FlyerMeDSTableAdapters.fly_tblOfferDiscountDetailTableAdapter();
                var offerDiscountTable = offerDiscountAdapter.GetDataOfferDiscount(username, innerPromoCode);

                if (offerDiscountTable.Rows.Count > 0)
                {
                    if (ordersCount > 1)
                    {
                        message = "This code is valid for only one flyer, so please select only one flyer to get discount.";
                    }
                    else
                    {
                        if ((Boolean)offerDiscountTable.Rows[0]["InPercentage"])
                        {
                            percentage = true;
                        }

                        discountRate = (Decimal)offerDiscountTable.Rows[0]["Discount"];
                    }
                }
                else
                {
                    var discountAdapter = new FlyerMeDSTableAdapters.fly_DiscountTableAdapter();
                    var discountTable = discountAdapter.GetDataByDiscountCode(innerPromoCode);

                    if (discountTable.Rows.Count == 0)
                    {
                        message = "Invalid coupon number or promotion code supplied.";
                    }
                    else
                    {
                        if ((Boolean)discountTable.Rows[0]["InPercentage"])
                        {
                            percentage = true;
                        }

                        discountRate = (Decimal)discountTable.Rows[0]["Discount"];
                    }
                }
            }

            result = new
                        {
                            DiscountRate = discountRate,
                            Message = message,
                            Percentage = percentage
                        };

            return result;  
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static Object CanPay(Object[] d)
        {
            Object result = null;
            String message = null;
            ValidationCollectedInfo info = null;

            try
            {
                var nvc = new NameValueCollection();

                foreach (Dictionary<String, Object> el in d)
                {
                    nvc.Add(el["name"] as String, el["value"] != null ? el["value"].ToString() : null);
                }

                info = ValidatePaymentInfo(nvc);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            result = new
                        {
                            Result = info != null,
                            Message = message,
                        };

            return result;
        }

        #region private

        private static DataTable GetOrders()
        {
            var result = new DataTable();
            var dcCol = new DataColumn("order_id", typeof(Int64));

            result.Columns.Add(dcCol);
            dcCol = new DataColumn("title", typeof(String));
            result.Columns.Add(dcCol);
            dcCol = new DataColumn("total_price", typeof(String));
            result.Columns.Add(dcCol);

            var ordersBLL = new OrdersBLL();
            var orderTable = ordersBLL.Adapter.GetOrdersByCustomerID(HttpContext.Current.User.Identity.Name, Order.flyerstatus.Incomplete.ToString());

            Func<DataRow, Boolean> canIncludeOrder = (dr) =>
            {
                return (dr["markup"] as String).HasText() &&
                       (dr["invoice_transaction_id"] as String).HasNoText() &&
                       ((Decimal)dr["tota_price"]) > 0M;
            };

            DataRow dataRow;
            DataRow orderDataRow;

            for (var i = 0; i < orderTable.Rows.Count; i++)
            {
                orderDataRow = orderTable.Rows[i];

                if (canIncludeOrder(orderDataRow))
                {
                    dataRow = result.NewRow();
                    dataRow["order_id"] = orderDataRow["order_id"];
                    dataRow["title"] = (orderDataRow["title"] as String).HasText() ? orderDataRow["title"] : orderDataRow["email_subject"];
                    dataRow["total_price"] = ((Decimal)orderDataRow["tota_price"]).FormatPrice();
                    result.Rows.Add(dataRow);
                }
            }

            return result;
        }

        private void SetRptTransactionHistory()
        {
            var dtOrders = GetOrders();

            rptTransactionHistory.DataSource = dtOrders;
            rptTransactionHistory.DataBind();

            IsEmpty = dtOrders.Rows.Count == 0;
        }

        private void SetDivTaxRate()
        {
            var taxRate = GetTaxRate();

            ltlTaxRate.Text = taxRate.ToString() + "%";

            if (taxRate == 0M)
            {
                divTaxRate.Attributes.Add("style", "display:none;");
            }
        }

        private static Decimal GetTaxRate()
        {
            var profile = ProfileCommon.Create(HttpContext.Current.User.Identity.Name) as ProfileCommon;
            var currentState = new State(profile.BusinessAddress.BusinessState);
            var result = 0.0M;

            if (currentState.Taxable)
            {
                result = Decimal.Parse(currentState.TaxRate);
            }

            return result;
        }

        private void ProcessPayment()
        {
            if (!Request.IsHttps())
            {
                throw new CartException("Please use HTTPS protocol.");
            }

            var info = ValidatePaymentInfo(Request.Form);
            var username = User.Identity.Name;
            
            if (info.TotalPrice.Equals(0M))
            {
                var transactionId = "5555555";

                SaveOrders(info, transactionId);
            }
            else
            {
                var transactionId = ProcessStripePayment(info);
                
                SaveOrders(info, transactionId);
            }
        }

        private void SaveOrders(ValidationCollectedInfo info, String transactionId)
        {
            var username = User.Identity.Name;

            foreach (var order in info.Orders)
            {
                order.invoice_transaction_id = transactionId;

                if (String.Compare(order.type, FlyerTypes.Custom.ToString(), true) == 0)
                {
                    order.status = Order.flyerstatus.Pending_Approval.ToString();
                }
                else
                {
                    order.status = Order.flyerstatus.Scheduled.ToString();
                }

                order.Save();
            }

            var processedOrderIds = String.Join(",", info.Orders.Select(o => o.order_id.ToString()).ToArray());
            var orderTransaction = new FlyerMeDSTableAdapters.fly_TransactionTableAdapter();

            orderTransaction.Insert(transactionId, username, processedOrderIds, info.PromoCode, info.SubTotal, info.Discount, info.TaxCost, info.TotalPrice);

            if (!String.IsNullOrEmpty(info.PromoCode))
            {
                var objOrder = new OrdersBLL();

                objOrder.UpdateDiscount(info.PromoCode);
                objOrder.UpdateOfferDiscount(username, info.PromoCode);
            }

            SendEmailReceipt(transactionId, username, info);
        }

        private static ValidationCollectedInfo ValidatePaymentInfo(NameValueCollection request)
        {
            var result = new ValidationCollectedInfo();

            if (String.IsNullOrEmpty(request["orderids"]))
            {
                throw new CartException("Please select some orders.");
            }

            var orderIds = request["orderids"].Split(',').Select(oi => Int64.Parse(oi)).ToArray();
            var dtOrders = GetOrders();
            var containsOrder = false;
            var subTotal = 0M;

            foreach (var orderId in orderIds)
            {
                foreach (DataRow row in dtOrders.Rows)
                {
                    if (row["order_id"].Equals(orderId))
                    {
                        subTotal += Decimal.Parse(row["total_price"].ToString().Remove(0, 1));
                        result.Orders.Add(new Order((Int32)orderId));
                        containsOrder = true;
                        break;
                    }
                }

                if (!containsOrder)
                {
                    throw new CartException(String.Format("Transaction history has no order with id {0}.", orderId.ToString()));
                }
            }

            if (String.IsNullOrEmpty(request["totalprice"]))
            {
                throw new CartException("Total price is required parameter.");
            }

            var totalOfRequest = Decimal.Parse(request["totalprice"]);
            var discountRate = 0M;
            var discount = 0M;
            var promoCode = request["promocode"];
            var promoCodeIsApplied = request.ParseCheckboxValue("promocodeisapplied");

            if (promoCodeIsApplied == true)
            {
                if (String.IsNullOrEmpty(promoCode) || promoCode.Trim().Length == 0)
                {
                    throw new CartException("Please do not remove promotional code after appliance.");
                }

                result.PromoCode = promoCode = promoCode.Trim();

                var discountRateModel = GetDiscountRate(promoCode, orderIds.Length);
                var tmpDiscountRate = (Decimal?)discountRateModel.GetType().GetProperty("DiscountRate").GetValue(discountRateModel, null);

                if (tmpDiscountRate > 0)
                {
                    discountRate = tmpDiscountRate.Value;

                    var percentage = (Boolean)discountRateModel.GetType().GetProperty("Percentage").GetValue(discountRateModel, null);

                    if (percentage)
                    {
                        discount = Math.Round(subTotal * discountRate / 100M, 2);

                        if (subTotal - discount < 0)
                        {
                            discount = subTotal;
                        }
                    }
                    else
                    {
                        discount = subTotal - discountRate;

                        if (discount < 0)
                        {
                            discount = subTotal;
                        }
                    }
                }
            }

            subTotal = subTotal - discount;

            var taxRate = GetTaxRate();
            var taxCost = Math.Round(subTotal * taxRate / 100M, 2);
            var total = subTotal + taxCost;

            if (Math.Abs(total - totalOfRequest) > 0.01M)
            {
                throw new CartException("Total price provided is incorrect.");
            }

            result.OrderIds = orderIds;
            result.DiscountRate = discountRate;
            result.Discount = discount;
            result.TaxRate = taxRate;
            result.TaxCost = taxCost;
            result.SubTotal = subTotal;
            result.TotalPrice = totalOfRequest;

            return result;
        }

        private void SendEmailReceipt(String transactionId, String customerEmail, ValidationCollectedInfo info)
        {
            var profile = Profile.GetProfile(customerEmail);
            var data = new NameValueCollection();
            
            data.Add("transactionid", Helper.GetEncodedUrlParameter(transactionId));
            data.Add("orderdate", Helper.GetEncodedUrlParameter(DateTime.Now.FormatDate()));
            data.Add("orderids", Helper.GetEncodedUrlParameter(String.Join(",", info.OrderIds.Select(oi => oi.ToString()).ToArray())));

            if (info.TaxCost > 0)
            {
                data.Add("taxrate", Helper.GetEncodedUrlParameter(info.TaxRate.FormatPrice()));
                data.Add("taxcost", Helper.GetEncodedUrlParameter(info.TaxCost.FormatPrice()));
            }

            var customerName = profile.FirstName + " " + profile.LastName;

            data.Add("customername", Helper.GetEncodedUrlParameter(customerName));
            data.Add("discount", Helper.GetEncodedUrlParameter(info.Discount.FormatPrice()));
            data.Add("subtotal", Helper.GetEncodedUrlParameter(info.SubTotal.FormatPrice()));
            data.Add("totalprice", Helper.GetEncodedUrlParameter(info.TotalPrice.FormatPrice()));

            var url = "~/flyer/markup/receipt.aspx?" + data.NameValueToQueryString();
            var markup = Helper.GetPageMarkup(url);
            var subject = clsUtility.SiteBrandName + " Receipt";
            var senderName = clsUtility.ProjectName + " Services";
            var recipientName = customerName;

            Helper.SendEmail(senderName, customerEmail, recipientName, subject, markup);

            try
            {
                var recipientEmail = clsUtility.ContactUsEmail;

                Helper.SendEmail(senderName, recipientEmail, recipientEmail, subject, markup);
            }
            catch
            {
            }
        }

        private String ProcessStripePayment(ValidationCollectedInfo info)
        {
            if (Request["stripetoken"].HasNoText())
            {
                throw new CartException("Stripe Token is required.");
            }

            var stripeToken = Request["stripeToken"];
            var stripeCustomerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = null;
            String existingSourceId = null;
            
            try
            {
                stripeCustomer = stripeCustomerService.Get(Profile.StripeCustomerId);

                if (stripeCustomer.Deleted == true)
                {
                    stripeCustomer = null;
                }
                else
                {
                    try
                    {
                        var stripeCardService = new StripeCardService();
                        var stripeCard = stripeCardService.Create(stripeCustomer.Id, new StripeCardCreateOptions
                                                                                            {
                                                                                                SourceToken = stripeToken
                                                                                            });
                        var scOld = stripeCustomer.SourceList.Data.FirstOrDefault(c => String.Compare(c.Fingerprint, stripeCard.Fingerprint, false) == 0);

                        if (scOld != null)
                        {
                            try
                            {
                                stripeCardService.Delete(stripeCustomer.Id, stripeCard.Id);
                            }
                            catch
                            {
                            }

                            existingSourceId = scOld.Id;
                        }
                        else
                        {
                            existingSourceId = stripeCard.Id;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (StripeException)
            {
            }

            if (stripeCustomer == null)
            {
                stripeCustomer = stripeCustomerService.Create(new StripeCustomerCreateOptions
                                                                    {
                                                                        Email = User.Identity.Name,
                                                                        Description = Profile.FirstName + " " + Profile.LastName,
                                                                        SourceToken = stripeToken
                                                                    });
                Profile.StripeCustomerId = stripeCustomer.Id;
                Profile.Save();
                existingSourceId = stripeCustomer.DefaultSourceId;
            }

            var stripeChargeService = new StripeChargeService();
            var stripeChargeOptions = new StripeChargeCreateOptions()
                                            {
                                                Amount = (Int32)(info.TotalPrice * 100),
                                                Currency = "USD",
                                                Description = info.Orders.Count == 1 ? String.Format("1 flyer (ID {0})", info.Orders[0].order_id.ToString()) : String.Format("{0} flyers (IDs {1})", info.Orders.Count.ToString(), String.Join(", ", info.OrderIds)),
                                                Capture = true,
                                                CustomerId = stripeCustomer.Id,
                                                SourceTokenOrExistingSourceId = existingSourceId
                                            };
            var stripeCharge = stripeChargeService.Create(stripeChargeOptions);
            var result = stripeCharge.Id;

            return result;
        }

        private class CartException : Exception
        {
            public CartException(String message) : base(message)
            {
            }
        }

        private class ValidationCollectedInfo
        {
            public ValidationCollectedInfo()
            {
                Orders = new List<Order>();
            }

            public Int64[] OrderIds { get; set; }

            public List<Order> Orders { get; set; }

            public Decimal TaxRate { get; set; }

            public Decimal TaxCost { get; set; }

            public Decimal DiscountRate { get; set; }

            public Decimal Discount { get; set; }

            public Decimal SubTotal { get; set; }

            public Decimal TotalPrice { get; set; }

            public String PromoCode { get; set; }
        }

        #endregion
    }
}