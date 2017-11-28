using FlyerMe;
using FlyerMe.BLL.CreateFlyer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI.WebControls;

public class PricesPresenter
{
	public PricesPresenter(IPricesView view)
	{
		this.view = view;
		request = HttpContext.Current.Request;
		session = HttpContext.Current.Session;
		cache = HttpContext.Current.Cache;
	}

	public void OnPageLoad()
	{
        var canHandle = request.IsGet() || (request.IsPost() && view.GetType().FullName.IndexOf("SelectMarketArea", StringComparison.OrdinalIgnoreCase) >= 0);

		if (canHandle)
		{
            view.RootUrl = clsUtility.GetRootHost;
			filter = Filter.Bind(view, request);

            view.SetFilter(filter);
			view.PageSize = 11;

			SetDdlState();
			SetPricesList();
			SetSelectedPricesList();
		}
	}

	public Int32 GetTotalListSize()
	{
		return selectedPricesList.Sum(i => i.Value.listsize);
	}

	public Decimal GetTotalAmount()
	{
		return selectedPricesList.Sum(i => i.Value.price);
	}

	public String GetFilterPrices()
	{
		return filter.Prices;
	}

	public Boolean IsMarketChecked(String value)
	{
		return filter.IsMarketChecked(value);
	}

	public Boolean IsFlyerChecked(String value)
	{
		return filter.IsFlyerChecked(value);
	}

	public Boolean IsPriceChecked(Int32 index)
	{
		var absoluteIndex = (view.PageNumber - 1) * view.PageSize + index;

		var result = selectedPricesList.ContainsKey(absoluteIndex);

		return result;
	}

	public Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow> GetSelectedPricesList()
	{
		return selectedPricesList;
	}

	#region private

	private readonly IPricesView view;
	private readonly HttpRequest request;
	private readonly HttpSessionState session;
	private readonly Cache cache;
	private Filter filter;
	private DataView pricesListDataSource;
	private ObjectDataSource marketObjectDataSource;
	private Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow> selectedPricesList;

	private void SetDdlState()
	{
		var dsStateTableAdaper = new FlyerMeDSTableAdapters.fly_statesTableAdapter();
		var dsStateDataTable = new FlyerMeDS.fly_statesDataTable();

		dsStateDataTable = dsStateTableAdaper.GetStates();

		var ddlState = view.DdlState;

		ddlState.DataTextField = "StateName";
		ddlState.DataValueField = "StateAbr";
		ddlState.DataSource = dsStateDataTable;
		ddlState.DataBind();
		ddlState.SelectedValue = filter.GetCurrentState(view.State);

		var shouldSetCurrentMarketState = ((filter != null && !String.IsNullOrEmpty(filter.State)) || (!String.IsNullOrEmpty(request["state"]))) &&
										  ddlState.Items.FindByValue(ddlState.SelectedValue) != null;

		if (shouldSetCurrentMarketState)
		{
			session["CurrentMarketState"] = ddlState.SelectedValue;
		}

		ddlState.Attributes.Add("data-clientname", "state");
	}

	private void SetPricesListDataSource()
	{
		var key = String.Format("PricesListDataSource_{0}&{1}&{2}", filter.State, filter.Market, filter.Flyer);

		pricesListDataSource = cache[key] as DataView;

		if (pricesListDataSource == null)
		{
			marketObjectDataSource = new ObjectDataSource();
			marketObjectDataSource.ID = "MarketObjectDataSource";
			marketObjectDataSource.OldValuesParameterFormatString = "original_{0}";
			marketObjectDataSource.SelectMethod = "GetMarketList";
			marketObjectDataSource.TypeName = "FlyerMe.FlyerBLL";

			var hfState = new HiddenField()
			                    {
				                    ID = "hfState",
				                    Value = view.State,
				                    Visible = false
			                    };

			var hfMarket = new HiddenField()
			                    {
				                    ID = "hfMarket",
				                    Value = filter.GetSelectedMarket(filter.Market),
				                    Visible = false
			                    };

			var hfFlyer = new HiddenField()
			                    {
				                    ID = "hfFlyer",
				                    Value = filter.GetSelectedFlyer(filter.Flyer),
				                    Visible = false
			                    };

			var p1 = new ControlParameter("stateID", "hfState", "Value");
			var p2 = new ControlParameter("markettype", "hfMarket", "Value");
			var p3 = new ControlParameter("flyerType", "hfFlyer", "Value");

			marketObjectDataSource.SelectParameters.Add(p1);
			marketObjectDataSource.SelectParameters.Add(p2);
			marketObjectDataSource.SelectParameters.Add(p3);

			view.AddControl(marketObjectDataSource);
			view.AddControl(hfState);
			view.AddControl(hfMarket);
			view.AddControl(hfFlyer);

			pricesListDataSource = marketObjectDataSource.Select() as DataView;

			cache.Add(key, pricesListDataSource, null, DateTime.Now.Add(new TimeSpan(0, 5, 0)), default(TimeSpan), CacheItemPriority.Normal, null);
		}
	}

