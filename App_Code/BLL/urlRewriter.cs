using System;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
public class urlRewriter : IConfigurationSectionHandler
{
    protected XmlNode _oRules = null;

    public  string GetSubstitution(string zPath)
    {
        Regex oReg;
        foreach (XmlNode oNode in _oRules.SelectNodes("rule"))
        {
            XmlNode objNode = oNode.SelectSingleNode("url/text()");
            string str = objNode.Value;
            oReg = new Regex(str.ToLower());
            Match oMatch = oReg.Match(zPath.ToLower());
            if (oMatch.Success == true)
            {

                return oReg.Replace(zPath.ToLower(), oNode.SelectSingleNode("rewrite/text()").Value.ToLower());

            }
        }
        return zPath.ToLower();
    }

    public static void Process()
    {
        urlRewriter oRewriter = (urlRewriter)ConfigurationManager.GetSection("urlRedirect/urlrewrites");
        string zSubst;
        zSubst = oRewriter.GetSubstitution(HttpContext.Current.Request.Path);
        if (zSubst.Length > 0)
        {
            HttpContext.Current.RewritePath(zSubst);
        }
    }

      object System.Configuration.IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
    {
        _oRules = section;
        return this;
    }

}