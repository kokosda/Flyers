using FlyerMe.Admin.Controls;
using FlyerMe.Admin.Controls.Grid;
using FlyerMe.Admin.Controls.Grid.Events;
using FlyerMe.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;

namespace FlyerMe.Admin.Others
{
    public partial class GenerateFeedsForSyndication : AdminPageBase
    {
        protected void Page_Load(Object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                InitFeedsDirectory();
                InitGrid();

                DataBind();
            }
            if (Request.IsGet())
            {
                GenerateFeedByRequest();
            }
        }

        public override void DataBind()
        {
            grid.DataBind();
        }

        protected void grid_RowHeadBinding(Object sender, RowHeadBindingEventArgs e)
        {
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Syndication Site" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell { Text = "Last Updated" });
            e.Grid.Head.HeaderCells.Add(new HeaderCell(ActionTypes.Edit));
        }

        protected void grid_RowDataBinding(Object sender, RowDataBindingEventArgs e)
        {
            var item = e.Data  as FeedItemModel;

            e.Grid.Body.Rows.Add(new BodyRow(e.BodyRowIndex));

            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = item.SyndicationSite,
                                                                    InputType = DataInputTypes.Submit,
                                                                    TextIsInvisible = true
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Text = item.LastUpdated.HasValue ? item.LastUpdated.ToString() : "Feed doesn't exist",
                                                                    Attributes = new Dictionary<String, String> { { "style", "color: " + ((!item.IsOutdated.HasValue || item.IsOutdated.Value) ? "red;" : "green;") } }
                                                                });
            e.Grid.Body.Rows[e.BodyRowIndex].DataCells.Add(new DataCell(e.Grid.Body.Rows[e.BodyRowIndex])
                                                                {
                                                                    Html = String.Format("<a href='{0}' target='_blank' class='button'>View feed</a>", ResolveUrl(item.AbsoluteFilePath)),
                                                                    Attributes = new Dictionary<String, String> { { "class", "detales" } }
                                                                });
        }

        protected void grid_RowCommanded(Object sender, RowCommandedEventArgs e)
        {
            try
            {
                var syndicationSite = (e.Sender as Button).Text;

                if (syndicationSite.HasNoText())
                {
                    message.MessageText = "Syndication site is required.";
                    message.MessageClass = MessageClassesEnum.System;
                }

                if (message.MessageText.HasNoText())
                {
                    var obj = new clsSyndication();
                    var generateMethodName = String.Format("Generate{0}Feed", syndicationSite);
                    MethodInfo generateMethodInfo = null;

                    try
                    {
                        generateMethodInfo = obj.GetType().GetMethod(generateMethodName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    }
                    catch
                    {
                    }

                    if (generateMethodInfo == null)
                    {
                        message.MessageText = "Method \"" + generateMethodName + "\" was not found. Please implement feed generation.";
                        message.MessageClass = MessageClassesEnum.System;
                    }

                    if (message.MessageText.HasNoText())
                    {
                        var profile = new ProfileCommon();

                        generateMethodInfo.Invoke(obj, new[] { profile });

                        message.MessageText = String.Format("{0} feed generated successfully.", syndicationSite);
                        message.MessageClass = MessageClassesEnum.Ok;
                    }
                }
            }
            catch (TargetInvocationException tex)
            {
                if (tex.InnerException != null)
                {
                    message.MessageText = tex.InnerException.Message;
                }
                else
                {
                    message.MessageText = tex.Message;
                }

                message.MessageClass = MessageClassesEnum.Error;
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
            }

            if (message.MessageClass == MessageClassesEnum.Ok)
            {
                message.RedirectToShowMessage();
            }
            else
            {
                message.ShowMessage();
            }
        }

        #region private

        private void InitFeedsDirectory()
        {
            var dirPath = Server.MapPath("~/xmlfeeds");

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
            catch (Exception ex)
            {
                message.MessageText = ex.Message;
                message.MessageClass = MessageClassesEnum.Error;
                message.ShowMessage();
            }
        }

        private void InitGrid()
        {
            var feedItemsList = BuildFeedItemsList();

            grid.GridDataSource.ArrayDataSource = new ArrayDataSource { Items = feedItemsList.FeedItems.ToArray() };
        }

        private FeedItemsList BuildFeedItemsList()
        {
            var result = new FeedItemsList();

            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "PropBot",
                                            AbsoluteFilePath = "~/xmlfeeds/propbot.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "Vast",
                                            AbsoluteFilePath = "~/xmlfeeds/vastfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "Trulia",
                                            AbsoluteFilePath = "~/xmlfeeds/truliafeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "Zillow",
                                            AbsoluteFilePath = "~/xmlfeeds/zillowfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "Oodle",
                                            AbsoluteFilePath = "~/xmlfeeds/oodlefeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "CLRSearch",
                                            AbsoluteFilePath = "~/xmlfeeds/clrsearch.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "OLX",
                                            AbsoluteFilePath = "~/xmlfeeds/olxfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "Trovit",
                                            AbsoluteFilePath = "~/xmlfeeds/trovitfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "HotPads",
                                            AbsoluteFilePath = "~/xmlfeeds/hotpadsfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "FrontDoors",
                                            AbsoluteFilePath = "~/xmlfeeds/frontdoorsfeed.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "CityCribs",
                                            AbsoluteFilePath = "~/xmlfeeds/citycribs.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "ClickAbleDirectory",
                                            AbsoluteFilePath = "~/xmlfeeds/clickablecitydirecotry.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "PropSmart",
                                            AbsoluteFilePath = "~/xmlfeeds/propsmart.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "RealestateAdvisor",
                                            AbsoluteFilePath = "~/xmlfeeds/realestateadvisor.xml"
                                        });
            result.FeedItems.Add(new FeedItemModel
                                        {
                                            SyndicationSite = "HomeScape",
                                            AbsoluteFilePath = "~/xmlfeeds/homescape.xml"
                                        });

            foreach (var item in result.FeedItems)
            {
                var file = new FileInfo(Server.MapPath(item.AbsoluteFilePath));

                if (file.Exists)
                {
                    item.LastUpdated = file.LastWriteTime;
                    item.IsOutdated = item.LastUpdated.Value.AddDays(7) < DateTime.Today;
                }
            }

            return result;
        }

        #region private

        private void GenerateFeedByRequest()
        {
            var feed = Request.QueryString["feed"];

            if (feed.HasText())
            {
                var button = new Button
                                    {
                                        Text = feed
                                    };

                grid_RowCommanded(null, new RowCommandedEventArgs(0, null, button, null));

                Response.ClearContent();
                Response.SuppressContent = true;
                Response.End();
            }
        }

        #endregion

        public sealed class FeedItemModel
        {
            public String SyndicationSite { get; set; }

            public DateTime? LastUpdated { get; set; }

            public String AbsoluteFilePath { get; set; }

            public Boolean? IsOutdated { get; set; }
        }

        public sealed class FeedItemsList
        {
            public FeedItemsList()
            {
                FeedItems = new List<FeedItemModel>();
            }

            public List<FeedItemModel> FeedItems { get; set; }
        }

        #endregion
    }
}
