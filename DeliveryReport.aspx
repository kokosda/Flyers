<%@ Page Language="C#" MasterPageFile="~/MasterPageAccount.master" AutoEventWireup="true" CodeFile="DeliveryReport.aspx.cs" 
Inherits="FlyerMe.DeliveryReport" Theme="" %>
<asp:Content ContentPlaceHolderID="cphBeforeContent" runat="server">
    <div class="flyer-menu">
    	<div class="content">
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="account">
        <form>
            <div class="form-content">
                <fieldset>
                    <legend>My Flyers - Delivery Report</legend>
                    <div class="block-delivery">
                        <p>Report data is available for flyers sent after 20th Jan 2013. Maximum views are typically achieved 24 hours after your flyer status has been marked as "Sent" in your MyFlyers directory.</p>
                        <div class="delivery">
                            <table cellpadding="0">
                                <tr>
                                    <td><strong>Flyer ID:</strong></td>
                                    <td><asp:Literal ID="ltlFlyerId" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td><strong>Flyer Delivery Date:</strong></td>
                                    <td><asp:Literal ID="ltlFlyerDeliveryDate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td><strong>Total Emails Sent</strong></td>
                                    <td><asp:Literal ID="ltlTotalEmailsSent" runat="server"></asp:Literal></td>	
                                </tr>
                                <tr>
                                    <td><strong>Interested Agents</strong></td>
                                    <td><asp:Literal ID="ltlInterestedAgents" runat="server"></asp:Literal></td>	
                                </tr>
                                <tr>
                                    <td><strong>Highly Interested Agents</strong></td>
                                    <td><asp:Literal ID="ltlHighlyInterestedAgents" runat="server"></asp:Literal></td>	
                                </tr>
                            </table>
                            <div id="piechart"></div>
                            <div class="text">
                                <p><strong>Interested Agents:</strong> Percentage of agents that have viewed your flyer.</p>
							    <p><strong>Highly Interested Agents:</strong> Percentage of agents that have viewed your flyer more than once.</p>
                            </div>
                        </div>
                        <div class="title">How to interpret flyer reports:</div>
                        <p>The reports should not be looked at as an absolute number of how many agents received or opened your flyer. The reports are simply a proxy of these numbers, as it is impossible to track exact email data with the various filters and blockers that are employed by the Internet Service Providers (ISPs) and on the users clients viewing device. As such, <strong>expect to see open rates in 5 to 10 percent range which is just a fraction of the actual delivery and open rates. However, comparatively, E-mail marketing campaigns result in a significantly higher response rate than traditional direct marketing, which averages just 1 to 2 percent response rates.</strong></p>
                        <p>Email marketing offers a better return on investment (ROI). Highly targeted audiences, lower costs (pennies per view/lead!) and improved response rates all contribute to email marketing's outstanding return on investment.</p>
                        <p>The flyer reports are designed to give you insight on how your flyers are performing. You should compare the results to previous flyers to gauge how the flyer was received by agents. You will find that making changes to your subject line message and flyer content can make a huge difference in the success of your flyer.</p>
                        <p>Put on your marketing hat and see how you can improve the performance of your flyers. Remember spam filters don't like and people usually do not open emails with subject lines that have symbols like "$$$, %, @, !) or statements that include, "FREE, or Save Millions!."</p>
                        <p>&nbsp;</p>
                        <p><strong>Note:</strong> We do save open counts report for up to 90 days after your flyer is sent. After 90 days the report may be removed from our database. But we can provide you data on demand if available in our database. For privacy purposes, we do not provide name, company or contact details of who opened your flyer.</p>
                    </div>
                </fieldset>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/google/charts/loader.js") %>"></script>
    <script type="text/javascript">
        (function ($, w) {
            $(function () {
                <% if (HasStatistics) { %>
                google.charts.load("current", { "packages": ["corechart"] });
                google.charts.setOnLoadCallback(drawChart);

                function drawChart() {
                    var data = google.visualization.arrayToDataTable([
                      ["Agents", "Emails"],
                      ["Interested Agents", <%= TotalOpenEmails %>],
                      ["Highly Interested Agents", <%= HighlyInterestedEmails %>]
                    ]);
                    var options = {
                        height: "100%",
                        chartArea: { "width": "100%", "height": "70%" },
                        legend: { position: "bottom", textStyle: { fontSize: 12 } },
                        chartArea: { width: "100%" },
                        is3D: true,
                    };
                    var chart = new google.visualization.PieChart(document.getElementById("piechart"));

                    chart.draw(data, options);
                }
                <% } %>
            });
        })(jQuery, window);
    </script>
</asp:Content>