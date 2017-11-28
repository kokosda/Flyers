using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FlyerMe.Controls
{
    public partial class Pager : UserControl
    {
        public Pager()
        {
            PageSize = 12;
        }

        public String PageName { get; set; }

        public Int32 PageNumber
        {
            get
            {
                if (!pageNumber.HasValue)
                {
                    Int32 number;

                    if (!Int32.TryParse(Request.QueryString["page"], out number))
                    {
                        pageNumber = 1;
                    }
                    else
                    {
                        pageNumber = number;
                    }
                }

                return pageNumber.Value;
            }
        }

        public Int32 ItemsCount { get; set; }

        public Int32 GetTotalPages()
        {
            return (Int32)Math.Ceiling((Decimal)ItemsCount / PageSize);
        }

        public Int32 PageSize { get; set; }

        public IFilter Filter
        {
            get
            {
                return filter ?? FilterBase.Get();
            }
            set
            {
                filter = value;
            }
        }

        protected String RootURL;

        protected void Page_Load(Object sender, EventArgs e)
        {
            RootURL = clsUtility.GetRootUrl;
            totalPages = GetTotalPages();

            SetPaging();
        }

        #region private

        private Int32? pageNumber;
        private HyperLink firstVisiblePagerHyperlink;
        private HyperLink lastVisiblePagerHyperlink;
        private Int32 totalPages;
        private IFilter filter;

        private void SetPaging()
        {
            if (ItemsCount == 0 || ItemsCount <= PageSize)
            {
                Visible = false;
            }
            else
            {
                if (totalPages > 0 && totalPages <= 7)
                {
                    SetPagingSimpleVisibility();
                }
                else
                {
                    SetPagingCalculatableVisibilty();
                }

                SetPagingNaviationUrls();
                SetPagingCssClasses();
            }
        }

        private void SetPagingSimpleVisibility()
        {
            liPreviousPage.Visible = true;
            liPage1.Visible = true;
            liPageDotPrevious.Visible = false;
            liPage2.Visible = false;
            liPage3.Visible = false;
            liPage4.Visible = false;
            liPage5.Visible = false;
            liPage6.Visible = false;
            liPage7.Visible = false;
            liPageDotNext.Visible = false;
            liLastPage.Visible = false;
            liNexPage.Visible = true;

            hlPage1.Text = "1";

            if (totalPages > 1)
            {
                liPage2.Visible = true;

                hlPage2.Text = "2";
            }
            if (totalPages > 2)
            {
                liPage3.Visible = true;

                hlPage3.Text = "3";
            }
            if (totalPages > 3)
            {
                liPage4.Visible = true;

                hlPage4.Text = "4";
            }
            if (totalPages > 4)
            {
                liPage5.Visible = true;

                hlPage5.Text = "5";
            }
            if (totalPages > 5)
            {
                liPage6.Visible = true;

                hlPage6.Text = "6";
            }
            if (totalPages > 6)
            {
                liPage7.Visible = true;

                hlPage7.Text = "7";
            }

            if (PageNumber == 1)
            {
                liPreviousPage.Visible = false;

                if (totalPages == PageNumber)
                {
                    liNexPage.Visible = false;
                }
            }
            else if (PageNumber == totalPages)
            {
                liNexPage.Visible = false;
            }

            hlLastPage.Text = totalPages.ToString();
        }

        private void SetPagingCalculatableVisibilty()
        {
            liPage7.Visible = false;
            hlPage1.Text = "1";
            firstVisiblePagerHyperlink = hlPage2;
            lastVisiblePagerHyperlink = hlPage6;

            Int32 remainder;
            var f = (Math.DivRem(PageNumber, 5, out remainder)) * 5;
            var totalF = (Math.DivRem(totalPages, 5, out remainder)) * 5;

            if (PageNumber <= 5)
            {
                liPage6.Visible = false;
                liPageDotPrevious.Visible = false;
                f = 2;
                lastVisiblePagerHyperlink = hlPage5;
            }
            else if (PageNumber >= totalF)
            {
                liPage3.Visible = false;
                liPage4.Visible = false;
                liPage5.Visible = false;
                liPage6.Visible = false;
                liPageDotNext.Visible = false;

                if (remainder == 0)
                {
                    liPage2.Visible = true;
                    liPage3.Visible = true;
                    liPage4.Visible = true;
                    liPage5.Visible = true;
                    liPage6.Visible = true;

                    if (PageNumber == totalF)
                    {
                        f -= 5;
                        liPage2.Visible = false;
                        liPageDotNext.Visible = false;
                        firstVisiblePagerHyperlink = hlPage3;
                    }
                }
                else
                {
                    if (remainder > 1)
                    {
                        liPage3.Visible = true;
                    }
                    if (remainder > 2)
                    {
                        liPage4.Visible = true;
                    }
                    if (remainder > 3)
                    {
                        liPage5.Visible = true;
                    }
                }
            }

            if ((PageNumber >= totalF - 5) && PageNumber > 5 && remainder == 0)
            {
                liPageDotNext.Visible = false;
            }
            if (PageNumber > 5 && PageNumber < 5 * 2)
            {
                liPage2.Visible = false;
                firstVisiblePagerHyperlink = hlPage3;
            }

            hlPage2.Text = (f).ToString();
            hlPage3.Text = (f + 1).ToString();
            hlPage4.Text = (f + 2).ToString();
            hlPage5.Text = (f + 3).ToString();
            hlPage6.Text = (f + 4).ToString();

            hlLastPage.Text = totalPages.ToString();

            if (PageNumber == 1)
            {
                liPreviousPage.Visible = false;
            }
            else if (PageNumber == totalPages)
            {
                liNexPage.Visible = false;
            }
        }

        private void SetPagingNaviationUrls()
        {
            hlPage1.NavigateUrl = String.Format("{0}{1}", RootURL, PageName);

            var pattern = RootURL + PageName;

            if (PageName.IndexOf("?") >= 0)
            {
                pattern += "&page={0}";
            }
            else
            {
                pattern += "?page={0}";
            }

            if (!Filter.IsEntityFieldsEmpty)
            {
                pattern += Filter.EntityFieldsQueryString;
                hlPage1.NavigateUrl = String.Format(pattern, "1");
            }

            hlPage2.NavigateUrl = String.Format(pattern, hlPage2.Text);
            hlPage3.NavigateUrl = String.Format(pattern, hlPage3.Text);
            hlPage4.NavigateUrl = String.Format(pattern, hlPage4.Text);
            hlPage5.NavigateUrl = String.Format(pattern, hlPage5.Text);
            hlPage6.NavigateUrl = String.Format(pattern, hlPage6.Text);
            hlPage7.NavigateUrl = String.Format(pattern, hlPage7.Text);
            hlLastPage.NavigateUrl = String.Format(pattern, hlLastPage.Text);

            if (PageNumber - 1 <= 1)
            {
                hlPreviousPage.NavigateUrl = hlPage1.NavigateUrl;
            }
            else
            {
                hlPreviousPage.NavigateUrl = String.Format(pattern, PageNumber - 1);
            }

            if (PageNumber + 1 >= totalPages)
            {
                hlNextPage.NavigateUrl = hlLastPage.NavigateUrl;
            }
            else
            {
                hlNextPage.NavigateUrl = String.Format(pattern, PageNumber + 1);
            }

            Int32 n;

            if (firstVisiblePagerHyperlink != null && hlPageDotPrevious.Visible)
            {
                if (Int32.TryParse(firstVisiblePagerHyperlink.Text, out n))
                {
                    if (n <= 1)
                    {
                        n = 1;
                    }

                    hlPageDotPrevious.NavigateUrl = String.Format(pattern, (n - 1).ToString());
                }
            }
            if (lastVisiblePagerHyperlink != null && hlPageDotNext.Visible)
            {
                if (Int32.TryParse(lastVisiblePagerHyperlink.Text, out n))
                {
                    if (n >= totalPages)
                    {
                        n = totalPages;
                    }

                    hlPageDotNext.NavigateUrl = String.Format(pattern, (n + 1).ToString());
                }
            }
        }

        private void SetPagingCssClasses()
        {
            HyperLink hl = null;
            var pn = PageNumber.ToString();
            Control hlContainer;
            var found = false;

            foreach (Control c in ulPager.Controls)
            {
                var t = c.GetType();

                hlContainer = c as HtmlGenericControl;

                if (hlContainer != null)
                {
                    foreach (var c2 in hlContainer.Controls)
                    {
                        hl = c2 as HyperLink;

                        if (hl != null && hl.Visible)
                        {
                            if (String.Compare(hl.Text, pn, false) == 0)
                            {
                                hl.CssClass = "active";
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }
            }
        }

        #endregion
    }
}