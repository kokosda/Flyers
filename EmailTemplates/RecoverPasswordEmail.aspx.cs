using FlyerMe.Controls;
using System;

namespace FlyerMe.EmailTemplates
{
    public partial class RecoverPasswordEmail : EmailMarkupPageBase
    {
        public String Login
        {
            get
            {
                return Request["login"];
            }
        }

        public String Password
        {
            get
            {
                return Request["password"];
            }
        }
    }
}