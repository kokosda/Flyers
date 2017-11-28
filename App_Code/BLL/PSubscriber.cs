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
    /// <summary>
    /// PSubscriber class
    /// </summary>
    [Serializable]
    public class PSubscriber
    {
        private long _psubscriber_id;
        private string _first_name;
        private string _middle_name;
        private string _last_name;
        private string _suffix;
        private string _address1;
        private string _address2;
        private string _city;
        private string _state;
        private string _zip;
        private string _phone;
        private string _fax;
        private string _email;
        private string _website;
        private string _market_counties;
        private string _market_area;
        private string _association_id;
        private string _msa_id;
        private bool _email_confirmed = false;
        private bool _active = false;

        public long psubscriber_id
        {
            get { return _psubscriber_id; }
            set { _psubscriber_id = value; }
        }

        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        public string middle_name
        {
            get { return _middle_name; }
            set { _middle_name = value; }
        }

        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        public string suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }

        public string address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }

        public string address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string city
        {
            get { return _city; }
            set { _city = value; }
        }

        public string state
        {
            get { return _state; }
            set { _state = value; }
        }

        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string website
        {
            get { return _website; }
            set { _website = value; }
        }

        public string market_counties
        {
            get { return _market_counties; }
            set { _market_counties = value; }
        }

        public string market_area
        {
            get { return _market_area; }
            set { _market_area = value; }
        }

        public string association_id
        {
            get { return _association_id; }
            set { _association_id = value; }
        }

        public string msa_id
        {
            get { return _msa_id; }
            set { _msa_id = value; }
        }

        public bool email_confirmed
        {
            get { return _email_confirmed; }
            set { _email_confirmed = value; }
        }

        public bool active
        {
            get { return _active; }
            set { _active = value; }
        }


        public PSubscriber()
        {
            //
        }

        public int Insert()
        {
            PSubscribersBLL ps = new PSubscribersBLL();
            return ps.Insert(this);
        }
    }
}
