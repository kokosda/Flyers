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
    /// The flyer layout class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------

    [System.ComponentModel.DataObject]
    [Serializable]
    public class LayoutsBLL
    {
        private fly_layoutsTableAdapter _layoutsTableAdapter = null;
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get flyer layout table adapter
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public fly_layoutsTableAdapter Adapter
        {
            get
            {
                if (_layoutsTableAdapter == null)
                    _layoutsTableAdapter = new fly_layoutsTableAdapter();

                return _layoutsTableAdapter;
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get all flyer layouts
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_layoutsDataTable GetLayouts()
        {
            return Adapter.GetLayouts();
        }

        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_layoutsDataTable GetSellerLayouts()
        {
            return Adapter.GetSellerLayouts();
        }
    }
}