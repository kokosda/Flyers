using FlyerMe.Controls;
using System;

namespace FlyerMe.Flyer.MarkUp
{
    public partial class C01 : PreviewFlyerWizardControlBase
    {
        protected override FlyerTypes FlyerType
        {
            get
            {
                return FlyerTypes.Custom;
            }
        }

        public Boolean IsLinkMarkupBlockVisible
        {
            get
            {
                return Flyer.Link.HasText();
            }
        }

        public String LinkMarkup
        {
            get
            {
                var result = Flyer.Link;
                var anchorFormatMarkup = "<a href='{0}'>{1}</a>";

                result = String.Format(anchorFormatMarkup, Link, result);

                return result;
            }
        }

        public String Link
        {
            get
            {
                var result = Flyer.Link;

                if (result.HasText())
                {
                    if (result.IndexOf('@') > 0)
                    {
                        result = "mailto:" + result;
                    }
                    else if (!(result.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || result.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
                    {
                        result = "http://" + result;
                    }
                }

                return result;
            }
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            InitFields(Profile);
        }
    }
}