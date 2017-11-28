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
    /// Subscriber business class - Handles all business logic for subscription
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class SubscribeBLL
    {
        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Initialize subscriber object 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public SubscribeBLL()
        {
            //
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Checks if a subscriber exists
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public bool SubscriberExists(int subscriber_id, string subscriber_lastname)
        {
            
            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();
            FlyerMeDS.fly_Subscribers_With_EmailDataTable dataTable = Adapter.GetSubscriberByIDLastName(subscriber_id, subscriber_lastname);
            return (dataTable.Count > 0);
        }

        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public bool SubscriberExistsByEmail(string email)
        {

            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();
            FlyerMeDS.fly_Subscribers_With_EmailDataTable dataTable = Adapter.GetSubscriberByEmail(email);
            return (dataTable.Count > 0);
        }

        [System.ComponentModel.DataObjectMethodAttribute
(System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_Subscribers_With_EmailDataTable GetSubscriberById(Int64 subscriberId)
        {

            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();

            var result = Adapter.GetSubscriberById(subscriberId);

            return result;
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Subscribe
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool Subscribe(Int64 subscriber_id)
        {
            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();
            return (Adapter.Subscribe(subscriber_id) > 0);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Unsubscribe
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool Unsubscribe(Int64 subscriber_id)
        {
            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();
            return (Adapter.Unsubscribe(subscriber_id) > 0);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Unsubscribe by email
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool UnsubscribeByEmail(string subscriber_email)
        {
            fly_Subscribers_With_EmailTableAdapter Adapter = new fly_Subscribers_With_EmailTableAdapter();
            return (Adapter.UnsubscribeByEmail(subscriber_email) > 0);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Returns valid email from the list provided
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        [System.ComponentModel.DataObjectMethodAttribute
        (System.ComponentModel.DataObjectMethodType.Select, true)]
        public FlyerMeDS.fly_ReturnOnlyValidEmailsDataTable GetOnlyValidEmails(string clientEmailList)
        {
            fly_ReturnOnlyValidEmailsTableAdapter Adapter = new fly_ReturnOnlyValidEmailsTableAdapter();
            return Adapter.GetOnlyValidEmails(clientEmailList);
        }

        /// -----------------------------------------------------------------------------
        ///<summary>
        /// Do not email
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public bool InsertDoNotEmail(string email, string state, string city, bool donotemail)
        {
            fly_DoNotEmailTableAdapter adapter = new fly_DoNotEmailTableAdapter();
            return (adapter.InsertDoNotEmail(email, state, city, donotemail) == 1);
        }

    }
}
