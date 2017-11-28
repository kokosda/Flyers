using System;
using System.Collections.Specialized;
using System.Web;

namespace FlyerMe
{
    public static class RequestHelper
    {
        public static Boolean IsPost(this HttpRequest request)
        {
            var result = false;

            if (request != null && String.Compare(request.HttpMethod, "post", true) == 0)
            {
                result = true;
            }

            return result;
        }

        public static Boolean IsGet(this HttpRequest request)
        {
            var result = false;

            if (request != null && String.Compare(request.HttpMethod, "get", true) == 0)
            {
                result = true;
            }

            return result;
        }

        public static String NameValueToQueryString(this NameValueCollection stringifiedCollection, Boolean leadingAmp = true)
        {
            String result = null;

            if (stringifiedCollection != null)
            {
                foreach(String n in stringifiedCollection)
                {
                    if (n.HasText())
                    {
                        result = String.Format("{0}&{1}={2}", result, n, stringifiedCollection[n]);
                    }
                }
            }

            if (!leadingAmp && result.HasText())
            {
                result = result.Substring(1, result.Length - 1);
            }

            return result;
        }

        public static Boolean? ParseCheckboxValue(this HttpRequest request, String key)
        {
            Boolean? result = null;

            if (!String.IsNullOrEmpty(key))
            {
                if (String.IsNullOrEmpty(request[key]))
                {
                    result = false;
                }
                else
                {
                    Boolean @bool;

                    if (Boolean.TryParse(request[key], out @bool))
                    {
                        result = @bool;
                    }
                    else if (String.Compare(request[key], "on", true) == 0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public static Boolean? ParseCheckboxValue(this NameValueCollection request, String key)
        {
            Boolean? result = null;

            if (!String.IsNullOrEmpty(key))
            {
                if (String.IsNullOrEmpty(request[key]))
                {
                    result = false;
                }
                else
                {
                    Boolean @bool;

                    if (Boolean.TryParse(request[key], out @bool))
                    {
                        result = @bool;
                    }
                    else if (String.Compare(request[key], "on", true) == 0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public static void RedirectToHttpsIfRequired(this HttpRequest request, HttpResponse response)
        {
            if (request == null || response == null)
            {
                return;
            }

            if (!request.IsHttps())
            {
                var uri = request.UrlToHttps();

                response.Redirect(uri.ToString(), true);
            }
        }

        public static void RedirectToHttpIfRequired(this HttpRequest request, HttpResponse response)
        {
            if (request == null || response == null)
            {
                return;
            }

            if (request.IsHttps())
            {
                var uri = request.UrlToHttp();

                response.Redirect(uri.ToString(), true);
            }
        }

        public static Uri UrlToHttps(this HttpRequest request)
        {
            Uri result = null;

            if (request != null)
            {
                if (!request.IsHttps())
                {
                    result = new UriBuilder("https", request.Url.Host, 443, request.Url.AbsolutePath, request.Url.Query).Uri;
                }
            }

            return result;
        }

        public static Uri UrlToHttps(this Uri uri)
        {
            var result = uri;

            if (uri != null)
            {
                if (!uri.IsHttps())
                {
                    result = new UriBuilder("https", uri.Host, 443, uri.AbsolutePath, uri.Query).Uri;
                }
            }

            return result;
        }

        public static Uri UrlToHttp(this HttpRequest request)
        {
            Uri result = null;

            if (request != null)
            {
                if (request.IsHttps())
                {
                    result = new UriBuilder("http", request.Url.Host, 80, request.Url.AbsolutePath, request.Url.Query).Uri;
                }
            }

            return result;
        }

        public static Boolean IsHttps(this HttpRequest request)
        {
            Boolean result = false;

            if (request != null)
            {
                result = String.Compare(request.Url.Scheme, "https", true) == 0;
            }

            return result;
        }

        public static Boolean IsHttps(this Uri uri)
        {
            Boolean result = false;

            if (uri != null)
            {
                result = String.Compare(uri.Scheme, "https", true) == 0;
            }

            return result;
        }
    }
}