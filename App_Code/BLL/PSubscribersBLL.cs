using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FlyerMeDSTableAdapters;

namespace FlyerMe
{
    [Serializable]
    [System.ComponentModel.DataObject]
    public class PSubscribersBLL
    {
        private fly_psubscribersTableAdapter _psubscribersTableAdapter = null;
        public fly_psubscribersTableAdapter Adapter
        {
            get
            {
                if (_psubscribersTableAdapter == null)
                    _psubscribersTableAdapter = new fly_psubscribersTableAdapter();

                return _psubscribersTableAdapter;
            }
        }

        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int Insert(PSubscriber psubscriber)
        {
            object result = Adapter.InsertPSubscriber(
                        psubscriber.first_name,
                        psubscriber.middle_name,
                        psubscriber.last_name,
                        psubscriber.suffix,
                        psubscriber.address1,
                        psubscriber.address2,
                        psubscriber.city,
                        psubscriber.state,
                        psubscriber.zip,
                        psubscriber.phone,
                        psubscriber.fax,
                        psubscriber.email,
                        psubscriber.website,
                        psubscriber.market_counties,
                        psubscriber.market_area,
                        psubscriber.association_id,
                        psubscriber.msa_id,
                        psubscriber.email_confirmed,
                        psubscriber.active);

            return Convert.ToInt32(result);
        }

        public bool PSubscriberVerifyEmail(string email)
        {
            int result = Adapter.PSubscriberVerifyEmail(email);

            return (result > 0);
        }

        public bool ActivateSubscriber(int subscriberid)
        {
            FlyerMeDS.fly_psubscribersDataTable psubs = Adapter.GetPSubscriberByID(subscriberid);
            if (psubs.Count == 0)
                // no matching record found, return false
                return false;

            FlyerMeDS.fly_psubscribersRow psubsrow = psubs[0];

            psubsrow.email_confirmed = true;
            psubsrow.active = true;

            // Update the order record
            int rowsAffected = Adapter.Update(psubsrow);

            // Return true if precisely one row was updated,
            // otherwise false
            return rowsAffected == 1;
        }

        public bool IsPSubscriber(int subscriberid)
        {
            FlyerMeDS.fly_psubscribersDataTable psubs = Adapter.GetPSubscriberByID(subscriberid);
            if (psubs.Count > 0) { return true; } else { return false; }
        }

    }
}
