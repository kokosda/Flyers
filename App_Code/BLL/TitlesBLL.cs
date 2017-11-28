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
    /// Flyer title class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [System.ComponentModel.DataObject]
    [Serializable]
    public class TitlesBLL
    {
        private fly_titlesTableAdapter _titlesTableAdapter = null;
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get flyer title table adapter
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public fly_titlesTableAdapter Adapter
        {
            get
            {
                if (_titlesTableAdapter == null)
                    _titlesTableAdapter = new fly_titlesTableAdapter();

                return _titlesTableAdapter;
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get all flyer titles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_titlesDataTable GetTitles()
        {
            return Adapter.GetTitles();
        }

    }
}
