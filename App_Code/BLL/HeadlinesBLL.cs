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
    /// The flyer headline class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [System.ComponentModel.DataObject]
    [Serializable]
    public class HeadlinesBLL
    {
        private fly_headlinesTableAdapter _headlinesTableAdapter = null;
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get headline table data adapter 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public fly_headlinesTableAdapter Adapter
        {
            get
            {
                if (_headlinesTableAdapter == null)
                    _headlinesTableAdapter = new fly_headlinesTableAdapter();

                return _headlinesTableAdapter;
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Fetch all active flyer headlines 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_headlinesDataTable GetActiveHeadlines()
        {
            return Adapter.GetActiveHeadlines();
        }
    }
}