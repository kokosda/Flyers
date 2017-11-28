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
    /// Order business logic class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------

    [Serializable]
    public class OrdersBLL
    {
        private fly_orderTableAdapter _orderTableAdapter = null;

        /// <summary>  
        ///<para>Get orde table adapter</para>  
        /// </summary>
        public fly_orderTableAdapter Adapter
        {
            get
            {
                if (_orderTableAdapter == null)
                    _orderTableAdapter = new fly_orderTableAdapter();

                return _orderTableAdapter;
            }
        }

        /// <summary>  
        ///<para>Deletes order</para>  
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Delete, true)]
        public bool Delete(int orderID)
        {
            int rowsAffected = Adapter.Delete(orderID);

            // Return true if precisely one row was deleted,
            // otherwise false
            return rowsAffected == 1;
        }

        /// <summary>  
        ///<para>Inserts new order</para>  
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Insert, true)]
        public int Insert(Order order)
        {
            object ret = Adapter.InsertOrder(
                    order.customer_id,
                    order.type,
                    order.photo_type,
                    order.market_state,
                    order.market_county,
                    order.market_association,
                    order.market_msa,
                    order.tota_price,
                    order.invoice_tax,
                    order.invoice_promocode,
                    order.invoice_transaction_id,
                    order.status,
                    order.headline,
                    order.theme,
                    order.layout,
                    order.delivery_date,
                    order.mls_number,
                    order.email_subject,
                    order.title,
                    order.sub_title,
                    order.prop_address1,
                    order.prop_address2,
                    order.prop_city,
                    order.prop_state,
                    order.prop_zipcode,
                    order.prop_desc,
                    order.prop_price,
                    order.bullet1,
                    order.bullet2,
                    order.bullet3,
                    order.bullet4,
                    order.bullet5,
                    order.bullet6,
                    order.bullet7,
                    order.bullet8,
                    order.custom_field1,
                    order.custom_field2,
                    order.custom_field3,
                    order.custom_field4,
                    order.custom_field5,
                    order.custom_field6,
                    order.custom_field7,
                    order.custom_field8,
                    order.custom_field9,
                    order.custom_field10,
                    order.custom_field_value1,
                    order.custom_field_value2,
                    order.custom_field_value3,
                    order.custom_field_value4,
                    order.custom_field_value5,
                    order.custom_field_value6,
                    order.custom_field_value7,
                    order.custom_field_value8,
                    order.custom_field_value9,
                    order.custom_field_value10,
                    order.mls_link,
                    order.virtualtour_link,
                    order.map_link,
                    order.use_mls_logo,
                    order.use_housing_logo,
                    order.markup,
                    order.flyer,
                    order.price_range_min,
                    order.price_range_max,
                    order.property_type,
                    order.sqft_range_min,
                    order.sqft_range_max,
                    order.location,
                    order.more_info,
                    order.buyer_message,
                    order.photo1,
                    order.photo2,
                    order.photo3,
                    order.photo4,
                    order.photo5,
                    order.photo6,
                    order.photo7,
                    order.photo8,
                    order.photo9,
                    order.photo10,
                    order.field1,
                    order.field2,
                    order.field3,
                    order.field4,
                    order.field5,
                    order.created_on,
                    order.updated_on,
                    order.sent_on,
                    order.Bedrooms,
                    order.FullBaths,
                    order.HalfBaths,
                    order.Parking,
                    order.SqFoots,
                    order.YearBuilt,
                    order.Floors,
                    order.LotSize,
                    order.Subdivision,
                    order.HOA,
                    order.PropertyFeatures,
                    order.PropertyFeaturesValues,
                    order.OtherPropertyFeatures,
                    order.Discount,
                    order.PropertyCategory,
                    order.PropertyType,
                    order.AptSuiteBldg,
                    order.OpenHouses
                    );

            return Convert.ToInt32(ret);
        }

        /// <summary>  
        ///<para>Update order</para>  
        /// </summary>
        [System.ComponentModel.DataObjectMethodAttribute
            (System.ComponentModel.DataObjectMethodType.Update, true)]
        public bool Update(Order order)
        {
            FlyerMeDS.fly_orderDataTable orders = Adapter.GetOrderByOrderID(Convert.ToInt32(order.order_id));
            if (orders.Count == 0)
                // no matching record found, return false
                return false;
            FlyerMeDS.fly_orderRow orderrow = orders[0];

            orderrow.customer_id = order.customer_id;
            if (order.type == null) orderrow.SettypeNull();
            else orderrow.type = order.type;
            if (order.photo_type == null) orderrow.Setphoto_typeNull();
            else orderrow.photo_type = order.photo_type;
            if (order.market_state == null) orderrow.Setmarket_stateNull();
            else orderrow.market_state = order.market_state;
            if (order.market_county == null) orderrow.Setmarket_countyNull();
            else orderrow.market_county = order.market_county;
            if (order.market_association == null) orderrow.Setmarket_associationNull();
            else orderrow.market_association = order.market_association;
            if (order.market_msa == null) orderrow.Setmarket_msaNull();
            else orderrow.market_msa = order.market_msa;
            orderrow.tota_price = order.tota_price;
            orderrow.invoice_tax = order.invoice_tax;
            if (order.invoice_promocode == null) orderrow.Setinvoice_promocodeNull();
            else orderrow.invoice_promocode = order.invoice_promocode;
            if (order.invoice_transaction_id == null) orderrow.Setinvoice_transaction_idNull();
            else orderrow.invoice_transaction_id = order.invoice_transaction_id;
            if (order.status == null) orderrow.SetstatusNull();
            else orderrow.status = order.status;
            if (order.headline == null) orderrow.SetheadlineNull();
            else orderrow.headline = order.headline;
            if (order.theme == null) orderrow.SetthemeNull();
            else orderrow.theme = order.theme;
            if (order.layout == null) orderrow.SetlayoutNull();
            else orderrow.layout = order.layout;
            if (order.delivery_date == null) orderrow.Setdelivery_dateNull();
            else orderrow.delivery_date = order.delivery_date;
            if (order.mls_number == null) orderrow.Setmls_numberNull();
            else orderrow.mls_number = order.mls_number;
            if (order.email_subject == null) orderrow.Setemail_subjectNull();
            else orderrow.email_subject = order.email_subject;
            if (order.title == null) orderrow.SettitleNull();
            else orderrow.title = order.title;
            if (order.sub_title == null) orderrow.Setsub_titleNull();
            else orderrow.sub_title = order.sub_title;
            if (order.prop_address1 == null) orderrow.Setprop_address1Null();
            else orderrow.prop_address1 = order.prop_address1;
            if (order.prop_address2 == null) orderrow.Setprop_address2Null();
            else orderrow.prop_address2 = order.prop_address2;
            if (order.prop_city == null) orderrow.Setprop_cityNull();
            else orderrow.prop_city = order.prop_city;
            if (order.prop_state == null) orderrow.Setprop_stateNull();
            else orderrow.prop_state = order.prop_state;
            if (order.prop_zipcode == null) orderrow.Setprop_zipcodeNull();
            else orderrow.prop_zipcode = order.prop_zipcode;
            if (order.prop_desc == null) orderrow.Setprop_descNull();
            else orderrow.prop_desc = order.prop_desc;
            if (order.prop_price == null) orderrow.Setprop_priceNull();
            else orderrow.prop_price = order.prop_price;
            if (order.bullet1 == null) orderrow.Setbullet1Null();
            else orderrow.bullet1 = order.bullet1;
            if (order.bullet2 == null) orderrow.Setbullet2Null();
            else orderrow.bullet2 = order.bullet2;
            if (order.bullet3 == null) orderrow.Setbullet3Null();
            else orderrow.bullet3 = order.bullet3;
            if (order.bullet4 == null) orderrow.Setbullet4Null();
            else orderrow.bullet4 = order.bullet4;
            if (order.bullet5 == null) orderrow.Setbullet5Null();
            else orderrow.bullet5 = order.bullet5;
            if (order.bullet6 == null) orderrow.Setbullet6Null();
            else orderrow.bullet6 = order.bullet6;
            if (order.bullet7 == null) orderrow.Setbullet7Null();
            else orderrow.bullet7 = order.bullet7;
            if (order.bullet8 == null) orderrow.Setbullet8Null();
            else orderrow.bullet8 = order.bullet8;
            if (order.custom_field1 == null) orderrow.Setcustom_field1Null();
            else orderrow.custom_field1 = order.custom_field1;
            if (order.custom_field2 == null) orderrow.Setcustom_field2Null();
            else orderrow.custom_field2 = order.custom_field2;
            if (order.custom_field3 == null) orderrow.Setcustom_field3Null();
            else orderrow.custom_field3 = order.custom_field3;
            if (order.custom_field4 == null) orderrow.Setcustom_field4Null();
            else orderrow.custom_field4 = order.custom_field4;
            if (order.custom_field5 == null) orderrow.Setcustom_field5Null();
            else orderrow.custom_field5 = order.custom_field5;
            if (order.custom_field6 == null) orderrow.Setcustom_field6Null();
            else orderrow.custom_field6 = order.custom_field6;
            if (order.custom_field7 == null) orderrow.Setcustom_field7Null();
            else orderrow.custom_field7 = order.custom_field7;
            if (order.custom_field8 == null) orderrow.Setcustom_field8Null();
            else orderrow.custom_field8 = order.custom_field8;
            if (order.custom_field9 == null) orderrow.Setcustom_field9Null();
            else orderrow.custom_field9 = order.custom_field9;
            if (order.custom_field10 == null) orderrow.Setcustom_field10Null();
            else orderrow.custom_field10 = order.custom_field10;
            if (order.custom_field_value1 == null) orderrow.Setcustom_field_value1Null();
            else orderrow.custom_field_value1 = order.custom_field_value1;
            if (order.custom_field_value2 == null) orderrow.Setcustom_field_value2Null();
            else orderrow.custom_field_value2 = order.custom_field_value2;
            if (order.custom_field_value3 == null) orderrow.Setcustom_field_value3Null();
            else orderrow.custom_field_value3 = order.custom_field_value3;
            if (order.custom_field_value4 == null) orderrow.Setcustom_field_value4Null();
            else orderrow.custom_field_value4 = order.custom_field_value4;
            if (order.custom_field_value5 == null) orderrow.Setcustom_field_value5Null();
            else orderrow.custom_field_value5 = order.custom_field_value5;
            if (order.custom_field_value6 == null) orderrow.Setcustom_field_value6Null();
            else orderrow.custom_field_value6 = order.custom_field_value6;
            if (order.custom_field_value7 == null) orderrow.Setcustom_field_value7Null();
            else orderrow.custom_field_value7 = order.custom_field_value7;
            if (order.custom_field_value8 == null) orderrow.Setcustom_field_value8Null();
            else orderrow.custom_field_value8 = order.custom_field_value8;
            if (order.custom_field_value9 == null) orderrow.Setcustom_field_value9Null();
            else orderrow.custom_field_value9 = order.custom_field_value9;
            if (order.custom_field_value10 == null) orderrow.Setcustom_field_value10Null();
            else orderrow.custom_field_value10 = order.custom_field_value10;
            if (order.mls_link == null) orderrow.Setmls_linkNull();
            else orderrow.mls_link = order.mls_link;
            if (order.virtualtour_link == null) orderrow.Setvirtualtour_linkNull();
            else orderrow.virtualtour_link = order.virtualtour_link;
            if (order.map_link == null) orderrow.Setmap_linkNull();
            else orderrow.map_link = order.map_link;
            //if (order.use_mls_logo == null) orderrow.Setuse_mls_logoNull();
            //else 
            orderrow.use_mls_logo = order.use_mls_logo;
            //if (order.use_housing_logo == null) orderrow.Setuse_housing_logoNull();
            //else 
            orderrow.use_housing_logo = order.use_housing_logo;
            if (order.markup == null) orderrow.SetmarkupNull();
            else orderrow.markup = order.markup;
            if (order.flyer == null) orderrow.SetflyerNull();
            else orderrow.flyer = order.flyer;
            if (order.price_range_min == null) orderrow.Setprice_range_minNull();
            else orderrow.price_range_min = order.price_range_min;
            if (order.price_range_max == null) orderrow.Setprice_range_maxNull();
            else orderrow.price_range_max = order.price_range_max;
            if (order.property_type == null) orderrow.Setproperty_typeNull();
            else orderrow.property_type = order.property_type;
            if (order.sqft_range_min == null) orderrow.Setsqft_range_minNull();
            else orderrow.sqft_range_min = order.sqft_range_min;
            if (order.sqft_range_max == null) orderrow.Setsqft_range_maxNull();
            else orderrow.sqft_range_max = order.sqft_range_max;
            if (order.location == null) orderrow.SetlocationNull();
            else orderrow.location = order.location;
            if (order.more_info == null) orderrow.Setmore_infoNull();
            else orderrow.more_info = order.more_info;
            if (order.buyer_message == null) orderrow.Setbuyer_messageNull();
            else orderrow.buyer_message = order.buyer_message;
            if (order.photo1 == null) orderrow.Setphoto1Null();
            else orderrow.photo1 = order.photo1;
            if (order.photo2 == null) orderrow.Setphoto2Null();
            else orderrow.photo2 = order.photo2;
            if (order.photo3 == null) orderrow.Setphoto3Null();
            else orderrow.photo3 = order.photo3;
            if (order.photo4 == null) orderrow.Setphoto4Null();
            else orderrow.photo4 = order.photo4;
            if (order.photo5 == null) orderrow.Setphoto5Null();
            else orderrow.photo5 = order.photo5;
            if (order.photo6 == null) orderrow.Setphoto6Null();
            else orderrow.photo6 = order.photo6;
            if (order.photo7 == null) orderrow.Setphoto7Null();
            else orderrow.photo7 = order.photo7;
            if (order.photo8 == null) orderrow.Setphoto8Null();
            else orderrow.photo8 = order.photo8;
            if (order.photo9 == null) orderrow.Setphoto9Null();
            else orderrow.photo9 = order.photo9;
            if (order.photo10 == null) orderrow.Setphoto10Null();
            else orderrow.photo10 = order.photo10;
            if (order.field1 == null) orderrow.Setfield1Null();
            else orderrow.field1 = order.field1;
            if (order.field2 == null) orderrow.Setfield2Null();
            else orderrow.field2 = order.field2;
            if (order.field3 == null) orderrow.Setfield3Null();
            else orderrow.field3 = order.field3;
            if (order.field4 == null) orderrow.Setfield4Null();
            else orderrow.field4 = order.field4;
            if (order.field5 == null) orderrow.Setfield5Null();
            else orderrow.field5 = order.field5;
            if (order.created_on == null) orderrow.Setcreated_onNull();
            else orderrow.created_on = order.created_on;
            if (order.updated_on == null) orderrow.Setupdated_onNull();
            else orderrow.updated_on = order.updated_on;
            if (order.sent_on == null) orderrow.Setsent_onNull();
            else orderrow.sent_on = order.sent_on;

            orderrow.LastPageNo = order.LastPageNo;

            //========New Fields==================================
            if (order.Bedrooms == null) orderrow.SetBedroomsNull();
            else orderrow.Bedrooms = order.Bedrooms;
            if (order.FullBaths == null) orderrow.SetFullBathsNull();
            else orderrow.FullBaths = order.FullBaths;
            if (order.HalfBaths == null) orderrow.SetHalfBathsNull();
            else orderrow.HalfBaths = order.HalfBaths;
            if (order.Parking == null) orderrow.SetParkingNull();
            else orderrow.Parking = order.Parking;
            if (order.SqFoots == null) orderrow.SetSqFootsNull();
            else orderrow.SqFoots = order.SqFoots;
            if (order.YearBuilt == null) orderrow.SetYearBuiltNull();
            else orderrow.YearBuilt = order.YearBuilt;
            if (order.Floors == null) orderrow.SetFloorsNull();
            else orderrow.Floors = order.Floors;
            if (order.LotSize == null) orderrow.SetLotSizeNull();
            else orderrow.LotSize = order.LotSize;
            if (order.Subdivision == null) orderrow.SetSubdivisionNull();
            else orderrow.Subdivision = order.Subdivision;
            if (order.HOA == null) orderrow.SetHOANull();
            else orderrow.HOA = order.HOA;
            if (order.PropertyFeatures == null) orderrow.SetPropertyFeaturesNull();
            else orderrow.PropertyFeatures = order.PropertyFeatures;
            if (order.PropertyFeaturesValues == null) orderrow.SetPropertyFeaturesValuesNull();
            else orderrow.PropertyFeaturesValues = order.PropertyFeaturesValues;
            if (order.OtherPropertyFeatures== null) orderrow.SetOtherPropertyFeaturesNull();
            else orderrow.OtherPropertyFeatures = order.OtherPropertyFeatures;

            orderrow.Discount = order.Discount;

            orderrow.fk_PropertyCategory = order.PropertyCategory;
            orderrow.fk_PropertyType = order.PropertyType;

            if (order.AptSuiteBldg == null)
            {
                orderrow.SetAptSuiteBldgNull();
            }
            else
            {
                orderrow.AptSuiteBldg = order.AptSuiteBldg;
            }

            if (order.OpenHouses == null)
            {
                orderrow.SetOpenHousesNull();
            }
            else
            {
                orderrow.OpenHouses = order.OpenHouses;
            }
            //====================================================


            // Update the order record
            int rowsAffected = Adapter.Update(orderrow);

            // Return true if precisely one row was updated,
            // otherwise false
            return rowsAffected == 1;
        }


        public bool UpdateDiscount(string DiscountCode)
        {
            fly_DiscountTableAdapter DiscountAdapter = new fly_DiscountTableAdapter();
            FlyerMeDS.fly_DiscountDataTable OrderDiscount = DiscountAdapter.GetDataForUpdate(DiscountCode);
            if (OrderDiscount.Count == 0)
                // no matching record found, return false
                return false;
            FlyerMeDS.fly_DiscountRow orderdiscountrow = OrderDiscount[0];
            orderdiscountrow.Disable = true;
            // Update the order record
            int rowsAffected = DiscountAdapter.Update(orderdiscountrow);

            // Return true if precisely one row was updated,
            // otherwise false
            return rowsAffected == 1;
        }

        public bool UpdateOfferDiscount(string email, string OfferDiscountCode)
        {
            fly_tblOfferDiscountDetailTableAdapter OfferDiscountAdapter = new fly_tblOfferDiscountDetailTableAdapter();
            FlyerMeDS.fly_tblOfferDiscountDetailDataTable OfferDiscount = OfferDiscountAdapter.GetDataOfferDiscount(email, OfferDiscountCode);
            if (OfferDiscount.Count == 0)
                // no matching record found, return false
                return false;
            FlyerMeDS.fly_tblOfferDiscountDetailRow offerdiscountrow = OfferDiscount[0];
            offerdiscountrow.Active = false;
            // Update the order record
            int rowsAffected = OfferDiscountAdapter.Update(offerdiscountrow);

            // Return true if precisely one row was updated,
            // otherwise false
            return rowsAffected == 1;
        }

    }
}
