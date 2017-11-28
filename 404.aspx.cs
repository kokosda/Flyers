using System;
using System.Web.UI;

namespace FlyerMe
{
    public partial class Error404 : Page
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            HandleNotFoundJpgImages();
            Response.StatusCode = 404;
        }

        #region private

        protected void HandleNotFoundJpgImages()
        {
            if (Request.Url.Query.IndexOf("order/", StringComparison.OrdinalIgnoreCase) >= 0 && 
                (Request.Url.Query.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                 Request.Url.Query.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                 Request.Url.Query.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)))
            {
                if (Request.UrlReferrer != null)
                {
                    if (Request.UrlReferrer.AbsolutePath.EndsWith("myflyers.aspx", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Redirect("~/images/no-photo.jpg", true);
                    }
                    else if (Request.UrlReferrer.AbsolutePath.EndsWith("search.aspx"))
                    {
                        Response.Redirect("~/images/no-photo-big.jpg", true);
                    }
                    else if (String.Compare(Request.UrlReferrer.AbsolutePath, ResolveUrl("~"), true) == 0 ||
                             Request.UrlReferrer.AbsolutePath.EndsWith("default.aspx", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Redirect("~/images/no-photo-front.jpg", true);
                    }
                    else
                    {
                        Response.Redirect("~/images/no-photo-big.jpg", true);
                    }
                }
                else
                {
                    Response.Redirect("~/images/no-photo-big.jpg", true);
                }
            }
        }

        #endregion
    }
}