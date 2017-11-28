<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Grid.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.GridControl" %>
<%@ Register Src="~/Admin/Controls/Grid/Head.ascx" TagPrefix="uc" TagName="Head" %>
<%@ Register Src="~/Admin/Controls/Grid/Body.ascx" TagPrefix="uc" TagName="Body" %>
<%@ Register Src="~/Controls/Pager.ascx" TagPrefix="uc" TagName="Pager" %>
<%@ Register Src="~/Admin/Controls/Grid/ExcelExporter.ascx" TagPrefix="uc" TagName="ExcelExporter" %>
<%@ Import Namespace="FlyerMe" %>
<uc:ExcelExporter ID="excelExporterTop" runat="server" />
<div class="table" id="divTable" runat="server">
    <asp:Literal ID="ltlPrehead" runat="server" Visible="false"></asp:Literal>
    <table>
        <uc:Head ID="head" runat="server" />
        <uc:Body ID="body" runat="server" />
    </table>
</div>
<uc:Pager ID="pager" runat="server" />
<asp:HiddenField ID="hfTotalRecords" runat="server" />
<uc:ExcelExporter ID="excelExporterBottom" runat="server" />