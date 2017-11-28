using FlyerMe;
using FlyerMe.Controls;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : PageBase
{
    protected String RootURL;

    private Search.Filter filter;

    protected override MetaObject MetaObject
    {
        get
        {
            return MetaObject.Create()
                             .SetPageTitle("Search for the Latest Real Estate Email Flyers | {0}", clsUtility.ProjectName)
                             .SetKeywords("real estate listings, property for sale, find property for sale, real estate property for sale, property houses for sale, {0}, real estate marketing, residential property for sale, homes property for sale, house property for sale, search property for sale", clsUtility.SiteBrandName.ToLower())
                             .SetDescription("Search for the latest real estate email flyers at {0}. Find beautiful real estate flyers with detailed information including real estate pricing and photos.", clsUtility.ProjectName);
        }
    }

    protected void Page_Load(Object sender, EventArgs e)
    {
        RootURL = clsUtility.GetRootHost;

        filter = Filter.Bind(Request);

        if (Request.IsGet())
        {
            if (filter.IsEmpty && Request.QueryString.Count > 0)
            {
                Response.Redirect("~/search.aspx");
            }
            else
            {
                var itemsCount = clsFlyers.BindFlyers(rptFlyers, filter.PageNumber - 1, "created_on Desc", filter.Zip, filter.Address, filter.City, filter.State);

                pager.ItemsCount = itemsCount;
                pager.Filter = filter;
                pager.PageSize = 12;
            }

            if (filter.IsEntityFieldsEmpty)
            {
                inpClearer.Visible = false;
            }
            else
            {
                divSearchForm.Attributes.Add("class", divSearchForm.Attributes["class"] + " full");
            }
        }
    }
    protected void rptFlyers_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        var hlShowFlyer = (HyperLink)e.Item.FindControl("hlShowFlyer");
        hlShowFlyer.NavigateUrl = String.Format("{0}showflyer.aspx?oid={1}", RootURL, DataBinder.Eval(e.Item.DataItem, "order_id"));

        var imgPhoto1 = (Image)e.Item.FindControl("imgPhoto1");
        var imageRelativePath = String.Empty;

        if (DataBinder.Eval(e.Item.DataItem, "Photo1").ToString().Trim() != "")
        {
            imageRelativePath = "order/" + DataBinder.Eval(e.Item.DataItem, "order_id") + "/photo1/" + DataBinder.Eval(e.Item.DataItem, "Photo1").ToString();
        }
        else
        {
            imageRelativePath = "images/no-photo-big.jpg";
        }

        imgPhoto1.ImageUrl = String.Format("{0}{1}", RootURL, imageRelativePath);

        var ltlAddress = (Literal)e.Item.FindControl("ltlAddress");
        ltlAddress.Text = String.Format("{0} {1}, {2}", DataBinder.Eval(e.Item.DataItem, "prop_address1"), DataBinder.Eval(e.Item.DataItem, "prop_city"), DataBinder.Eval(e.Item.DataItem, "prop_state"));
        imgPhoto1.AlternateText = ltlAddress.Text;

        var ltlPrice = (Literal)e.Item.FindControl("ltlPrice");
        ltlPrice.Text = DataBinder.Eval(e.Item.DataItem, "prop_price").ToString().FormatPrice();

        var ltlBedrooms = (Literal)e.Item.FindControl("ltlBedrooms");
        ltlBedrooms.Text = DataBinder.Eval(e.Item.DataItem, "bedrooms") != null ? DataBinder.Eval(e.Item.DataItem, "bedrooms").ToString() : "0";

        var ltlFullBaths = (Literal)e.Item.FindControl("ltlFullBaths");
        ltlFullBaths.Text = DataBinder.Eval(e.Item.DataItem, "FullBaths") != null && DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString().Trim() != "" ? DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString() : "";

        var ltlYearBuilt = (Literal)e.Item.FindControl("ltlYearBuilt");
        ltlYearBuilt.Text = DataBinder.Eval(e.Item.DataItem, "YearBuilt") != null && DataBinder.Eval(e.Item.DataItem, "YearBuilt").ToString().Trim() != "" ?  DataBinder.Eval(e.Item.DataItem, "YearBuilt").ToString() : "";

        var ltlSquareFeet = (Literal)e.Item.FindControl("ltlSquareFeet");
        ltlSquareFeet.Text = DataBinder.Eval(e.Item.DataItem, "SqFoots") != null && DataBinder.Eval(e.Item.DataItem, "SqFoots").ToString().Trim() != "" ? DataBinder.Eval(e.Item.DataItem, "SqFoots").ToString() : "0";
    }

    private class Filter : FilterBase
    {
        private Filter(String address, String city, String state, String zip, Int32 pageNumber, NameValueCollection entityFieldsQuery)
        {
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            PageNumber = pageNumber;
            this.entityFieldsQuery = entityFieldsQuery;
        }

        public static Filter Bind(HttpRequest request)
        {
            Int32 pageNumber;

            if (!Int32.TryParse(request.QueryString["page"], out pageNumber))
            {
                pageNumber = 1;
            }

            var entityFieldsQuery = new NameValueCollection();
            entityFieldsQuery.Add(request.QueryString);
            entityFieldsQuery.Remove("page");

            var result = new Filter(request.QueryString["address"] != null ? request.QueryString["address"].Trim() : String.Empty,
                                    request.QueryString["city"] != null ? request.QueryString["city"].Trim() : String.Empty,
                                    request.QueryString["state"] != null ? request.QueryString["state"].Trim() : String.Empty,
                                    request.QueryString["zip"] != null ? request.QueryString["zip"].Trim() : String.Empty,
                                    pageNumber,
                                    entityFieldsQuery);

            return result;
        }

        public String Address { get; private set; }

        public String City { get; private set; }

        public String State { get; private set; }

        public String Zip { get; private set; }

        public Int32 PageNumber { get; private set; }

        public Boolean IsEmpty
        {
            get
            {
                if (!isEmpty.HasValue)
                {
                    isEmpty = IsEntityFieldsEmpty && PageNumber <= 1;
                }

                return isEmpty.Value;
            }
        }

        public override Boolean IsEntityFieldsEmpty
        {
            get
            {
                if (!isEntityFieldsEmpty.HasValue)
                {
                    isEntityFieldsEmpty = String.IsNullOrEmpty(Address) && String.IsNullOrEmpty(City) && String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Zip);
                }

                return isEntityFieldsEmpty.Value;
            }
        }

        public override String EntityFieldsQueryString
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

        private Boolean? isEmpty;
        private Boolean? isEntityFieldsEmpty;
        private NameValueCollection entityFieldsQuery;
        private String entityFieldsQueryString;
    }
}