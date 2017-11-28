using FlyerMe.Controls;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyerMe
{
    public partial class Default : PageBase
    {
        protected override string ScriptsBundleName
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return "~/bundles/scripts/defaultpageauthenticated";
                }

                return "~/bundles/scripts/defaultpage";
            }
        }

        protected override MetaObject MetaObject
        {
            get
            {
                return MetaObject.Create()
                                 .SetPageTitle("Create and Send Your Beautiful Email Flyer in Seconds | {0}", clsUtility.ProjectName)
                                 .SetTitle("Custom Real Estate Email Marketing Flyers and Real Estate Marketing Services from {0}", clsUtility.ProjectName)
                                 .SetKeywords("real estate flyers, real estate email flyers, real estate marketing, real estate email marketing, realtor marketing, real estate listings, property for sale, virtual tours, real estate property listing, homes virtual tour, virtual tours of real estate, virtual tours of houses, home listings")
                                 .SetDescription("{0} offers the most effective and affordable real estate marketing solutions by allowing real estate agents to create real estate email flyers, personal property websites, free virtual tours and also post property Ads to top syndication engines for real estate listing", clsUtility.ProjectName);
            }
        }

        protected String RootURL;
        protected Int32 random;

        protected void Page_Load(object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootHost;

            FlyerMeDSTableAdapters.fly_orderTableAdapter AdapterRecentFlyers = new FlyerMeDSTableAdapters.fly_orderTableAdapter();
            FlyerMeDS.fly_orderDataTable dtRecentFlyers = AdapterRecentFlyers.GetRecent4Flyers();
            rRecentFlyers.DataSource = dtRecentFlyers;
            rRecentFlyers.DataBind();
            divRecent.Visible = dtRecentFlyers.Count > 0;

            GetTestimonials();
        }

        private void GetTestimonials()
        {
            clsMisc objMisc = new clsMisc();
            DataTable dt = objMisc.getCustomerTestimonials();
            if (dt != null && dt.Rows.Count > 0)
            {
                var array = new Int32[2];
                var random = new Random();

                array[0] = random.Next(0, dt.Rows.Count);
                array[1] = random.Next(0, dt.Rows.Count);

                var iterations = 10;

                while (array[0] == array[1] && iterations > 0)
                {
                    array[1] = random.Next(0, dt.Rows.Count);
                    iterations--;
                }

                var dataSource = new DataTable();
                var dataColumns = new DataColumn[dt.Columns.Count];
                var i = 0;

                foreach (DataColumn dc in dt.Columns)
                {
                    dataColumns[i] = new DataColumn(dc.ColumnName, dc.DataType);
                    i++;
                }

                dataSource.Columns.AddRange(dataColumns);

                foreach (var a in array)
                {
                    dataSource.Rows.Add(dt.Rows[a].ItemArray.Clone() as Object[]);
                }

                dataSource.AcceptChanges();

                rTestimonials.DataSource = dataSource;
                rTestimonials.DataBind();
            }
        }

        protected void rRecentFlyers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var lblPrice = (Label)e.Item.FindControl("lblPrice");
            lblPrice.Text = DataBinder.Eval(e.Item.DataItem, "prop_price").ToString().FormatPrice();
        }

        protected void rTestimonials_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            Literal ltlFullname = (Literal)e.Item.FindControl("ltlFullname");
            ltlFullname.Text = String.Format("{0} {1}", DataBinder.Eval(e.Item.DataItem, "FirstName"), DataBinder.Eval(e.Item.DataItem, "LastName"));

            Literal ltlCompany = (Literal)e.Item.FindControl("ltlCompany");
            ltlCompany.Text = DataBinder.Eval(e.Item.DataItem, "Company").ToString();

            Literal ltlMessage = (Literal)e.Item.FindControl("ltlMessage");
            ltlMessage.Text = DataBinder.Eval(e.Item.DataItem, "Message").ToString();

            Image imgUser = (Image)e.Item.FindControl("imgUser");
            var imgUrl = DataBinder.Eval(e.Item.DataItem, "ImagePath").ToString();

            imgUser.ImageUrl = String.IsNullOrEmpty(imgUrl) ? "images/user-pic_184.png" : imgUrl;
            imgUser.AlternateText = ltlFullname.Text;
        }
    }
}