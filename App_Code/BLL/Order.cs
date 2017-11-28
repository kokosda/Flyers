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
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// Order class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------

    [Serializable]
    public class Order
    {
        #region Declarations

        //Enum Flyer Status
        public enum flyerstatus { Incomplete, Scheduled, Sent, Archived, Expried, Queued, Pending_Approval };

        private long _order_id = 0;
        private string _customer_id = string.Empty;
        private string _type = string.Empty;
        private string _photo_type = string.Empty;
        private string _market_state = string.Empty;
        private string _market_county = string.Empty;
        private string _market_association = string.Empty;
        private string _market_msa = string.Empty;
        private decimal _tota_price = 0;
        private decimal _invoice_tax = 0;
        private string _invoice_promocode = string.Empty;
        private string _invoice_transaction_id = string.Empty;
        private string _status = string.Empty;
        private string _headline = string.Empty;
        private string _theme = string.Empty;
        private string _layout = string.Empty;
        private DateTime _delivery_date = DateTime.Now;
        private string _mls_number = string.Empty;
        private string _email_subject = string.Empty;
        private string _title = string.Empty;
        private string _sub_title = string.Empty;
        private string _prop_address1 = string.Empty;
        private string _prop_address2 = string.Empty;
        private string _prop_city = string.Empty;
        private string _prop_state = string.Empty;
        private string _prop_zipcode = string.Empty;
        private string _prop_desc = string.Empty;
        private string _prop_price = string.Empty;
        private string _bullet1 = string.Empty;
        private string _bullet2 = string.Empty;
        private string _bullet3 = string.Empty;
        private string _bullet4 = string.Empty;
        private string _bullet5 = string.Empty;
        private string _bullet6 = string.Empty;
        private string _bullet7 = string.Empty;
        private string _bullet8 = string.Empty;
        private string _custom_field1 = string.Empty;
        private string _custom_field2 = string.Empty;
        private string _custom_field3 = string.Empty;
        private string _custom_field4 = string.Empty;
        private string _custom_field5 = string.Empty;
        private string _custom_field6 = string.Empty;
        private string _custom_field7 = string.Empty;
        private string _custom_field8 = string.Empty;
        private string _custom_field9 = string.Empty;
        private string _custom_field10 = string.Empty;
        private string _custom_field_value1 = string.Empty;
        private string _custom_field_value2 = string.Empty;
        private string _custom_field_value3 = string.Empty;
        private string _custom_field_value4 = string.Empty;
        private string _custom_field_value5 = string.Empty;
        private string _custom_field_value6 = string.Empty;
        private string _custom_field_value7 = string.Empty;
        private string _custom_field_value8 = string.Empty;
        private string _custom_field_value9 = string.Empty;
        private string _custom_field_value10 = string.Empty;
        private string _mls_link = string.Empty;
        private string _virtualtour_link = string.Empty;
        private string _map_link = string.Empty;
        private bool _use_mls_logo = false;
        private bool _use_housing_logo = false;
        private string _markup = string.Empty;
        private string _flyer = string.Empty;
        private string _price_range_min = string.Empty;
        private string _price_range_max = string.Empty;
        private string _property_type = string.Empty;
        private string _sqft_range_min = string.Empty;
        private string _sqft_range_max = string.Empty;
        private string _location = string.Empty;
        private string _more_info = string.Empty;
        private string _buyer_message = string.Empty;
        private string _photo1 = string.Empty;
        private string _photo2 = string.Empty;
        private string _photo3 = string.Empty;
        private string _photo4 = string.Empty;
        private string _photo5 = string.Empty;
        private string _photo6 = string.Empty;
        private string _photo7 = string.Empty;
        private string _photo8 = string.Empty;
        private string _photo9 = string.Empty;
        private string _photo10 = string.Empty;
        private string _field1 = string.Empty;
        private string _field2 = string.Empty;
        private string _field3 = string.Empty;
        private string _field4 = string.Empty;
        private string _field5 = string.Empty;
        private DateTime _created_on = DateTime.Now;
        private DateTime _updated_on;
        private DateTime _sent_on;
        private bool _found = false;
        //============New Fields==========
        private int _LastPageNo = 0;
        private string _Bedrooms = string.Empty;
        private string _FullBaths = string.Empty;
        private string _HalfBaths = string.Empty;
        private string _Parking = string.Empty;
        private string _SqFoots = string.Empty;
        private string _YearBuilt = string.Empty;
        private string _LotSize = string.Empty;
        private string _Subdivision = string.Empty;
        private string _HOA = string.Empty;
        private string _Floors = string.Empty;
        private string _PropertyFeatures = string.Empty;
        private string _PropertyFeaturesValues = string.Empty;
        private string _OtherPropertyFeatures = string.Empty;
        private decimal _Discount = 0;
        private int _PropertyCategory = 0;
        private int _PropertyType = 0;
        private bool _IsSyndicat = false;
        private string _AptSuiteBldg = string.Empty;
        private String _OpenHouses = String.Empty;
        //====================================================

        #endregion

        #region Properties

        public long order_id
        {
            get { return _order_id; }
            set { _order_id = value; }
        }

        public string customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string photo_type
        {
            get { return _photo_type; }
            set { _photo_type = value; }
        }

        public string market_state
        {
            get { return _market_state; }
            set { _market_state = value; }
        }

        public string market_county
        {
            get { return _market_county; }
            set { _market_county = value; }
        }

        public string market_association
        {
            get { return _market_association; }
            set { _market_association = value; }
        }

        public string market_msa
        {
            get { return _market_msa; }
            set { _market_msa = value; }
        }

        public decimal tota_price
        {
            get { return _tota_price; }
            set { _tota_price = value; }
        }

        public decimal invoice_tax
        {
            get { return _invoice_tax; }
            set { _invoice_tax = value; }
        }

        public string invoice_promocode
        {
            get { return _invoice_promocode; }
            set { _invoice_promocode = value; }
        }

        public string invoice_transaction_id
        {
            get { return _invoice_transaction_id; }
            set { _invoice_transaction_id = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string headline
        {
            get { return _headline; }
            set { _headline = value; }
        }

        public string theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        public string layout
        {
            get { return _layout; }
            set { _layout = value; }
        }

        public DateTime delivery_date
        {
            get { return _delivery_date; }
            set { _delivery_date = value; }
        }

        public string mls_number
        {
            get { return _mls_number; }
            set { _mls_number = value; }
        }

        public string email_subject
        {
            get { return _email_subject; }
            set { _email_subject = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string sub_title
        {
            get { return _sub_title; }
            set { _sub_title = value; }
        }

        public string prop_address1
        {
            get { return _prop_address1; }
            set { _prop_address1 = value; }
        }

        public string prop_address2
        {
            get { return _prop_address2; }
            set { _prop_address2 = value; }
        }

        public string prop_city
        {
            get { return _prop_city; }
            set { _prop_city = value; }
        }

        public string prop_state
        {
            get { return _prop_state; }
            set { _prop_state = value; }
        }

        public string prop_zipcode
        {
            get { return _prop_zipcode; }
            set { _prop_zipcode = value; }
        }

        public string prop_desc
        {
            get { return _prop_desc; }
            set { _prop_desc = value; }
        }

        public string prop_price
        {
            get { return _prop_price; }
            set { _prop_price = value; }
        }

        public string bullet1
        {
            get { return _bullet1; }
            set { _bullet1 = value; }
        }

        public string bullet2
        {
            get { return _bullet2; }
            set { _bullet2 = value; }
        }

        public string bullet3
        {
            get { return _bullet3; }
            set { _bullet3 = value; }
        }

        public string bullet4
        {
            get { return _bullet4; }
            set { _bullet4 = value; }
        }

        public string bullet5
        {
            get { return _bullet5; }
            set { _bullet5 = value; }
        }

        public string bullet6
        {
            get { return _bullet6; }
            set { _bullet6 = value; }
        }

        public string bullet7
        {
            get { return _bullet7; }
            set { _bullet7 = value; }
        }

        public string bullet8
        {
            get { return _bullet8; }
            set { _bullet8 = value; }
        }

        public string custom_field1
        {
            get { return _custom_field1; }
            set { _custom_field1 = value; }
        }

        public string custom_field2
        {
            get { return _custom_field2; }
            set { _custom_field2 = value; }
        }

        public string custom_field3
        {
            get { return _custom_field3; }
            set { _custom_field3 = value; }
        }

        public string custom_field4
        {
            get { return _custom_field4; }
            set { _custom_field4 = value; }
        }

        public string custom_field5
        {
            get { return _custom_field5; }
            set { _custom_field5 = value; }
        }

        public string custom_field6
        {
            get { return _custom_field6; }
            set { _custom_field6 = value; }
        }

        public string custom_field7
        {
            get { return _custom_field7; }
            set { _custom_field7 = value; }
        }

        public string custom_field8
        {
            get { return _custom_field8; }
            set { _custom_field8 = value; }
        }

        public string custom_field9
        {
            get { return _custom_field9; }
            set { _custom_field9 = value; }
        }

        public string custom_field10
        {
            get { return _custom_field10; }
            set { _custom_field10 = value; }
        }

        public string custom_field_value1
        {
            get { return _custom_field_value1; }
            set { _custom_field_value1 = value; }
        }

        public string custom_field_value2
        {
            get { return _custom_field_value2; }
            set { _custom_field_value2 = value; }
        }

        public string custom_field_value3
        {
            get { return _custom_field_value3; }
            set { _custom_field_value3 = value; }
        }

        public string custom_field_value4
        {
            get { return _custom_field_value4; }
            set { _custom_field_value4 = value; }
        }

        public string custom_field_value5
        {
            get { return _custom_field_value5; }
            set { _custom_field_value5 = value; }
        }

        public string custom_field_value6
        {
            get { return _custom_field_value6; }
            set { _custom_field_value6 = value; }
        }

        public string custom_field_value7
        {
            get { return _custom_field_value7; }
            set { _custom_field_value7 = value; }
        }

        public string custom_field_value8
        {
            get { return _custom_field_value8; }
            set { _custom_field_value8 = value; }
        }

        public string custom_field_value9
        {
            get { return _custom_field_value9; }
            set { _custom_field_value9 = value; }
        }

        public string custom_field_value10
        {
            get { return _custom_field_value10; }
            set { _custom_field_value10 = value; }
        }

        public string mls_link
        {
            get { return _mls_link; }
            set { _mls_link = value; }
        }

        public string virtualtour_link
        {
            get { return _virtualtour_link; }
            set { _virtualtour_link = value; }
        }

        public string map_link
        {
            get { return _map_link; }
            set { _map_link = value; }
        }

        public bool use_mls_logo
        {
            get { return _use_mls_logo; }
            set { _use_mls_logo = value; }
        }

        public bool use_housing_logo
        {
            get { return _use_housing_logo; }
            set { _use_housing_logo = value; }
        }

        public string markup
        {
            get { return _markup; }
            set { _markup = value; }
        }

        public string flyer
        {
            get { return _flyer; }
            set { _flyer = value; }
        }

        public string price_range_min
        {
            get { return _price_range_min; }
            set { _price_range_min = value; }
        }

        public string price_range_max
        {
            get { return _price_range_max; }
            set { _price_range_max = value; }
        }

        public string property_type
        {
            get { return _property_type; }
            set { _property_type = value; }
        }

        public string sqft_range_min
        {
            get { return _sqft_range_min; }
            set { _sqft_range_min = value; }
        }

        public string sqft_range_max
        {
            get { return _sqft_range_max; }
            set { _sqft_range_max = value; }
        }

        public string location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string more_info
        {
            get { return _more_info; }
            set { _more_info = value; }
        }

        public string buyer_message
        {
            get { return _buyer_message; }
            set { _buyer_message = value; }
        }

        public string photo1
        {
            get { return _photo1; }
            set { _photo1 = value; }
        }

        public string photo2
        {
            get { return _photo2; }
            set { _photo2 = value; }
        }

        public string photo3
        {
            get { return _photo3; }
            set { _photo3 = value; }
        }

        public string photo4
        {
            get { return _photo4; }
            set { _photo4 = value; }
        }

        public string photo5
        {
            get { return _photo5; }
            set { _photo5 = value; }
        }

        public string photo6
        {
            get { return _photo6; }
            set { _photo6 = value; }
        }

        public string photo7
        {
            get { return _photo7; }
            set { _photo7 = value; }
        }

        public string photo8
        {
            get { return _photo8; }
            set { _photo8 = value; }
        }

        public string photo9
        {
            get { return _photo9; }
            set { _photo9 = value; }
        }

        public string photo10
        {
            get { return _photo10; }
            set { _photo10 = value; }
        }

        public string field1
        {
            get { return _field1; }
            set { _field1 = value; }
        }

        public string field2
        {
            get { return _field2; }
            set { _field2 = value; }
        }

        public string field3
        {
            get { return _field3; }
            set { _field3 = value; }
        }

        public string field4
        {
            get { return _field4; }
            set { _field4 = value; }
        }

        public string field5
        {
            get { return _field5; }
            set { _field5 = value; }
        }

        public DateTime created_on
        {
            get { return _created_on; }
            set { _created_on = value; }
        }

        public DateTime updated_on
        {
            get { return _updated_on; }
            set { _updated_on = value; }
        }

        public DateTime sent_on
        {
            get { return _sent_on; }
            set { _sent_on = value; }
        }

        //================New Fields==================
        public int LastPageNo
        {
            get { return _LastPageNo; }
            set { _LastPageNo = value; }
        }

        public string Bedrooms
        {
            get { return _Bedrooms; }
            set { _Bedrooms = value; }
        }

        public string FullBaths
        {
            get { return _FullBaths; }
            set { _FullBaths = value; }
        }

        public string HalfBaths
        {
            get { return _HalfBaths; }
            set { _HalfBaths = value; }
        }

        public string Parking
        {
            get { return _Parking; }
            set { _Parking = value; }
        }

        public string SqFoots
        {
            get { return _SqFoots; }
            set { _SqFoots = value; }
        }

        public string YearBuilt
        {
            get { return _YearBuilt; }
            set { _YearBuilt = value; }
        }

        public string Floors
        {
            get { return _Floors; }
            set { _Floors = value; }
        }

        public string LotSize
        {
            get { return _LotSize; }
            set { _LotSize = value; }
        }

        public string Subdivision
        {
            get { return _Subdivision; }
            set { _Subdivision = value; }
        }

        public string HOA
        {
            get { return _HOA; }
            set { _HOA = value; }
        }

        public string PropertyFeatures
        {
            get { return _PropertyFeatures; }
            set { _PropertyFeatures = value; }
        }

        public string PropertyFeaturesValues
        {
            get { return _PropertyFeaturesValues; }
            set { _PropertyFeaturesValues = value; }
        }

        public string OtherPropertyFeatures
        {
            get { return _OtherPropertyFeatures; }
            set { _OtherPropertyFeatures = value; }
        }

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public int PropertyCategory
        {
            get { return _PropertyCategory; }
            set { _PropertyCategory = value; }
        }

        public int PropertyType
        {
            get { return _PropertyType; }
            set { _PropertyType = value; }
        }

        public bool IsSyndicat
        {
            get { return _IsSyndicat; }
            set { _IsSyndicat = value; }
        }

        public String AptSuiteBldg
        {
            get { return _AptSuiteBldg; }
            set { _AptSuiteBldg = value; }
        }

        public String OpenHouses
        {
            get { return _OpenHouses; }
            set { _OpenHouses = value; }
        }

        //============================================


        public bool Found
        {
            get { return _found; }
        }

        public Order()
        {

        }

        #endregion

        #region Functions/Routines

        /// -----------------------------------------------------------------------------///
        /// <summary>  
        ///<para>Initialize order object using orderid supplied</para>  
        /// </summary> 
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------/// 
        public Order(int orderid)
        {
            OrdersBLL ordersBLL = new OrdersBLL();
            FlyerMeDS.fly_orderDataTable orderTable = ordersBLL.Adapter.GetOrderByOrderID(orderid);

            if (orderTable.Rows.Count > 0)
            {
                FlyerMeDS.fly_orderRow data = (FlyerMeDS.fly_orderRow)orderTable.Rows[0];

                if (!Convert.IsDBNull(data["order_id"])) _order_id = (long)data["order_id"];
                if (!Convert.IsDBNull(data["customer_id"])) _customer_id = (string)data["customer_id"];
                if (!Convert.IsDBNull(data["type"])) _type = (string)data["type"];
                if (!Convert.IsDBNull(data["photo_type"])) _photo_type = (string)data["photo_type"];
                if (!Convert.IsDBNull(data["market_state"])) _market_state = (string)data["market_state"];
                if (!Convert.IsDBNull(data["market_county"])) _market_county = (string)data["market_county"];
                if (!Convert.IsDBNull(data["market_association"])) _market_association = (string)data["market_association"];
                if (!Convert.IsDBNull(data["market_msa"])) _market_msa = (string)data["market_msa"];
                if (!Convert.IsDBNull(data["tota_price"])) _tota_price = (decimal)data["tota_price"];
                if (!Convert.IsDBNull(data["invoice_tax"])) _invoice_tax = (decimal)data["invoice_tax"];
                if (!Convert.IsDBNull(data["invoice_promocode"])) _invoice_promocode = (string)data["invoice_promocode"];
                if (!Convert.IsDBNull(data["invoice_transaction_id"])) _invoice_transaction_id = (string)data["invoice_transaction_id"];
                if (!Convert.IsDBNull(data["status"])) _status = (string)data["status"];
                if (!Convert.IsDBNull(data["headline"])) _headline = (string)data["headline"];
                if (!Convert.IsDBNull(data["theme"])) _theme = (string)data["theme"];
                if (!Convert.IsDBNull(data["layout"])) _layout = (string)data["layout"];
                if (!Convert.IsDBNull(data["delivery_date"])) _delivery_date = (DateTime)data["delivery_date"];
                if (!Convert.IsDBNull(data["mls_number"])) _mls_number = (string)data["mls_number"];
                if (!Convert.IsDBNull(data["email_subject"])) _email_subject = (string)data["email_subject"];
                if (!Convert.IsDBNull(data["title"])) _title = (string)data["title"];
                if (!Convert.IsDBNull(data["sub_title"])) _sub_title = (string)data["sub_title"];
                if (!Convert.IsDBNull(data["prop_address1"])) _prop_address1 = (string)data["prop_address1"];
                if (!Convert.IsDBNull(data["prop_address2"])) _prop_address2 = (string)data["prop_address2"];
                if (!Convert.IsDBNull(data["prop_city"])) _prop_city = (string)data["prop_city"];
                if (!Convert.IsDBNull(data["prop_state"])) _prop_state = (string)data["prop_state"];
                if (!Convert.IsDBNull(data["prop_zipcode"])) _prop_zipcode = (string)data["prop_zipcode"];
                if (!Convert.IsDBNull(data["prop_desc"])) _prop_desc = (string)data["prop_desc"];
                if (!Convert.IsDBNull(data["prop_price"])) _prop_price = (string)data["prop_price"];
                if (!Convert.IsDBNull(data["bullet1"])) _bullet1 = (string)data["bullet1"];
                if (!Convert.IsDBNull(data["bullet2"])) _bullet2 = (string)data["bullet2"];
                if (!Convert.IsDBNull(data["bullet3"])) _bullet3 = (string)data["bullet3"];
                if (!Convert.IsDBNull(data["bullet4"])) _bullet4 = (string)data["bullet4"];
                if (!Convert.IsDBNull(data["bullet5"])) _bullet5 = (string)data["bullet5"];
                if (!Convert.IsDBNull(data["bullet6"])) _bullet6 = (string)data["bullet6"];
                if (!Convert.IsDBNull(data["bullet7"])) _bullet7 = (string)data["bullet7"];
                if (!Convert.IsDBNull(data["bullet8"])) _bullet8 = (string)data["bullet8"];
                if (!Convert.IsDBNull(data["custom_field1"])) _custom_field1 = (string)data["custom_field1"];
                if (!Convert.IsDBNull(data["custom_field2"])) _custom_field2 = (string)data["custom_field2"];
                if (!Convert.IsDBNull(data["custom_field3"])) _custom_field3 = (string)data["custom_field3"];
                if (!Convert.IsDBNull(data["custom_field4"])) _custom_field4 = (string)data["custom_field4"];
                if (!Convert.IsDBNull(data["custom_field5"])) _custom_field5 = (string)data["custom_field5"];
                if (!Convert.IsDBNull(data["custom_field6"])) _custom_field6 = (string)data["custom_field6"];
                if (!Convert.IsDBNull(data["custom_field7"])) _custom_field7 = (string)data["custom_field7"];
                if (!Convert.IsDBNull(data["custom_field8"])) _custom_field8 = (string)data["custom_field8"];
                if (!Convert.IsDBNull(data["custom_field9"])) _custom_field9 = (string)data["custom_field9"];
                if (!Convert.IsDBNull(data["custom_field10"])) _custom_field10 = (string)data["custom_field10"];
                if (!Convert.IsDBNull(data["custom_field_value1"])) _custom_field_value1 = (string)data["custom_field_value1"];
                if (!Convert.IsDBNull(data["custom_field_value2"])) _custom_field_value2 = (string)data["custom_field_value2"];
                if (!Convert.IsDBNull(data["custom_field_value3"])) _custom_field_value3 = (string)data["custom_field_value3"];
                if (!Convert.IsDBNull(data["custom_field_value4"])) _custom_field_value4 = (string)data["custom_field_value4"];
                if (!Convert.IsDBNull(data["custom_field_value5"])) _custom_field_value5 = (string)data["custom_field_value5"];
                if (!Convert.IsDBNull(data["custom_field_value6"])) _custom_field_value6 = (string)data["custom_field_value6"];
                if (!Convert.IsDBNull(data["custom_field_value7"])) _custom_field_value7 = (string)data["custom_field_value7"];
                if (!Convert.IsDBNull(data["custom_field_value8"])) _custom_field_value8 = (string)data["custom_field_value8"];
                if (!Convert.IsDBNull(data["custom_field_value9"])) _custom_field_value9 = (string)data["custom_field_value9"];
                if (!Convert.IsDBNull(data["custom_field_value10"])) _custom_field_value10 = (string)data["custom_field_value10"];
                if (!Convert.IsDBNull(data["mls_link"])) _mls_link = (string)data["mls_link"];
                if (!Convert.IsDBNull(data["virtualtour_link"])) _virtualtour_link = (string)data["virtualtour_link"];
                if (!Convert.IsDBNull(data["map_link"])) _map_link = (string)data["map_link"];
                if (!Convert.IsDBNull(data["use_mls_logo"])) _use_mls_logo = (bool)data["use_mls_logo"];
                if (!Convert.IsDBNull(data["use_housing_logo"])) _use_housing_logo = (bool)data["use_housing_logo"];
                if (!Convert.IsDBNull(data["markup"])) _markup = (string)data["markup"];
                if (!Convert.IsDBNull(data["flyer"])) _flyer = (string)data["flyer"];
                if (!Convert.IsDBNull(data["price_range_min"])) _price_range_min = (string)data["price_range_min"];
                if (!Convert.IsDBNull(data["price_range_max"])) _price_range_max = (string)data["price_range_max"];
                if (!Convert.IsDBNull(data["property_type"])) _property_type = (string)data["property_type"];
                if (!Convert.IsDBNull(data["sqft_range_min"])) _sqft_range_min = (string)data["sqft_range_min"];
                if (!Convert.IsDBNull(data["sqft_range_max"])) _sqft_range_max = (string)data["sqft_range_max"];
                if (!Convert.IsDBNull(data["location"])) _location = (string)data["location"];
                if (!Convert.IsDBNull(data["more_info"])) _more_info = (string)data["more_info"];
                if (!Convert.IsDBNull(data["buyer_message"])) _buyer_message = (string)data["buyer_message"];
                if (!Convert.IsDBNull(data["photo1"])) _photo1 = (string)data["photo1"];
                if (!Convert.IsDBNull(data["photo2"])) _photo2 = (string)data["photo2"];
                if (!Convert.IsDBNull(data["photo3"])) _photo3 = (string)data["photo3"];
                if (!Convert.IsDBNull(data["photo4"])) _photo4 = (string)data["photo4"];
                if (!Convert.IsDBNull(data["photo5"])) _photo5 = (string)data["photo5"];
                if (!Convert.IsDBNull(data["photo6"])) _photo6 = (string)data["photo6"];
                if (!Convert.IsDBNull(data["photo7"])) _photo7 = (string)data["photo7"];
                if (!Convert.IsDBNull(data["photo8"])) _photo8 = (string)data["photo8"];
                if (!Convert.IsDBNull(data["photo9"])) _photo9 = (string)data["photo9"];
                if (!Convert.IsDBNull(data["photo10"])) _photo10 = (string)data["photo10"];
                if (!Convert.IsDBNull(data["field1"])) _field1 = (string)data["field1"];
                if (!Convert.IsDBNull(data["field2"])) _field2 = (string)data["field2"];
                if (!Convert.IsDBNull(data["field3"])) _field3 = (string)data["field3"];
                if (!Convert.IsDBNull(data["field4"])) _field4 = (string)data["field4"];
                if (!Convert.IsDBNull(data["field5"])) _field5 = (string)data["field5"];
                if (!Convert.IsDBNull(data["created_on"])) _created_on = (DateTime)data["created_on"];
                if (!Convert.IsDBNull(data["updated_on"])) _updated_on = (DateTime)data["updated_on"];
                if (!Convert.IsDBNull(data["sent_on"])) _sent_on = (DateTime)data["sent_on"];
                //============New Fields========================================================================
                if (!Convert.IsDBNull(data["LastPageNo"])) _LastPageNo = (int)data["LastPageNo"];
                if (!Convert.IsDBNull(data["Bedrooms"])) _Bedrooms = (string)data["Bedrooms"];
                if (!Convert.IsDBNull(data["FullBaths"])) _FullBaths = (string)data["FullBaths"];
                if (!Convert.IsDBNull(data["HalfBaths"])) _HalfBaths = (string)data["HalfBaths"];
                if (!Convert.IsDBNull(data["Parking"])) _Parking = (string)data["Parking"];
                if (!Convert.IsDBNull(data["SqFoots"])) _SqFoots = (string)data["SqFoots"];
                if (!Convert.IsDBNull(data["YearBuilt"])) _YearBuilt = (string)data["YearBuilt"];
                if (!Convert.IsDBNull(data["Floors"])) _Floors = (string)data["Floors"];
                if (!Convert.IsDBNull(data["LotSize"])) _LotSize = (string)data["LotSize"];
                if (!Convert.IsDBNull(data["Subdivision"])) _Subdivision = (string)data["Subdivision"];
                if (!Convert.IsDBNull(data["HOA"])) _HOA = (string)data["HOA"];
                if (!Convert.IsDBNull(data["PropertyFeatures"])) _PropertyFeatures = (string)data["PropertyFeatures"];
                if (!Convert.IsDBNull(data["PropertyFeaturesValues"])) _PropertyFeaturesValues = (string)data["PropertyFeaturesValues"];
                if (!Convert.IsDBNull(data["OtherPropertyFeatures"])) _OtherPropertyFeatures = (string)data["OtherPropertyFeatures"];
                if (!Convert.IsDBNull(data["Discount"])) _Discount = (decimal)data["Discount"];
                if (!Convert.IsDBNull(data["fk_PropertyCategory"])) _PropertyCategory = (int)data["fk_PropertyCategory"];
                if (!Convert.IsDBNull(data["fk_PropertyType"])) _PropertyType = (int)data["fk_PropertyType"];
                if (!Convert.IsDBNull(data["IsSyndicat"])) _IsSyndicat = (bool)data["IsSyndicat"];
                if (!Convert.IsDBNull(data["AptSuiteBldg"])) _AptSuiteBldg = (String)data["AptSuiteBldg"];
                if (!Convert.IsDBNull(data["OpenHouses"])) _OpenHouses = (String)data["OpenHouses"];
                //===============================================================================================
            }
        }

        /// -----------------------------------------------------------------------------///
        /// <summary>  
        ///<para>Saves current order data</para>  
        /// </summary>  
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------/// 
        public void Save()
        {
            if (this.order_id == 0) 
            { 
                Insert(); 
            } else 
            { 
                Update(); 
            }
        }

        /// -----------------------------------------------------------------------------///
        /// <summary>  
        ///<para>Inserts new order row</para>  
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------/// 
        protected void Insert()
        {
            OrdersBLL orderBll = new OrdersBLL();
            this.order_id = orderBll.Insert(this);
        }

        /// -----------------------------------------------------------------------------/// 
        /// <summary>  
        ///<para>Update current order informaiton</para>  
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------/// 
        protected void Update()
        {
            OrdersBLL orderBll = new OrdersBLL();
            bool success = orderBll.Update(this);
        }


        #endregion

        public static Order CreateSellerOrder(String customerid)
        {
            var result = new Order();

            result.customer_id = customerid;
            result.type = FlyerTypes.Seller.ToString().ToLower();
            result.status = Order.flyerstatus.Incomplete.ToString();
            result.delivery_date = DateTime.Now;
            result.created_on = DateTime.Now;
            result.updated_on = DateTime.Now;
            result.sent_on = DateTime.Now;
            result.use_mls_logo = true;
            result.use_housing_logo = true;
            result.headline = String.Empty;
            result.theme = String.Empty;
            result.layout = "L21";
            result.headline = "H01";
            result.theme = "T01";
            result.map_link = String.Empty;
            result.field5 = Helper.GetCustomerNameByEmail(customerid);
            result.Save();

            return result;
        }

        public static Order CreateBuyerOrder(String customerid)
        {
            var result = new Order();

            result.customer_id = customerid;
            result.type = FlyerTypes.Buyer.ToString().ToLower();
            result.status = Order.flyerstatus.Incomplete.ToString();
            result.delivery_date = DateTime.Now;
            result.created_on = DateTime.Now;
            result.updated_on = DateTime.Now;
            result.sent_on = DateTime.Now;
            result.use_mls_logo = true;
            result.use_housing_logo = true;
            result.headline = String.Empty;
            result.theme = String.Empty;
            result.layout = "B61";
            result.theme = "T01";
            result.map_link = String.Empty;
            result.field5 = Helper.GetCustomerNameByEmail(customerid);
            result.Save();

            return result;
        }

        public static Order CreateCustomOrder(string customerid)
        {
            var result = new Order();

            result.customer_id = customerid;
            result.type = FlyerTypes.Custom.ToString().ToLower();
            result.status = Order.flyerstatus.Incomplete.ToString();
            result.delivery_date = DateTime.Now;
            result.created_on = DateTime.Now;
            result.updated_on = DateTime.Now;
            result.sent_on = DateTime.Now;
            result.use_mls_logo = true;
            result.use_housing_logo = true;
            result.headline = String.Empty;
            result.theme = String.Empty;
            result.layout = "C01";
            result.theme = "T01";
            result.map_link = String.Empty;
            result.field5 = Helper.GetCustomerNameByEmail(customerid);
            result.Save();

            return result;
        }
    }
}