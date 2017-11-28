using System;
using System.Web.UI;

namespace FlyerMe.Controls
{
    public abstract class MetaControl : UserControl
    {
        public abstract void Set(MetaObject value);
    }
}