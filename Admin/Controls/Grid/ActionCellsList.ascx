<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActionCellsList.ascx.cs" Inherits="FlyerMe.Admin.Controls.Grid.ActionCellsList" %>
<%@ Register Src="~/Admin/Controls/Grid/ActionCell.ascx" TagPrefix="uc" TagName="ActionCell" %>
<asp:Repeater ID="rptCells" runat="server" OnItemDataBound="rptCells_ItemDataBound">
    <ItemTemplate>
        <uc:ActionCell id="cell" runat="server" />
    </ItemTemplate>
</asp:Repeater>