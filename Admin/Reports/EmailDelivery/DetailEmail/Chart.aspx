<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="FlyerMe.Admin.Reports.EmailDelivery.DetailEmail.Chart" Theme="" MasterPageFile="~/Admin/MasterPage.master" %>
<%@ Register Src="~/Admin/Controls/Message.ascx" TagPrefix="uc" TagName="Message" %>
<asp:Content ContentPlaceHolderID="cphContent" runat="server">
    <div class="conteiner">
        <div class="outside-content">
            <div class="contnet">
                <div class="back">
                    <asp:HyperLink runat="server" NavigateUrl="~/admin/reports/emaildelivery/detailemail.aspx"><span></span>Back to Detail Email report</asp:HyperLink>
                </div>
                <uc:Message ID="message" runat="server" />
                <div class="indent"></div>
                <div id="piechart" class="search"></div>
                <div class="indent"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/google/charts/loader.js") %>"></script>
    <script type="text/javascript">
        (function ($, w) {
            $(function () {
                <% if (PieSlices.Length > 0) { %>
                google.charts.load("current", { "packages": ["corechart"] });
                google.charts.setOnLoadCallback(drawChart);

                function drawChart() {
                    var data = google.visualization.arrayToDataTable([
                        ["Agents", "Emails"],
                        <%= GetJsArrayElements() %>
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