<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataCellsList.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.DataCellsList" %>
<%@ Register Src="~/Admin/Controls/Grid/DataCell.ascx" TagPrefix="uc" TagName="DataCell" %>
<asp:Repeater ID="rptCells" runat="server" OnItemDataBound="rptCells_ItemDataBound">
    <ItemTemplate>
        <uc:DataCell ID="cell" runat="server" />
    </ItemTemplate>
</asp:Repeater>