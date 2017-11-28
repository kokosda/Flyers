using System;
using System.Web.Optimization;

namespace FlyerMe
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterDefaultPageBundles(bundles);
            RegisterPricesPageBundles(bundles);
            RegisterSamplesPageBundles(bundles);
            RegisterSubscribePageBundles(bundles);
            RegisterUnsubscribePageBundles(bundles);
            RegisterSignUpPageBundles(bundles);
            RegisterLogInPageBundles(bundles);
            RegisterRecoverPasswordPageBundles(bundles);
            RegisterFaqPageBundles(bundles);
            RegisterContactsPageBundles(bundles);
            RegisterMyFlyersPageBundles(bundles);
            RegisterProfilePageBundles(bundles);
            RegisterCartPageBundles(bundles);
            RegisterSendToClientsPageBundles(bundles);
            RegisterCreateFlyerSellersAgentPageBundles(bundles);
            RegisterCreateFlyerCustomPageBundles(bundles);
            RegisterCreateFlyerBuyersAgentPageBundles(bundles);
            RegisterAdminLoginPageBundles(bundles);
            RegisterAdminDefaultPageBundles(bundles);
            RegisterAdminFlyersPageBundles(bundles);
            RegisterAdminFlyersDetailsPageBundles(bundles);
            RegisterAdminAgentsPageBundles(bundles);
            RegisterAdminOthersCustomerTestimonialsPageBundles(bundles);
            RegisterAdminReportsUnsubscriptionPageBundles(bundles);
            RegisterAdminReportsCustomerReportPageBundles(bundles);
        }

        #region private

        private static String[] GetMasterPageScriptsBegin()
        {
            return new[]
                        {
                            "~/js/jquery/jquery.js",
                            "~/js/owl.carousel.js",
                            "~/scripts/masterpage.js",
                            "~/scripts/elements.js",
                            "~/js/jquery.validate/jquery.validate.js"
                        };
        }

        private static String[] GetMasterPageScriptsEnd()
        {
            return new[]
                        {
                            "~/scripts/socksHelper.js"
                        };
        }

        private static String[] GetAdminMasterPageScriptsBegin()
        {
            return new[]
                        {
                            "~/js/jquery/jquery.js"
                        };
        }

        private static void RegisterDefaultPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/default").Include(GetMasterPageScriptsBegin())
                            .Include(GetMasterPageScriptsEnd()));

            bundles.Add(new ScriptBundle("~/bundles/scripts/defaultauthenticated").Include(GetMasterPageScriptsBegin()));

            bundles.Add(new ScriptBundle("~/bundles/scripts/defaultpage").Include(GetMasterPageScriptsBegin())
                .Include("~/js/jquery.viewportchecker.js")
                .Include(GetMasterPageScriptsEnd()));

            bundles.Add(new ScriptBundle("~/bundles/scripts/defaultpageauthenticated").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.viewportchecker.js"));
        }

        private static void RegisterPricesPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/prices").Include(GetMasterPageScriptsBegin())
                            .Include("~/scripts/prices.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterSamplesPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/samples").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/scripts/samples.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterSubscribePageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/subscribe").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/scripts/subscribe.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterUnsubscribePageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/unsubscribe").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/scripts/unsubscribe.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterSignUpPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/signup").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/js/jquery.maskedinput/jquery.maskedinput.js")
                            .Include("~/scripts/signup.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterLogInPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/login").Include(GetMasterPageScriptsBegin())
                            .Include("~/scripts/login.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterRecoverPasswordPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/recoverPassword").Include(GetMasterPageScriptsBegin())
                            .Include("~/scripts/recoverPassword.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterFaqPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/faq").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterContactsPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/contacts").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/scripts/contacts.js")
                            .Include(GetMasterPageScriptsEnd()));
        }

        private static void RegisterMyFlyersPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/myflyers").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/scripts/myflyers.js"));
        }

        private static void RegisterProfilePageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/profile").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/js/jquery.maskedinput/jquery.maskedinput.js")
                            .Include("~/scripts/profile.js"));
        }

        private static void RegisterCartPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/cart").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/scripts/cart.js"));
        }

        private static void RegisterSendToClientsPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/sendtoclients").Include(GetMasterPageScriptsBegin())
                            .Include("~/scripts/sendToClients.js"));
        }

        private static void RegisterCreateFlyerSellersAgentPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/createflyer/sellersagent").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/js/jquery.maskedinput/jquery.maskedinput.js")
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/scripts/history.js")
                            .Include("~/scripts/createflyer/createflyer.js"));
        }

        private static void RegisterCreateFlyerCustomPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/createflyer/custom").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/scripts/history.js")
                            .Include("~/scripts/createflyer/createflyer.js"));
        }

        private static void RegisterCreateFlyerBuyersAgentPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/createflyer/buyersagent").Include(GetMasterPageScriptsBegin())
                            .Include("~/js/jquery.validate/additional-methods.js")
                            .Include("~/js/jquery.maskedinput/jquery.maskedinput.js")
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/js/jquery.mcustomscrollbar.concat.js")
                            .Include("~/js/jquery.ui.touch-punch.js")
                            .Include("~/scripts/history.js")
                            .Include("~/scripts/createflyer/createflyer.js"));
        }

        private static void RegisterAdminLoginPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/login")
                            .Include("~/js/jquery/jquery.js"));
        }

        private static void RegisterAdminDefaultPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/default").Include(GetAdminMasterPageScriptsBegin()));
        }

        private static void RegisterAdminFlyersPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/flyers").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/admin/js/flyers/flyers.js"));
        }

        private static void RegisterAdminAgentsPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/agents").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/admin/js/agents.js"));
        }

        private static void RegisterAdminFlyersDetailsPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/flyers/details").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/js/ace/ace.js")
                            .Include("~/js/ace/theme-crimson_editor.js")
                            .Include("~/js/ace/mode-html.js")
                            .Include("~/admin/js/flyers/details.js"));
        }

        private static void RegisterAdminOthersCustomerTestimonialsPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/others/customertestimonials").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/admin/js/others/customerTestimonials.js")
                            .Include("~/admin/js/script.js"));
        }

        private static void RegisterAdminReportsUnsubscriptionPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/reports/unsubscribption").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/admin/js/reports/unsubscribption.js")
                            .Include("~/admin/js/script.js"));
        }

        private static void RegisterAdminReportsCustomerReportPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/admin/reports/customerreport").Include(GetAdminMasterPageScriptsBegin())
                            .Include("~/css/jquery.ui/jquery-ui.js")
                            .Include("~/admin/js/reports/customerReport.js")
                            .Include("~/admin/js/script.js"));
        }

        #endregion
    }
}