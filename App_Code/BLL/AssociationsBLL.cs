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
    /// The Association class
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [System.ComponentModel.DataObject]
    [Serializable]
    public class AssociationsBLL
    {
        #region Declarations
        private string _association_id;
        private string _association_name;
        private int _association_listsize;
        private string _association_state;
        private bool _active;

        #endregion

        #region Properties

        public string association_id
        {
            get { return _association_id; }
            set { _association_id = value; }
        }

        public string association_name
        {
            get { return _association_name; }
            set { _association_name = value; }
        }

        public int association_listsize
        {
            get { return _association_listsize; }
            set { _association_listsize = value; }
        }

        public string association_state
        {
            get { return _association_state; }
            set { _association_state = value; }
        }

        public bool active
        {
            get { return _active; }
            set { _active = value; }
        }

        #endregion

        private fly_associationsTableAdapter _associationsTableAdapter = null;
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Get assocation table adapter
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public fly_associationsTableAdapter Adapter
        {
            get
            {
                if (_associationsTableAdapter == null)
                    _associationsTableAdapter = new fly_associationsTableAdapter();

                return _associationsTableAdapter;
            }
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Fetch associations by state
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_associationsDataTable GetAssociationsByState(string state)
        {
            return Adapter.GetAssociationsByState(state);
        }

    }
}
