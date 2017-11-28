using FlyerMe.Controls;
using System;
using System.Collections.Specialized;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class Receipt : EmailMarkupPageBase
    {
        public String OrderDate
        {
            get
            {
                String result = null;
                DateTime temp;

                if (DateTime.TryParse(Request["orderdate"], out temp))
                {
                    result = temp.FormatDate();
                }

                return result;
            }
        }

        public String TransactionId
        {
            get
            {
                return Request["transactionid"];
            }
        }

        public String CustomerName
        {
            get
            {
                return Request["customername"];
            }
        }

        public Order[] Orders
        {
            get
            {
                return GetOrders();
            }
        }
        
        public String TaxRate
        {
            get
            {
                return Request["taxrate"];
            }
        }

        public String TaxCost
        {
            get
            {
                return Request["taxcost"];
            }
        }

        public String Discount
        {
            get
            {
                return Request["discount"];
            }
        }

        public String SubTotal
        {
            get
            {
                return Request["subtotal"];
            }
        }

        public String TotalPrice
        {
            get
            {
                return Request["totalprice"];
            }
        }

        #region private

        private Order[] GetOrders()
        {
            Order[] result = null;

            if (!String.IsNullOrEmpty(Request["orderids"]))
            {
                var orderIds = Request["orderids"].Split(',');

                result = new Order[orderIds.Length];

                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = Helper.GetOrder(orderIds[i], Response, User.Identity.Name);
                }
            }

            return result;
        }

        #endregion
    }
}