using System;
using System.Web.UI;

namespace FlyerMe.Controls.MasterPage
{
    public partial class Meta : MetaControl
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
        }

        public override void Set(MetaObject value)
        {
            if (value != null)
            {
                if (value.PageTitle.HasText())
                {
                    Page.Title = value.PageTitle;
                }

                if (value.Title.HasText())
                {
                    metaTitle.Text = String.Format("<meta name=\"title\" content=\"{0}\" />", value.Title);
                    metaTitle.Visible = true;
                }

                if (value.Keywords.HasText())
                {
                    metaKeywords.Text = String.Format("<meta name=\"keywords\" content=\"{0}\" />", value.Keywords);
                    metaKeywords.Visible = true;
                }

                if (value.Description.HasText())
                {
                    metaDescription.Text = String.Format("<meta name=\"description\" content=\"{0}\" />", value.Description);
                    metaDescription.Visible = true;
                }
            }
        }
    }
}
