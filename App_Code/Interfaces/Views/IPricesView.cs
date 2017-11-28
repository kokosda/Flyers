using FlyerMe;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public interface IPricesView
{
    String State { get; }

    String Market { get; }

    String Flyer { get; }

    String PricesJoin { get; }

    Int32 PageSize { get; set; }

    DropDownList DdlState { get; }

    IList<FlyerMeDS.fly_sp_GetListSizeRow> PricesList { get; set; }

    Int32 PageNumber { get; }

    Boolean ShouldSaveSelectedPricesIntoFlyer { get; }

    String GetFilterPrices();

    Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow> GetSelectedPricesList();

    Int32 GetTotalListSize();

    Decimal GetTotalAmount();

    void SetFilter(IFilter filter);

    void AddControl(Control control);

    void SetItemsCount(Int32 value);

    String RootUrl { get; set; }
}