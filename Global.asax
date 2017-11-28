<%@ Application Language="C#" %>

<script runat="server">

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        urlRewriter.Process();
        System.Threading.Thread.CurrentThread.CurrentCulture = clsUtility.OriginalCulture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = clsUtility.OriginalCulture;
    }

    void Application_Start(Object sender, EventArgs e) 
    {
        FlyerMe.BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
        new System.Threading.Tasks.TaskFactory().StartNew(() => new Project.Tasks.XmlFeeds.GenerationScheduler().Schedule());
    }
       
</script>
