using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace FlyerMe
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// State class - Business class interface for State object 
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class State
    {
        private int _state_id;
        private string _state_abr;
        private string _state_name;
        private bool _state_status;
        private bool _taxable;
        private string _tax_rate;

        public int StateID
        {
            get { return _state_id; }
            set { _state_id = value; }
        }

        public string StateAbr
        {
            get { return _state_abr; }
            set { _state_abr = value; }
        }

        public string StateName
        {
            get { return _state_name; }
            set { _state_name = value; }
        }

        public bool StateStatus
        {
            get { return _state_status; }
            set { _state_status = value; }
        }

        public bool Taxable
        {
            get { return _taxable; }
            set { _taxable = value; }
        }

        public string TaxRate
        {
            get { return _tax_rate; }
            set { _tax_rate = value; }
        }


        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Initialize promo object
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public State(string stateId)
        {
            StatesBLL stateBll = new StatesBLL();
            FlyerMeDS.fly_statesDataTable stateDataTable = stateBll.GetStateByStateID(stateId);

            if (stateDataTable.Rows.Count > 0)
            {
                FlyerMeDS.fly_statesRow row = (FlyerMeDS.fly_statesRow)stateDataTable.Rows[0];

                this.StateID = row.StateID;
                this.StateAbr = row.StateAbr;
                this.StateName = row.StateName;
                this.StateStatus = row.StateStatus;
                this.Taxable = row.Taxable;
                this.TaxRate = row.TaxRate;
            }
        }
    }
}
