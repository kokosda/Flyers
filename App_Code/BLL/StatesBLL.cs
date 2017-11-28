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
    /// State business class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [System.ComponentModel.DataObject]
    [Serializable]
    public class StatesBLL
    {
        private fly_statesTableAdapter _statesTableAdapter = null;
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get state table adapter
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public fly_statesTableAdapter Adapter
        {
            get
            {
                if (_statesTableAdapter == null)
                    _statesTableAdapter = new fly_statesTableAdapter();

                return _statesTableAdapter;
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get all states
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_statesDataTable GetStates()
        {
            return Adapter.GetStates();
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get state by state id
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_statesDataTable GetStateByStateID(string stateId)
        {
            return Adapter.GetStateByStateID(stateId);
        }

    }
}
