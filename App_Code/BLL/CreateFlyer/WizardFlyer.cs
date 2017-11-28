using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe.BLL.CreateFlyer
{
    [Serializable]
    public sealed class WizardFlyer
    {
        private WizardFlyer(FlyerTypes flyerType)
        {
            FlyerType = flyerType;
            Photos = new String[10];
        }

        public FlyerTypes FlyerType { get; private set; }

        public Int64? OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                if (!orderId.HasValue)
                {
                    orderId = value;
                }
                else
                {
                    throw new Exception("Recreate flyer to set related order.");
                }
            }
        }

        public String FlyerTitle { get; set; }

        public String EmailSubject { get; set; }

        public String Name { get; set; }

        public String Phone { get; set; }

        public String Ext { get; set; }

        public String Email { get; set; }

        public String OfferType { get; set; }

        public String PropertyType { get; set; }

        public String ResidentialType { get; set; }

        public String PropertyCategoryId { get; set; }

        public String StreetAddress { get; set; }

        public String AptSuiteBldg { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public String MapLink { get; set; }

        public Boolean? AddMap { get; set; }

        public String Location { get; set; }

        public String MlsNumber { get; set; }

        public String YearBuilt { get; set; }

        public String Subdivision { get; set; }

        public String Hoa { get; set; }

        public String Bedrooms { get; set; }

        public String Bathrooms { get; set; }

        public String LotSize { get; set; }

        public String Sqft { get; set; }

        public String Floors { get; set; }

        public String Parking { get; set; }

        public String OpenHouses { get; set; }

        public String Description { get; set; }
        
        [DisplayName("Central A/C")]
        [Amenity]
        public Boolean CentralAc { get; set; }

        [DisplayName("Central heat")]
        [Amenity]
        public Boolean CentralHeat { get; set; }

        [DisplayName("Fireplace")]
        [Amenity]
        public Boolean Fireplace { get; set; }

        [DisplayName("High/Vaulted ceiling")]
        [Amenity]
        public Boolean HighVaultedCeiling { get; set; }

        [DisplayName("Walk-in closet")]
        [Amenity]
        public Boolean WalkInCloset { get; set; }

        [DisplayName("Hardwood floor")]
        [Amenity]
        public Boolean HardwoodFloor { get; set; }

        [DisplayName("Tile floor")]
        [Amenity]
        public Boolean TileFloor { get; set; }

        [DisplayName("Family room")]
        [Amenity]
        public Boolean FamilyRoom { get; set; }

        [DisplayName("Living room")]
        [Amenity]
        public Boolean LivingRoom { get; set; }

        [DisplayName("Bonus/Rec room")]
        [Amenity]
        public Boolean BonusRecRoom { get; set; }

        [DisplayName("Office/Den")]
        [Amenity]
        public Boolean OfficeDen { get; set; }

        [DisplayName("Dining room")]
        [Amenity]
        public Boolean DiningRoom { get; set; }

        [DisplayName("Breakfast nook")]
        [Amenity]
        public Boolean BreakfastNook { get; set; }

        [DisplayName("Dishwasher")]
        [Amenity]
        public Boolean Dishwasher { get; set; }

        [DisplayName("Refrigerator")]
        [Amenity]
        public Boolean Refrigerator { get; set; }

        [DisplayName("Attic")]
        [Amenity]
        public Boolean Attic { get; set; }

        [DisplayName("Granite countertop")]
        [Amenity]
        public Boolean GraniteCountertop { get; set; }

        [DisplayName("Microwave")]
        [Amenity]
        public Boolean Microwave { get; set; }

        [DisplayName("Stainless steel appliances")]
        [Amenity]
        public Boolean StainlessSteelAppliances { get; set; }

        [DisplayName("Stove/Oven")]
        [Amenity]
        public Boolean StoveOven { get; set; }

        [DisplayName("Basement")]
        [Amenity]
        public Boolean Basement { get; set; }

        [DisplayName("Washer")]
        [Amenity]
        public Boolean Washer { get; set; }

        [DisplayName("Dryer")]
        [Amenity]
        public Boolean Dryer { get; set; }

        [DisplayName("Laundry area - inside")]
        [Amenity]
        public Boolean LaundryAreaInside { get; set; }

        [DisplayName("Laundry area - garage")]
        [Amenity]
        public Boolean LaundryAreaGarage { get; set; }

        [DisplayName("Balcony, Deck, or Patio")]
        [Amenity]
        public Boolean BalconyDeckPatio { get; set; }

        [DisplayName("Yard")]
        [Amenity]
        public Boolean Yard { get; set; }

        [DisplayName("Swimming pool")]
        [Amenity]
        public Boolean SwimmingPool { get; set; }

        [DisplayName("Jacuzzi/ Whirlpool")]
        [Amenity]
        public Boolean JacuzziWhirlpool { get; set; }

        [DisplayName("Sauna")]
        [Amenity]
        public Boolean Sauna { get; set; }

        public String[] Photos { get; set; }

        public String PriceRent { get; set; }

        public Decimal? Price { get; set; }

        public Decimal? PriceMin { get; set; }

        public Decimal? PriceMax { get; set; }

        public String PerArea { get; set; }

        public String RentPeriod { get; set; }

        public String Link { get; set; }

        public String Layout { get; set; }

        public String Markup { get; set; }

        public String MarketState { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public String MarketType { get; set; }

        public PricesList SelectedPricesList { get; set; }

        public Boolean FlyerTitleStepCompleted { get; set; }

        public Boolean ContactDetailsStepCompleted { get; set; }

        public Boolean PropertyTypeStepCompleted { get; set; }

        public Boolean LocationStepCompleted { get; set; }

        public Boolean DesiredLocationStepCompleted { get; set; }

        public Boolean DescriptionStepCompleted { get; set; }

        public Boolean AmenitiesStepCompleted { get; set; }

        public Boolean PhotoStepCompleted { get; set; }

        public Boolean PriceStepCompleted { get; set; }

        public Boolean PriceRangeStepCompleted { get; set; }

        public Boolean ChooseFlyerStepCompleted { get; set; }

        public Boolean UploadFlyerStepCompleted { get; set; }

        public Boolean SelectMarketAreaStepCompleted { get; set; }

        public String[] GetAmenities()
        {
            var result = GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(AmenityAttribute), false).Count() > 0 &&
                                                              (Boolean)p.GetValue(this, null) == true)
                                                  .Select(p => (p.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0] as DisplayNameAttribute).DisplayName)
                                                  .ToArray();

            return result;
        }

        public String GetFullAddress()
        {
            var sb = new StringBuilder(StreetAddress);

            if (!String.IsNullOrEmpty(AptSuiteBldg))
            {
                sb.AppendFormat(" {0}", AptSuiteBldg);
            }
            if (!String.IsNullOrEmpty(State))
            {
                sb.AppendFormat(" {0}", State);
            }
            if (!String.IsNullOrEmpty(ZipCode))
            {
                sb.AppendFormat(" {0}", ZipCode);
            }

            var result = sb.ToString();

            return result;
        }

        public void UpdateMarkup()
        {
            Markup = Helper.GetFlyerMarkup(Layout);
        }

        public Order ToOrder()
        {
            Order result = null;

            if (OrderId.HasValue)
            {
                result = new Order((Int32)OrderId.Value);
            }

            var canCreateOrder = result == null && HttpContext.Current != null && HttpContext.Current.User != null;

            if (canCreateOrder)
            {
                if (FlyerType == FlyerTypes.Seller)
                {
                    result = Order.CreateSellerOrder(HttpContext.Current.User.Identity.Name);
                }
                else if (FlyerType == FlyerTypes.Custom)
                {
                    result = Order.CreateCustomOrder(HttpContext.Current.User.Identity.Name);
                }
                else if (FlyerTypes.Buyer == FlyerTypes.Buyer)
                {
                    result = Order.CreateBuyerOrder(HttpContext.Current.User.Identity.Name);
                }

                if (result != null)
                {
                    OrderId = result.order_id;
                    Name = result.field5;
                    Layout = result.layout;
                    DeliveryDate = result.delivery_date;
                }
            }

            if (result != null)
            {
                var orderReflectionProperties = result.GetType().GetProperties().ToDictionary(k => k.Name, v => v);

                result.type = FlyerType.ToString().ToLower();
                result.market_state = MarketState;

                if (SelectedPricesList != null && (!String.IsNullOrEmpty(MarketType)))
                {
                    result.market_county = String.Empty;
                    result.market_association = String.Empty;
                    result.market_msa = String.Empty;

                    var marketTypeProperty = orderReflectionProperties["market_" + MarketType.ToLower()];

                    if (marketTypeProperty != null)
                    {
                        marketTypeProperty.SetValue(result, SelectedPricesList.Markets, null);
                    }

                    result.tota_price = SelectedPricesList.TotalPrice;
                }

                result.headline = result.headline ?? String.Empty;
                result.theme = result.theme ?? String.Empty;
                result.layout = Layout ?? String.Empty;
                result.delivery_date = DeliveryDate ?? result.delivery_date;
                result.mls_number = MlsNumber ?? String.Empty;
                result.email_subject = EmailSubject ?? String.Empty;
                result.title = FlyerTitle ?? String.Empty;
                result.sub_title = result.sub_title ?? String.Empty;
                result.prop_address1 = StreetAddress ?? String.Empty;
                result.prop_address2 = result.prop_address2 ?? String.Empty;
                result.AptSuiteBldg = AptSuiteBldg ?? String.Empty;
                result.prop_city = City ?? String.Empty;
                result.prop_state = State ?? String.Empty;
                result.prop_zipcode = ZipCode ?? String.Empty;
                result.OpenHouses = OpenHouses ?? String.Empty;
                result.prop_desc = Description ?? String.Empty;

                if (!(String.IsNullOrEmpty(PriceRent)))
                {
                    if (String.Compare(PriceRent, "price", true) == 0)
                    {
                        result.prop_price = String.Format("PRICE|{0}|{1}|", Price.HasValue ? Price.ToString() : String.Empty, PerArea);
                    }
                    else if (String.Compare(PriceRent, "rent", true) == 0)
                    {
                        result.prop_price = String.Format("RENT|{0}|{1}|{2}", Price.HasValue ? Price.ToString() : String.Empty, PerArea, RentPeriod);
                    }
                }
                else
                {
                    result.prop_price = result.prop_price ?? String.Empty;
                }

                result.mls_link = result.mls_link ?? String.Empty;
                
                if (FlyerType == FlyerTypes.Custom)
                {
                    result.virtualtour_link = Link.HasText() ? Link : String.Empty;
                }

                result.map_link = MapLink ?? String.Empty;
                result.markup = Markup ?? String.Empty;
                result.price_range_min = PriceMin.HasValue ? PriceMin.Value.ToString() : String.Empty;
                result.price_range_max = PriceMax.HasValue ? PriceMax.Value.ToString() : String.Empty;
                result.location = Location ?? String.Empty;
                result.more_info = result.more_info ?? String.Empty;
                result.buyer_message = result.buyer_message ?? String.Empty;

                for (var i = 0; i < Photos.Length; i++)
                {
                    var key = "photo" + (i + 1).ToString();

                    if (orderReflectionProperties.ContainsKey(key))
                    {
                        orderReflectionProperties[key].SetValue(result, Photos[i] ?? String.Empty, null);
                    }
                }

                result.field1 = Email ?? String.Empty;
                result.field2 = Phone ?? String.Empty;
                result.field3 = Ext ?? String.Empty;
                result.field5 = Name ?? Helper.GetCustomerNameByEmail(result.customer_id);
                result.Bedrooms = Bedrooms ?? String.Empty;
                result.FullBaths = Bathrooms ?? String.Empty;
                result.Parking = Parking ?? String.Empty;
                result.SqFoots = Sqft ?? String.Empty;
                result.YearBuilt = YearBuilt ?? String.Empty;
                result.Floors = Floors ?? String.Empty;
                result.LotSize = LotSize ?? String.Empty;
                result.Subdivision = Subdivision ?? String.Empty;
                result.HOA = Hoa;

                var steps = GetSteps();
                var stepIndex = 0;

                foreach (var kvp in steps)
                {
                    if (!kvp.Value)
                    {
                        result.LastPageNo = stepIndex;
                        break;
                    }

                    stepIndex++;
                }

                var amenities = GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(AmenityAttribute), false).Length > 0)
                                                         .ToDictionary(k => (k.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0] as DisplayNameAttribute).DisplayName, v => v.GetValue(this, null).ToString());

                result.PropertyFeatures = String.Join(":", amenities.Keys.ToArray());
                result.PropertyFeaturesValues = String.Join(":", amenities.Values.ToArray());
                result.OtherPropertyFeatures = result.OtherPropertyFeatures ?? String.Empty;

                Int32 propertyCategoryId;

                if (Int32.TryParse(PropertyCategoryId, out propertyCategoryId))
                {
                    result.PropertyCategory = propertyCategoryId;
                }

                Int32 residentialType;

                if (Int32.TryParse(ResidentialType, out residentialType))
                {
                    result.PropertyType = residentialType;
                }
            }

            return result;
        }

        public String GetResidentialTypeName()
        {
            String result = null;

            if (ResidentialType.HasText())
            {
                var sqlString = "select PropertyType from fly_PropertyType where PropertyTypeID=" + ResidentialType;

                using (var dataObj = new clsData())
                {
                    dataObj.strSql = sqlString;

                    var dt = dataObj.GetDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["PropertyType"] as String;
                    }
                }
            }

            return result;
        }

        public static WizardFlyer FromOrder(Order order)
        {
            if (order == null)
            {
                throw new Exception("order parameter is required");
            }

            var result = WizardFlyer.SetFlyer((FlyerTypes)Enum.Parse(typeof(FlyerTypes), order.type, true));

            if (order.order_id > 0)
            {
                result.OrderId = order.order_id;
            }

            Func<String, String> getStringValue = (value) => value.HasNoText() ? null : value;

            result.FlyerTitle = order.title.HasNoText() ? null : order.title.Trim();
            result.EmailSubject = getStringValue(order.email_subject);

            if (result.FlyerTitle.HasText() && result.EmailSubject.HasText())
            {
                result.FlyerTitleStepCompleted = true;
            }

            result.Name = getStringValue(order.field5) ?? Helper.GetCustomerNameByEmail(order.customer_id);
            result.Phone = getStringValue(order.field2);
            result.Ext = getStringValue(order.field3);
            result.Email = getStringValue(order.field1);

            if (result.Name.HasText() || result.Phone.HasText() || result.Email.HasText())
            {
                result.ContactDetailsStepCompleted = true;
            }

            String offerType;
            String propertyType;

            if (order.PropertyCategory > 0)
            {
                ParsePropertyCategory(order.PropertyCategory, out offerType, out propertyType);
                result.OfferType = offerType;
                result.PropertyType = propertyType;
                result.PropertyCategoryId = order.PropertyCategory.ToString();
            }

            if (order.PropertyType > 0)
            {
                result.ResidentialType = order.PropertyType.ToString(); 
            }

            if (result.OfferType.HasText() && result.PropertyType.HasText() && result.ResidentialType.HasText())
            {
                result.PropertyTypeStepCompleted = true;
            }

            result.StreetAddress = order.prop_address1.HasNoText() ? null : order.prop_address1.Trim();
            result.AptSuiteBldg = getStringValue(order.AptSuiteBldg);
            result.City = order.prop_city.HasNoText() ? null : order.prop_city.Trim();
            result.State = getStringValue(order.prop_state);
            result.ZipCode = getStringValue(order.prop_zipcode);
            result.MapLink = getStringValue(order.map_link);

            if (result.MapLink.HasText())
            {
                result.AddMap = true;
            }

            if (result.StreetAddress.HasText() || result.AptSuiteBldg.HasText() || result.City.HasText() || result.State.HasText() || result.ZipCode.HasText())
            {
                result.LocationStepCompleted = true;
            }

            result.Location = getStringValue(order.location);

            if (result.Location.HasText())
            {
                result.DesiredLocationStepCompleted = true;
            }

            result.MlsNumber = getStringValue(order.mls_number);
            result.YearBuilt = getStringValue(order.YearBuilt);
            result.Subdivision = getStringValue(order.Subdivision);
            result.Hoa = getStringValue(order.HOA);
            result.Bedrooms = getStringValue(order.Bedrooms);
            result.Bathrooms = getStringValue(order.FullBaths);
            result.LotSize = getStringValue(order.LotSize);
            result.Sqft = getStringValue(order.SqFoots);
            result.Floors = getStringValue(order.Floors);
            result.Parking = getStringValue(order.Parking);
            result.OpenHouses = getStringValue(order.OpenHouses);
            result.Description = getStringValue(order.prop_desc);

            if (result.MlsNumber.HasText() || result.YearBuilt.HasText() || result.Subdivision.HasText() ||
                result.Hoa.HasText() || result.Bedrooms.HasText() || result.Bathrooms.HasText() ||
                result.LotSize.HasText() || result.Sqft.HasText() || result.Floors.HasText() ||
                result.Parking.HasText() || result.OpenHouses.HasText() || result.Description.HasText())
            {
                result.DescriptionStepCompleted = true;
            }

            if (result.FlyerType == FlyerTypes.Custom)
            {
                result.Link = getStringValue(order.virtualtour_link);
            }

            var amenities = result.GetType().GetProperties()
                                            .Where(p => p.GetCustomAttributes(typeof(AmenityAttribute), false).Length > 0)
                                            .ToDictionary(k => (k.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0] as DisplayNameAttribute).DisplayName.ToLower(), v => v);

            if (order.PropertyFeatures.HasText() && order.PropertyFeaturesValues.HasText())
            {
                var keys = order.PropertyFeatures.Split(':');
                var values = order.PropertyFeaturesValues.Split(':');
                Boolean value;

                if (keys.Length == values.Length)
                {
                    for (var i = 0; i < keys.Length; i++)
                    {
                        if (amenities.ContainsKey(keys[i].ToLower()))
                        {
                            value = Boolean.Parse(values[i]);
                            amenities[keys[i].ToLower()].SetValue(result, value, null);

                            if (value)
                            {
                                result.AmenitiesStepCompleted = true;
                            }
                        }
                    }
                }
            }

            var orderReflectionProperties = order.GetType().GetProperties().ToDictionary(k => k.Name, v => v);

            for (var i = 0; i < result.Photos.Length; i++)
            {
                var key = "photo" + (i + 1).ToString();

                if (orderReflectionProperties.ContainsKey(key))
                {
                    result.Photos[i] = getStringValue(orderReflectionProperties[key].GetValue(order, null) as String);

                    if (result.Photos[i].HasText())
                    {
                        result.PhotoStepCompleted = true;
                    }
                }
            }

            if (result.FlyerType == FlyerTypes.Custom)
            {
                result.PhotoStepCompleted = false;

                if (result.Photos[0].HasText())
                {
                    result.UploadFlyerStepCompleted = true;
                }
            }

            Decimal @decimal;

            if (order.prop_price.HasText())
            {
                var split = order.prop_price.Split('|');

                if (split.Length > 0)
                {
                    result.PriceRent = getStringValue(split[0]);
                    
                    if (split.Length > 1)
                    {
                        if (Decimal.TryParse(split[1], out @decimal))
                        {
                            result.Price = @decimal;
                        }

                        if (split.Length > 2)
                        {
                            result.PerArea = getStringValue(split[2]);

                            if (split.Length > 3)
                            {
                                result.RentPeriod = getStringValue(split[3]);
                            }
                        }
                    }
                }
            }

            if (result.PriceRent.HasText() || result.Price > 0 || result.PerArea.HasText() || result.RentPeriod.HasText())
            {
                result.PriceStepCompleted = true;
            }

            if (order.price_range_min.HasText())
            {
                if (Decimal.TryParse(order.price_range_min, out @decimal))
                {
                    result.PriceMin = @decimal;
                }
            }
            if (order.price_range_max.HasText())
            {
                if (Decimal.TryParse(order.price_range_max, out @decimal))
                {
                    result.PriceMax = @decimal;
                }
            }

            if (result.PriceMin > 0 || result.PriceMax > 0)
            {
                result.PriceRangeStepCompleted = true;
            }

            result.Layout = getStringValue(order.layout);
            result.Markup = getStringValue(order.markup);

            if (result.Markup.HasText())
            {
                result.ChooseFlyerStepCompleted = true;
            }

            result.MarketState = getStringValue(order.market_state);
            
            if (!order.delivery_date.Equals(default(DateTime)))
            {
                result.DeliveryDate = order.delivery_date;
            }

            String markets = null;

            if (getStringValue(order.market_county) != null)
            {
                result.MarketType = "county";
                markets = order.market_county;
            }
            else if (getStringValue(order.market_association) != null)
            {
                result.MarketType = "association";
                markets = order.market_association;
            }
            else if (getStringValue(order.market_msa) != null)
            {
                result.MarketType = "msa";
                markets = order.market_msa;
            }

            if (result.MarketState.HasText() && result.MarketType.HasText() && markets.HasText())
            {
                result.SelectedPricesList = PricesList.FromParameters(order, result.MarketType, markets);
            }

            if (result.SelectedPricesList != null && result.SelectedPricesList.Items.Count > 0)
            {
                result.SelectMarketAreaStepCompleted = true;
            }

            return result;
        }

        public static WizardFlyer GetFlyer()
        {
            return HttpContext.Current.Session[GetKey()] as WizardFlyer;
        }

        public static WizardFlyer SetFlyer(FlyerTypes flyerType)
        {
            var result = new WizardFlyer(flyerType);

            HttpContext.Current.Session[GetKey()] = result;

            return result;
        }

        public static void DeleteFlyer()
        {
            HttpContext.Current.Session.Remove(GetKey());
        }

        public class AmenityAttribute : Attribute
        {
        }

        #region private

        private Int64? orderId;

        private static String GetKey()
        {
            var result = String.Format("{0}", typeof(WizardFlyer).FullName);

            return result;
        }

        private Dictionary<String, Boolean> GetSteps()
        {
            Dictionary<String, Boolean> result = null;
            String[] steps = null;

            if (FlyerType == FlyerTypes.Seller)
            {
                steps = new String[] 
                                {
                                    "FlyerTitleStepCompleted",
                                    "ContactDetailsStepCompleted",
                                    "PropertyTypeStepCompleted",
                                    "LocationStepCompleted",
                                    "DescriptionStepCompleted",
                                    "AmenitiesStepCompleted",
                                    "PhotoStepCompleted",
                                    "PriceStepCompleted",
                                    "ChooseFlyerStepCompleted"
                                };
            }
            else if (FlyerType == FlyerTypes.Buyer)
            {
                steps = new String[] 
                                {
                                    "FlyerTitleStepCompleted",
                                    "ContactDetailsStepCompleted",
                                    "DesiredLocationStepCompleted",
                                    "DescriptionStepCompleted",
                                    "AmenitiesStepCompleted",
                                    "PriceRangeStepCompleted",
                                    "ChooseFlyerStepCompleted"
                                };
            }
            else if (FlyerType == FlyerTypes.Custom)
            {
                steps = new String[] 
                                {
                                    "UploadFlyerStepCompleted"
                                };
            }
            
            var properties = GetType().GetProperties().Where(p => p.Name.EndsWith("StepCompleted", StringComparison.OrdinalIgnoreCase))
                                                      .ToDictionary(k => k.Name, v => v);

            if (steps != null)
            {
                result = new Dictionary<String, Boolean>();

                foreach (var s in steps)
                {
                    if (properties.ContainsKey(s) && (!result.ContainsKey(s)))
                    {
                        result.Add(s, (Boolean)properties[s].GetValue(this, null));
                    }
                }
            }

            return result;
        }

        private static void ParsePropertyCategory(Int32 propertyCategoryId, out String offerType, out String propertyType)
        {
            offerType = null;
            propertyType = null;

            var sdsPropertyCategory = new SqlDataSource(ConfigurationManager.ConnectionStrings["projectDBConnectionString"].ConnectionString, "SELECT * FROM [fly_PropertyCategory] WHERE CategoryID = " + propertyCategoryId.ToString());
            var data = sdsPropertyCategory.Select(new DataSourceSelectArguments());
            String category = null;

            foreach (DataRowView drv in data)
            {
                category = drv["Category"] as String;

                if (!String.IsNullOrEmpty(category))
                {
                    break;
                }
            }

            if (!String.IsNullOrEmpty(category))
            {
                var split = System.Text.RegularExpressions.Regex.Split(category, " - ");

                if (split.Length > 0)
                {
                    offerType = split[0];

                    if (split.Length > 1)
                    {
                        propertyType = split[1];
                    }
                }
            }
        }

        #endregion
    }
}