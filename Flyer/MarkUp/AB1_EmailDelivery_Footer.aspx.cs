using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AB1_EmailDelivery_Footer : EmailMarkupPageBase
    {
        public String UnsubscribeQuery
        {
            get
            {
                return Request["unsubscribequery"];
            }
        }

        public String OrderId
        {
            get
            {
                return Request["orderid"];
            }
        }

        public String SubscriberId
        {
            get
            {
                return Request["subscriberid"];
            }
        }
        public String UnsubscribeKey
        {
            get
            {
                return Request["unsubscribekey"];
            }
        }

        public String SubscriberEmailId
        {
            get
            {
                return Request["subscriberemailid"];
            }
        }
    }
}