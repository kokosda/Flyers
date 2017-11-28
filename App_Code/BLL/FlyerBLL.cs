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
    /// Flyer business logic class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [System.ComponentModel.DataObject]
    [Serializable]
    public class FlyerBLL
    {
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get Market List based on state, market type and flyer type
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_sp_GetListSizeDataTable GetMarketList(string stateID, string marketType, string flyerType)
        {
            fly_sp_GetListSizeTableAdapter Adapter = new fly_sp_GetListSizeTableAdapter();
            return Adapter.GetMarketList(stateID, marketType, flyerType);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get Market List Size based on state, market type and flyer type
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_sp_GetListSizeDataTable GetListSizeByMarketList(string stateID, string marketType, string flyerType, string marketlist)
        {
            fly_sp_GetListSizeTableAdapter Adapter = new fly_sp_GetListSizeTableAdapter();
            return Adapter.GetListSizeByMarketList(stateID, marketType, flyerType, marketlist);
        }

    }
}
