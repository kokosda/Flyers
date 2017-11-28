using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class AB_SendToClients_Footer : EmailMarkupPageBase
    {
        public String Email
        {
            get
            {
                return Request["email"];
            }
        }
    }
}