	private void SetPricesList()
	{
		SetPricesListDataSource();

		view.SetItemsCount(pricesListDataSource.Count);

		view.PricesList = pricesListDataSource.Table.Rows.Cast<FlyerMeDS.fly_sp_GetListSizeRow>()
										                 .Skip((view.PageNumber - 1) * view.PageSize)
										                 .Take(view.PageSize)
										                 .ToList();
	}

	private void SetSelectedPricesList()
	{
		if (!String.IsNullOrEmpty(filter.Prices))
		{
			Int32 f;
			selectedPricesList = filter.Prices.Split(',').Select(i =>
			                                                {
				                                                if (Int32.TryParse(i, out f) && f >= 0 && f < pricesListDataSource.Count)
				                                                {
					                                                return f;
				                                                }

				                                                return -1;
			                                                })
											.Where(i => i != -1)
											.Distinct()
											.ToDictionary(i => i, i => pricesListDataSource.Table.Rows[i] as FlyerMeDS.fly_sp_GetListSizeRow);

            if (view.ShouldSaveSelectedPricesIntoFlyer)
            {
                var flyer = WizardFlyer.GetFlyer();

                if (flyer != null)
                {
                    flyer.SelectedPricesList = PricesList.FromPricesList(selectedPricesList);
                }
            }
		}
		else
		{
			selectedPricesList = new Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow>();
		}
	}

	private class Filter : FilterBase
	{
		private Filter()
		{
		}

		private Filter(String state, String market, String flyer, String prices)
		{
			State = state;
			Market = market;
			Flyer = flyer;
			Prices = prices;
            entityFieldsQuery = new NameValueCollection();

            entityFieldsQuery.Add("state", State);
            entityFieldsQuery.Add("market", Market);
            entityFieldsQuery.Add("flyer", Flyer);
            entityFieldsQuery.Add("prices", Prices);
		}

		public static Filter Bind(IPricesView view, HttpRequest request)
		{
			var filter = new Filter
			                    {
				                    State = view.State,
				                    Market = view.Market,
				                    Flyer = view.Flyer,
                                    Prices = view.PricesJoin
			                    };

            var result = new Filter(filter.GetCurrentState(filter.State),
                                    filter.GetSelectedMarket(filter.Market),
                                    filter.GetSelectedFlyer(filter.Flyer),
                                    filter.Prices);

			return result;
		}

		public String State { get; private set; }

		public String Market { get; private set; }

		public String Flyer { get; private set; }

		public String Prices { get; private set; }

		public override Boolean IsEntityFieldsEmpty
		{
			get
			{
				if (!isEntityFieldsEmpty.HasValue)
				{
					isEntityFieldsEmpty = String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Market) && String.IsNullOrEmpty(Flyer) && String.IsNullOrEmpty(Prices);
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

		public Boolean IsMarketChecked(String value)
		{
			var result = false;

			if (!String.IsNullOrEmpty(Market))
			{
				result = String.Compare(value, Market, true) == 0;
			}

			if (!result)
			{
				result = String.Compare(value, "county", true) == 0;
			}

			return result;
		}

		public Boolean IsFlyerChecked(String value)
		{
			var result = false;

			if (!String.IsNullOrEmpty(Flyer))
			{
				result = String.Compare(value, Flyer, true) == 0;
			}

			if (!result)
			{
				result = String.Compare(value, "seller", true) == 0;
			}

			return result;
		}

		public String GetSelectedMarket(String market)
		{
            var result = market;

			if (String.IsNullOrEmpty(result))
			{
				result = "county";
			}

			if (!IsMarketChecked(result))
			{
				result = "association";

				if (!IsMarketChecked(result))
				{
					result = "msa";
				}
			}

			return result;
		}

		public String GetSelectedFlyer(String flyer)
		{
			var result = flyer;

			if (String.IsNullOrEmpty(result))
			{
				result = FlyerTypes.Seller.ToString().ToLower();
			}

			if (!IsFlyerChecked(result))
			{
				result = FlyerTypes.Buyer.ToString().ToLower();

				if (!IsFlyerChecked(result))
				{
					result = FlyerTypes.Custom.ToString().ToLower();
				}
			}

			return result;
		}

		public String GetCurrentState(String state)
		{
			var result = state;

			if (String.IsNullOrEmpty(result))
			{
				var session = HttpContext.Current.Session;

				result = session["CurrentMarketState"] as String;

				if (String.IsNullOrEmpty(result))
				{
					result = state;
				}
			}

			return result;
		}

		#region private

		private Boolean? isEntityFieldsEmpty;
		private readonly NameValueCollection entityFieldsQuery;
		private String entityFieldsQueryString;

		#endregion
	}

	#endregion
}